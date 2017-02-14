using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N2L_Need_2_Log.core
{
    class PasswordCheckEventArgs
    {
        private readonly bool result;
        public PasswordCheckEventArgs(bool result)
        {
            this.result = result;
        }
        public bool Result { get { return this.result; } }
    }
}