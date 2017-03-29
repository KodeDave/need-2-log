using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Common;
using N2L_Need_2_Log.Properties;

namespace N2L_Need_2_Log.core
{
    public abstract class DbAction : IDBRequest
    {
        protected DbConnection conn;
        protected static DbAction instance;

        protected DbAction() { }

        public virtual ConnectionState isConnected { get { return instance.conn.State; } }
        //public static DbAction Connect { get; }
        public abstract ConnectionState Disconnect { get; }
        protected abstract void Test();
        protected abstract ConnectionState Create();

        public abstract DbDataReader GetMainView();
        public abstract DbDataReader GetRecordInfo(int entryId);
        public abstract DbDataReader GetRecordsType();

        public abstract int Insert(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue);
        public abstract int Update(int entryId, string entryName, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue);
        public abstract int Erase(int entryId);
        public abstract string CheckEntryName(string entryName);
        public abstract void Backup(string fullName, string password);
        public abstract void ChangeSettings(string newPassword);
    }
}
