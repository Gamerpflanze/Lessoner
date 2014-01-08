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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StoredVars.Objects = new StoredVars();
        }

        /// <summary>
        /// Ruft den Login des Benutzers ab
        /// </summary>
        /// <param name="Username">Der Benutzername</param>
        /// <param name="Passwort">Das Passwort</param>
        /// <returns>Ein string der Angibt ob man eingeloggt wurde oder nicht (siehe: Code/Enums)</returns>
        [WebMethod]
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
                        StoredVars.Objects.Rights = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    catch (NullReferenceException)
                    {
                        return LoginReturns.LoginDenited;
                    }
                    catch (Exception ex)
                    {
                        return LoginReturns.Error;
                    }
                }
            }
            return LoginReturns.LoginConfirmed;//true=angemeldet, false=falscher login 
        }
    }
}