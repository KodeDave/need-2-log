namespace Core.DataStruct
{
    /// <summary>
    /// Interfaccia che rappresenta le informazioni utili a descrivere un record
    /// </summary>
    public interface IDBRecord
    {
        /// <summary>
        /// Proprietà che rappresenta l'id associato al record
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Proprietà che rappresenta il nome associato al record
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Proprietà che rappresenta l'icona associata al record
        /// </summary>
        string Icon { get; set; }
        /// <summary>
        /// Proprietà che rappresenta le note associate al record
        /// </summary>
        string Note { get; set; }
        /// <summary>
        /// Proprietà che rappresenta la password dell'account salvato nel record
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// Proprietà che rappresenta lo url associato al record
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// Propreità che rappresenta lo username dell'account salvato nel record
        /// </summary>
        string Username { get; set; }
        /// <summary>
        /// Proprietà che rappresenta l'id del tipo predefinito associato al record
        /// </summary>
        int TypeId { get; set; }
    }
}
