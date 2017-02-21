using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    class DBConnection
    {
        private static DBConnection instance;
        private ConnectionState Connected { get; set; }
        private SQLiteConnection conn;

        private DBConnection()
        {
        }

        public static DBConnection Connect
        {
            get
            {
                if (instance == null)
                {
                    string connectionString = "Data Source=" + Settings.Default.dbpath +
                        "; Version=3; Password = " + Settings.Default.password_hash + ";";
                    instance = new DBConnection();
                    instance.Connected = ConnectionState.Closed;
                    instance.conn = new SQLiteConnection(connectionString);
                    try
                    {
                        switch (Settings.Default.db_exist)
                        {
                            case true:
                                instance.conn.Open();
                                instance.Connected = instance.conn.State;
                                break;
                            default:
                                instance.Connected = instance.Create();
                                break;
                        }
                    }
                    catch (SQLiteException)
                    {
                        throw;
                    }
                }
                return instance;
            }
        }
        public ConnectionState CheckConnection()
        {
            return Connected;
        }
        private ConnectionState Create()
        {
            SQLiteConnection.CreateFile(Settings.Default.dbpath);
            conn.Open();
            SQLiteCommand query = new SQLiteCommand(System.IO.File.ReadAllText(Settings.Default.dbscript_path),
                instance.conn);
            query.ExecuteNonQuery();
            instance.conn.ChangePassword(Settings.Default.password_hash);
            return conn.State;
        }
        public bool Insert()
        {
            SQLiteCommand sql = new SQLiteCommand("", conn);
            sql.ExecuteNonQuery();
            return true;
        }
        public DataTable getData()
        {
            DataTable dt = new DataTable();
            var da = new SQLiteDataAdapter();
            ////////////////////////////////////////
            /*DataSet ds = new System.Data.DataSet();
            var da = new SQLiteDataAdapter(sql, conn);
            da.Fill(ds);
            Grid.DataSource = ds.Tables[0].DefaultView;
            return ds;*/
            return instance.conn.GetSchema();
        }
        public bool Update()
        {
            return true;
        }
        public bool Erase()
        {
            return true;
        }
        public bool Backup()
        {
            return true;
        }
        public bool ChangeSettings()
        {
            return true;
        }
    }
}
