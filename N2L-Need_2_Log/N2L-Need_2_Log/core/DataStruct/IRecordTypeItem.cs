namespace N2L_Need_2_Log.core.DataStruct
{
    /// <summary>
    /// Interfaccia che rappresenta le informazioni utili a descrivere un tipo predefinito di record
    /// </summary>
    public interface IRecordTypeItem
    {
        /// <summary>
        /// Restituisce il nome del record
        /// </summary>
        /// <returns>nome del record</returns>
        string GetName();
        /// <summary>
        /// Restituisce il nome dell'icona di riferimento senza il percorso
        /// </summary>
        /// <returns>nome dell'icona senza percorso</returns>
        string GetIcon();
        /// <summary>
        /// Restituisce l'url associato al record di default
        /// </summary>
        /// <returns>url predefinito associato al record</returns>
        string GetUrl();
    }
}
