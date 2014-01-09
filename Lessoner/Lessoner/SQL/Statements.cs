using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner.SQL
{
    public static class Statements
    {
        /// <summary>
        /// Gibt die Rechte und die ID des Benutzers zurück. @Email = die Email, @Passwort = Das Gehashte Passwort in einer länge von 16byte
        /// </summary>
        public const string GetUserRights = @"SELECT r.ID, a.ID as LoginID FROM tbanmeldung as a
                                            JOIN tbrechte as r
                                            ON r.ID = a.ID
                                            WHERE Email=@Email AND Passwort = @Passwort";

        /// <summary>
        /// Gibt die Informationen eines Lehrers Zurück. @LoginID = Die Anmelde ID des Lehrers
        /// </summary>
        public const string GetTeacherInfos = @"SELECT l.ID, a.Email, l.Titel, l.Vorname, l.Name, l.Strasse, l.Hausnummer, l.PLZ, l.Ort, l.KlasseID FROM tbanmeldung as a
                                                JOIN tblehrer as l
                                                ON l.AnmeldungID = a.ID
                                                WHERE a.ID = @LoginID";
    }
}