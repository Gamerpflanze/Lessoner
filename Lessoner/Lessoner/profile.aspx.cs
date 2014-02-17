﻿using System;
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