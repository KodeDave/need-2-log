using System;
using System.IO;
using System.Net;
using Core;

namespace ConsoleTest
{
    /// <summary>
    /// Classe di test per verificare il corretto funzionamento del Core.
    /// </summary>
    internal class N2LTest
    {
        /// <summary>
        /// Punto di ingresso principale del programma di test della libreria. Tale programma punta a testare tutte le funzionalità della DLL Core.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            bool result;
            Console.WriteLine("INIZIO DI TUTTI I TEST");
            result = TestController();
            Console.Write("Test su classe Controller: ");
            Console.WriteLine((result) ? "OK" : "ERRORE");
            if (result)
            {
                result = TestCript();
                Console.Write("Test su classe Cript: ");
                Console.WriteLine((result) ? "OK" : "ERRORE");
                if (result)
                {
                    result = TestDBConnector();
                    Console.Write("Test su classe DBConnector: ");
                    Console.WriteLine((result) ? "OK" : "ERRORE");
                }
            }
            Console.WriteLine((result) ?
                "TUTTI I TEST SONO STATI SUPERATI" :
                "ERRORE RISCONTRATO IN UNO DEI TEST");
            Console.WriteLine("PREMI INVIO PER TERMINARE");
            Console.ReadKey();
        }

        /// <summary>
        /// Metodo di supporto ai test. Tramite Internet produce una parola in maniera randomica.
        /// </summary>
        /// <returns></returns>
        protected static string GetRequest()
        {
            try
            {
                string toReturn = String.Empty;
                Stream objStream;
                StreamReader objReader;
                WebRequest wr = WebRequest.Create("http://setgetgo.com/randomword/get.php");
                WebProxy myProxy = new WebProxy();

                myProxy.BypassProxyOnLocal = true;
                wr.Proxy = myProxy;
                objStream = wr.GetResponse().GetResponseStream();
                objReader = new StreamReader(objStream);
                toReturn = objReader.ReadLine();

                return toReturn;
            }
            catch (Exception)
            {
                return "ErroreInGetRequest";
            }
        }
        /// <summary>
        /// Serie di test per verificare il corretto funzionamento della classe Controller.
        /// </summary>
        /// <returns> true se il db viene/è eliminato, altrimenti false</returns>
        protected static bool TestController()
        {
            Core.Controller.CheckSettings();
            bool DbExist = Core.Controller.DbExist;
            if (Core.Controller.DbExist)
            {
                Console.Write("ELiminazione vecchio Database: ");
                System.IO.File.Delete("..\\..\\..\\Core\\Resources\\Db\\DBN2L.sqlite3");
                Console.WriteLine("OK");
                Controller.CheckSettings();
                DbExist = Core.Controller.DbExist;
            }
            return !Core.Controller.DbExist;
        }
        /// <summary>
        /// Serie di test per verificare il corretto funzionamento della classe Cript.
        /// </summary>
        /// <returns> true se la classe risponde correttamente, altrimenti false</returns>
        protected static bool TestCript()
        {
            Random r = new Random();
            int i = r.Next(8, 16);
            int nNAC = r.Next(0, i);
            string testWord = GetRequest();
            string hash = Cript.ComputeHash(testWord, null);
            bool result;

            Console.WriteLine("\t\"Cript.ComputeHash(\"" + testWord + "\", null)\"->" + hash);
            result = Cript.Confirm(testWord, hash);
            Console.WriteLine("\t\"Cript.Confirm(\"" + testWord + "\", " + hash + "\")\"->" + result);
            Console.WriteLine("\t\"Cript.GenerateRandomly("
                + i + ", " + nNAC + ")\"->\"" + Cript.GenerateRandomly(i, nNAC) + "\"");
            return result;
        }
        /// <summary>
        /// Serie di test per verificare il corretto funzionamento della classe DBConnector.
        /// </summary>
        /// <returns> true se la classe risponde correttamente, altrimenti false</returns>
        protected static bool TestDBConnector()
        {
            Random r = new Random();
            string password = GetRequest();
            bool toReturn = true;
            int entryId;
            string entryName;
            int typeId;
            string iconName;
            string noteValue;
            string passwordValue;
            string urlValue;
            string usernameValue;
            int result;
            int j;
            DBConnector db = DBConnector.Connect;

            Console.Write("\tcreazione e connessione db: ");
            if (db == null)
            {
                toReturn = false;
                Console.WriteLine("FALLITO");
            }
            else
            {
                if (db.isConnected == System.Data.ConnectionState.Open)
                    Console.WriteLine("OK");
                else
                {
                    toReturn = false;
                    Console.WriteLine("FALLITO");
                }
            }
            if (toReturn)
            {
                Console.Write("\tCambio password in \"" + password + "\": ");
                Core.Controller.Password = Core.Cript.ComputeHash(password, null);
                db.ChangeSettings(Core.Controller.Password);
                Console.WriteLine("OK");
                Console.Write("\tDisconnessione: ");
                if (db.Disconnect == System.Data.ConnectionState.Closed)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    toReturn = false;
                    Console.WriteLine("FALLITO");
                }
            }
            if (toReturn)
            {
                db = null;
                db = DBConnector.Connect;
                Console.Write("\tConessione: ");
                if (db != null)
                {
                    if (db.isConnected == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("OK");
                    }
                }
                else
                {
                    toReturn = false;
                    Console.WriteLine("FALLITO");
                }
            }
            if (toReturn)
            {
                j = 0;
                Console.Write("\t\"GetRecordsType()\":");
                using (System.Data.Common.DbDataReader data = db.GetRecordsType())
                {
                    while (data.Read())
                    {
                        j++;
                    }
                    if (j > 0)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        toReturn = false;
                        Console.WriteLine("FALLITO");
                    }
                }
                if (toReturn)
                {
                    Console.Write("\tCreazione di tre nuovi contatti: ");
                    for (int i = 0; i < 3; i++)
                    {
                        entryName = GetRequest();
                        typeId = r.Next(1, j);
                        noteValue = GetRequest();
                        passwordValue = GetRequest();
                        urlValue = "http://www.test.org";
                        usernameValue = GetRequest();
                        usernameValue = db.CheckEntryName(usernameValue);
                        result = db.Insert(entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);
                        Console.Write((i + 1) + " | ");
                    }
                    j = 0;
                    using (System.Data.Common.DbDataReader data = db.GetMainView())
                    {
                        while (data.Read())
                        {
                            j++;
                        }
                    }
                    if (j > 0)
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        toReturn = false;
                        Console.WriteLine("FALLITO");
                    }
                }
            }
            if (toReturn)
            {
                j = 0;
                Console.Write("\tModifica di un elemento: ");
                entryId = r.Next(0, j);
                entryName = String.Empty;
                typeId = 1;
                iconName = String.Empty;
                noteValue = GetRequest();
                passwordValue = GetRequest();
                urlValue = "http://www." + GetRequest() + ".org";
                usernameValue = GetRequest();
                result = db.Update(entryId, entryName, typeId, iconName, noteValue, passwordValue, urlValue, usernameValue);
                for (int i = 1; i <= 3; i++)
                {
                    using (System.Data.Common.DbDataReader data = db.GetRecordInfo(i))
                    {
                        while (data.Read())
                        {
                            j++;
                        }
                    }
                }
                if (j == 3)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("FALLITO");
                    toReturn = false;
                }
            }
            if (toReturn)
            {
                Console.Write("\tElimino un elemento: ");
                if (db.Erase(r.Next(1, 3)) > 0)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    toReturn = false;
                    Console.WriteLine("FALLITO");
                }
            }
            if (toReturn)
            {
                Console.Write("\tCreo database di backup: ");
                if (File.Exists("..\\..\\..\\Core\\Resources\\Db\\backup.sqlite3"))
                    File.Delete("..\\..\\..\\Core\\Resources\\Db\\backup.sqlite3");
                db.Backup("..\\..\\..\\Core\\Resources\\Db\\backup.sqlite3", null);
                if (File.Exists("..\\..\\..\\Core\\Resources\\Db\\backup.sqlite3"))
                {
                    File.Delete("..\\..\\..\\Core\\Resources\\Db\\backup.sqlite3");
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("FALLITO");
                    toReturn = false;
                }
            }
            return toReturn;
        }
    }
}
