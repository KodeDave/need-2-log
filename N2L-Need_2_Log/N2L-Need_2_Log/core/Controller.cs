using System;
using System.IO;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// Fornisce due metodi static usati per mantenere aggiornate le impostazioni del sistema
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
            Settings.Default.logged = false;
        }   
    }
}
