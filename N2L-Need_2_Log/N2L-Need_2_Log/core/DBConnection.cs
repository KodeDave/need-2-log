using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// Rappresenza la connessione all'origine dati dell'applicazione, effettuabile tramite
    /// una sola possibile istanza dell'oggetto.
    /// L'unicità della connessione è assicurata tramite il pattern Singleton
    /// </summary>
    public class DBConnection : IDBRequest

    {
        private SQLiteConnection conn;
        /// <summary>
        /// Rappresenta l'unica istanza di DBConnection creabile
        /// </summary>
        protected static DBConnection instance;
       
        private DBConnection() { }

        /// <summary>
        /// Descrive lo stato corrente della connessione all'origine dati dell'applicazione
        /// </summary>
        public ConnectionState isConnected { get { return instance.conn.State; } }
               
        /// <summary>
        /// Fornisce accesso alla connessione alla origine dati della applicazione
        /// </summary>
        public static DBConnection Connect
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        string connectionString = "Data Source=" + Settings.Default.dbpath +
                            "; Version=3; Password=" + Settings.Default.password;
                        instance = new DBConnection();
                        instance.conn = new SQLiteConnection(connectionString);
                        switch (File.Exists(Settings.Default.dbpath))
                        {
                            case true:
                                instance.conn.Open();
                                break;
                            default:
                                instance.Create();
                                break;
                        }
                        instance.Test();
                    }
                    if (instance.isConnected != ConnectionState.Open)
                    {
                        instance.conn.Open();
                    }
                }catch (SQLiteException sqle)
                {
                    var a = instance.Disconnect;
                    instance.conn.Dispose();
                    instance = null;
                    throw sqle;
                }
                return instance;
            }
        }
        /// <summary>
        /// Permette la disconnessione dalla origine data della applicazione, chiudendo la comunicazione in modo sicuro
        /// </summary>
        public ConnectionState Disconnect
        {
            get
            {
                try
                {
                    instance.conn.Close();
                    return instance.isConnected;
                }
                catch (SQLiteException sqle)
                {
                    throw sqle;
                }
            }
        }
        /// <summary>
        /// Verifica che i parametri inseriti (per esempio la password) siano corretti e che la connessione sia apribile.
        /// Una System.Data.SQLite.SQLiteException viene prodotta se degli errori sono riscontrati.
        /// </summary>
        private void Test()
        {
            try
            {
                var a = new SQLiteCommand("SELECT * FROM sqlite_master;", conn);
                a.VerifyOnly();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Se non è ancora stato creato il database, provvede a creare il file usando uno script sql presente
        /// nella cartella Resources
        /// </summary>
        /// <returns>Lo stato della connessione</returns>
        private ConnectionState Create()
        {
            SQLiteConnection.CreateFile(Settings.Default.dbpath);
            try
            {
                instance.conn.Open();
                SQLiteCommand query = new SQLiteCommand(File.ReadAllText(Settings.Default.dbscript_path), instance.conn);
                query.ExecuteNonQuery();
                instance.conn.ChangePassword(Settings.Default.password);
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
            return conn.State;
        }

        /// <summary>
        /// Metodo utile ad ottenere una istanza di tipo System.Data.SQLite.SQLiteDataReader contenente tutti
        /// i record presenti nel database locale con le loro informazioni generali, leggibile tramite
        /// una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        public DbDataReader GetMainView()
        {
            try
            {
                return new SQLiteCommand(QueryString.Create(Querable.MainMenuItems), conn).ExecuteReader();
            }
            catch(SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile ad ottenere una istanza di tipo System.Data.SQLite.SQLiteDataReader contenente tutte
        /// le informazioni presenti nel database locale relative al record indicato 
        /// leggibile tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <param name="entryId">identificativo del record nel database</param>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        public DbDataReader GetRecordInfo(int entryId)
        {
            try { 
                return new SQLiteCommand(QueryString.Create(Querable.DetailedInfo, entryId), conn).ExecuteReader();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile ad ottenere dal db tutti i tipi predefiniti già esistenti e preconfigurati
        /// leggibili tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        public DbDataReader GetRecordsType()
        {
            try {
                return new SQLiteCommand(QueryString.Create(Querable.AdmittedType), conn).ExecuteReader();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }

        /// <summary>
        /// Metodo utile a creare un nuovo record all'interno del database
        /// </summary>
        /// <param name="entryName">nome del record</param>
        /// <param name="typeId">tipologia del record</param>
        /// <param name="noteValue">note aggiuntive</param>
        /// <param name="passwordValue">password del record</param>
        /// <param name="urlValue">url del sito associato al record</param>
        /// <param name="usernameValue">username associata al record</param>
        /// <returns>numero di righe inserite a seguito del comando</returns>
        public int Insert(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            try { 
                string queryString = QueryString.Create(Querable.CreateNewEntry, entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);

                 SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);

                return sql.ExecuteNonQuery();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile ad aggiornare/modificare un record già esistente nel database
        /// </summary>
        /// <param name="entryId">id del record all'interno del database</param>
        /// <param name="entryName">nome del record</param>
        /// <param name="typeId">Id del tipo predefinito associato al record</param>
        /// <param name="iconName">nome dell'icona associata al record</param>
        /// <param name="noteValue">note aggiuntive</param>
        /// <param name="passwordValue">password del record</param>
        /// <param name="urlValue">url del sito associato al record</param>
        /// <param name="usernameValue">username associato al record</param>
        /// <returns>numero di righe modificate a seguito del comando</returns>
        public int Update(int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            try { 
                string queryString = QueryString.Create(Querable.UpdateEntry, entryId, entryName, typeId, iconName, noteValue, passwordValue, urlValue, usernameValue);

                SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);
                return sql.ExecuteNonQuery();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile a cancellare tutti gli elementi associati ad un record dal database.
        /// </summary>
        /// <param name="entryId">Id del record presente nel database</param>
        /// <returns>numero di righe eliminate a seguito del comando</returns>
        public int Erase(int entryId)
        {
            try { 
                string queryString = QueryString.Create(Querable.EraseEntry, entryId);

                SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);

                return sql.ExecuteNonQuery();
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile a verificare che il nome associato ad un record che si sta per
        /// inserire sia univoco
        /// </summary>
        /// <param name="entryName">nome del record</param>
        /// <returns>nome del record eventualmente modificato per rispettare l'univocità</returns>
        public string CheckEntryName(string entryName)
        {
            string toReturn = String.Empty;
            try
            {
                string queryString = QueryString.Create(Querable.CheckEntryName, entryName);

                SQLiteCommand sql = new SQLiteCommand(queryString, conn);
                using (SQLiteDataReader data = sql.ExecuteReader())
                {
                    while (data.Read())
                    {
                        toReturn = data[0].ToString();
                    }
                }
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
            return toReturn;
        }
        /// <summary>
        /// Metodo utile a creare una copia di backup del database
        /// </summary>
        /// <param name="fullName"> nome completo di path del database di backup</param>
        /// <param name="password"> password che si vuole settare per proteggere il nuovo db</param>
        public void Backup(string fullName, string password)
        {
            try { 
                SQLiteConnection.CreateFile(fullName);
                using (var destination = new SQLiteConnection("Data Source=" + fullName + "; Version=3;"))
                {
                    destination.Open();
                    if (!String.IsNullOrEmpty(password))
                    {
                        destination.ChangePassword(password);
                    }
                    instance.conn.BackupDatabase(destination, "main", "main", -1, null, 500);
                    destination.Close();
                    destination.Dispose();
                }
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
        /// <summary>
        /// Metodo utile a modificare/assegnare una nuova password a protezione del database locale.
        /// </summary>
        /// <param name="newPassword"> nuova password</param>
        public void ChangeSettings(string newPassword)
        {
            try { 
                instance.conn.ChangePassword(newPassword);
            }
            catch (SQLiteException sqle)
            {
                throw sqle;
            }
        }
    }
}
