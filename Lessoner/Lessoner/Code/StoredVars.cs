﻿using System;
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
        public int Rights = -1;
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
        public bool Loggedin = false;

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
    }
}