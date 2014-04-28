using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
namespace Lessoner
{
    public partial class SetNewPasswort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool HasKey = false;
            foreach(string i in Request.QueryString.Keys)
            {
                if(i=="key")
                {
                    HasKey = true;
                    break;
                }
            }
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.CountSetPasswortParameter;
                    cmd.Parameters.AddWithValue("@Parameter", Request.QueryString["key"]);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) <= 0 && HasKey || !HasKey)
                    {
                        Response.Redirect("/default.aspx", true);
                    }
                    con.Close();
                }
            }
        }

        protected void SetPassword_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = SQL.Statements.SetFirstPasswort;
                    cmd.Parameters.AddWithValue("@Passwort", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(Passwort.Text)));
                    cmd.Parameters.AddWithValue("@Parameter", Request.QueryString["key"]);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            Response.Redirect("/default.aspx", true);
        }
    }
}