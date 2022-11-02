using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Sql
{
    public class StoreProcedure
    {
        private string _SpName = String.Empty;
        private List<StoredPreoceduresParameter> _list = new List<StoredPreoceduresParameter>();
        private string _errorMessage = String.Empty;
        private int commandTimeOut = 30;

        public StoreProcedure(string storedProcedureName)
        {
            this._SpName = storedProcedureName;
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            _list.Add(new StoredPreoceduresParameter(parameterName, parameterValue));
        }

        public List<StoredPreoceduresParameter> Items
        {
            get { return _list; }
            set { _list = value; }
        }

        public string SpName
        {
            get { return _SpName; }
            set { _SpName = value; }
        }

        public string Error
        {
            get { return _errorMessage; }
        }



        public Boolean ExecuteStoredProcedure(string connectionString)
        {
            var result = false;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(_SpName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = commandTimeOut;
            for (int cont = 0; cont < _list.Count; cont++)
            {
                if (_list[cont].ParamaterValue == null)
                    command.Parameters.AddWithValue(_list[cont].ParameterName, DBNull.Value);
                else
                    command.Parameters.AddWithValue(_list[cont].ParameterName, _list[cont].ParamaterValue);
            }
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                _errorMessage = String.Empty;
                //SqlConnection.ClearAllPools();
                result =  true;
            }
            catch (SqlException error)
            {
                _errorMessage = error.Message;
                //onnection.Close();
                // SqlConnection.ClearAllPools();
                result = false;
            }
            finally
            {
                connection.Close();
                //SqlConnection.ClearAllPools();
            }
            return result;
        }



        public DataTable ExecuteQuery(string connectionString)
        {
            DataTable query = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter command = new SqlDataAdapter(_SpName, connection);
            command.SelectCommand.CommandType = CommandType.StoredProcedure;
            command.SelectCommand.CommandTimeout = commandTimeOut;
            for (int cont = 0; cont < _list.Count; cont++)
            {
                if (_list[cont].ParamaterValue == null)
                    command.SelectCommand.Parameters.AddWithValue(_list[cont].ParameterName, DBNull.Value);
                else
                    command.SelectCommand.Parameters.AddWithValue(_list[cont].ParameterName, _list[cont].ParamaterValue);
            }
            try
            {
                connection.Open();
                command.Fill(query);
                _errorMessage = String.Empty;
            }
            catch (SqlException error)
            {
                _errorMessage = error.Message;
                //connection.Close();
                //SqlConnection.ClearAllPools();
            }
            finally
            {
                connection.Close();
                //SqlConnection.ClearAllPools();
            }

            return query;
        }
    }
}
