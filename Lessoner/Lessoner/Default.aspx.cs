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

                OpenLogin.Style.Add("display", "none");
                PageDropDown.Style.Add("display", "block");
                ReadyPageDropDown();
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
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ErrorDisplay", "LoginFailed()", true);
                return;
            }
            catch(NoLoginException)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ErrorDisplay", "LoginFailed()", true);
                return;
            }
            LoginControlls.CssClass = "";
            LoginControlls.Controls.Clear();
            LinkButton ProfileLink = new LinkButton();
            ProfileLink.Text = Username;
            LoginControlls.Controls.Add(ProfileLink);
            OpenLogin.Style.Add("display", "none");
            PageDropDown.Style.Add("display", "block");
            ReadyPageDropDown();
        }
        private void ReadyPageDropDown()
        {
            User.InnerText = StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
            if(!StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
            {
                LinkLessonerBuilder.Style.Add("display", "none");
            }
            if (!StoredVars.Objects.Rights["studentmanagement"]["permission"])
            {
                LinkStudentManagement.Style.Add("display", "none");
            }
            if (!StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
            {
                //LinkLessonerBuilder.Dispose();
            }
        }
    }
}