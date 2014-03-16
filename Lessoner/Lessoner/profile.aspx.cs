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
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
#if !DEBUG
                if (!StoredVars.Objects.Loggedin)
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
                if (!StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
#endif
            int ProfileID;
            if(Request.QueryString.AllKeys.Contains("profileid"))
            {
                ProfileID = Convert.ToInt32(Request.QueryString["profileid"]);
            }
            else
            {
                ProfileID = -1;
            }

            if(ProfileID==-1)
            {
                if(StoredVars.Objects.Rights["login"]["isteacher"])
                {
                    string title = StoredVars.Objects.Title;
                    lblProfileOf.Text="";
                    if(title!="")
                    {
                        lblProfileOf.Text += title + " ";
                    }
                    else
                    {
                    }
                    lblProfileOf.Text += StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
                    PersonTyle.Text = "Lehrer";
                }
                else
                { 
                    PersonTyle.Text = "Schueler";
                }

                if (StoredVars.Objects.KlasseName == "")
                {
                    Class.Text = "Keine";
                }
                else
                {
                    Class.Text = StoredVars.Objects.KlasseName;
                }
                Place.Text = StoredVars.Objects.Ort;
                Plz.Text = StoredVars.Objects.PLZ;
                Street.Text = StoredVars.Objects.Strasse;
                Homenumber.Text = StoredVars.Objects.HSN;
            }
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static string[] GetData()
        {
            //TODO: Root PW setzen!
            //TODO-Pasi: Anderes Statement setzen! GetStudentProfile
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    List<string> profiledata = new List<string>();
                    cmd.CommandText = SQL.Statements.GetStudentInfos;
                    con.Open();
                    cmd.Parameters.AddWithValue("@LoginID", StoredVars.Objects.ID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            profiledata.Add(reader["Email"].ToString());
                            profiledata.Add(reader["Vorname"].ToString());
                            profiledata.Add(reader["Name"].ToString());
                            profiledata.Add(reader["Strasse"].ToString());
                            profiledata.Add(reader["Hausnummer"].ToString());
                            profiledata.Add(reader["PLZ"].ToString());
                            profiledata.Add(reader["Ort"].ToString());
                            profiledata.Add(reader["Path"].ToString());
                        }
                    }
                    con.Close();
                    profiledata.Add(StoredVars.Objects.KlasseName);
                    return profiledata.ToArray();
                }
            }
        }
        [WebMethod]
        public static string CheckLoggedin()
        {
            if (StoredVars.Objects.Loggedin)
            {
                return StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
            }
            else
            {
                return "";
            }
        }
    }
}