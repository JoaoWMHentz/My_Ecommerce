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
                return string.Empty;
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

        public static double tryToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public static string StringPiece(string str, string separator = " - ", int part = 1)
        {
            try
            {
                return str.Split(separator)[part - 1];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string getImageTypeFromHtmlBase64(string base64)
        {
            //Base 64: data:image/jpeg;base64,{data}
            try
            {
                string type = StringPiece(base64, ",");
                type = type.Replace("data:image/", "");
                type = type.Replace(";base64", "");
                type = type.Trim();
                return type;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
