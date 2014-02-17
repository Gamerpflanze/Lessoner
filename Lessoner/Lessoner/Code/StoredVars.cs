using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner
{
    /// <summary>
    /// Speichert Objekte Klassenübergreifend pro session
    /// </summary>
    public class StoredVars
    {
        //Variablen nicht statisch als public deklarieren
        public Dictionary<string, Dictionary<string, bool>> Rights = new Dictionary<string,Dictionary<string,bool>>();
        public int ID = -1;
        public string Vorname = "";
        public string Nachname = "";
        public string EMail = "";
        public string Title = "";
        public string Strasse= "";
        public string HSN = "";
        public string Ort = "";
        public string PLZ = "";
        public int KlasseID = -1;
        public string KlasseName = "";
        public bool Loggedin = false;

        //Cache
        public LessonerBuilderCache LessonerBuilder = new LessonerBuilderCache();


        /// <summary>
        /// Gibt die instanz von StoredVars Zurück
        /// </summary>
        public static StoredVars Objects
        {
            get
            {
                return HttpContext.Current.Session["Session"] as StoredVars;
            }
            set
            {
                HttpContext.Current.Session["Session"] = value;
            }
        }
        public static void AddRight(string Group, string Name, bool Value)
        {
            if (!StoredVars.Objects.Rights.ContainsKey(Group))
            {
                StoredVars.Objects.Rights.Add(Group, new Dictionary<string, bool>());
            }
            StoredVars.Objects.Rights[Group].Add(Name, Value);
        }
    }
}