using Microsoft.Data.SqlClient;
namespace Helpers.connection
{
    public class DBSave: IDisposable
    {
        /*  
        Exemplo de Uso:
          using (DBSave sql = new DBSave(conexao, "MyTable"))
                {
                    sql.addKey("codigo", 1);
                    sql.addPar("nome", 'Teste');
                    sql.Save(False);
                }
         */
        public DBSave(Connection connection, string table)
        {
            this.con = connection;
            this.table = table;
            parameters = new List<Parameter>();
        }
        //Passa a coluna e um valor para ser montado o SQL
        public List<Parameter> parameters { get; set; }
        //Conexao com o banco
        public Connection con { get; set; }
        //Tabela do Banco
        public string table { get; set; }
        //Delata um cadastro no banco e depois o salva
        public void deleteInsert()
        {
            delete();
            Persist(insert());
        }
        //Salva um objeto, Existe = True faz um Update no banco, Existe = False faz um insert no banco
        public void save(bool Existe)
        {
            SqlCommand cmd = new SqlCommand();
            if (Existe)
            {
                cmd = update();
            }
            else
            {
                cmd = insert();
            }
            Persist(cmd);
        }
        //deleta do banco
        public void delete()
        {
            SqlCommand cmd = new SqlCommand();
            cmd = getDelete();
            Persist(cmd);
        }
        //Persiste os dados no banco
        private void Persist(SqlCommand cmd)
        {
            cmd.Connection = con.Connect();
            SqlTransaction tran = con.sqlConnection.BeginTransaction();
            try
            {
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw new Exception("Erro ao salvar no banco de dados: " + e.Message);
            }
            finally
            {
                con.Disconnect();
                tran.Dispose();
            }
        }
        //São passados os parametros para serem salvos no banco
        public void addPar(string column, object value)
        {
            this.parameters.Add(new Parameter(column, value));
        }
        //Key da tabela é usada na Where do Update e do Delete ao persistir no banco
        public void addKey(string column, object value)
        {
            this.parameters.Add(new Parameter(column, value, true));
        }

        private SqlCommand getDelete()
        {
            SqlCommand command = new SqlCommand();

            string sql = $"DELETE FROM {table}  {GetWhere()}";
            command.CommandText = sql;
            return GetCmdParameterskey(command);
        }

        private SqlCommand update()
        {
            string parameter = GetParametersUpdate();

            SqlCommand command = new SqlCommand();

            string sql = $"UPDATE {table} SET {parameter} {GetWhere()}";
            command.CommandText = sql;
            
            return GetCmdParameters(command); ;
        }

        private SqlCommand insert()
        {
            string coluns = GetColunsInsert();
            string parameter = GetParametersInsert();

            SqlCommand command = new SqlCommand();

            string sql = $"INSERT INTO {table} ({coluns}) VALUES ({parameter})";
            command.CommandText = sql;
            
            return GetCmdParameters(command);
        }

        private SqlCommand GetCmdParameterskey(SqlCommand command)
        {
            foreach (Parameter pa in GetKeys())
            {
                SqlParameter par = new SqlParameter(pa.column, pa.value);
                command.Parameters.Add(par);
            }
            return command;
        }

        private SqlCommand GetCmdParameters(SqlCommand command)
        {
            foreach (Parameter pa in parameters)
            {
                SqlParameter par = new SqlParameter(pa.column, pa.value);
                command.Parameters.Add(par);
            }
            return command;
        }

        private string GetWhere()
        {
            string exit = "WHERE 1 = 1 \n";
            string ands = "";
            {
                foreach (var col in GetKeys())
                {
                    ands += "AND " + col.column + " = @" + col.column + "\n";
                }
            }
            return exit + ands;
        }

        private List<Parameter> GetKeys()
        {
            List<Parameter> KeysList = new List<Parameter>();
            foreach (var par in parameters)
            {
                if (par.isKey)
                {
                    KeysList.Add(par);
                }
            }
            return KeysList;
        }

        private string GetParametersUpdate()
        {
            string exit = "";
            foreach (var par in parameters)
            {
                if (!par.isKey)
                {
                    exit += "," + par.column + " = @" + par.column;
                }
            }
            return exit.Remove(0, 1);
        }

        private string GetParametersInsert()
        {
            string exit = "";
            foreach (var par in parameters)
            {
                exit += ",@" + par.column;
            }
            return exit.Remove(0, 1);
        }

        private string GetColunsInsert()
        {
            string exit = "";
            foreach (var col in parameters)
            {
                exit += "," + col.column;
            }
            return exit.Remove(0, 1);
        }

        public void Dispose()
        {
            con.Dispose();
        }
    }
}
