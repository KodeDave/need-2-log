namespace Core.DataStruct
{
    /// <summary>
    /// Classe che rappresenta un tipo predefinito di record presente sul database
    /// </summary>
    public class RecordTypeItem : IRecordTypeItem
    {
        /// <summary>
        /// Rappresenta il codice identificativo del record.
        /// </summary>
        protected int Id { get; set; }
        /// <summary>
        /// Rappresenta il nome associato al record.
        /// </summary>
        protected string Name { get; set; }
        /// <summary>
        /// Rappresenta il nome dell'icona di riferimento senza il suo percorso.
        /// </summary>
        protected string DefaultIcon { get; set; }
        /// <summary>
        /// Rappresenta l'URL associato al record.
        /// </summary>
        protected string Url { get; set; }

        /// <summary>
        /// Costruttore della classe RecordTypeItem.
        /// </summary>
        /// <param name="id">id del record</param>
        /// <param name="name">nome predefinito</param>
        /// <param name="defaultIcon">icona predefinita</param>
        /// <param name="url">url predefinito</param>
        public RecordTypeItem(int id, string name, string defaultIcon, string url)
        {
            Id = id;
            Name = name;
            DefaultIcon = defaultIcon;
            Url = url;
        }

        /// <summary>
        /// Restituisce il codice identificativo del record.
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return Id;
        }
        /// <summary>
        /// Restituisce il nome del record.
        /// </summary>
        /// <returns>nome del record</returns>
        public string GetName()
        {
            return Name;
        }
        /// <summary>
        /// Restituisce il nome dell'icona di riferimento senza il percorso.
        /// </summary>
        /// <returns>nome dell'icona senza percorso</returns>
        public string GetIcon()
        {
            return DefaultIcon;
        }
        /// <summary>
        /// Restituisce l'url associato al record di default.
        /// </summary>
        /// <returns>url predefinito associato al record</returns>
        public string GetUrl()
        {
            return Url;
        }
    }
}
