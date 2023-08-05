using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.connection
{
    public class SelectHelper
    {
        public SelectHelper(Connection conexao, string tabela)
        {
            this.con = conexao;
            Tabela = tabela;
            Parametros = new List<ParameterSelect>();
        }
        public List<ParameterSelect> Parametros { get; set; }
        public Connection con { get; set; }
        public string Tabela { get; set; }

        public void addPar(string column, string value = "", string whereOperator = "")
        {
            this.Parametros.Add(new ParameterSelect(column, value, whereOperator));
        }
        public string getSelect()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT");
            getColuns(sql);
            sql.AppendLine($"FROM {Tabela}");
            sql.AppendLine("WHERE 1 = 1");
            foreach (ParameterSelect parametro in Parametros)
            {
                if ((converter.Converter.tryToString(parametro.Value) != "") || (converter.Converter.tryToInt(parametro.Value) != 0))
                {
                    sql.AppendLine($"AND {parametro.Column} {parametro.Operator} {DbHelper.toSqlString(converter.Converter.tryToString(parametro.Value))}");
                }
            }
            return sql.ToString();
        }

        private void getColuns(StringBuilder sql)
        {
            bool primeira = true;
            foreach (ParameterSelect parametro in Parametros)
            {
                if (primeira)
                {
                    sql.AppendLine(parametro.Column);
                }
                else
                {
                    sql.AppendLine(", " + parametro.Column);
                }
                primeira = false;
            }
        }
    }
}