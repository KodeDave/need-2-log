using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace N2L_Need_2_Log.core
{
    public interface IDBConnection //: Singleton
    {
        IDBConnection Connect { get; }
        ConnectionState isConnected { get; }
        ConnectionState Create();

        System.Data.Common.DbDataReader GetMainView();
        System.Data.Common.DbDataReader GetRecordInfo(int entryId);
        System.Data.Common.DbDataReader GetRecordsType();

        int Insert(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue);
        int Update(int entryId, string entryName, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue);
        int Erase(int entryId);

        bool Backup(string fullName, string password);
        bool ChangeSettings(string newPassword);
    }
}