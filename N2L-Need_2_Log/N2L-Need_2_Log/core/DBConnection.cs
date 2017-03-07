using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// 
    /// </summary>
    class DBConnection //: IDBConnection
    {

        private static /*new*/ DBConnection instance;
        /// <summary>
        /// 
        /// </summary>
        private SQLiteConnection conn;
        public ConnectionState isConnected { get { return instance.conn.State; } }

        /// <summary>
        /// 
        /// </summary>
        private DBConnection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static DBConnection Connect
        {
            get
            {
                if (instance == null)
                {
                    string connectionString = "Data Source=" + Settings.Default.dbpath + "; Version=3; Password=" + Settings.Default.password;
                    instance = new DBConnection();
                    instance.conn = new SQLiteConnection(connectionString);
                    try
                    {
                        switch (System.IO.File.Exists(Settings.Default.dbpath))
                        //switch (Settings.Default.db_exist)
                        {
                            case true:
                                instance.conn.Open();
                                break;
                            default:
                                instance.Create();
                                break;
                        }
                    }
                    catch (SQLiteException sqle)
                    {
                        if (sqle.ResultCode == SQLiteErrorCode.Auth ||
                                sqle.ResultCode == SQLiteErrorCode.Auth ||
                                sqle.ResultCode == SQLiteErrorCode.IoErr_Auth)
                        {

                        }

                    }
                }
                if (instance.isConnected != ConnectionState.Open)
                {
                    instance.conn.Open();
                }
                return instance;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ConnectionState Disconnect
        {
            get
            {
                instance.conn.Close();
                return instance.isConnected;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ConnectionState Create()
        {
            SQLiteConnection.CreateFile(Settings.Default.dbpath);
            try
            {
                instance.conn.Open();
                SQLiteCommand query = new SQLiteCommand(System.IO.File.ReadAllText(Settings.Default.dbscript_path),
                    instance.conn);
                query.ExecuteNonQuery();
                instance.conn.ChangePassword(Settings.Default.password_hash);
            }
            catch (SQLiteException sqle)
            {
                switch (sqle.ResultCode)
                {
                    ///case(SQLiteErrorCode.)
                }
            }
            return conn.State;
        }

        /// <summary>
        /// Metodo utile ad ottenere una istanza di tipo System.Data.SQLite.SQLiteDataReader contenente tutti
        /// i record presenti nel database locale con le loro informazioni generali, leggibile tramite
        /// una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>System.DataSqlite.SQLiteDataReader : System.Data.Common.DbDataReader</returns>
        public SQLiteDataReader GetMainView()
        {
            return new SQLiteCommand(QueryString.Create(Querable.MainMenuItems), conn).ExecuteReader();
        }
        /// <summary>
        /// Metodo utile ad ottenere una istanza di tipo System.Data.SQLite.SQLiteDataReader contenente tutte
        /// le informazioni presenti nel database locale relative al record indicato 
        /// leggibile tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <param name="entryId">identificativo del record nel database</param>
        /// <returns>System.DataSqlite.SQLiteDataReader : System.Data.Common.DbDataReader</returns>
        public SQLiteDataReader GetRecordInfo(int entryId)
        {
            return new SQLiteCommand(QueryString.Create(Querable.DetailedInfo, entryId), conn).ExecuteReader();
        }
        /// <summary>
        /// Metodo utile ad ottenere dal db tutti i tipi predefiniti già esistenti e preconfigurati
        /// leggibili tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>System.DataSqlite.SQLiteDataReader : System.Data.Common.DbDataReader</returns>
        public SQLiteDataReader GetRecordsType()
        {

            return new SQLiteCommand(QueryString.Create(Querable.AdmittedType), conn).ExecuteReader();
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
            string queryString = QueryString.Create(Querable.CreateNewEntry, entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);

            SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);

            return sql.ExecuteNonQuery();
        }
        /// <summary>
        /// Metodo utile ad aggiornare/modificare un record già esistente nel database
        /// </summary>
        /// <param name="entryId">id del record all'interno del database</param>
        /// <param name="entryName">nome del record</param>
        /// <param name="iconName">nome dell'icona associata al record</param>
        /// <param name="noteValue">note aggiuntive</param>
        /// <param name="passwordValue">password del record</param>
        /// <param name="urlValue">url del sito associato al record</param>
        /// <param name="usernameValue">username associato al record</param>
        /// <returns>numero di righe modificate a seguito del comando</returns>
        public int Update(int entryId, string entryName, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string queryString = QueryString.Create(Querable.UpdateEntry, entryId, entryName, iconName, noteValue, passwordValue, urlValue, usernameValue);

            SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);
            return sql.ExecuteNonQuery();
        }
        /// <summary>
        /// Metodo utile a cancellare tutti gli elementi associati ad un record dal database.
        /// </summary>
        /// <param name="EntryId"> </param>
        /// <returns>numero di righe eliminate a seguito del comando</returns>
        public int Erase(int entryId)
        {
            string queryString = QueryString.Create(Querable.EraseEntry, entryId);

            SQLiteCommand sql = new SQLiteCommand(queryString, instance.conn);

            return sql.ExecuteNonQuery();
        }

        /// <summary>
        /// Metodo utile a creare una copia di backup del database
        /// </summary>
        /// <param name="fullName"> nome completo di path del database di backup</param>
        /// <param name="password"> password che si vuole settare per proteggere il nuovo db</param>
        /// <returns>true</returns>
        public bool Backup(string fullName, string password)
        {
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
            return true;
        }
        /// <summary>
        /// Metodo utile a modificare/assegnare una nuova password a protezione del database locale.
        /// </summary>
        /// <param name="newPassword"> nuova password</param>
        /// <returns></returns>
        public bool ChangeSettings(string newPassword)
        {
            instance.conn.ChangePassword(newPassword);
            return true;
        }
    }
}
