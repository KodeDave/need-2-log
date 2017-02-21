using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    public static class Controller
    {
        public static void CheckSettings()
        {
            if (!File.Exists(Settings.Default.dbpath))
            {
                Settings.Default.db_exist = false;
            }
            if(Settings.Default.password_hash.Length == 0)
            {
                Settings.Default.password_hash = Cript.ComputeHash(Settings.Default.default_password, null); 
            }
        }    
        public static void OnClose()
        {
            Settings.Default.logged = false;
        }   
    }
}
