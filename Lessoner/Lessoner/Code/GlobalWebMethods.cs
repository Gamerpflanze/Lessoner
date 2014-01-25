﻿using System;
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
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        MD5 Hasher = MD5.Create();
                        byte[] HashedPassword = Hasher.ComputeHash(Encoding.Unicode.GetBytes(Passwort));
                        cmd.CommandText = SQL.Statements.GetUserRights;
                        cmd.Parameters.AddWithValue("@Email", Username);
                        cmd.Parameters.AddWithValue("@Passwort", HashedPassword);
                        con.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            int i = 0;//Zur Überprüfung ob EIN(!!!!) login mit diesem benutzer existiert
                            while (reader.Read())
                            {
                                i++;
                                StoredVars.Objects.Rights = Convert.ToInt32(reader["RechteID"]);
                                StoredVars.Objects.ID = Convert.ToInt32(reader["LoginID"]);
                            }
                            if (i == 0)
                            {
                                return LoginReturns.LoginDenited;
                            }
                            if (i > 1)
                            {
                                return LoginReturns.MultipleUserError;
                            }
                        }

                        if (StoredVars.Objects.Rights >= 2)//TODO:rootlogin
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
                                    return LoginReturns.LoginDenited;
                                }
                                if (i > 1)
                                {
                                    return LoginReturns.MultipleUserError;
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
                                }
                                if (i == 0)
                                {
                                    return LoginReturns.LoginDenited;
                                }
                                if (i > 1)
                                {
                                    return LoginReturns.MultipleUserError;
                                }
                            }
                        }
                        con.Close();
                    }
                    catch (NullReferenceException)
                    {
                        return LoginReturns.LoginDenited;
                    }
                    catch (Exception ex)
                    {
                        return LoginReturns.ExeptionError;
                    }
                }
            }
            StoredVars.Objects.Loggedin = true;
            return StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
        }
    }
}