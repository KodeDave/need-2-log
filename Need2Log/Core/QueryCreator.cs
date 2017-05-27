using System;

namespace Core
{
    /// <summary>
    /// Classe astratta che dichiara il metodo pubblico ereditato dall'interfaccia e i metodi protetti utili alle classi concrete.
    /// </summary>
    internal abstract class QueryCreator : IQueryCreator
    {
        /// <summary>
        /// Metodo per richiedere la stringa di connessione, la stringa di test
        /// della connessione, i tipi ammissibili come parametro dalla classe o per
        /// richiedere di restituire tutti i record utili nella schermata principale
        /// </summary>
        /// <param name="query">Connect o Test o AdmittedType o MainMenuItems</param>
        /// <returns>string - query selezionata</returns>
        public string Create(Querable query)
        {
            string toReturn = string.Empty;

            switch (query)
            {
                case Querable.Connect:
                    toReturn = Connect();
                    break;
                case Querable.Test:
                    toReturn = Test();
                    break;
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
        /// Metodo per richiedere tutte le informazioni su un record o per eliminarlo
        /// </summary>
        /// <param name="query">DetailedInfo o EraseEntry</param>
        /// <param name="entryId">ID del record selezionato</param>
        /// <returns>string - query per richiedere tutti i dati di un elemento del db</returns>
        public string Create(Querable query, int entryId)
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
        /// Metodo per richiedere l'inserimento di un nuovo record.
        /// </summary>
        /// <param name="query">CreateNewEntry</param>
        /// <param name="entryName">nome del record assegnato</param>
        /// <param name="typeId">tipologia base di account</param>
        /// <param name="noteValue">valore della nota associato</param>
        /// <param name="passwordValue">valore della password associata</param>
        /// <param name="urlValue">valore dell'URL associato</param>
        /// <param name="usernameValue">valore dell'username associato</param>
        /// <returns>string - query per inserire un nuovo elemento nel db</returns>
        public string Create(Querable query, string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = string.Empty;

            if (query.Equals(Querable.CreateNewEntry))
            {
                toReturn = CreateNewEntry(entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);
            }

            return toReturn;
        }
        /// <summary>
        /// Metodo per richiedere la modifica di un record già esistente
        /// </summary>
        /// <param name="query">UpdateEntry</param>
        /// <param name="entryId">id del record da modificare</param>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="iconName">nuova icona da associare</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>string - query per modificare un record</returns>
        public string Create(Querable query, int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue)
        {
            string toReturn = string.Empty;

            if (query.Equals(Querable.UpdateEntry))
            {
                toReturn = UpdateEntry(entryId, entryName, typeId, iconName, noteValue, passwordValue, urlValue, usernameValue);
            }

            return toReturn;
        }
        /// <summary>
        /// Metodo per richiedere la verifica e la eventuale modifica del nome del nuovo record
        /// che si sta per inserire nel database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="entryName"></param>
        /// <returns>string - query per richiedere la verifica/modifica di un nome</returns>
        public string Create(Querable query, string entryName)
        {
            string toReturn = String.Empty;
            string entry = (String.IsNullOrEmpty(entryName) ? "Account" : entryName);

            if (query.Equals(Querable.CheckEntryName))
            {
                toReturn = CheckEntryName(entry);
            }

            return toReturn;
        }

        /// <summary>
        /// Restituisce la query per connettersi alla istanza di DB selezionata
        /// </summary>
        /// <returns>string - Query per connettersi al DB</returns>
        protected abstract string Connect();
        /// <summary>
        /// Restituisce la query per testare la connessione alla istanza di DB selezionata
        /// </summary>
        /// <returns>string - Query per testare la connessione al DB</returns>
        protected abstract string Test();
        /// <summary>
        /// Restituisce la query per render noti tutti i tipi predefiniti disponibili
        /// </summary>
        /// <returns>string - Query per accedere a tutta la view type</returns>
        protected abstract string AdmittedType();
        /// <summary>
        /// Restituisce la query per rendere noti tutti i record presenti e impostare il main menu
        /// </summary>
        /// <returns>string - query per accedere a tutta la view main</returns>
        protected abstract string MainMenuItems();
        /// <summary>
        /// Restituisce la query per rendere note tutte le informazioni relative ad un record
        /// </summary>
        /// <param name="entryId">id del record da ricercare</param>
        /// <returns>string - query per accedere alla view DetailedInfo</returns>
        protected abstract string DetailedInfo(int entryId);
        /// <summary>
        /// Restituisce la query per validare il nome del record prima di tentare di inserirlo
        /// </summary>
        /// <param name="entryName">nome da associare al nuovo record</param>
        /// <returns>string - query per verificare la validità di un nome prima di inserirlo nel db</returns>
        protected abstract string CheckEntryName(string entryName);
        /// <summary>
        /// Restituisce la query per inserire un nuovo recod nel db
        /// </summary>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="typeId">tipologia base di account</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>string - query per inserire in tutte le tabelle i valori relativi</returns>
        protected abstract string CreateNewEntry(string entryName, int typeId, string noteValue, string passwordValue, string urlValue, string usernameValue);
        /// <summary>
        /// Restituisce la query per eliminare tutte le informazioni riferite ad un record
        /// </summary>
        /// <param name="entryId">id del record da eliminare</param>
        /// <returns>string - query per eliminare i dati da ogni tabella</returns>
        protected abstract string EraseEntry(int entryId);
        /// <summary>
        /// Restituisce la query per modificare i dati specificati di un record
        /// </summary>
        /// <param name="entryId">id del record da modificare</param>
        /// <param name="entryName">nuovo nome da associare</param>
        /// <param name="typeId">id del tipo da modificare</param>
        /// <param name="iconName">nuova icona da associare</param>
        /// <param name="noteValue">nuovo valore delle note da associare</param>
        /// <param name="passwordValue">nuovo valore della password da associare</param>
        /// <param name="urlValue">nuovo valore dell'url da associare</param>
        /// <param name="usernameValue">nuovo username da associare</param>
        /// <returns>string - query per modificare i dati di un record nelle tabelle specificate</returns>
        protected abstract string UpdateEntry(int entryId, string entryName, int typeId, string iconName, string noteValue, string passwordValue, string urlValue, string usernameValue);
    }
}
