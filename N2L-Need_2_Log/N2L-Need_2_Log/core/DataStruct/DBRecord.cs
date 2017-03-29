namespace N2L_Need_2_Log.core.DataStruct
{
    /// <summary>
    /// Classe che rappresenta le informazioni relative ad un record
    /// </summary>
    class DBRecord : IDBRecord
    {
        /// <summary>
        /// Proprietà che rappresenta l'id associato al record
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Proprietà che rappresenta il nome associato al record
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Proprietà che rappresenta l'icona associata al record
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Proprietà che rappresenta le note associate al record
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Proprietà che rappresenta la password dell'account salvato nel record
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Propreità che rappresenta lo url associato al record
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Propreità che rappresenta lo username dell'account salvato nel record
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Proprietà che rappresenta l'id del tipo predefinito associato al record
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Costruttore della classe DBRecord
        /// </summary>
        /// <param name="id">identificato del record sul db</param>
        /// <param name="name">nome del record</param>
        /// <param name="defaultIcon">icona associata al record</param>
        /// <param name="note">note associate al record</param>
        /// <param name="password">password associata al record</param>
        /// <param name="url">url associato al record</param>
        /// <param name="username">username associata al record</param>
        /// <param name="typeId">identificativo del tipo predefinito associato al record</param>
        public DBRecord(int id, string name, string defaultIcon, string note, string password, string url, string username, int typeId)
        {
            Id = id;
            Name = name;
            Icon = defaultIcon;
            Note = note;
            Password = password;
            Url = url;
            Username = username;
            TypeId = typeId;
        }
    }
}
