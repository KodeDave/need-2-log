using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N2L_Need_2_Log.core
{
    public class Singleton
    {
        protected static Singleton instance;

        protected Singleton() { }

        protected static Singleton Instance { get {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
