using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;
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
                //return StoredVars.Objects.Lessoner.WeekIndex;
                return Convert.ToInt32(ViewState["WeekIndex"]);
            }
            set
            {
                //StoredVars.Objects.Lessoner.WeekIndex = value;
                ViewState["WeekIndex"] = value;
            }
        }
        List<DateTime> WeekBegins
        {
            get
            {
                //return StoredVars.Objects.Lessoner.WeekBegins;
                return ViewState["WeekBegins"] as List<DateTime>;
            }
            set
            {
                //StoredVars.Objects.Lessoner.WeekBegins = value;
                ViewState["WeekBegins"] = value;
            }
        }
        LessonerCache.Selecter SelectedTimeTable
        {
            get
            {
                //return StoredVars.Objects.Lessoner.SelectedTimeTable;
                LessonerCache.Selecter s = new LessonerCache.Selecter();
                s.ID = Convert.ToInt32(ViewState["SelectedTimeTableID"]);
                s.Name = Convert.ToString(ViewState["SelectedTimeTableName"]);
                s.Week = Convert.ToInt32(ViewState["SelectedTimeTableWeek"]);
                return s;
            }
            set
            {
                //StoredVars.Objects.Lessoner.SelectedTimeTable = value;
                //ViewState["SelectedTimeTable"] = value;

                ViewState["SelectedTimeTableID"] = value.ID;
                ViewState["SelectedTimeTableName"] = value.Name;
                ViewState["SelectedTimeTableWeek"] = value.Week;
            }
        }
        bool TeacherLessons
        {
            get
            {
                //return StoredVars.Objects.Lessoner.TeacherLessons;
                return Convert.ToBoolean(ViewState["TeacherLessons"]);
            }
            set
            {
                //StoredVars.Objects.Lessoner.TeacherLessons = value;
                ViewState["TeacherLessons"] = value;
            }
        }
        List<Lesson> Lessons = new List<Lesson>();
        string Script = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            InitialiseLessoner();
        }
        private void InitLists()
        {
            WeekBegins = new List<DateTime>();
            Lessons = new List<Lesson>();
            SelectedTimeTable = new LessonerCache.Selecter();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadComplete += new EventHandler(Page_LoadComplete);
            if (!Page.IsPostBack)
            {
                InitLists();
#if !DEBUG
                if (!StoredVars.Objects.Loggedin)
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
                int RemovedElements = 0;

                if (!StoredVars.Objects.Rights["login"]["isteacher"])
                {
                    if (!StoredVars.Objects.Rights["lessoner"]["openteacher"])
                    {
                        RemovedElements++;
                        ClassTeacherSwitchContainer.Controls.Clear();
                        ClassTeacherSwitchContainer.Style.Add("display", "none");
                    }
                    if (!StoredVars.Objects.Rights["lessoner"]["chooseclass"])
                    {
                        RemovedElements++;
                        if (!StoredVars.Objects.Rights["lessoner"]["openteacher"])
                        {
                            ClassTeacherSelecter.Style.Add("display", "none");
                        }
                        else
                        {
                            lbtnOpenClassMenu.Disabled = true;
                        }

                        LessonerCache.Selecter stt = SelectedTimeTable;
                        stt.ID = StoredVars.Objects.KlasseID;
                        stt.Name = StoredVars.Objects.KlasseName;
                        SelectedTimeTable = stt;
                        lbtnOpenClassMenu.Attributes["data-id"] = SelectedTimeTable.ID.ToString();
                        lbtnOpenClassMenu.InnerHtml = SelectedTimeTable.Name + "<span class=\"caret\"></span>";
                    }
                }
                if(!StoredVars.Objects.Rights["lessoner"]["uploadfile"])
                {
                    UploadButton.Style.Add("display", "none");
                    FileUploader.Style.Add("display", "none");
                }
                if (!StoredVars.Objects.Rights["lessoner"]["uploadfile"])
                {
                    Script += "CantDeleteFiles();";
                }
                if (RemovedElements > 0)
                {
                    if (RemovedElements == 1)
                    {
                        WeekSelectContainer.Attributes.Add("class", "col-md-4");
                        PrintButtonContainer.Attributes.Add("class", "col-md-4 hidden-sm");
                        ClassTeacherSwitchContainer.Attributes.Add("class", "col-md-4");
                        ClassTeacherSelecter.Attributes.Add("class", "col-md-4");
                    }
                    else
                    {
                        WeekSelectContainer.Attributes.Add("class", "col-md-6");
                        PrintButtonContainer.Attributes.Add("class", "col-md-6 hidden-sm");
                        ClassTeacherSwitchContainer.Attributes.Add("class", "col-md-6");
                        ClassTeacherSelecter.Attributes.Add("class", "col-md-6");
                    }
                }
                if (!StoredVars.Objects.Rights["lessoner"]["editlessoninfo"])
                {
                    ApplyInfoText.Style.Add("display", "none");
                    LessonInfoText.Enabled = false;

                }
#endif
                if (!Page.IsPostBack)
                {
                    TeacherLessons = false;
                }
                if (StoredVars.Objects.Loggedin)
                {
                    PageDropDown.Style.Add("display", "block");
                    ReadyPageDropDown();
                }
                DateTime Date = DateTime.Now.Date;
                Date = Date.AddDays(-((double)HelperMethods.DayOfWeekToNumber(Date.DayOfWeek) - 1));
                for (int i = 0; i < 6; i++)
                {
                    WeekBegins.Add(Date);
                    Date = Date.AddDays(7);
                }
                txtWeekBegin.Text = WeekBegins[WeekIndex].ToString("dd.MM.yyyy");

                btnLastDate.Attributes.Add("disabled", "disabled");
            }
            if (!IsPostBack)
            {
                if (StoredVars.Objects.Rights["login"]["isteacher"])
                {
                    LessonerCache.Selecter stt = SelectedTimeTable;
                    stt.ID = -1;
                    stt.Name = "";
                    SelectedTimeTable = stt;
                }
                else
                {
                    LessonerCache.Selecter stt = SelectedTimeTable;
                    stt.ID = StoredVars.Objects.KlasseID;
                    stt.Name = StoredVars.Objects.KlasseName;
                    SelectedTimeTable = stt;
                }
            }
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
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
                            ClassLink.OnClientClick = "jQuery('#LoadingModal').modal({backdrop:'static', keyboard:false});";
                            ClassLink.Text = reader["Name"].ToString();
                            if (first)
                            {
                                first = false;
                                if (SelectedTimeTable.ID == -1)
                                {
                                    LessonerCache.Selecter c = SelectedTimeTable;
                                    c.ID = Convert.ToInt32(reader["ID"].ToString());
                                    c.Name = reader["Name"].ToString();
                                    SelectedTimeTable = c;
                                }
                            }
                            ClassLink.Click += new EventHandler(ClassSelect_Click);
                            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(ClassLink);
                            li.Controls.Add(ClassLink);
                            ClassList.Controls.Add(li);
                        }
                    }

                    cmd.CommandText = SQL.Statements.GetTeacher;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            LinkButton TeacherLink = new LinkButton();
                            TeacherLink.Attributes.Add("data-id", reader["ID"].ToString());
                            TeacherLink.OnClientClick = "jQuery('#LoadingModal').modal({backdrop:'static', keyboard:false});";
                            TeacherLink.Text = reader["Name"].ToString();

                            TeacherLink.Click += new EventHandler(TeacherSelect_Click);
                            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(TeacherLink);
                            li.Controls.Add(TeacherLink);
                            TeacherList.Controls.Add(li);
                        }
                    }
                    string ErrorText = "";
                    cmd.CommandText = SQL.Statements.CountTeacher;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        ErrorText += "Es existieren momentan keine Lehrer. ";
                    }
                    cmd.CommandText = SQL.Statements.CountClasses;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        ErrorText += "Es existieren momentan keine Klassen. ";
                    }
                    if (ErrorText != "")
                    {
                        tbTimetable.Style.Add("display", "none");
                        Header.Style.Add("display", "none");
                        ErrorMessage.Style["display"] = "inline";
                        if (StoredVars.Objects.Rights["login"]["isteacher"])
                        {
                            ErrorMessage.InnerText = ErrorText + "Diese wurden noch nicht erstellt. Daher ist diese Seite noch nicht Verfügbar.";
                        }
                        else
                        {
                            ErrorMessage.InnerText = ErrorText + "Diese Seite ist momentan noch nicht verfügbar.";

                        }
                    }
                    con.Close();
                }
            }
            if (TeacherLessons)
            {
                TeacherMenu.InnerHtml = SelectedTimeTable.Name + "<span class=\"caret\"></span>";
                TeacherMenu.Attributes.Add("data-id", SelectedTimeTable.ID.ToString());
            }
            else
            {
                lbtnOpenClassMenu.Attributes["data-id"] = SelectedTimeTable.ID.ToString();
                lbtnOpenClassMenu.InnerHtml = SelectedTimeTable.Name + "<span class=\"caret\"></span>";
            }

            if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
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

            LessonerCache.Selecter c = SelectedTimeTable;
            c.ID = Convert.ToInt32(ClassButton.Attributes["data-id"]);
            c.Name = ClassButton.Text;
            SelectedTimeTable = c;
            ClearLoadingIndicator();
            LoadLessoner();
        }
        protected void TeacherSelect_Click(object sender, EventArgs e)
        {
            LinkButton TeacherButton = sender as LinkButton;

            TeacherMenu.InnerHtml = TeacherButton.Text + "<span class=\"caret\"></span>";
            TeacherMenu.Attributes["data-id"] = TeacherButton.Attributes["data-id"];

            LessonerCache.Selecter c = SelectedTimeTable;
            c.ID = Convert.ToInt32(TeacherButton.Attributes["data-id"]);
            c.Name = TeacherButton.Text;
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
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
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
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@KlasseID", ClassID);
                    cmd.Parameters.AddWithValue("@Datum", WeekBegins[WeekIndex]);
                    if (!TeacherLessons)
                    {
                        cmd.CommandText = SQL.Statements.CheckForLessoner;
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            try
                            {
                                cmd.CommandText = SQL.Statements.InsertEmptyLessoner;
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            {

                            }
                        }
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
                    cmd.Parameters.Clear();
                    if (TeacherLessons)
                    {
                        cmd.CommandText = SQL.Statements.GetLessonPerDayTeacher;
                        cmd.Parameters.AddWithValue("@LehrerID", SelectedTimeTable.ID);
                        cmd.Parameters.AddWithValue("@TagInfoID", -1);
                        cmd.Parameters.AddWithValue("@Woche", WeekBegins[WeekIndex]);
                    }
                    else
                    {
                        cmd.CommandText = SQL.Statements.GetLessonPerDay;
                        cmd.Parameters.AddWithValue("@TagID", -1);
                    }
                    int j = 0;
                    for (int i = 0; i < AvailableDays.Count; i++)
                    {
                        if (TeacherLessons)
                        {
                            cmd.Parameters["@TagInfoID"].Value = i + 1;
                        }
                        else
                        {
                            cmd.Parameters["@TagID"].Value = AvailableDays[i].ID;
                            if (!AvailableDays[i].FindetStatt)
                            {
                                continue;
                            }
                        }
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

                                LinkButton LessonClick = new LinkButton();
                                LessonClick.Click += new EventHandler(Lesson_Click);
                                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(LessonClick);
                                /*height: 100%;
                                width: 100%;
                                position: relative;
                                display: block;*/
                                //(tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes.Add("data-id", reader["ID"].ToString());
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes.Add("onclick", "LoadLessonInfoModal(" + reader["ID"].ToString() + ", true)");
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(TextBig);
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(TextSmall);
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes.Add("data-listid", j.ToString());
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Attributes["data-infotype"] = "1";
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Style.Add("position", "relative");
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Style.Add("pading", "0px");
                                (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(LessonClick);
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
                    for (int i = 0; i < Lessons.Count; i++)
                    {
                        if (Lessons[i].StundeEnde > MaxLesson)
                        {
                            MaxLesson = Lessons[i].StundeEnde;
                        }
                    }
                    int Count = tbTimetable.Controls.Count;
                    for (int i = MaxLesson + 1; i < Count; i++)
                    {
                        tbTimetable.Controls.RemoveAt(MaxLesson + 1);
                    }
                }
            }
        }
        private HtmlTable BuildTimeCell(int Lession, TimeSpan Begin, TimeSpan End)
        {
            HtmlTable TimeTB = new HtmlTable();

            TimeTB.Attributes.Add("class", "HourTable");

            HtmlTableRow Top = new HtmlTableRow();
            HtmlTableRow Bottom = new HtmlTableRow();

            HtmlTableCell NumberCell = new HtmlTableCell();
            NumberCell.Attributes.Add("class", "HourNumber");
            HtmlTableCell BeginCell = new HtmlTableCell();
            BeginCell.Attributes.Add("class", "HourCell");
            HtmlTableCell EndCell = new HtmlTableCell();
            EndCell.Attributes.Add("class", "HourCell");

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
        protected void Lesson_Click(object sender, EventArgs e)
        {
            LoadLessoner();
        }
        private void ClearLoadingIndicator()
        {
            Script += "jQuery('#LoadingModal').modal('hide');";
        }
        private void Page_LoadComplete(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", Script, true);
        }

        protected void FileUploader_UploadedComplete(object sender, EventArgs e)
        {

        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(CurrentLesson.Value);
            if (FileUploader.HasFile)
            {
                string NewFilePath = Server.MapPath("Data\\Files\\") + Guid.NewGuid().ToString("N") + Path.GetExtension(FileUploader.FileName);
                using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
                {
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();

                        cmd.CommandText = SQL.Statements.InsertFile;
                        cmd.Parameters.AddWithValue("@LessonID", ID);
                        cmd.Parameters.AddWithValue("@Path", NewFilePath);
                        cmd.Parameters.AddWithValue("@FileName", FileUploader.FileName);
                        cmd.ExecuteNonQuery();
                        FileUploader.SaveAs(NewFilePath);
                        con.Close();
                    }
                }
            }
            ReloadLessonInfoModal();
        }

        protected void FileUploader_UploadedFileError(object sender, EventArgs e)
        {


        }
        private void ReloadLessonInfoModal()
        {
            int ID = Convert.ToInt32(CurrentLesson.Value);
            dynamic Data = LoadLessonInfoModal(ID);
            LessonInfoText.Text = Data[0];
            for (int i = 0; i < Data[1][0].Count; i++)
            {
                TableRow Row = new TableRow();
                TableCell FileName = new TableCell();
                TableCell DownloadButtonCell = new TableCell();
                HtmlGenericControl DownloadButton = new HtmlGenericControl("button");
                HtmlGenericControl RemoveButton = new HtmlGenericControl("button");

                FileName.Text = Data[1][1][i];
                DownloadButton.Attributes.Add("type", "button");
                DownloadButton.Attributes.Add("class", "btn btn-primary");
                DownloadButton.Attributes.Add("onclick", "window.location.href='/Data/FileDownload.aspx?File=" + Data[1][0][i] + "'");
                DownloadButton.InnerHtml = "<span class=\"glyphicon glyphicon-download\"></span>";

                RemoveButton.Attributes.Add("type", "button");
                RemoveButton.Attributes.Add("class", "btn btn-danger");
                RemoveButton.Attributes.Add("onclick", "DeleteFile(" + "'" + Data[1][0][i] + "'" + ',' + ID + ")");
                RemoveButton.InnerHtml = "<span class=\"glyphicon glyphicon-remove\"></span>";


                DownloadButtonCell.Style.Add("width", "96px");
                DownloadButtonCell.Controls.Add(RemoveButton);
                DownloadButtonCell.Controls.Add(DownloadButton);

                Row.Controls.Add(FileName);
                Row.Controls.Add(DownloadButtonCell);
                FileTable.Controls.Add(Row);
            }
            Script += JavascriptCaller.ReOpenLessonInfoModal;
        }
        protected void ApplyInfoText_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.UpdateInfo;
                    cmd.Parameters.AddWithValue("@Information", LessonInfoText.Text);
                    cmd.Parameters.AddWithValue("@ID", CurrentLesson.Value);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            ReloadLessonInfoModal();
            LoadLessoner();
            Script += "jQuery('#LoadingModal').modal('hide');";
        }
        [WebMethod]
        public static dynamic LoadLessonInfoModal(int ID)
        {
            dynamic Return = new dynamic[2];
            Return[0] = "";
            Return[1] = new dynamic[2];
            Return[1][0] = new List<string>();
            Return[1][1] = new List<string>();
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.GetLessonInfoText;
                    cmd.Parameters.AddWithValue("@ID", ID);
                    con.Open();
                    Return[0] = cmd.ExecuteScalar().ToString();
                    cmd.CommandText = SQL.Statements.GetFiles;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Return[1][0].Add(Path.GetFileName(reader["Path"].ToString()));
                            Return[1][1].Add(reader["FileName"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return Return;
        }
        [WebMethod]
        public static void DeleteFile(string FileName)
        {
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.DeleteFile;
                    cmd.Parameters.AddWithValue("@FileName", FileName);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        private void ReadyPageDropDown()
        {
            User.InnerText = StoredVars.Objects.Vorname + " " + StoredVars.Objects.Nachname;
            if (!StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
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

        protected void TeacherSwitch_Click(object sender, EventArgs e)
        {
            TeacherLessons = true;

            LessonerCache.Selecter s = new LessonerCache.Selecter();
            s.Name = (TeacherList.Controls[1].Controls[0] as LinkButton).Text;
            s.ID = Convert.ToInt32((TeacherList.Controls[1].Controls[0] as LinkButton).Attributes["data-id"]);
            s.Week = SelectedTimeTable.Week;
            SelectedTimeTable = s;

            TeacherMenu.InnerHtml = s.Name + "<span class=\"caret\"></span>";
            TeacherMenu.Attributes.Add("data-id", s.ID.ToString());
            SelectClass.Style.Add("display", "none");
            SelectTeacher.Style.Add("display", "block");
            ClassTeacherSwitchButton.InnerHtml = "Lehrer" + "<span class=\"caret\"></span>";
            LoadLessoner();
        }

        protected void ClassSwitch_Click(object sender, EventArgs e)
        {
            TeacherLessons = false;

            LessonerCache.Selecter s = new LessonerCache.Selecter();
            s.Name = (ClassList.Controls[1].Controls[0] as LinkButton).Text;
            s.ID = Convert.ToInt32((ClassList.Controls[1].Controls[0] as LinkButton).Attributes["data-id"]);
            s.Week = SelectedTimeTable.Week;
            SelectedTimeTable = s;

            lbtnOpenClassMenu.InnerHtml = s.Name + "<span class=\"caret\"></span>";
            lbtnOpenClassMenu.Attributes.Add("data-id", s.ID.ToString());
            SelectClass.Style.Add("display", "block");
            SelectTeacher.Style.Add("display", "none");
            ClassTeacherSwitchButton.InnerHtml = "Klasse" + "<span class=\"caret\"></span>";

            if (!StoredVars.Objects.Rights["lessoner"]["chooseclass"])
            {
                LessonerCache.Selecter stt = SelectedTimeTable;
                stt.ID = StoredVars.Objects.KlasseID;
                stt.Name = StoredVars.Objects.KlasseName;
                SelectedTimeTable = stt;
                lbtnOpenClassMenu.Attributes["data-id"] = SelectedTimeTable.ID.ToString();
                lbtnOpenClassMenu.InnerHtml = SelectedTimeTable.Name + "<span class=\"caret\"></span>";
            }

            LoadLessoner();
        }
        protected void Logoutbutton_Click(object sender, EventArgs e)
        {
            StoredVars.Objects = new StoredVars();
            Response.Redirect("/default.aspx", true);
        }
    }
}