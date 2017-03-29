namespace N2L_Need_2_Log.core.DataStruct
{
    /// <summary>
    /// Classe che rappresenta un tipo predefinito di record presente sul database
    /// </summary>
    class RecordTypeItem : IRecordTypeItem
    {
        protected int Id { get; set; }
        protected string Name { get; set; }
        protected string DefaultIcon { get; set; }
        protected string Url { get; set; }

        /// <summary>
        /// Costruttore della classe RecordTypeItem
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
        /// Restituisce il nome del record
        /// </summary>
        /// <returns>nome del record</returns>
        public string GetName()
        {
            return Name;
        }
        /// <summary>
        /// Restituisce il nome dell'icona di riferimento senza il percorso
        /// </summary>
        /// <returns>nome dell'icona senza percorso</returns>
        public string GetIcon()
        {
            return DefaultIcon;
        }
        /// <summary>
        /// Restituisce l'url associato al record di default
        /// </summary>
        /// <returns>url predefinito associato al record</returns>
        public string GetUrl()
        {
            return Url;
        }
    }
}