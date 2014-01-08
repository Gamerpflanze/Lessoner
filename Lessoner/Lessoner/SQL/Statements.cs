using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner.SQL
{
    public static class Statements
    {
        /// <summary>
        /// Gibt die Rechte des Benutzers zurück
        /// </summary>
        public static string GetUserRights = @"SELECT r.ID FROM tbanmeldung as a
                                               JOIN tbrechte as r
                                               ON r.ID = a.ID
                                               WHERE Email=@Email AND Passwort = @Passwort";
    }
}