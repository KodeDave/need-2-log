using System;
using System.IO;
using Core.Properties;
using System.Data.Common;

namespace Core
{
    /// <summary>
    /// Fornisce due metodi statici e 5 proprietà usati per mantenere aggiornate le impostazioni del sistema in apertura ed in chiusura della applicazione.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Proprietà usata per restituire e configurare l'impostazione "password".
        /// </summary>
        public static string Password
        {
            get { return Settings.Default.password; }
            set
            {
                Settings.Default.password = value;
            }
        }
        /// <summary>
        /// Proprietà usata per restituire e configurare l'impostazione "db_used_type".
        /// </summary>
        public static AdmittedDbType DbUsedType
        {
            get { return (AdmittedDbType)Settings.Default.db_used_type; }
            set
            {
                Settings.Default.db_used_type = (int)value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Proprietà usata per restituire e configurare l'impostazione "username".
        /// </summary>
        public static string Username
        {
            get { return Settings.Default.username; }
            set
            {
                Settings.Default.username = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Proprietà usata per restituire e configurare l'impostazione "logged".
        /// </summary>
        public static bool Logged
        {
            get { return Settings.Default.logged; }
            set
            {
                Settings.Default.logged = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Proprietà usata per restituire e configurare l'impostazione "db_exist".
        /// </summary>
        public static bool DbExist
        {
            get { return Settings.Default.db_exist; }
            set
            {
                Settings.Default.db_exist = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// verifica che il database esista e modifica il valore db_exist in false nel caso in cui non esistesse
        /// </summary>
        public static void CheckSettings()
        {
            switch ((AdmittedDbType)Settings.Default.db_used_type)
            {
                case AdmittedDbType.SQLite:
                    if (!File.Exists(Settings.Default.db_path_sqlite))
                    {
                        Settings.Default.db_exist = false;
                    }
                    else
                    {
                        Settings.Default.db_exist = true;
                    }
                    Settings.Default.password = String.Empty;
                    break;
                default:
                    break;
            }
            Settings.Default.logged = false;
        }
        /// <summary>
        /// modifica il valore logged delle impostazioni in false
        /// </summary>
        public static void OnClose()
        {
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case AdmittedDbType.SQLite:
                        Settings.Default.password = String.Empty;
                        break;
                    default:
                        break;
                }
                var db = DBConnector.Connect.Disconnect;
            }
            catch (DbException)
            {

            }
            finally
            {
                Settings.Default.logged = false;
                Settings.Default.Save();
            }
        }
    }
}
