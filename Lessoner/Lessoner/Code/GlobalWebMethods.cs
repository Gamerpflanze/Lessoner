using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Lessoner
{
    /// <summary>
    /// Ruft den Login des Benutzers ab
    /// </summary>
    /// <param name="Username">Der Benutzername</param>
    /// <param name="Passwort">Das Passwort</param>
    /// <returns>Ein string der Angibt ob man eingeloggt wurde oder nicht (siehe: Code/Enums)</returns>
    public class GlobalWebMethods
    {
        public static string GetLoginData(string Username, string Passwort)
        {
            //TODO: Datenbank abfrage für login
            StoredVars.Objects = new StoredVars();
            //TODO: Root PW setzen!
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {

                    SHA1 Hasher = SHA1.Create();
                    byte[] HashedPassword = Hasher.ComputeHash(Encoding.UTF8.GetBytes(Passwort));
                    cmd.CommandText = SQL.Statements.GetUserRights;
                    cmd.Parameters.AddWithValue("@Email", Username);
                    cmd.Parameters.AddWithValue("@Passwort", HashedPassword);
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StoredVars.AddRight((string)reader["RechtGruppe"], (string)reader["RechtName"], (bool)reader["RechtWert"]);
                            StoredVars.Objects.ID = Convert.ToInt32(reader["LoginID"]);
                        }
                        if (StoredVars.Objects.ID == -1)
                        {
                            throw new NoLoginException();
                        }
                        StoredVars.Objects.Loggedin = true;
                    }

                    if (StoredVars.Objects.Rights["login"]["isteacher"])//TODO:rootlogin
                    {
                        //Lehrer
                        cmd.CommandText = SQL.Statements.GetTeacherInfos;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LoginID", StoredVars.Objects.ID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                i++;
                                //l.ID, a.Email, l.Titel, l.Vorname, l.Name, l.Strasse, l.Hausnummer, l.PLZ, l.Ort, l.KlasseID
                                StoredVars.Objects.EMail = Username;
                                if (!DBNull.Value.Equals(reader["Titel"]))
                                {
                                    StoredVars.Objects.Title = Convert.ToString(reader["Titel"]);
                                }
                                StoredVars.Objects.Vorname = Convert.ToString(reader["Vorname"]);
                                StoredVars.Objects.Nachname = Convert.ToString(reader["Name"]);
                                StoredVars.Objects.Strasse = Convert.ToString(reader["Strasse"]);
                                StoredVars.Objects.HSN = Convert.ToString(reader["Hausnummer"]);
                                StoredVars.Objects.PLZ = Convert.ToString(reader["PLZ"]);
                                StoredVars.Objects.Ort = Convert.ToString(reader["Ort"]);
                                if (!DBNull.Value.Equals(reader["KlasseID"]))
                                {
                                    StoredVars.Objects.KlasseID = Convert.ToInt32(reader["KlasseID"]);
                                }
                            }
                            if (i == 0)
                            {
                                throw new MultipleUserException();
                            }
                            if (i > 1)
                            {
                                //return ErrorReturns.MultipleUserError;
                                throw new NoLoginException();
                            }
                        }
                    }
                    else
                    {
                        //Schueler
                        cmd.CommandText = SQL.Statements.GetStudentInfos;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LoginID", StoredVars.Objects.ID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                i++;
                                //l.ID, a.Email, l.Titel, l.Vorname, l.Name, l.Strasse, l.Hausnummer, l.PLZ, l.Ort, l.KlasseID
                                StoredVars.Objects.EMail = Username;
                                StoredVars.Objects.Vorname = Convert.ToString(reader["Vorname"]);
                                StoredVars.Objects.Nachname = Convert.ToString(reader["Name"]);
                                StoredVars.Objects.Strasse = Convert.ToString(reader["Strasse"]);
                                StoredVars.Objects.HSN = Convert.ToString(reader["Hausnummer"]);
                                StoredVars.Objects.PLZ = Convert.ToString(reader["PLZ"]);
                                StoredVars.Objects.Ort = Convert.ToString(reader["Ort"]);
                                StoredVars.Objects.KlasseID = Convert.ToInt32(reader["KlasseID"]);
                                StoredVars.Objects.KlasseName = Convert.ToString(reader["KlassenName"]);
                            }
                            if (i == 0)
                            {
                                throw new NoLoginException();
                            }
                            if (i > 1)
                            {
                                throw new MultipleUserException();
                            }
                        }
                    }
                    con.Close();

                }
            }
            StoredVars.Objects.Loggedin = true;
            return StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
        }
        public static Lesson[][] GetLessonerBuilder(int KlasseID, DateTime Date)
        {

            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.CommandText = SQL.Statements.CountLessoner;
                        cmd.Parameters.AddWithValue("@KlasseID", KlasseID);
                        cmd.Parameters.AddWithValue("@Datum", Date.ToString("yyyy-MM-dd"));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            List<Lesson> LessionList = new List<Lesson>();
                            Lesson[][] ReturnLessions = new Lesson[5][];

                            cmd.CommandText = SQL.Statements.GetLessonerBuilder;
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Lesson Current = new Lesson();

                                    if (DBNull.Value.Equals(reader["information"]))
                                    {
                                        Current.Information = Convert.ToString(reader["information"]);
                                    }
                                    Current.FindetStatt = Convert.ToBoolean(reader["FindetStatt"]);
                                    if (Current.FindetStatt)
                                    {
                                        Current.ID = Convert.ToInt32(reader["FachID"]);
                                        Current.NameLong = Convert.ToString(reader["Name"]);
                                        Current.NameShot = Convert.ToString(reader["NameKurz"]);
                                        Current.FachModID = Convert.ToInt32(reader["FachModID"]);
                                        Current.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);
                                        Current.StundeBeginn = Convert.ToInt32(reader["Stunde_Beginn"]);
                                        Current.StundeEnde = Convert.ToInt32(reader["Stunde_Ende"]);
                                        LessionList.Add(Current);
                                    }
                                }
                            }
                            con.Close();
                            //Inizialisieren von ReturnLessions
                            for (int i = 1; i <= 5; i++)
                            {
                                int IndexCount = 0;
                                for (int j = 0; j < LessionList.Count; j++)
                                {
                                    if (LessionList[j].TagInfoID == i)
                                    {
                                        IndexCount++;
                                    }
                                }
                                ReturnLessions[i - 1] = new Lesson[IndexCount];
                            }

                            //Einsortieren von Fächern
                            /*for(int i = 0; i<ReturnLessions.Length; i++)
                            {
                                for(int j = 0; j<ReturnLessions[i].Length; j++)
                                {
                                    for(int k = 0; k<LessionList.Count; k++)
                                    {
                                        if(LessionList[k].TagInfoID = i && j )
                                    }
                                }
                            }*/
                            for (int i = 1; i <= 5; i++)
                            {
                                List<Lesson> SortedLessions = new List<Lesson>();
                                int CurrentLession = 1;
                                for (int j = 0; j < LessionList.Count; j++)
                                {
                                    if (LessionList[j].TagInfoID == i)
                                    {
                                        if (LessionList[j].StundeBeginn == CurrentLession)
                                        {
                                            SortedLessions.Add(LessionList[j]);
                                            CurrentLession = LessionList[j].StundeEnde + 1;
                                        }
                                    }
                                }
                                ReturnLessions[i - 1] = SortedLessions.ToArray();
                            }

                            return ReturnLessions;
                        }
                        else
                        {
                            return new Lesson[5][];
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Fehlerbehebung
                    }
                }
                return new Lesson[5][];
            }
        }
        public static void SetDefaultStudent(int AnmeldungID)
        {

        }
    }
}
