using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper
{
    public class SQLHelper
    {
        public static string connectionString { get; set; }

        public static DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(connectionString,CommandType.Text,commandText,null);
        }

        private static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText,params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataSet(connection, commandType,commandText,commandParameters);
            }
        }

        private static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            throw new NotImplementedException();
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transation, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((commandText==null)||(commandText.Length==0))
            {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transation != null)
            {
                if (transation.Connection == null)
                {
                    throw new ArgumentNullException("The transation was rollbacked or commited,please provide an open transaction","transaction");
                }
                command.Transaction = transation;
            }
            command.CommandText = commandText;
            if (commandParameters != null)
            {
                AttachParameters(command,commandParameters);
            }
            
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (commandParameters !=null)
            {
                foreach (SqlParameter paramter in commandParameters)
                {
                    if (paramter != null)
                    {
                        if (((paramter.Direction==ParameterDirection.InputOutput)|| (paramter.Direction==ParameterDirection.Input)) && (paramter.Value==null))
                        {
                            paramter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(paramter);
                    }

                }

            }
          
        }
    }
}
