using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql;
namespace Lessoner
{
    public partial class LessonerBuilder : System.Web.UI.Page
    {
        //=================================================================
        //Globale Variablen

        int WeekIndex
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.WeekIndex;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.WeekIndex = value;
            }
        }

        List<DateTime> WeekBegins
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.WeekBegins;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.WeekBegins = value;
            }
        }

        //=================================================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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


                DateTime Date = DateTime.Now.Date;
                Date = Date.AddDays(-((double)HelperMethods.DayOfWeekToNumber(Date.DayOfWeek) - 1));
                for (int i = 0; i < 6; i++)
                {
                    StoredVars.Objects.LessonerBuilder.WeekBegins.Add(Date);
                    Date = Date.AddDays(7);
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");

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

        protected void btnLastDate_Click(object sender, EventArgs e)
        {
            if (WeekIndex > 0)
            {
                btnNextDate.Attributes.Remove("disabled");
                WeekIndex--;
                if (WeekIndex == 0)
                {
                    btnLastDate.Attributes.Add("disabled", "disabled");
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");
            }
        }

        protected void btnNextDate_Click(object sender, EventArgs e)
        {
            if (WeekIndex < WeekBegins.Count() - 1)
            {
                btnNextDate.Attributes.Remove("disabled");
                WeekIndex++;
                if (WeekIndex == WeekBegins.Count())
                {
                    btnLastDate.Attributes.Add("disabled", "disabled");
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");
            }
        }

        [WebMethod]
        public static dynamic GetData()
        {
            if (StoredVars.Objects.Loggedin)
            {
                if (StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
                {
                    dynamic[] Return = new dynamic[3];
                    Return[0] = new dynamic[6];
                    DateTime Date = DateTime.Now.Date;
                    Date = Date.AddDays(-((double)HelperMethods.DayOfWeekToNumber(Date.DayOfWeek) - 1));
                    for (int i = 0; i < Return[0].Length; i++)
                    {
                        Return[0][i] = Date.ToString("dd.MM.yyyy");
                        Date = Date.AddDays(7);
                    }
                    using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
                    {
                        using (MySqlCommand cmd = con.CreateCommand())
                        {
                            try
                            {
                                cmd.CommandText = SQL.Statements.GetFaecherverteilung;
                                con.Open();

                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    List<dynamic> Faecherverteilung = new List<dynamic>();
                                    while (reader.Read())
                                    {
                                        List<dynamic> FaecherZeiten = new List<dynamic>();
                                        FaecherZeiten.Add(reader["Stunde"]);
                                        FaecherZeiten.Add(TimeSpan.Parse(reader["Uhrzeit"].ToString()).ToString(@"hh\:mm"));
                                        FaecherZeiten.Add(TimeSpan.Parse(reader["Ende"].ToString()).ToString(@"hh\:mm"));
                                        Faecherverteilung.Add(FaecherZeiten.ToArray());
                                    }
                                    Return[1] = Faecherverteilung.ToArray();
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    //using()
                    Return[2] = GlobalWebMethods.GetLessonerBuilder(1, DateTime.Parse(Return[0][0]));
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