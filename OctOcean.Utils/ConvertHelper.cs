using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Utils
{
    public  class ConvertHelper
    {
        public static string GetString(object obj)
        {
            return ( obj == null || obj == DBNull.Value ) ? "" : obj.ToString();
        }


        public static int ToInt32(string str)
        {
            int result = 0;
            int.TryParse(str, out result);
            return result;
        }

        public static int ToInt32(object obj)
        {
            return  ToInt32(GetString(obj));
        }


        public static long ToInt64(string str)
        {

            long result = 0;
            long.TryParse(str, out result);
            return result;
        }

        public static long ToInt64(object obj)
        {
            return ToInt64(GetString(obj));
        }
    }
}
