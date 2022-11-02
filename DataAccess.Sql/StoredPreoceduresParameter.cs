using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Sql
{
    /// <summary>
    /// An stored procedure parameter
    /// </summary>
    public class StoredPreoceduresParameter
    {
        private string _name = String.Empty;
        private object _value = null;
        public StoredPreoceduresParameter(string parameterName, object parameterValue)
        {
            this._name = parameterName;
            this._value = parameterValue;
        }
        public string ParameterName
        {
            get { return _name; }
            set { _name = value; }
        }

        public object ParamaterValue
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
