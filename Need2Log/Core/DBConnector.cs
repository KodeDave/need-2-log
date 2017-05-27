using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Core.Properties;

namespace Core
{
    /// <summary>
    /// Rappresenta la connessione all'origine dati dell'applicazione con SQLite,
    /// effettuabile tramite una sola possibile istanza dell'oggetto.
    /// L'unicità della connessione è assicurata tramite il pattern Singleton.
    /// </summary>
    public class DBConnector : IDBConnector
    {
        /// <summary>
        /// Rappresenta l'istanza reale di collegamento al database tramite l'interfaccia DbConnection
        /// </summary>
        private IDbConnection conn;
        /// <summary>
        /// Rappresenta l'unica istanza di DBConnector creabile
        /// </summary>
        protected static DBConnector instance;
        /// <summary>
        /// Rappresenta l'istanza del creatore di stringhe di query da presentare al database tramite la istanza conn
        /// </summary>
        private QueryCreator queryCreator = null;

        /// <summary>
        /// Costruttore della classe DBConnector.
        /// </summary>
        private DBConnector()
        {
            switch ((AdmittedDbType)Settings.Default.db_used_type)
            {
                case (AdmittedDbType.SQLite):
                    queryCreator = new SQLiteQueryCreator();
                    break;
                default:
                    queryCreator = null;
                    break;
            }
        }

        /// <summary>
        /// Descrive lo stato corrente della connessione all'origine dati dell'applicazione
        /// </summary>
        public ConnectionState isConnected
        {
            get
            {
                return ((instance.conn != null ) ?
                    instance.conn.State :
                    ConnectionState.Closed);
            }
        }

        /// <summary>
        /// Fornisce accesso alla connessione alla origine dati della applicazione
        /// </summary>
        public static DBConnector Connect
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        instance = new DBConnector();
                        instance.conn = new SQLiteConnection(instance.queryCreator.Create(Querable.Connect));
                        switch ((AdmittedDbType)Settings.Default.db_used_type)
                        {
                            case (AdmittedDbType.SQLite):
                            default:
                                switch (File.Exists(Settings.Default.db_path_sqlite))
                                {
                                    case true:
                                        instance.conn.Open();
                                        break;
                                    default:
                                        instance.Create();
                                        break;
                                }
                                break;
                        }
                        instance.Test();
                    }
                    if (instance.isConnected != ConnectionState.Open)
                    {
                        instance.conn.Open();
                    }
                }
                catch (DbException dbe)
                {
                    ConnectionState a = instance.Disconnect;
                    throw dbe;
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
                    var toReturn = instance.isConnected;
                    instance.conn.Dispose();
                    instance.conn = null;
                    instance = null;
                    return toReturn;
                }
                catch (DbException dbe)
                {
                    throw dbe;
                }
            }
        }
        
        /// <summary>
        /// Verifica che i parametri inseriti (per esempio la password) siano corretti e che la connessione sia apribile.
        /// Una System.Data.SQLite.DbException viene prodotta se degli errori sono riscontrati.
        /// </summary>
        private void Test()
        {
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        var a = new SQLiteCommand(queryCreator.Create(Querable.Test), (SQLiteConnection)conn);
                        a.VerifyOnly();
                        break;
                    default:
                        break;
                }

            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
        /// <summary>
        /// Se non è ancora stato creato il database, provvede a creare il file usando uno script sql presente
        /// nella cartella Resources
        /// </summary>
        /// <returns>Lo stato della connessione</returns>
        private ConnectionState Create()
        {
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        SQLiteConnection.CreateFile(Settings.Default.db_path_sqlite);
                        ((SQLiteConnection)instance.conn).Open();
                        SQLiteCommand query = new SQLiteCommand(File.ReadAllText(Settings.Default.db_scriptpath_sqlite), (SQLiteConnection)instance.conn);
                        query.ExecuteNonQuery();
                        ((SQLiteConnection)instance.conn).ChangePassword(Settings.Default.password);
                        break;
                    default:
                        break;
                }
            }
            catch (DbException dbe)
            {
                throw dbe;
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
            DbDataReader toReturn;
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        toReturn = new SQLiteCommand(queryCreator.Create(Querable.MainMenuItems), (SQLiteConnection)conn).ExecuteReader();
                        break;
                    default:
                        toReturn = null;
                        break;
                }
                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
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
            DbDataReader toReturn;

            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        toReturn = new SQLiteCommand(queryCreator.Create(Querable.DetailedInfo, entryId), (SQLiteConnection)conn).ExecuteReader();
                        break;
                    default:
                        toReturn = null;
                        break;
                }
                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
        /// <summary>
        /// Metodo utile ad ottenere dal db tutti i tipi predefiniti già esistenti e preconfigurati
        /// leggibili tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        public DbDataReader GetRecordsType()
        {
            DbDataReader toReturn;

            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        toReturn = new SQLiteCommand(queryCreator.Create(Querable.AdmittedType),
                            (SQLiteConnection)instance.conn).ExecuteReader();
                        break;
                    default:
                        toReturn = null;
                        break;
                }
                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
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
            DbCommand sql;
            int toReturn;
            try
            {
                string queryString = queryCreator.Create(Querable.CreateNewEntry, entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        sql = new SQLiteCommand(queryString, (SQLiteConnection)instance.conn);
                        toReturn = sql.ExecuteNonQuery();
                        break;
                    default:
                        toReturn = 0;
                        sql = null;
                        break;
                }
                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
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
            DbCommand sql;
            int toReturn;
            try
            {
                string queryString = queryCreator.Create(Querable.UpdateEntry, entryId, entryName, typeId, iconName, noteValue, passwordValue, urlValue, usernameValue);
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        sql = new SQLiteCommand(queryString, (SQLiteConnection)instance.conn);
                        toReturn = sql.ExecuteNonQuery();
                        break;
                    default:
                        toReturn = 0;
                        break;
                }

                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
        /// <summary>
        /// Metodo utile a cancellare tutti gli elementi associati ad un record dal database.
        /// </summary>
        /// <param name="entryId">Id del record presente nel database</param>
        /// <returns>numero di righe eliminate a seguito del comando</returns>
        public int Erase(int entryId)
        {
            DbCommand sql;
            int toReturn;

            try
            {
                string queryString = queryCreator.Create(Querable.EraseEntry, entryId);
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        sql = new SQLiteCommand(queryString, (SQLiteConnection)instance.conn);
                        toReturn = sql.ExecuteNonQuery();
                        break;
                    default:
                        toReturn = 0;
                        break;
                }
                return toReturn;
            }
            catch (DbException dbe)
            {
                throw dbe;
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
            DbCommand sql;
            string toReturn = String.Empty;

            try
            {
                string queryString = queryCreator.Create(Querable.CheckEntryName, entryName);
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        sql = new SQLiteCommand(queryString, (SQLiteConnection)conn);
                        using (SQLiteDataReader data = (SQLiteDataReader)sql.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                toReturn = data[0].ToString();
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (DbException dbe)
            {
                throw dbe;
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
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        SQLiteConnection.CreateFile(fullName);
                        using (var destination = new SQLiteConnection("Data Source=" + fullName + "; Version=3;"))
                        {
                            destination.Open();
                            if (!String.IsNullOrEmpty(password))
                            {
                                destination.ChangePassword(password);
                            }
                            ((SQLiteConnection)instance.conn).BackupDatabase(destination, "main", "main", -1, null, 500);
                            destination.Close();
                            destination.Dispose();
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
        /// <summary>
        /// Metodo utile a modificare/assegnare una nuova password a protezione del database locale.
        /// </summary>
        /// <param name="newPassword"> nuova password</param>
        public void ChangeSettings(string newPassword)
        {
            try
            {
                switch ((AdmittedDbType)Settings.Default.db_used_type)
                {
                    case (AdmittedDbType.SQLite):
                        ((SQLiteConnection)instance.conn).ChangePassword(newPassword);
                        Settings.Default.password = newPassword;
                        break;
                    default:
                        break;
                }
            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
    }
}
