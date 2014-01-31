using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Lessoner
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string[] informations()
        {
            string[] information = new string[9];
            information[0] = StoredVars.Objects.Title;
            information[1] = StoredVars.Objects.Vorname;
            information[2] = StoredVars.Objects.Nachname;
            information[3] = StoredVars.Objects.Strasse;
            information[4] = StoredVars.Objects.HSN;
            information[5] = StoredVars.Objects.PLZ;
            information[6] = StoredVars.Objects.Ort;

            return information;
        }
    }
}