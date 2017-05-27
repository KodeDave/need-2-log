namespace Core
{   
     /// <summary>
     /// Enumerato utile a definire in modo fisso quale tipo di query è necessario restituire
     /// </summary>
    public enum Querable
    {
        /// <summary>
        /// Se è necessario effettuare la connessione al DB.
        /// </summary>
        Connect = 1,
        /// <summary>
        /// Se è necessario testare la connessione e le credenziali con cui si accede al DB.
        /// </summary>
        Test,
        /// <summary>
        /// Se è necessario conoscere tutti i tipi predefiniti presenti nel DB e le loro relative informazioni
        /// </summary>
        AdmittedType,
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
    /// Interfaccia che fornisce un metodo basato su quattro overload utile a ed inoltrare la corretta query sql al database dell'applicativo.
    /// </summary>
    internal interface IQueryCreator
    {
        /// <summary>
        /// Overload per richiedere la stringa di connessione, la stringa di test
        /// della connessione, i tipi ammissibili come parametro dalla classe o per
        /// richiedere di restituire tutti i record utili nella schermata principale
        /// </summary>
        /// <param name="query">Connect o Test o AdmittedType o MainMenuItems</param>
        /// <returns>string - query selezionata</returns>
        string Create(Querable query);
        /// <summary>
        /// Overload per richiedere tutte le informazioni su un record o per eliminarlo
        /// </summary>
        /// <param name="query">DetailedInfo o EraseEntry</param>
        /// <param name="entryId">ID del record selezionato</param>
        /// <returns></returns>
        string Create(Querable query, int entryId);
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
        string Create(Querable query, string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue);
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
        string Create(Querable query, int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue);
        /// <summary>
        /// Overload per richiedere la verifica e la eventuale modifica del nome del nuovo record
        /// che si sta per inserire nel database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="entryName"></param>
        /// <returns></returns>
        string Create(Querable query, string entryName);
    }
}
