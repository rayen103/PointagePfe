using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSTAdmin
{
    public class Settings
    {
        static public string GetConnectionString(string ServerName,string User, string Password, string DataBase)
        {
            return "Data Source="+ServerName+";User ID="+User+";Password="+Password+";Initial Catalog="+DataBase+"";
        }

        static public bool IsEmpty(string Value)
        {
            if (string.IsNullOrEmpty(Value) || string.IsNullOrWhiteSpace(Value))
                return true;

            return false;
        }
    }
}
