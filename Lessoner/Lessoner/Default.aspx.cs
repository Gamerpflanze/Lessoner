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
            if (StoredVars.Objects.Loggedin)
            {
                foreach (Control c in LoginControlls.Controls)
                {
                    c.Visible = false;
                }
                LinkButton ProfileLink = new LinkButton();
                ProfileLink.Text = StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
                LoginControlls.Controls.Add(ProfileLink);
                LoginElements.Visible = false;
            }
            else
            {
                UserElements.Visible = false;
            }
        }
        protected void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string Username;
            try
            {
                Username = GlobalWebMethods.GetLoginData(txtUsername.Text, txtPasswort.Text);
            }
            catch(MultipleUserException)
            {

                return;
            }
            catch(NoLoginException)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page),"ErrorDisplay","alert('beeeb')",true);
                return;
            }
            LoginControlls.Controls.Clear();
            LinkButton ProfileLink = new LinkButton();
            ProfileLink.Text = Username;
            LoginControlls.Controls.Add(ProfileLink);
        }
    }
}