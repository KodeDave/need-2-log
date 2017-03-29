using System.Data.Common;

namespace N2L_Need_2_Log.core
{
    /// <summary>
    /// Interfaccia che rappresenta le operazioni che devono poter essere eseguite
    /// sull'origine dati della applicazione.
    /// </summary>
    interface IDBRequest
    {
        /// <summary>
        /// Rappresenta un metodo utile ad ottenere una istanza di System.Data.Common.DbDataReader
        /// contenente tutti i record presenti nel database locale con le loro informazioni generali
        /// </summary>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        DbDataReader GetMainView();
        /// <summary>
        /// Rappresenta un metodo utile ad ottenere una istanza di tipo System.Data.Common.DbDataReader
        /// contenente tutte le informazioni presenti nel database locale relative al record indicato 
        /// </summary>
        /// <param name="entryId">identificativo del record nel database</param>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        DbDataReader GetRecordInfo(int entryId);
        /// <summary>
        /// Rappresenta un metodo utile ad ottenere dal db tutti i tipi predefinitigià esistenti
        /// e preconfigurati leggibili tramite una istanza di System.Data.Common.DbDataReader
        /// </summary>
        /// <returns>Ritorna un oggetto System.Data.Common.DbDataReader</returns>
        DbDataReader GetRecordsType();

        /// <summary>
        /// Rappresenta un metodo utile a creare un nuovo record all'interno del database
        /// </summary>
        /// <param name="entryName">nome del record</param>
        /// <param name="typeId">tipologia del record</param>
        /// <param name="noteValue">note aggiuntive</param>
        /// <param name="passwordValue">password del record</param>
        /// <param name="urlValue">url del sito associato al record</param>
        /// <param name="usernameValue">username associata al record</param>
        /// <returns>numero di righe inserite a seguito del comando</returns>
        int Insert(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue);
        /// <summary>
        /// Rappresenta un metodo utile ad aggiornare/modificare un record già esistente nel database
        /// </summary>
        /// <param name="entryId">id del record all'interno del database</param>
        /// <param name="entryName">nome del record</param>
        /// <param name="typeId">Id del tipo predefinito associato al record</param>
        /// <param name="iconName">nome dell'icona associata al record</param>
        /// <param name="noteValue">note aggiuntive</param>
        /// <param name="passwordValue">password del record</param>
        /// <param name="urlValue">url del sito associato al record</param>
        /// <param name="usernameValue">username associato al record</param>
        /// <returns>numero di righe modificate a seguito del comando</returns>
        int Update(int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue);
        /// <summary>
        /// Rappresenta un metodo utile a cancellare tutti gli elementi associati ad un record dal database.
        /// </summary>
        /// <param name="entryId">Id del record presente nel database</param>
        /// <returns>numero di righe eliminate a seguito del comando</returns>
        int Erase(int entryId);
        /// <summary>
        /// Rappresenta un metodo utile a verificare che il nome associato
        /// ad un record che si sta per inserire sia univoco
        /// </summary>
        /// <param name="entryName">nome del record</param>
        /// <returns>nome del record eventualmente modificato per rispettare l'univocità</returns>
        string CheckEntryName(string entryName);
        /// <summary>
        /// Rappresenta un metodo utile a creare una copia di backup del database
        /// </summary>
        /// <param name="fullName"> nome completo di path del database di backup</param>
        /// <param name="password"> password che si vuole settare per proteggere il nuovo db</param>
        void Backup(string fullName, string password);
        /// <summary>
        /// Rappresenta un metodo utile a modificare/assegnare una nuova password a protezione del database locale.
        /// </summary>
        /// <param name="newPassword"> nuova password</param>
        void ChangeSettings(string newPassword);
    }
}
