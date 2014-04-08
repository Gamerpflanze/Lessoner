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

            OldPasswort.Attributes.Add("onKeyDown", "jQuery('#OldPass').removeClass('has-error');");
            NewPassword1.Attributes.Add("onKeyDown", "jQuery('#NewPass1').removeClass('has-error');jQuery('#NewPass2').removeClass('has-error');");
            NewPassword2.Attributes.Add("onKeyDown", "jQuery('#NewPass1').removeClass('has-error');jQuery('#NewPass2').removeClass('has-error');");
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
            OpenLogin.Style.Add("display", "none");
            PageDropDown.Style.Add("display", "block");
            ReadyPageDropDown();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseLogin", "jQuery('#LoginModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
        }
        private void ReadyPageDropDown()
        {
            User.InnerText = StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
            LinkLessoner.Style["display"] = "inline";
            if(StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
            {
                LinkLessonerBuilder.Style["display"] = "inline";
            }
            if (StoredVars.Objects.Rights["studentmanagement"]["permission"])
            {
                LinkStudentManagement.Style["display"] = "inline";
            }
            if (StoredVars.Objects.Rights["teachermanagement"]["permission"])
            {
                LinkTeacherMamagement.Style["display"] = "inline";
            }
        }

        protected void Logoutbutton_Click(object sender, EventArgs e)
        {
            StoredVars.Objects = new StoredVars();
            Response.Redirect("/default.aspx", true);
        }

        protected void SavePassword_Click(object sender, EventArgs e)
        {
            using(MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using(MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.CheckPassword;
                    cmd.Parameters.AddWithValue("@Email", StoredVars.Objects.EMail);
                    cmd.Parameters.AddWithValue("@Passwort", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(OldPasswort.Text)));
                    if(Convert.ToInt32(cmd.ExecuteScalar())==1)
                    {
                        if(NewPassword1.Text==NewPassword2.Text&&NewPassword1.Text!="")
                        {
                            cmd.CommandText = SQL.Statements.InsertNewPasswort;
                            cmd.Parameters.AddWithValue("@NewPasswort", SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(NewPassword1.Text)));
                            cmd.ExecuteNonQuery();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ClosePWChanger", "jQuery('#ChangePasswordModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NewPassError", "jQuery('#NewPass1').addClass('has-error');jQuery('#NewPass2').addClass('has-error');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OldPassError", "jQuery('#OldPass').addClass('has-error');", true);
                    }
                    con.Close();
                }
            }
        }
    }
}