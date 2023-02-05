using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Helpers.converter;
using System.Diagnostics;
using System.Collections.Generic;

namespace Helpers.connection
{
    public class DbHelper
    {
        //Retorna um dataReader de Uma instrução SQL
        public static IDataReader ReturnDataReader(string Sql, Connection con)
        {
            con.Disconnect();
            IDataReader dr;
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = Sql;
                try
                {
                    cmd.Connection = con.Connect();
                    dr = cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return dr;
        }
        //Monta o replace para evitar sql injection
        public static string toSqlString(string str)
        {
            Regex.Replace(str, "'", "''");
            Regex.Replace(str, ";", "");
            Regex.Replace(str, "%", "");
            str = str.Insert(0, "'");
            str = str.Insert(str.Length, "'");
            return str.ToString();
        }
        //Transforma um linha de um dataReader em um objeto simples
        public static object dataReaderLineToObject(IDataReader dr, object obj)
        {
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(obj, dr[prop.Name], null);
                }
                catch (Exception)
                {
                    prop.SetValue(obj, null, null);
                }
            }
            return obj;
        }
        //Salva um objeto simples no banco
        public static void saveObjectDeleteInsert(Connection con, object obj, string table)
        {
            DBSave save = new DBSave(con, table);
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    if (prop.GetCustomAttributes().FirstOrDefault().GetType() == typeof(KeyAttribute))
                    {
                        save.addKey(prop.Name.ToLower(), prop.GetValue(obj, null));
                    }
                    else if (prop.GetCustomAttributes().FirstOrDefault().GetType() == typeof(SaveAttribute))
                    {
                        if (prop.GetValue(obj, null) != null)
                        {
                            save.addPar(prop.Name.ToLower(), prop.GetValue(obj, null));
                        }
                    }
                }
            }
            save.deleteInsert();
        }

        public static void saveObject(Connection con, object obj, string table, bool exists)
        {
            DBSave save = new DBSave(con, table);
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    string columnName = "";
                    foreach (Attribute at in prop.GetCustomAttributes())
                    {
                        if (at.GetType() == typeof(ColumnNameAttribute))
                        {
                            ColumnNameAttribute Col = (ColumnNameAttribute)at;
                            columnName = Col.colName;
                        }
                        if (at.GetType() == typeof(KeyAutoIncrementAttribute) && exists)
                        {
                            if (columnName == "")
                            {
                                save.addKey(prop.Name.ToLower(), prop.GetValue(obj, null));
                            }
                            else
                            {
                                save.addKey(columnName, prop.GetValue(obj, null));
                            }
                        }
                        else if (at.GetType() == typeof(KeyAttribute))
                        {
                            if (columnName == "")
                            {
                                save.addKey(prop.Name.ToLower(), prop.GetValue(obj, null));
                            }
                            else
                            {
                                save.addKey(columnName, prop.GetValue(obj, null));
                            }
                        }
                        else if (at.GetType() == typeof(SaveAttribute))
                        {
                            if (prop.GetValue(obj, null) != null)
                            {
                                if (columnName == "")
                                {
                                    save.addPar(prop.Name.ToLower(), prop.GetValue(obj, null));
                                }
                                else
                                {
                                    save.addPar(columnName, prop.GetValue(obj, null));
                                }
                            }
                        }

                    }
                }
            }
            try
            {
                save.save(exists);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static List< object> getObject(Connection con, object obj, string table)
        {
            SelectHelper select = new SelectHelper(con, table);

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                string columnName = "";
                foreach (Attribute at in prop.GetCustomAttributes())
                {
                    if (at.GetType() == typeof(ColumnNameAttribute))
                    {
                        ColumnNameAttribute Col = (ColumnNameAttribute)at;
                        columnName = Col.colName;
                    }
                    else if (at.GetType() == typeof(SaveAttribute) || at.GetType() == typeof(KeyAutoIncrementAttribute) || at.GetType() == typeof(KeyAttribute))
                    {
                        if (prop.GetValue(obj, null) != null)
                        {
                            if (columnName == "")
                            {
                                select.addPar(prop.Name.ToLower(), Converter.tryToString(prop.GetValue(obj, null)), "=");
                            }
                            else
                            {
                                select.addPar(columnName, Converter.tryToString(prop.GetValue(obj, null)), "=");
                            }
                        }
                    }
                }
            }
            var returnObj = new List<object>();

            using (IDataReader dr = DbHelper.ReturnDataReader(select.getSelect(), con))
            {
                returnObj.Add( dataReaderLineToObject(dr, obj));
            }
            return returnObj;
        }

        //Transforma uma lista de String em um IN de sql ex: IN ('A', 'B', 'C')
        public static string toSqlInString(List<string> ListObj)
        {
            string sql = " IN (";
            bool first = true;
            foreach (string obj in ListObj)
            {
                if (first)
                {
                    sql += toSqlString(obj.ToString());
                    first = false;
                }
                else
                {
                    sql += "," + toSqlString(obj.ToString());
                }
            }
            sql += ")";
            return sql;
        }
        //Transforma uma lista de Int em um IN de sql ex: IN (1, 2, 3)
        public static string toSqlInInt(List<int> ListObj)
        {
            string sql = " IN (";
            bool first = true;
            foreach (int obj in ListObj)
            {
                if (first)
                {
                    sql += toSqlString(obj.ToString());
                    first = false;
                }
                else
                {
                    sql += "," + toSqlString(obj.ToString());
                }
            }
            sql += ")";
            return sql;
        }
        public static int GetLastId(string table, string colunm, Connection con)
        {
            using (IDataReader dr = DbHelper.ReturnDataReader($"SELECT MAX({colunm}) AS codProduto FROM {table}", con))
            {
                if (dr.Read())
                {
                    int retorno = Converter.tryToInt(dr["codProduto"] + "");
                    dr.Close();
                    con.Disconnect();
                    return retorno;
                }
            }
            return 0;
        }
    }


    //Atributo para mostrar para a função Save object que isso é uma Key da Tabela
    public class KeyAttribute : Attribute
    {
        public bool isKey = true;
    }
    public class KeyAutoIncrementAttribute : Attribute
    {
        public bool isKeyAutoIncrement = true;
    }
    //Atributo para mostrar para a função Save object que isso é para ser salvo
    public class SaveAttribute : Attribute
    {
        public bool isSave = true;
    }
    //Caso o nome da coluna seja diferente do nome do objeto
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string colName)
        {
            this.colName = colName;
        }

        public string colName { get; set; }
    }
}


