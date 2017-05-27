using System;
using Core.Properties;

namespace Core
{
    /// <summary>
    /// Classe che implementa, specificamente per i database di tipo SQLite, il metodo pubblico ereditato dall'interfaccia e i metodi protetti ereditati dalla classe astratta.
    /// </summary>
    internal class SQLiteQueryCreator : QueryCreator
    {
        internal SQLiteQueryCreator()
        {
        }

        /// <summary>
        /// Restituisce la query per connettersi alla istanza di DB SQLite
        /// </summary>
        /// <returns>string - Query per connettersi al DB SQLite</returns>
        protected override string Connect()
        {
            string toReturn = "Data Source=" + Settings.Default.db_path_sqlite +
                            "; Version=3; Password=" + Settings.Default.password;
            Console.WriteLine(toReturn);

            return(toReturn);
        }
        /// <summary>
        /// Restituisce la query per testare la connessione alla istanza di DB SQLite
        /// </summary>
        /// <returns>string - Query per testare la connessione al DB SQLite</returns>
        protected override string Test()
        {
            return "SELECT * FROM sqlite_master;";
        }
        /// <summary>
        /// Restituisce la query per render noti tutti i tipi predefiniti disponibili
        /// </summary>
        /// <returns>string - Query per accedere a tutta la view type</returns>
        protected override string AdmittedType()
        {
            return ("SELECT * FROM `type` ORDER BY `id` ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// Restituisce la query per rendere noti tutti i record presenti e impostare il main menu
        /// </summary>
        /// <returns>string - query per accedere a tutta la view main</returns>
        protected override string MainMenuItems()
        {
            return ("SELECT * FROM `main` ORDER BY `ID` ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// Restituisce la query per rendere note tutte le informazioni relative ad un record
        /// </summary>
        /// <param name="entryId">id del record da ricercare</param>
        /// <returns>string - query per accedere alla view DetailedInfo</returns>
        protected override string DetailedInfo(int entryId)
        {
            return ("SELECT * FROM `DetailedInfo`" +
                ((entryId > 0 || !entryId.Equals(null)) ? (" WHERE `ID` = " + entryId) : ("")) +
                " ORDER BY `ID` ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// Restituisce la query per validare il nome del record prima di tentare di inserirlo
        /// </summary>
        /// <param name="entryName">nome da associare al nuovo record</param>
        /// <returns>string - query per verificare la validità di un nome prima di inserirlo nel db</returns>
        protected override string CheckEntryName(string entryName)
        {
            string toReturn = String.Empty;

            if (!String.IsNullOrEmpty(entryName))
            {
                toReturn = "SELECT CASE" +
                    "(SELECT count(name) FROM entry WHERE " +
                        "(name GLOB(\'" + entryName + "\') OR name GLOB(\'" + entryName + "_*\'))) " +
                            "WHEN 0 THEN \'" + entryName + "\' " +
                            "ELSE PRINTF(\'" + entryName + "_%d\'," +
                                "(SELECT count(name) FROM entry WHERE" +
                                    "(name GLOB(\'" + entryName + "\') OR name GLOB(\'" + entryName + "_*\'))))" +
                " END;";
            }
            return toReturn;
        }
        /// <summary>
        /// Restituisce la query per inserire un nuovo recod nel db
        /// </summary>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="typeId">tipologia base di account</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>string - query per inserire in tutte le tabelle i valori relativi</returns>
        protected override string CreateNewEntry(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string entry = (String.IsNullOrEmpty(entryName)) ? ("(SELECT PRINTF(\'Record_%d\', (SELECT max(id) + 1 FROM `entry`)))") : ('\'' + entryName + '\'');
            int type = (typeId.Equals(null)) ? (1) : (typeId);
            string note = (String.IsNullOrEmpty(noteValue)) ? ("NULL") : ('\'' + noteValue + '\'');
            string password = (String.IsNullOrEmpty(passwordValue)) ? ("NULL") : ('\'' + passwordValue + '\'');
            string url = (String.IsNullOrEmpty(urlValue) || urlValue.Equals("NULL")) ?
                    ("SELECT `url` FROM `type` WHERE `type`.`id` = " +
                        "(SELECT `type_id` FROM `entry` WHERE `entry`.`id` = " +
                            "(SELECT max(id) FROM `entry`))") :
                    ('\'' + urlValue + '\'');
            string username = (String.IsNullOrEmpty(usernameValue)) ? ("NULL") : ('\'' + usernameValue + '\'');

            return String.Format("BEGIN TRANSACTION;\n" +
                "INSERT INTO `entry` VALUES ((SELECT max(id) + 1 FROM `entry`), {0}, {1});\n" +
                "INSERT INTO `icon` VALUES ((SELECT max(id) + 1 FROM `icon`), (SELECT max(id) FROM `entry`)," +
                    "(SELECT `default_icon` FROM `type` WHERE `type`.`id` = (SELECT `type_id` FROM `entry` WHERE `entry`.`id` = (SELECT max(id) FROM `entry`))));\n" +
                "INSERT INTO `note` VALUES ((SELECT max(id) + 1 FROM `note`),{2},(SELECT max(id) FROM `entry`));\n" +
                "INSERT INTO `password` VALUES ((SELECT max(id) + 1 FROM `password`),{3},(SELECT max(id) FROM `entry`));\n" +
                "INSERT INTO `url` VALUES ((SELECT max(id) + 1 FROM `url`), ({4}), (SELECT max(id) FROM `entry`));\n" +
                "INSERT INTO `username` VALUES ((SELECT max(id) + 1 FROM `username`),{5},(SELECT max(id) FROM `entry`));\n" +
                "COMMIT;", entry, type, note, password, url, username);
        }
        /// <summary>
        /// Restituisce la query per eliminare tutte le informazioni riferite ad un record
        /// </summary>
        /// <param name="entryId">id del record da eliminare</param>
        /// <returns>string - query per eliminare i dati da ogni tabella</returns>
        protected override string EraseEntry(int entryId)
        {
            return String.Format("BEGIN TRANSACTION;\n" +
                "DELETE FROM `entry` WHERE `id` = {0};\n" +
                "DELETE FROM `icon` WHERE `id` = {0};\n" +
                "DELETE FROM `note` WHERE `id` = {0};\n" +
                "DELETE FROM `password` WHERE `id` = {0};\n" +
                "DELETE FROM `url` WHERE `id` = {0};\n" +
                "DELETE FROM `username` WHERE `id` = {0};\n" +
                "COMMIT;", entryId);
        }
        /// <summary>
        /// Restituisce la query per modificare i dati specificati di un record
        /// </summary>
        /// <param name="entryId">id del record da modificare</param>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="typeId">id del tipo da modificare</param>
        /// <param name="iconName">nuova icona da associare</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>string - query per modificare i dati di un record nelle tabelle specificate</returns>
        protected override string UpdateEntry(int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = String.Empty;
            if (!String.IsNullOrEmpty(entryName))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `entry` SET `name` = '{0}', `type_id` = {1} WHERE `id` = {2};\n", entryName, typeId, entryId));
            }
            if (!String.IsNullOrEmpty(iconName))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `icon` SET `name` = '{0}' WHERE `id` = {1};\n", iconName, entryId));
            }
            if (!String.IsNullOrEmpty(noteValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `note` SET `value` = '{0}' WHERE `id` = {1};\n", noteValue, entryId));
            }
            if (!String.IsNullOrEmpty(passwordValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `password` SET `value` = '{0}' WHERE `id` = {1};\n", passwordValue, entryId));
            }
            if (!String.IsNullOrEmpty(urlValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `url` SET `value` = '{0}' WHERE `id` = {1};\n", urlValue, entryId));
            }
            if (!String.IsNullOrEmpty(usernameValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION;\n" : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE `username` SET `value` = '{0}' WHERE `id` = {1};\n", usernameValue, entryId));
            }

            if (!String.IsNullOrEmpty(toReturn))
            {
                toReturn = String.Concat(toReturn, "COMMIT;");
            }

            return toReturn;
        }
    }
}
