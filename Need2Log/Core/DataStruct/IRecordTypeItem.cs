namespace Core.DataStruct
{
    /// <summary>
    /// Interfaccia che rappresenta un tipo predefinito di record presente sul database
    /// </summary>
    public interface IRecordTypeItem
    {
        /// <summary>
        /// Restituisce il codice identificativo del record.
        /// </summary>
        /// <returns></returns>
        int GetId();
        /// <summary>
        /// Restituisce il nome del record.
        /// </summary>
        /// <returns>nome del record</returns>
        string GetName();
        /// <summary>
        /// Restituisce il nome dell'icona di riferimento senza il percorso.
        /// </summary>
        /// <returns>nome dell'icona senza percorso</returns>
        string GetIcon();
        /// <summary>
        /// Restituisce l'url associato al record di default.
        /// </summary>
        /// <returns>url predefinito associato al record</returns>
        string GetUrl();
    }
}
