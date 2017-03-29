using System;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// enumerato utile a definire in modo fisso quale tipo di query è necessario restituire
    /// </summary>
    enum Querable
    {
        /// <summary>
        /// Se è necessario conoscere tutti i tipi predefiniti presenti nel DB e le loro relative informazioni
        /// </summary>
        AdmittedType = 1,
        /// <summary>
        /// Se è necessario conoscere le informazioni base di tutti i record per popolare il menù principale
        /// </summary>
        MainMenuItems,
        /// <summary>
        /// Se è necessario conoscere le informazioni dettagliate relative ad un record del DB
        /// </summary>
        DetailedInfo,
        /// <summary>
        /// Se è necessario creare un nuovo record
        /// </summary>
        CreateNewEntry,
        /// <summary>
        /// Se è necessario eliminare un record già esistente
        /// </summary>
        EraseEntry,
        /// <summary>
        /// Se è necessario aggiornare un record già esistente
        /// </summary>
        UpdateEntry,
        /// <summary>
        /// Se è necessario verificare la correttezza di un nome da associare ad un nuovo record
        /// </summary>
        CheckEntryName
    };
    /// <summary>
    /// Fornisce un metodo static basato su quattro overload utile a 
    /// creare ed inoltrare la corretta query sql al database dell'applicativo.
    /// restituisce null se qualcosa non è corretto.
    /// </summary>
    static class QueryString
    {
        /// <summary>
        /// Overload per richiedere i tipi ammissibili come parametro dalla classe
        /// o per richiedere di restituire tutti i record utili nella schermata principale
        /// </summary>
        /// <param name="query">AdmittedType o MainMenuItems</param>
        /// <returns>query selezionata</returns>
        public static string Create(Querable query)
        {
            string toReturn = string.Empty;

            switch (query)
            {
                case Querable.AdmittedType:
                    toReturn = AdmittedType();
                    break;
                case Querable.MainMenuItems:
                    toReturn = MainMenuItems();
                    break;
            }

            return toReturn;
        }
        /// <summary>
        /// Overload per richiedere tutte le informazioni su un record o per eliminarlo
        /// </summary>
        /// <param name="query">DetailedInfo o EraseEntry</param>
        /// <param name="entryId">ID del record selezionato</param>
        /// <returns></returns>
        public static string Create(Querable query, int entryId)
        {
            string toReturn = string.Empty;

            switch (query)
            {
                case Querable.DetailedInfo:
                    toReturn = DetailedInfo(entryId);
                    break;
                case Querable.EraseEntry:
                    toReturn = EraseEntry(entryId);
                    break;
            }

            return toReturn;
        }
        /// <summary>
        /// Overload per richiedere l'inserimento di un nuovo record.
        /// </summary>
        /// <param name="query">CreateNewEntry</param>
        /// <param name="entryName">nome del record assegnato</param>
        /// <param name="typeId">tipologia base di account</param>
        /// <param name="noteValue">valore della nota associato</param>
        /// <param name="passwordValue">valore della password associata</param>
        /// <param name="urlValue">valore dell'URL associato</param>
        /// <param name="usernameValue">valore dell'username associato</param>
        /// <returns></returns>
        public static string Create(Querable query, string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = string.Empty;

            if (query.Equals(Querable.CreateNewEntry))
            {
                toReturn = CreateNewEntry(entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);
            }

            return toReturn;
        }
        /// <summary>
        /// Overload per richiedere la modifica di un record già esistente
        /// </summary>
        /// <param name="query">UpdateEntry</param>
        /// <param name="entryId">id del record da modificare</param>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="iconName">nuova icona da associare</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns></returns>
        public static string Create(Querable query, int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = string.Empty;

            if (query.Equals(Querable.UpdateEntry))
            {
                toReturn = UpdateEntry(entryId, entryName, typeId, iconName, noteValue, passwordValue, urlValue, usernameValue);
            }

            return toReturn;
        }
        /// <summary>
        /// Overload per richiedere la verifica e la eventuale modifica del nome del nuovo record
        /// che si sta per inserire nel database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public static string Create(Querable query, string entryName)
        {
            string toReturn = String.Empty;

            if (query.Equals(Querable.CheckEntryName))
            {
                toReturn = CheckEntryName(entryName);
            }

            return toReturn;
        }

        /// <summary>
        /// Restituisce la query per render noti tutti i tipi predefiniti disponibili
        /// </summary>
        /// <returns>Query per accedere a tutta la view type</returns>
        private static string AdmittedType()
        {
            return ("SELECT * FROM `type` ORDER BY `id` ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// Restituisce la query per rendere noti tutti i record presenti e impostare il main menu
        /// </summary>
        /// <returns>query per accedere a tutta la view main</returns>
        private static string MainMenuItems()
        {
            return ("SELECT * FROM `main` ORDER BY `ID` ASC LIMIT 0, 50000;");

        }
        /// <summary>
        /// Restituisce la query per rendere note tutte le informazioni relative ad un record
        /// </summary>
        /// <param name="entryId">id del record da ricercare</param>
        /// <returns>query per accedere alla view DetailedInfo</returns>
        private static string DetailedInfo(int entryId)
        {
            return ("SELECT * FROM `DetailedInfo`" +
                ((entryId > 0 || !entryId.Equals(null)) ? (" WHERE `ID` = " + entryId) : ("")) +
                " ORDER BY `ID` ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// Restituisce la query per validare il nome del record prima di tentare di inserirlo
        /// </summary>
        /// <param name="entryName">nome da associare al nuovo record</param>
        /// <returns></returns>
        private static string CheckEntryName(string entryName)
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
        /// <returns>query per inserire in tutte le tabelle i valori relativi</returns>
        private static string CreateNewEntry(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
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
        /// <returns>query per eliminare i dati da ogni tabella</returns>
        private static string EraseEntry(int entryId)
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
        /// <param name="iconName">nuova icona da associare</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>query per modificare i dati di un record nelle tabelle specificate</returns>
        private static string UpdateEntry(int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
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
