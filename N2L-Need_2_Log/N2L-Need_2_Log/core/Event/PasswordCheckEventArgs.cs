using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace N2L_Need_2_Log.core
{
    class PasswordCheckEventArgs : EventArgs
    {
        public PasswordCheckEventArgs(SQLiteErrorCode sqle)
        {
            //SQLITEHANDLER
        }
        private readonly bool result;
        public PasswordCheckEventArgs(bool result)
        {
            this.result = result;
        }
        public bool Result { get { return this.result; } }
    }
}