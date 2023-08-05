

using System;
using System.Runtime.Serialization;

namespace Helpers.connection
{
    public class Parameter
    {
        public Parameter(string column, object value, bool isKey = false)
        {
            this.column = column;
            this.value = value;
            this.isKey = isKey;
        }
        public bool isKey { get; set; }
        public string column { get; set; }
        public object value { get; set; }
    }
}
