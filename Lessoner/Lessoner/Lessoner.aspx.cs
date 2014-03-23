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
    public partial class Lessoner : System.Web.UI.Page
    {
        int WeekIndex
        {
            get
            {
                return StoredVars.Objects.Lessoner.WeekIndex;
            }
            set
            {
                StoredVars.Objects.Lessoner.WeekIndex = value;
            }
        }
        List<DateTime> WeekBegins
        {
            get
            {
                return StoredVars.Objects.Lessoner.WeekBegins;
            }
            set
            {
                StoredVars.Objects.Lessoner.WeekBegins = value;
            }
        }
        LessonerCache.ClassSelector SelectedTimeTable
        {
            get
            {
                return StoredVars.Objects.Lessoner.SelectedTimeTable;
            }
            set
            {
                StoredVars.Objects.Lessoner.SelectedTimeTable = value;
            }
        }

        List<Lesson> Lessons
        {
            get
            {
                return StoredVars.Objects.Lessoner.Lessons;
            }
            set
            {
                StoredVars.Objects.Lessoner.Lessons = value;
            }
        }
        string Script = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Http Errors 'nd shit

            //EventHandler
            this.LoadComplete += new EventHandler(Page_LoadComplete);
            //this.PreRender += new System.EventHandler(this.LessonerBuilder_PreRender);
            //
            Script += JavascriptCaller.ClearCopyModal;
            if (!Page.IsPostBack)
            {
#if !DEBUG
                if (!StoredVars.Objects.Loggedin)
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
                if (!StoredVars.Objects.Rights["lessoner"]["permission"])
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
#endif
                DateTime Date = DateTime.Now.Date;
                Date = Date.AddDays(-((double)HelperMethods.DayOfWeekToNumber(Date.DayOfWeek) - 1));
                for (int i = 0; i < 6; i++)
                {
                    StoredVars.Objects.Lessoner.WeekBegins.Add(Date);
                    Date = Date.AddDays(7);
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");
                LessonerCache.ClassSelector cl = SelectedTimeTable;
                cl.ClassID = -1;
                SelectedTimeTable = cl;
                btnLastDate.Attributes.Add("disabled", "disabled");
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
                            ClassLink.OnClientClick = "OpenLoadingIndicator('true');";
                            ClassLink.Text = reader["Name"].ToString();
                            if (first)
                            {
                                first = false;
                                if (SelectedTimeTable.ClassID == -1)
                                {
                                    LessonerCache.ClassSelector c = SelectedTimeTable;
                                    c.ClassID = Convert.ToInt32(reader["ID"].ToString());
                                    c.ClassName = reader["Name"].ToString();
                                    SelectedTimeTable = c;
                                }
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
            lbtnOpenClassMenu.Attributes["data-id"] = SelectedTimeTable.ClassID.ToString();
            lbtnOpenClassMenu.InnerHtml = SelectedTimeTable.ClassName + "<span class=\"caret\"></span>";

            if (!Page.IsPostBack)
            {
                LoadLessoner();
            }
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
            LoadLessoner();
            ClearLoadingIndicator();
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
            LoadLessoner();
            ClearLoadingIndicator();
        }
        protected void ClassSelect_Click(object sender, EventArgs e)
        {
            LinkButton ClassButton = sender as LinkButton;

            lbtnOpenClassMenu.InnerHtml = ClassButton.Text + "<span class=\"caret\"></span>";
            lbtnOpenClassMenu.Attributes["data-id"] = ClassButton.Attributes["data-id"];

            LessonerCache.ClassSelector c = SelectedTimeTable;
            c.ClassID = Convert.ToInt32(ClassButton.Attributes["data-id"]);
            c.ClassName = ClassButton.Text;
            SelectedTimeTable = c;
            ClearLoadingIndicator();
            LoadLessoner();
        }
        private struct Day //TODO: Vieleicht später als Klasse
        {
            public bool FindetStatt;
            public int ID;
            public int TagInfoID;
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
            Lessons.Clear();
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@KlasseID", ClassID);
                    cmd.Parameters.AddWithValue("@Datum", Week);
                    cmd.CommandText = SQL.Statements.CheckForLessoner;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        cmd.CommandText = SQL.Statements.InsertEmptyLessoner;
                        cmd.ExecuteNonQuery();
                    }
                    cmd.CommandText = SQL.Statements.GetDayInformations;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            (tbTimetable.Controls[0].Controls[Convert.ToInt32(reader["TagInfoID"])] as TableHeaderCell).Attributes.Add("data-id", reader["TagID"].ToString());
                            Day d = new Day();
                            d.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);
                            (tbTimetable.Controls[0].Controls[d.TagInfoID] as TableCell).Attributes.Add("data-takesplace", reader["FindetStatt"].ToString());
                            if (Convert.ToBoolean(reader["FindetStatt"]))
                            {
                                d.FindetStatt = true;
                                d.ID = Convert.ToInt32(reader["TagID"]);
                                AvailableDays.Add(d);
                            }
                            else
                            {
                                d.FindetStatt = false;
                                d.ID = Convert.ToInt32(reader["TagID"]);
                                AvailableDays.Add(d);
                                (tbTimetable.Controls[1].Controls[d.TagInfoID] as TableCell).Attributes["data-infotype"] = "2";
                                (tbTimetable.Controls[1].Controls[d.TagInfoID] as TableCell).Text = reader["Information"].ToString();
                                (tbTimetable.Controls[1].Controls[d.TagInfoID] as TableCell).CssClass = "danger LessonerDayFree";
                            }
                        }
                    }
                    cmd.CommandText = SQL.Statements.GetLessonPerDay;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@TagID", -1);
                    int j = 0;
                    for (int i = 0; i < AvailableDays.Count; i++)
                    {
                        if (AvailableDays[i].FindetStatt)
                        {
                            cmd.Parameters["@TagID"].Value = AvailableDays[i].ID;
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Lesson l = new Lesson();
                                    int Begin = Convert.ToInt32(reader["Stunde_Beginn"]);
                                    HtmlGenericControl TextBig = new HtmlGenericControl("span");
                                    HtmlGenericControl TextSmall = new HtmlGenericControl("span");

                                    TextBig.Style.Add("text-align", "center");
                                    TextBig.InnerText = reader["FachName"].ToString();
                                    TextBig.Attributes.Add("class", "visible-lg");

                                    TextSmall.Style.Add("text-align", "center");
                                    TextSmall.InnerText = reader["FachNameKurz"].ToString();
                                    TextSmall.Attributes.Add("class", "hidden-lg");

                                    l.ID = Convert.ToInt32(reader["ID"]);
                                    l.LehrerID = Convert.ToInt32(reader["LehrerID"]);
                                    l.FachID = Convert.ToInt32(reader["FachID"]);
                                    l.TagID = AvailableDays[i].ID;
                                    l.StundeBeginn = Convert.ToInt32(reader["Stunde_Beginn"]);
                                    l.StundeEnde = Convert.ToInt32(reader["Stunde_Ende"]);
                                    l.FachModID = Convert.ToInt32(reader["FachModID"]);
                                    l.NameLong = reader["FachName"].ToString();
                                    l.NameShot = reader["FachNameKurz"].ToString();
                                    l.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);

                                    (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(TextBig);
                                    (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(TextSmall);
                                    (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes.Add("data-listid", j.ToString());
                                    (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes["data-infotype"] = "1";

                                    if (l.FachModID == 2)
                                    {
                                        (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).CssClass += " warning";
                                    }
                                    else if (l.FachModID == 3)
                                    {
                                        (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).CssClass += " danger";
                                    }
                                    Lessons.Add(l);
                                    j++;
                                }
                            }
                        }
                    }
                    con.Close();
                    for (int i = 0; i < AvailableDays.Count; i++)
                    {
                        if (!AvailableDays[i].FindetStatt)
                        {
                            (tbTimetable.Controls[1].Controls[AvailableDays[i].TagInfoID] as TableCell).RowSpan = tbTimetable.Controls.Count - 1;
                            for (int k = 2; k < tbTimetable.Controls.Count; k++)
                            {
                                (tbTimetable.Controls[k].Controls[AvailableDays[i].TagInfoID] as TableCell).Style.Add("display", "none");
                            }
                        }
                    }
                    for (int i = 0; i < Lessons.Count; i++)
                    {
                        if (Lessons[i].StundeBeginn < Lessons[i].StundeEnde)
                        {
                            int LessonLength = Lessons[i].StundeEnde - Lessons[i].StundeBeginn + 1;
                            (tbTimetable.Controls[Lessons[i].StundeBeginn].Controls[Lessons[i].TagInfoID] as TableCell).RowSpan = LessonLength;
                            for (int k = 1; k < LessonLength; k++)
                            {
                                (tbTimetable.Controls[Lessons[i].StundeBeginn + k].Controls[Lessons[i].TagInfoID] as TableCell).Style.Add("display", "none");
                            }
                        }
                    }
                    int MaxLesson = 0;
                    for(int i = 0; i<Lessons.Count; i++)
                    {
                        if(Lessons[i].StundeEnde>MaxLesson)
                        {
                            MaxLesson = Lessons[i].StundeEnde;
                        }
                    }
                    int Count = tbTimetable.Controls.Count;
                    for(int i = MaxLesson+1; i<Count; i++)
                    {
                        tbTimetable.Controls.RemoveAt(MaxLesson+1);
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
            NumberCell.Style.Add("font-size", "16px;");
            NumberCell.Style.Add("font-weight", "bold");
            BeginCell.InnerText = Begin.ToString(@"hh\:mm");
            EndCell.InnerText = End.ToString(@"hh\:mm");

            Top.Controls.Add(NumberCell);
            Top.Controls.Add(BeginCell);

            Bottom.Controls.Add(EndCell);

            TimeTB.Controls.Add(Top);
            TimeTB.Controls.Add(Bottom);

            return TimeTB;
        }
        private void CloseLoadingIndicator()
        {
            Script += JavascriptCaller.CloseLoadingIndicator;
        }
        private void ClearLoadingIndicator()
        {
            Script += JavascriptCaller.ClearLoadingIndicator;
        }
        private void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", Script, true);
        }
    }
}