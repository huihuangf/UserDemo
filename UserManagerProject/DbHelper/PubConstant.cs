using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper
{
    public class PubConstant
    {
        public PubConstant()
        {
        
        }

        public static string GetConfig(string key)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(key))
            {
                string str2 = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(str2))
                {
                    str = str2;
                }
            }
            return str;
        }
    }
}
