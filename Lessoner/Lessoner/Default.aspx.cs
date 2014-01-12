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
            //SELECT Count(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'dbLessoner'
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Count(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'dbLessoner'";
                    con.Open();
                    if(Convert.ToInt32(cmd.ExecuteScalar())==0)
                    {
                        cmd.CommandText = SQL.Statements.CreateDatabase;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
        }
        [WebMethod]
        public static string CheckLoggedin()
        {
            if (StoredVars.Objects.Loggedin)
            {
                return StoredVars.Objects.Vorname +" "+ StoredVars.Objects.Nachname;   
            }
            else 
            {
                return "";
            }
        }
        [WebMethod]
        public static string GetLoginData(string Username, string Passwort)
        {
            return GlobalWebMethods.GetLoginData(Username, Passwort);
        }
    }
}