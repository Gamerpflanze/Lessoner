using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
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
        //Dient zur Kürzung
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

        //TODO: Exception handling
        protected void Page_Load(object sender, EventArgs e)
        {
            //EventHandler
            this.PreRender += new System.EventHandler(this.LessonerBuilder_PreRender);
            //
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
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.GetClasses;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool first = true;
                        while (reader.Read())
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            LinkButton ClassLink = new LinkButton();

                            ClassLink.Attributes.Add("data-id", reader["ID"].ToString());
                            ClassLink.Text = reader["Name"].ToString();
                            if(first)
                            {
                                first = false;
                                lbtnOpenClassMenu.Text = ClassLink.Text + "<span class=\"caret\"></span>";
                                lbtnOpenClassMenu.Attributes.Add("data-id", reader["ID"].ToString());
                            }
                            ClassLink.Click += new EventHandler(ClassSelect_Click);
                            
                            li.Controls.Add(ClassLink);
                            ClassList.Controls.Add(li);
                        }
                    }
                    con.Close();
                }
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
                btnLastDate.Attributes.Remove("disabled");
                WeekIndex++;

                if (WeekIndex == WeekBegins.Count() - 1)
                {
                    btnNextDate.Attributes.Add("disabled", "disabled");
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");
            }
        }
        protected void ClassSelect_Click(object sender, EventArgs e)
        {
            LinkButton ClassButton = sender as LinkButton;

            lbtnOpenClassMenu.Text = ClassButton.Text+"<span class=\"caret\"></span>";
            lbtnOpenClassMenu.Attributes["data-id"] = ClassButton.Attributes["data-id"];
        }

        private void LoadLessoner()
        {
            DateTime Week = Convert.ToDateTime(txtWeekBegin.Text);
            int ClassID = Convert.ToInt32(lbtnOpenClassMenu.Attributes["data-id"]);
            List<bool> AvailableDays = new List<bool>();

            using(MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.GetFaecherverteilung;
                    con.Open();
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            TableRow row = new TableRow();
                            TableCell TimeCell = new TableCell();
                            TimeCell.Controls.Add(BuildTimeCell(Convert.ToInt32(reader["Stunde"]), TimeSpan.Parse(reader["Uhrzeit"].ToString()), TimeSpan.Parse(reader["Ende"].ToString())));
                            row.Controls.Add(TimeCell);
                            for(int i = 0; i<5; i++)
                            {
                                row.Controls.Add(new TableCell());
                            }
                            tbTimetable.Controls.Add(row);
                        }
                    }
                    cmd.CommandText = SQL.Statements.GetDayInformations;
                    cmd.Parameters.AddWithValue("@KlasseID",ClassID);
                    cmd.Parameters.AddWithValue("@Datum", Week);

                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 1; reader.Read(); i++)//LOL It works, we shall keep it!!!!!!!!!!!!!!!!!!!
                        {
                            if (Convert.ToBoolean(reader["FindetStatt"]))
                            {
                                AvailableDays.Add(true);
                            }
                            else
                            {
                                AvailableDays.Add(false);
                                (tbTimetable.Controls[1].Controls[i] as TableCell).Text = reader["Information"].ToString();
                                (tbTimetable.Controls[1].Controls[i] as TableCell).CssClass = "danger LessonerDayFree";
                            }
                        }
                    }
                    con.Close();
                }
            }
        }

        private HtmlTable BuildTimeCell(int Lession, TimeSpan Begin, TimeSpan End)
        {
            HtmlTable TimeTB = new HtmlTable();
            
            HtmlTableRow Top = new HtmlTableRow();
            HtmlTableRow Bottom = new HtmlTableRow();

            HtmlTableCell NumberCell = new HtmlTableCell();
            HtmlTableCell BeginCell = new HtmlTableCell();
            HtmlTableCell EndCell = new HtmlTableCell();

            NumberCell.RowSpan = 2;
            NumberCell.InnerText = Lession.ToString();
            NumberCell.Style.Add("font-size","16px;");
            BeginCell.InnerText = Begin.ToString(@"hh\:mm");
            EndCell.InnerText = End.ToString(@"hh\:mm");

            Top.Controls.Add(NumberCell);
            Top.Controls.Add(BeginCell);

            Bottom.Controls.Add(EndCell);

            TimeTB.Controls.Add(Top);
            TimeTB.Controls.Add(Bottom);

            return TimeTB;
        }
        //Tasks==================================================================



        //Webmethoden============================================================
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

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        private void LessonerBuilder_PreRender(object sender, EventArgs e)
        {
            LoadLessoner();
        }
    }
}