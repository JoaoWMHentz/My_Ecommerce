using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.converter
{
    public class Converter
    {
        public static int tryToInt(object value)
        {
			try
			{
				return Convert.ToInt32(value);
			}
			catch (Exception)
			{
				return 0;
			}
        }
        public static string tryToString(object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static string tryToBase64String(object value)
        {
            try
            {
                return Convert.ToBase64String((byte[])value);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
