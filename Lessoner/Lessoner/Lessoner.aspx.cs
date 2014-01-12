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
        [WebMethod]
        public static string GetLoginData(string Username, string Passwort)
        {
            return GlobalWebMethods.GetLoginData(Username, Passwort);
        }
    }
}