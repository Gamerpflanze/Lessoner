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
            InitialiseLessoner();
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
        private struct Day //TODO: Vieleicht später als Klasse
        {
            public bool FindetStatt;
            public int ID;
        }
        private void InitialiseLessoner()
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.GetFaecherverteilung;
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableRow row = new TableRow();
                            TableCell TimeCell = new TableCell();
                            TimeCell.Controls.Add(BuildTimeCell(Convert.ToInt32(reader["Stunde"]), TimeSpan.Parse(reader["Uhrzeit"].ToString()), TimeSpan.Parse(reader["Ende"].ToString())));
                            row.Controls.Add(TimeCell);
                            for (int i = 0; i < 5; i++)
                            {
                                TableCell Lesson = new TableCell();
                                Lesson.CssClass = "LessonCell";
                                row.Attributes.Add("data-infotype", "0");
                                //Empty Controll Elements
                                Panel EmptyControlls = new Panel();
                                EmptyControlls.Style.Add("z-index", "999");
                                LinkButton AddButton = new LinkButton();
                                AddButton.CssClass = "LessonEditButton btn-xs";
                                HtmlGenericControl AddIcon = new HtmlGenericControl("span");
                                AddIcon.Attributes.Add("class", "glyphicon glyphicon-plus");
                                EmptyControlls.Style.Add("float", "right");
                                AddButton.Controls.Add(AddIcon);
                                AddButton.Click += new EventHandler(this.AddLession_Click);
                                EmptyControlls.Controls.Add(AddButton);
                                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(AddButton);

                                //EditControllElements
                                Panel EditControlls = new Panel();
                                LinkButton EditButton = new LinkButton();
                                HtmlGenericControl EditIcon = new HtmlGenericControl("span");
                                
                                EditIcon.Attributes.Add("class", "glyphicon glyphicon-pencil");
                                
                                EditButton.CssClass = "LessonEditButton btn-xs";
                                EditButton.Style.Add("float", "right");
                                EditButton.Controls.Add(EditIcon);
                                EditButton.Click += new EventHandler(this.EditLession_Click);
                                EditControlls.Style.Add("z-index", "999");
                                EditControlls.Controls.Add(EditButton);
                                
                                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(EditButton);

                                LinkButton RemoveButton = new LinkButton();
                                HtmlGenericControl RemoveIcon = new HtmlGenericControl("span");

                                RemoveIcon.Attributes.Add("class", "glyphicon glyphicon-remove");

                                RemoveButton.CssClass = "LessonEditButton btn-xs";
                                RemoveButton.Style.Add("float", "right");
                                RemoveButton.Controls.Add(RemoveIcon);
                                RemoveButton.Click += new EventHandler(this.RemoveLession_Click);

                                EditControlls.Controls.Add(RemoveButton);

                                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(RemoveButton);

                                Lesson.Controls.Add(EmptyControlls);
                                Lesson.Controls.Add(EditControlls);

                                row.Controls.Add(Lesson);
                            }
                            tbTimetable.Controls.Add(row);
                        }
                    }
                    con.Close();
                }
            }
        }
        private void LoadLessoner()
        {
            //data-infotypes: 0=empty, 1=stunde, 2=tagentfall (benötigt für buttons)
            DateTime Week = Convert.ToDateTime(txtWeekBegin.Text);
            int ClassID = Convert.ToInt32(lbtnOpenClassMenu.Attributes["data-id"]);
            List<Day> AvailableDays = new List<Day>();
            List<Lesson> Lessons = new List<Lesson>();

            using(MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.GetDayInformations;
                    cmd.Parameters.AddWithValue("@KlasseID",ClassID);
                    cmd.Parameters.AddWithValue("@Datum", Week);

                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 1; reader.Read(); i++)//LOL It works, we shall keep it!!!!!!!!!!!!!!!!!!!
                        {
                            Day d = new Day();
                            if (Convert.ToBoolean(reader["FindetStatt"]))
                            {
                                d.FindetStatt = true;
                                d.ID=Convert.ToInt32(reader["TagID"]);
                                AvailableDays.Add(d);
                            }
                            else
                            {
                                d.FindetStatt = false;
                                d.ID = Convert.ToInt32(reader["TagID"]);
                                AvailableDays.Add(d);
                                (tbTimetable.Controls[1].Controls[i] as TableCell).Attributes["data-infotype"] = "2";
                                (tbTimetable.Controls[1].Controls[i] as TableCell).Text = reader["Information"].ToString();
                                (tbTimetable.Controls[1].Controls[i] as TableCell).CssClass = "danger LessonerDayFree";
                            }
                        }
                    }
                    cmd.CommandText = SQL.Statements.GetLessonPerDay;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TagID",-1);
                    int j = 0;
                    for (int i = 0; i < AvailableDays.Count; i++ )
                    {
                        if (AvailableDays[i].FindetStatt)
                        {
                            cmd.Parameters["@TagID"].Value = AvailableDays[i].ID;
                            using(MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //TODO:Fächer hinzufügen mit Controll Buttons
                                    Lesson l = new Lesson();
                                    int Begin = Convert.ToInt32(reader["Stunde_Beginn"]);//steht hier für Kürzung
                                    (tbTimetable.Controls[Begin].Controls[i + 1] as TableCell).Attributes["data-infotype"] = "1";
                                    HtmlGenericControl Text = new HtmlGenericControl("span");
                                    Text.Style.Add("position", "absolute");
                                    Text.InnerText = reader["FachName"].ToString();
                                    (tbTimetable.Controls[Begin].Controls[i + 1] as TableCell).Controls.Add(Text);//See?
                                    (tbTimetable.Controls[Begin].Controls[i + 1] as TableCell).Attributes.Add("data-listid",j.ToString());
                                    l.ID = Convert.ToInt32(reader["ID"]);
                                    l.LehrerID = Convert.ToInt32(reader["LehrerID"]);
                                    l.FachModID= Convert.ToInt32(reader["FachID"]);
                                    l.TagID= Convert.ToInt32(reader["LehrerID"]);
                                    l.StundeBeginn = Convert.ToInt32(reader["Stunde_Beginn"]);
                                    l.StundeEnde = Convert.ToInt32(reader["Stunde_Ende"]);
                                    l.FachModID = Convert.ToInt32(reader["FachModID"]);
                                    l.NameLong = reader["FachName"].ToString();
                                    l.NameShot = reader["FachNameKurz"].ToString();
                                    l.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);
                                    if(l.FachModID==2)
                                    {
                                        (tbTimetable.Controls[Begin].Controls[i + 1] as TableCell).CssClass += " warning";
                                    }
                                    else if(l.FachModID==3)
                                    {
                                        (tbTimetable.Controls[Begin].Controls[i + 1] as TableCell).CssClass += " danger";
                                    }
                                    Lessons.Add(l);
                                    j++;
                                }
                            }
                        }
                    }
                    con.Close();
                    //Einrücken von Tagen
                    for (int i = 0; i < AvailableDays.Count; i++ )
                    {
                        if(!AvailableDays[i].FindetStatt)
                        {
                            (tbTimetable.Controls[1].Controls[i + 1] as TableCell).RowSpan = tbTimetable.Controls.Count - 1;
                            for (int k = 2; k < tbTimetable.Controls.Count; k++ )
                            {
                                (tbTimetable.Controls[k].Controls[i + 1] as TableCell).Style.Add("display", "none");
                            }
                        }
                    }
                    //Einrücken von Fächern
                    for(int i = 0; i<Lessons.Count; i++)
                    {
                        if(Lessons[i].StundeBeginn<Lessons[i].StundeEnde)
                        {
                            int LessonLength = Lessons[i].StundeEnde - Lessons[i].StundeBeginn + 1;
                            (tbTimetable.Controls[Lessons[i].StundeBeginn].Controls[Lessons[i].TagInfoID] as TableCell).RowSpan = LessonLength;
                            for(int k = 1; k<LessonLength; k++)
                            {
                                (tbTimetable.Controls[Lessons[i].StundeBeginn + k].Controls[Lessons[i].TagInfoID] as TableCell).Style.Add("display", "none");
                            }
                        }
                    }
                    //Hinzufügen der Kontrollelemente
                    for(int i = 1; i<tbTimetable.Controls.Count; i++)
                    {
                        for(int k = 1; k<tbTimetable.Controls[i].Controls.Count; k++)
                        {
                            int InfoType = Convert.ToInt32((tbTimetable.Controls[i].Controls[k] as TableCell).Attributes["data-infotype"]);
                            if(InfoType == 0)
                            {
                                tbTimetable.Controls[i].Controls[k].Controls.RemoveAt(1);
                            }
                            else if(InfoType == 1)
                            {
                                tbTimetable.Controls[i].Controls[k].Controls.RemoveAt(0);
                            }
                            else 
                            {
                                tbTimetable.Controls[i].Controls[k].Controls.Clear();
                            }
                        }
                    }
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
        protected void RemoveLession_Click(object sender, EventArgs e)
        {

        }
        protected void EditLession_Click(object sender, EventArgs e)
        {

        }
        protected void AddLession_Click(object sender, EventArgs e)
        {
            int i = 0;
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