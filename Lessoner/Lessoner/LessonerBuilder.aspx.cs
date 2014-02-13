using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
namespace Lessoner
{
    public partial class LessonerBuilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.StatusCode = 403;
            Response.End(); 
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

        [WebMethod]
        public static dynamic GetData()
        {
            if (StoredVars.Objects.Loggedin)
            {
                if (StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
                {
                    dynamic[] Return = new dynamic[1];
                    Return[0] = new dynamic[6];
                    DateTime Date = DateTime.Now.Date;
                    Date = Date.AddDays(-((double)HelperMethods.DayOfWeekToNumber(Date.DayOfWeek) - 1));
                    for (int i = 0; i < Return[0].Length; i++)
                    {
                        Return[0][i] = Date.ToString("dd.MM.yyyy");
                        Date = Date.AddDays(7);
                    }
                    return Return;
                }
                else
                {
                    return ErrorReturns.NoAccessAllowed;
                }
            }
            else
            {
                return ErrorReturns.NotLoggedIn;
            }
        }
    }
}