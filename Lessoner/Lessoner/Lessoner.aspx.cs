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
        public static List<List<string>>[] GetDays()
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    List<List<string>>[] Days = new List<List<string>>[5];
                    for (int i = 0; i < Days.Length; i++ )
                    {
                        Days[i] = new List<List<string>>();
                    }
                    cmd.CommandText = SQL.Statements.GetDays;
                    con.Open();
                    cmd.Parameters.AddWithValue("@KlassenID", StoredVars.Objects.KlasseID);
                    cmd.Parameters.AddWithValue("@i",-1);//Wert ändert sich in der for schleife
                    int CurrentLession = 0;
                    //For-Schleife für Montag bis Freitag
                    for (int x = 1; x <= 5; x++)
                    {
                        cmd.Parameters["@i"].Value=x;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Days[x - 1].Add(new List<string>());
                                Days[x - 1][CurrentLession].Add(reader["FindetStatt"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Name"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Titel"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Vorname"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Nachname"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Stunde_Beginn"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Stunde_Ende"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Stunde"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["Uhrzeit"].ToString());
                                Days[x - 1][CurrentLession].Add(reader["TagName"].ToString());
                                CurrentLession++;
                            }
                        }
                        CurrentLession = 0;
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