using System;
using System.IO;
using System.Data.SQLite;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// Fornisce due metodi static usati per mantenere aggiornate le impostazioni del sistema
    /// in apertura ed in chiusura della applicazione
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// verifica che il database esista e modifica il valore db_exist in false nel caso in cui non esistesse
        /// </summary>
        public static void CheckSettings()
        {
            if (!File.Exists(Settings.Default.dbpath))
            {
                Settings.Default.db_exist = false;
            }
            else
            {
                Settings.Default.db_exist = true;
            }
            Settings.Default.logged = false;
            Settings.Default.password = String.Empty;
        }    
        /// <summary>
        /// modifica il valore logged delle impostazioni in false
        /// </summary>
        public static void OnClose()
        {
            Settings.Default.password = String.Empty;
            try
            {
                var db = DBConnection.Connect.Disconnect;
            }
            catch(SQLiteException sqle)
            {
                
            }
            Settings.Default.logged = false;
            Properties.Settings.Default.Save();
        }
    }
}
