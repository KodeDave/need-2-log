using System;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// enumerato utile a definire in modo fisso quale tipo di query è necessario restituire
    /// </summary>
    enum Querable
    {
        AdmittedType = 1,
        MainMenuItems,
        DetailedInfo,
        CreateNewEntry,
        EraseEntry,
        UpdateEntry
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
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="entryName"></param>
        /// <param name="typeId"></param>
        /// <param name="noteValue"></param>
        /// <param name="passwordValue"></param>
        /// <param name="urlValue"></param>
        /// <param name="usernameValue"></param>
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
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="entryId"></param>
        /// <param name="entryName"></param>
        /// <param name="iconName"></param>
        /// <param name="typeId"></param>
        /// <param name="noteValue"></param>
        /// <param name="passwordValue"></param>
        /// <param name="urlValue"></param>
        /// <param name="usernameValue"></param>
        /// <returns></returns>
        public static string Create(Querable query, int entryId, string entryName, string iconName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = string.Empty;

            if (query.Equals(Querable.UpdateEntry))
            {
                toReturn = UpdateEntry(entryId, entryName, iconName, noteValue, passwordValue, urlValue, usernameValue);
            }

            return toReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Query per accedere a tutta la view type</returns>
        private static string AdmittedType()
        {
            return ("SELECT * FROM type ORDER BY id ASC LIMIT 0, 50000;");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string MainMenuItems()
        {
            return ("SELECT * FROM main ORDER BY ID ASC LIMIT 0, 50000;");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        private static string DetailedInfo(int entryId)
        {
            return ("SELECT * FROM DetailedInfo "+
                ((entryId > 0)?(" WHERE ID = " + entryId):("")) +
                " ORDER BY `ID` ASC LIMIT 0, 50000;");
            //SELECT entry.id AS ID, entry.name AS NAME, icon.name AS IMAGE, note.value as NOTE, password.value AS PASSWORD,
            //        url.value AS URL, username.value AS USERNAME
            //    FROM entry, icon, note, password, url, username
            //    WHERE icon.entry_id = entry.id AND note.entry_id = entry.id AND
            //        password.entry_id = entry.id AND url.entry_id = entry.id AND username.entry_id = entry.id;
            //"SELECT entry.id AS ID, entry.name AS NAME, icon.name AS IMAGE, url.value AS URL " +
            //    "FROM entry, icon, url WHERE icon.entry_id = entry.id AND url.entry_id = entry.id;"
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="typeId"></param>
        /// <param name="noteValue"></param>
        /// <param name="passwordValue"></param>
        /// <param name="urlValue"></param>
        /// <param name="usernameValue"></param>
        /// <returns></returns>
        private static string CreateNewEntry(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string entry = (String.IsNullOrEmpty(entryName)) ? ("'Record_' + (SELECT max(id) + 1 FROM entry)") : ('\'' + entryName + '\'');
            int type = (typeId.Equals(null)) ? (1) : (typeId);
            string note = (String.IsNullOrEmpty(noteValue)) ? ("NULL") : ('\'' + noteValue + '\'');
            string password = (String.IsNullOrEmpty(passwordValue)) ? ("NULL") : ('\'' + passwordValue + '\'');
            string url = (String.IsNullOrEmpty(urlValue)) ? ("SELECT url FROM type WHERE type.id = (SELECT type_id FROM entry WHERE entry.id = (SELECT max(id) FROM entry)))")
                                                : ('\'' + urlValue + '\'');
            string username = (usernameValue.Equals(null)) ? ("NULL") : ('\'' + usernameValue + '\'');

            return String.Format("BEGIN TRANSACTION; " +
                "INSERT INTO `entry` VALUES ((SELECT max(id) + 1 FROM entry), {0}, {1}); " +
                "INSERT INTO icon VALUES ((SELECT max(id) + 1 FROM icon), (SELECT max(id) FROM entry), " +
                    "(SELECT default_icon FROM type WHERE type.id = (SELECT type_id FROM entry WHERE entry.id = (SELECT max(id) FROM entry))));" +
                "INSERT INTO note VALUES ((SELECT max(id) + 1 FROM note),{2},(SELECT max(id) FROM entry));" +
                "INSERT INTO password VALUES ((SELECT max(id) + 1 FROM password),'{3}',(SELECT max(id) FROM entry));" +
                "INSERT INTO url VALUES ((SELECT max(id) + 1 FROM url), {4},SELECT max(id) FROM entry));" +
                "INSERT INTO username VALUES ((SELECT max(id) + 1 FROM username),'{5}',(SELECT max(id) FROM entry)); " +
                "COMMIT;", entry, type, note, password, url, username);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <returns></returns>
        private static string EraseEntry(int entryId)
        {
            return String.Format("BEGIN TRANSACTION; " +
                "DELETE FROM entry WHERE id = {0}; " +
                "DELETE FROM note WHERE id = {0}; " +
                "DELETE FROM password WHERE id = {0}; " +
                "DELETE FROM url WHERE id = {0}; " +
                "DELETE FROM username WHERE id = {0}; " +
                "COMMIT;", entryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="entryName"></param>
        /// <param name="iconName"></param>
        /// <param name="noteValue"></param>
        /// <param name="passwordValue"></param>
        /// <param name="urlValue"></param>
        /// <param name="usernameValue"></param>
        /// <returns></returns>
        private static string UpdateEntry(int entryId, string entryName, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            //
            string toReturn = String.Empty;
            if (!String.IsNullOrEmpty(entryName))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE entry SET name = {0} WHERE id = {1}; ", entryName, entryId));
            }
            if (!String.IsNullOrEmpty(iconName))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE icon SET name = {0} WHERE id = {1}; ", iconName, entryId));
            }
            if (!String.IsNullOrEmpty(noteValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE note SET value = {0} WHERE id = {1}; ", noteValue, entryId));
            }
            if (!String.IsNullOrEmpty(passwordValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE password SET value = {0} WHERE id = {1}; ", passwordValue, entryId));
            }
            if (!String.IsNullOrEmpty(urlValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE url SET value = {0} WHERE id = {1}; ", urlValue, entryId));
            }
            if (!String.IsNullOrEmpty(usernameValue))
            {
                toReturn = String.IsNullOrEmpty(toReturn) ? "BEGIN TRANSACTION; " : toReturn;
                toReturn = String.Concat(toReturn, String.Format("UPDATE username SET value = {0} WHERE id = {1}; ", usernameValue, entryId));
            }

            if (!String.IsNullOrEmpty(toReturn))
            {
                toReturn = String.Concat(toReturn, "COMMIT;");
            }

            return toReturn;
        }
    }
}
