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
        public static string[] information()
        {
            string[] informations = new string[9];
            informations[0] = StoredVars.Objects.Title;
            informations[1] = StoredVars.Objects.Vorname;
            informations[2] = StoredVars.Objects.Nachname;
            informations[3] = StoredVars.Objects.Strasse;
            informations[4] = StoredVars.Objects.HSN;
            informations[5] = StoredVars.Objects.PLZ;
            informations[6] = StoredVars.Objects.Ort;

            return informations;
        }
    }
}