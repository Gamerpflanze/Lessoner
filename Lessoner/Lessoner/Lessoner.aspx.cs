using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using MySql.Data.MySqlClient;
namespace Lessoner
{
    public partial class Lessoner : System.Web.UI.Page
    {
        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static List<string>[] GetDays()
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    List<string>[] Days = new List<string>[5];
                    cmd.CommandText = SQL.Statements.GetDays;
                    con.Open();
                    cmd.Parameters.AddWithValue("@KlassenID", StoredVars.Objects.KlasseID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //For-Schleife für Montag bis Freitag
                            for (int x = 1; x <= 5; x++)
                            {
                                cmd.Parameters.AddWithValue("@i", x);
                                
                                Days[x - 1].Add(reader["FindetStatt"].ToString());
                                Days[x - 1].Add(reader["Name"].ToString());
                                Days[x - 1].Add(reader["Titel"].ToString());
                                Days[x - 1].Add(reader["Vorname"].ToString());
                                Days[x - 1].Add(reader["Nachname"].ToString());
                                Days[x - 1].Add(reader["Stunde_Beginn"].ToString());
                                Days[x - 1].Add(reader["Stunde_Ende"].ToString());
                                Days[x - 1].Add(reader["Stunde"].ToString());
                                Days[x - 1].Add(reader["Uhrzeit"].ToString());
                                Days[x - 1].Add(reader["TagName"].ToString());
                            }
                        }
                    }
                    con.Close();

                    return Days;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (StoredVars.Objects.Loggedin)
            {
                foreach (Control c in LoginControlls.Controls)
                {
                    c.Visible = false;
                }
                LinkButton ProfileLink = new LinkButton();
                ProfileLink.Text = StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
                LoginControlls.Controls.Add(ProfileLink);
            }
        }
        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string Username = GlobalWebMethods.GetLoginData(txtUsername.Text, txtPasswort.Text);
            LoginControlls.Controls.Clear();
            LinkButton ProfileLink = new LinkButton();
            ProfileLink.Text = Username;
            LoginControlls.Controls.Add(ProfileLink);
        }
    }
}