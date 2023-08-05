namespace Helpers.connection
{
    public class ParameterSelect
    {
        public ParameterSelect(string column, object value, string WhereOperator = "=")
        {
            this.Column = column;
            this.Value = value;
            this.Operator = WhereOperator;
        }

        public string Operator { get; set; }

        public string Column { get; set; }

        public object Value { get; set; }
    }
}