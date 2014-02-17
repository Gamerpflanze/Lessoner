using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace Lessoner
{
    public partial class Lessoner : System.Web.UI.Page
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
    }
}