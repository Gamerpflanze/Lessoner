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
using System.Threading.Tasks;
namespace Lessoner
{
    public partial class LessonerBuilder : System.Web.UI.Page
    {
        //=================================================================
        //Globale Variablen
        //Dient zur Kürzung
        /*int WeekIndex
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
        LessonerBuilderCache.ClassSelector SelectedTimeTable
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.SelectedTimeTable;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.SelectedTimeTable = value;
            }
        }*/
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
        LessonerBuilderCache.ClassSelector SelectedTimeTable
        {
            get
            {
                //return StoredVars.Objects.Lessoner.SelectedTimeTable;
                LessonerBuilderCache.ClassSelector s = new LessonerBuilderCache.ClassSelector();
                s.ClassID = Convert.ToInt32(ViewState["SelectedTimeTableClassID"]);
                s.ClassName = Convert.ToString(ViewState["SelectedTimeTableClassName"]);
                s.Week = Convert.ToInt32(ViewState["SelectedTimeTableWeek"]);
                return s;
            }
            set
            {
                //StoredVars.Objects.Lessoner.SelectedTimeTable = value;
                //ViewState["SelectedTimeTable"] = value;

                ViewState["SelectedTimeTableClassID"] = value.ClassID;
                ViewState["SelectedTimeTableClassName"] = value.ClassName;
                ViewState["SelectedTimeTableWeek"] = value.Week;
            }
        }
        Dictionary<int, string> Teacher
        {
            get
            {
                //return StoredVars.Objects.LessonerBuilder.Modal.Teacher;
                return ViewState["Teacher"] as Dictionary<int, string>;
            }
            set
            {
                //StoredVars.Objects.LessonerBuilder.Modal.Teacher = value;
                ViewState["Teacher"] = value;
            }
        }
        Dictionary<int, string> Rooms
        {
            get
            {
                //return StoredVars.Objects.LessonerBuilder.Modal.Rooms;
                return ViewState["Rooms"] as Dictionary<int, string>;
            }
            set
            {
                //StoredVars.Objects.LessonerBuilder.Modal.Rooms = value;
                ViewState["Rooms"] = value;
            }
        }
        Dictionary<int, string> LessonName
        {
            get
            {
                //return StoredVars.Objects.LessonerBuilder.Modal.LessonName;
                return ViewState["LessonName"] as Dictionary<int, string>;
            }
            set
            {
                //StoredVars.Objects.LessonerBuilder.Modal.LessonName = value;
                ViewState["LessonName"] = value;
            }
        }
        Dictionary<int, string> LessonModifier
        {
            get
            {
                //return StoredVars.Objects.LessonerBuilder.Modal.LessonModifier;
                return ViewState["LessonModifier"] as Dictionary<int, string>;
            }
            set
            {
                //StoredVars.Objects.LessonerBuilder.Modal.LessonModifier = value;
                ViewState["LessonModifier"] = value;
            }
        }
        /*List<Lesson> Lessons
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.Lessons;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.Lessons = value;
            }
        }*/
        List<Lesson> Lessons = new List<Lesson>();
        string Script = "";
        private void InitDictonaries()
        {
            WeekBegins = new List<DateTime>();
            Teacher = new Dictionary<int, string>();
            Rooms = new Dictionary<int, string>();
            LessonName = new Dictionary<int, string>();
            LessonModifier = new Dictionary<int, string>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadComplete += new EventHandler(Page_LoadComplete);
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    Script += JavascriptCaller.ClearCopyModal;

                    if (!Page.IsPostBack)
                    {
                        InitDictonaries();
#if !DEBUG
                        if (!StoredVars.Objects.Loggedin)
                        {
                            Response.Clear();
                            Response.StatusCode = 403;
                            Response.End();
                            return;
                        }
                        if (!StoredVars.Objects.Rights["lessonerbuilder"]["permission"])
                        {
                            Response.Clear();
                            Response.StatusCode = 403;
                            Response.End();
                            return;
                        }
#endif
                        //btnLastDate.Attributes.Add("disabled", "disabled");
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
                        LessonerBuilderCache.ClassSelector cl = SelectedTimeTable;
                        cl.ClassID = -1;
                        SelectedTimeTable = cl;
                        btnLastDate.Attributes.Add("disabled", "disabled");

                        Teacher.Clear();
                        LessonName.Clear();
                        LessonModifier.Clear();
                        Rooms.Clear();
                        cmd.CommandText = SQL.Statements.LessonerBuilderGetTeacher;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Teacher.Add(Convert.ToInt32(reader["ID"]), reader["Name"].ToString());
                            }
                        }
                        cmd.CommandText = SQL.Statements.LessonerBuilderGetLessonNames;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LessonName.Add(Convert.ToInt32(reader["ID"]), reader["Name"].ToString());
                            }
                        }
                        cmd.CommandText = SQL.Statements.LessonerBuilderGetLessonMods;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LessonModifier.Add(Convert.ToInt32(reader["ID"]), reader["Bezeichnung"].ToString());
                            }
                        }
                        cmd.CommandText = SQL.Statements.GetRooms;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Rooms.Add(Convert.ToInt32(reader["ID"]), reader["Name"].ToString());
                            }
                        }
                        Rooms.Add(-1, "Kein Raum");
                    }
                    cmd.Parameters.Clear();
                    cmd.CommandText = SQL.Statements.GetClasses;
                    //con.Open();

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
                                    LessonerBuilderCache.ClassSelector c = SelectedTimeTable;
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
                    if(ErrorText!="")
                    {
                        tbTimetable.Style.Add("display", "none");
                        Header.Style.Add("display", "none");
                        ErrorMessage.Style["display"]="inline";
                        ErrorMessage.InnerText = ErrorText + "Bitte erstellen Sie diese zuerst.";
                    }
                    con.Close();
                }
            }
            foreach (int i in Teacher.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink TeacherLink = new HyperLink();

                TeacherLink.Attributes.Add("data-id", i.ToString());
                TeacherLink.Text = Teacher[i];
                li.Controls.Add(TeacherLink);
                TeacherLink.Attributes.Add("onclick", "Teacher_Click(this);");// += new EventHandler(Teacher_Click);
                ulTeacher.Controls.Add(li);
            }
            foreach (int i in LessonName.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink LessonNameLink = new HyperLink();

                LessonNameLink.Attributes.Add("data-id", i.ToString());
                LessonNameLink.Text = LessonName[i];
                li.Controls.Add(LessonNameLink);
                LessonNameLink.Attributes.Add("onclick", "LessonName_Click(this);");
                ulLessonNames.Controls.Add(li);
            }
            foreach (int i in LessonModifier.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink LessonModLink = new HyperLink();

                LessonModLink.Attributes.Add("data-id", i.ToString());
                LessonModLink.Text = LessonModifier[i];
                li.Controls.Add(LessonModLink);
                LessonModLink.Attributes.Add("onclick", "LessonMod_Click(this);");
                ulLessonMod.Controls.Add(li);
            }
            foreach (int i in Rooms.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink RoomLink = new HyperLink();

                RoomLink.Attributes.Add("data-id", i.ToString());
                RoomLink.Text = Rooms[i];
                li.Controls.Add(RoomLink);
                RoomLink.Attributes.Add("onclick", "Room_Click(this);");///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                RoomList.Controls.Add(li);
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

            LessonerBuilderCache.ClassSelector c = SelectedTimeTable;
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
                                AddButton.OnClientClick = "OpenLoadingIndicator('true')";
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
                                EditButton.OnClientClick = "OpenLoadingIndicator('true')";
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
                                RemoveButton.OnClientClick = "OpenLoadingIndicator('true')";
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
            Lessons.Clear();
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@KlasseID", ClassID);
                    cmd.Parameters.AddWithValue("@Datum", Week);
                    cmd.CommandText = SQL.Statements.CheckForLessoner;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        try
                        {
                            cmd.CommandText = SQL.Statements.InsertEmptyLessoner;//Funktioniert nur wenn es Klassen gibt. Da Stundenplan ohne Klassen nicht angezeigt wird, es aber trozdem dies ausgeführt wird, ist  try catch hier
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
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
                                    int Begin = Convert.ToInt32(reader["Stunde_Beginn"]);//steht hier für Kürzung
                                    HtmlGenericControl Text = new HtmlGenericControl("span");
                                    Text.Style.Add("position", "absolute");
                                    Text.InnerText = reader["FachName"].ToString();
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
                                    if (!DBNull.Value.Equals(reader["RaumID"]))
                                    {
                                        l.RoomID = Convert.ToInt32(reader["RaumID"]);
                                        l.RoomName = reader["RaumName"].ToString();
                                    }
                                    (tbTimetable.Controls[Begin].Controls[l.TagInfoID] as TableCell).Controls.Add(Text);//See?
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
                    //Einrücken von Tagen
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
                    //Einrücken von Fächern
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
                    //Hinzufügen der Kontrollelemente
                    for (int i = 1; i < tbTimetable.Controls.Count; i++)
                    {
                        for (int k = 1; k < tbTimetable.Controls[i].Controls.Count; k++)
                        {
                            int InfoType = Convert.ToInt32((tbTimetable.Controls[i].Controls[k] as TableCell).Attributes["data-infotype"]);
                            if (InfoType == 0)
                            {
                                (tbTimetable.Controls[i].Controls[k].Controls[1] as Panel).Style.Add("display", "none");
                            }
                            else if (InfoType == 1)
                            {
                                (tbTimetable.Controls[i].Controls[k].Controls[0] as Panel).Style["display"] = "none";
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
        protected void RemoveLession_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            int ListID = Convert.ToInt32((((sender as Control).Parent as Control).Parent as TableCell).Attributes["data-listid"]);
            btnDeleteConfirm.Attributes.Add("data-id", Lessons[ListID].ID.ToString());
            ClearLoadingIndicator();
            Script += JavascriptCaller.OpenDeleteConfirmModal;
        }
        protected void EditLession_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            TableCell LessonCell = ((sender as Control).Parent as Control).Parent as TableCell;
            Lesson lesson = Lessons[Convert.ToInt32(LessonCell.Attributes["data-listid"])];
            btnApply.Attributes["data-lessonid"] = lesson.ID.ToString();
            ddTeacher.InnerHtml = Teacher[lesson.LehrerID] + "<span class=\"caret\"></span>";
            ddLessonName.InnerHtml = LessonName[lesson.FachID] + "<span class=\"caret\"></span>";
            ddLessonMod.InnerHtml = LessonModifier[lesson.FachModID] + "<span class=\"caret\"></span>";
            ddRoom.InnerHtml = Rooms[lesson.RoomID] + "<span class=\"caret\"></span>";

            Modal_TeacherID.Value = lesson.LehrerID.ToString();
            Modal_LessonNameID.Value = lesson.FachID.ToString();
            Modal_LessonModID.Value = lesson.FachModID.ToString();
            Modal_RoomID.Value = lesson.RoomID.ToString();

            txtCountBegin.Text = lesson.StundeBeginn.ToString();
            txtCountEnd.Text = lesson.StundeEnde.ToString();

            Modal_LessonBegin.Value = lesson.StundeBeginn.ToString();
            Modal_LessonEnd.Value = lesson.StundeEnde.ToString();

            ClearLoadingIndicator();
            Script += JavascriptCaller.OpenLessonEditModal;
            txtCountEnd.Attributes.Add("data-totalmax", tbTimetable.Controls.Count.ToString());
        }
        protected void AddLession_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            int Row = tbTimetable.Controls.IndexOf((((sender as Control).Parent as Control).Parent as TableCell).Parent as TableRow);
            int DayID = Convert.ToInt32((tbTimetable.Controls[0].Controls[tbTimetable.Controls[Row].Controls.IndexOf(((sender as Control).Parent as Control).Parent as TableCell)] as TableHeaderCell).Attributes["data-id"]);
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@TagID", DayID);
                    cmd.Parameters.AddWithValue("@Stunde", Row);
                    cmd.CommandText = SQL.Statements.InsertDefaultLesson;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            LoadLessoner();
            TableCell LessonCell = ((sender as Control).Parent as Control).Parent as TableCell;
            Lesson lesson = Lessons[Convert.ToInt32(LessonCell.Attributes["data-listid"])];
            btnApply.Attributes["data-lessonid"] = lesson.ID.ToString();
            ddTeacher.InnerHtml = Teacher[lesson.LehrerID] + "<span class=\"caret\"></span>";
            ddLessonName.InnerHtml = LessonName[lesson.FachID] + "<span class=\"caret\"></span>";
            ddLessonMod.InnerHtml = LessonModifier[lesson.FachModID] + "<span class=\"caret\"></span>";
            ddRoom.InnerHtml = Rooms[lesson.RoomID] + "<span class=\"caret\"></span>";

            Modal_TeacherID.Value = lesson.LehrerID.ToString();
            Modal_LessonNameID.Value = lesson.FachID.ToString();
            Modal_LessonModID.Value = lesson.FachModID.ToString();
            Modal_RoomID.Value = lesson.RoomID.ToString();

            txtCountBegin.Text = lesson.StundeBeginn.ToString();
            txtCountEnd.Text = lesson.StundeEnde.ToString();

            Modal_LessonBegin.Value = lesson.StundeBeginn.ToString();
            Modal_LessonEnd.Value = lesson.StundeEnde.ToString();
            ClearLoadingIndicator();
            Script += JavascriptCaller.OpenLessonEditModal;
        }
        private void KeepEditModalOpen()
        {
            Script += JavascriptCaller.KeepEditModal;
        }
        private void HideEditModal()
        {
            Script += JavascriptCaller.HideEditModal;
        }
        private void HideEditModalNoAbort()
        {
            Script += JavascriptCaller.HideEditModalNoAbort;
        }
        private void KeepAbortModal()
        {
            Script += JavascriptCaller.KeepAbortModalOpen;
        }
        protected void IncEnd_Click(object sender, EventArgs e)
        {
            int Count = Convert.ToInt32(txtCountEnd.Text);
            if (Count < tbTimetable.Controls.Count - 1)
            {
                Count++;
                txtCountEnd.Text = Count.ToString();
            }
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void DecEnd_Click(object sender, EventArgs e)
        {
            int Count = Convert.ToInt32(txtCountEnd.Text);
            int Min = Convert.ToInt32(txtCountBegin.Text);
            if (Count > Min)
            {
                Count--;
                txtCountEnd.Text = Count.ToString();
            }
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void IncBegin_Click(object sender, EventArgs e)
        {
            int Count = Convert.ToInt32(txtCountBegin.Text);
            int Max = Convert.ToInt32(txtCountEnd.Text);
            if (Count < Max)
            {
                Count++;
                txtCountBegin.Text = Count.ToString();
            }
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void DecBegin_Click(object sender, EventArgs e)
        {
            int Count = Convert.ToInt32(txtCountBegin.Text);
            if (Count > 1)
            {
                Count--;
                txtCountBegin.Text = Count.ToString();
            }
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void LessonName_Click(object sender, EventArgs e)
        {
            ddLessonName.Attributes["data-id"] = (sender as LinkButton).Attributes["data-id"];
            ddLessonName.InnerHtml = (sender as LinkButton).Text + "<span class=\"caret\"></span>";
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void LessonMod_Click(object sender, EventArgs e)
        {
            ddLessonMod.Attributes["data-id"] = (sender as LinkButton).Attributes["data-id"];
            ddLessonMod.InnerHtml = (sender as LinkButton).Text + "<span class=\"caret\"></span>";
            KeepEditModalOpen();
            LoadLessoner();
        }
        protected void Apply_Click(object sender, EventArgs e)
        {
            Control btn = (Control)sender;
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.UpdateLesson;

                    cmd.Parameters.AddWithValue("@ID", btnApply.Attributes["data-lessonid"]);
                    cmd.Parameters.AddWithValue("@LehrerID", Modal_TeacherID.Value);
                    cmd.Parameters.AddWithValue("@FachID", Modal_LessonNameID.Value);
                    cmd.Parameters.AddWithValue("@FachModID", Modal_LessonModID.Value);
                    cmd.Parameters.AddWithValue("@StundeBeginn", Modal_LessonBegin.Value);
                    cmd.Parameters.AddWithValue("@StundeEnde", Modal_LessonEnd.Value);
                    if (Modal_RoomID.Value == "-1")
                    {
                        cmd.Parameters.AddWithValue("@RaumID", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RaumID", Modal_RoomID.Value);

                    }
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            ClearLoadingIndicator();
            LoadLessoner();
        }
        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.DeleteLesson;
                    cmd.Parameters.AddWithValue("@ID", (sender as Button).Attributes["data-id"]);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadLessoner();
            ClearLoadingIndicator();
        }
        protected void EditDay_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            TableHeaderCell DayCell = (sender as Control).Parent as TableHeaderCell;
            chkTakesPlace.Checked = Convert.ToBoolean(DayCell.Attributes["data-takesplace"]);
            txtDayInfo.Text = (tbTimetable.Controls[1].Controls[tbTimetable.Controls[0].Controls.IndexOf(DayCell)] as TableCell).Text;
            ClearLoadingIndicator();
            Script += JavascriptCaller.OpenEditDayModal;
            btnApplyDay.Attributes.Add("data-id", DayCell.Attributes["data-id"]);
        }

        protected void ApplyDay_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.UpdateDay;
                    cmd.Parameters.AddWithValue("@FindetStatt", Convert.ToByte(chkTakesPlace.Checked));
                    cmd.Parameters.AddWithValue("@ID", btnApplyDay.Attributes["data-id"]);
                    cmd.Parameters.AddWithValue("@Information", txtDayInfo.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadLessoner();
            ClearLoadingIndicator();
            if ((sender as Control).ID == "btnApplyDay")
            {
                Script += JavascriptCaller.HideEditDayModal;
            }
            else
            {
                Script += JavascriptCaller.HideEditDayModalWithAbort;
            }

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
        struct DayClone
        {
            public int ID;
            public bool FindetStatt;
            public string Information;
            public int DayNameID;
        }
        protected void btnCopyTimeTable_Click(object sender, EventArgs e)
        {
            DateTime StartDate = Convert.ToDateTime(Modal_CopyStartDate.Value);
            DateTime EndDate = Convert.ToDateTime(Modal_CopyEndDate.Value);
            int WeekSpan = Convert.ToInt32(Modal_WeekSpace.Value);
            DateTime CurrentDate = StartDate;
            int weekindex = WeekIndex;
            List<DateTime> weekbegins = WeekBegins;
            int Class = Convert.ToInt32(lbtnOpenClassMenu.Attributes["data-id"]);
            Task LessonerDBWriter = new Task(() => CopyLessoner(StartDate, EndDate, WeekSpan, Class, weekindex, weekbegins), TaskCreationOptions.LongRunning);
            LessonerDBWriter.Start();
            LoadLessoner();
        }

        private void LessonerBuilder_PreRender(object sender, EventArgs e)
        {
            LoadLessoner();
        }

        protected void AddRoom_Click(object sender, EventArgs e)
        {
            int ID = -1;
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.InsertRoom;
                    cmd.Parameters.AddWithValue("@RaumName", RoomName.Text);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            HtmlGenericControl li = new HtmlGenericControl("li");
            HyperLink RoomLink = new HyperLink();

            RoomLink.Attributes.Add("data-id", ID.ToString());
            RoomLink.Text = RoomName.Text;
            li.Controls.Add(RoomLink);
            RoomLink.Attributes.Add("onclick", "Room_Click(this)");
            RoomList.Controls.Add(li);
            Rooms.Add(ID, RoomName.Text);
            Script += @"jQuery('#NewRoomModal').modal('hide');
                        jQuery(document).ready(function(){
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                        });";
        }

        protected void RemoveRoom_Click(object sender, EventArgs e)
        {
            if (Modal_RoomID.Value != "-1")
            {
                using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
                {
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = SQL.Statements.RemoveRoom;
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Modal_RoomID.Value));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                Rooms.Remove(Convert.ToInt32(Modal_RoomID.Value));

                if (Rooms.Count > 0)
                {
                    int Count = RoomList.Controls.Count;
                    for (int i = 1; i < Count; i++)
                    {
                        RoomList.Controls.RemoveAt(1);
                    }
                    bool first = true;
                    foreach (int i in Rooms.Keys)
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HyperLink RoomLink = new HyperLink();

                        RoomLink.Attributes.Add("data-id", i.ToString());
                        RoomLink.Text = Rooms[i];
                        li.Controls.Add(RoomLink);
                        RoomLink.Attributes.Add("onclick", "Room_Click(this);");///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        RoomList.Controls.Add(li);
                        if (first)
                        {
                            first = false;

                            ddRoom.InnerHtml = Rooms[i] + "<span class=\"caret\"></span>";
                            Modal_RoomID.Value = i.ToString();
                        }
                    }

                }
                else
                {
                    ddRoom.InnerHtml = "Kein Raum" + "<span class=\"caret\"></span>";
                    Modal_RoomID.Value = "-1";
                }
            }
            Script += @"jQuery('#RemoveRoomConfirm').modal('hide');
                        jQuery(document).ready(function(){
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                        });";
        }

        protected void AddLesson_Click(object sender, EventArgs e)
        {
            int ID;
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.InsertNewLessonName;
                    cmd.Parameters.AddWithValue("@Name", NormalLessonName.Text);
                    cmd.Parameters.AddWithValue("@Namekurz", ShortLessonName.Text);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            LessonName.Add(ID, NormalLessonName.Text);
            Script += @"jQuery('#NewLessonModal').modal('hide');
                        jQuery(document).ready(function(){
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                        });";
            ulLessonNames.Controls.Clear();
            foreach (int i in LessonName.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                HyperLink LessonNameLink = new HyperLink();

                LessonNameLink.Attributes.Add("data-id", i.ToString());
                LessonNameLink.Text = LessonName[i];
                li.Controls.Add(LessonNameLink);
                LessonNameLink.Attributes.Add("onclick", "LessonName_Click(this);");
                ulLessonNames.Controls.Add(li);
            }
        }

        protected void RemoveLessonNameButton_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            if (Modal_LessonNameID.Value == "-1")
            {
                return;
            }
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.CountLessons;
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
                    {
                        Script += @"
                        jQuery(document).ready(function(){
                            jQuery('#RemoveLessonNameConfirm').modal('hide');
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                            jQuery('#LastSubject').modal({backdrop:false});
                        });";
                        return;
                    }
                    else
                    {
                        cmd.CommandText = SQL.Statements.DeleteLessonName;
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Modal_LessonNameID.Value));
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (MySqlException)
                        {
                            Script += @"
                        jQuery(document).ready(function(){
                            jQuery('#RemoveLessonNameConfirm').modal('hide');
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                            jQuery('#LessonNameNotDeleted').modal({backdrop:false});
                        });";

                            con.Close();
                            return;
                        }
                    }
                    con.Close();
                }
            }
            LessonName.Remove(Convert.ToInt32(Modal_LessonNameID.Value));

            if (LessonName.Count > 0)
            {
                int Count = ulLessonNames.Controls.Count;
                for (int i = 1; i < Count; i++)
                {
                    ulLessonNames.Controls.RemoveAt(1);
                }
                bool first = true;

                foreach (int i in LessonName.Keys)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HyperLink LessonNameLink = new HyperLink();

                    LessonNameLink.Attributes.Add("data-id", i.ToString());
                    LessonNameLink.Text = LessonName[i];
                    li.Controls.Add(LessonNameLink);
                    LessonNameLink.Attributes.Add("onclick", "LessonName_Click(this);");
                    ulLessonNames.Controls.Add(li);
                    if (first)
                    {
                        first = false;

                        ddLessonName.InnerHtml = LessonName[i] + "<span class=\"caret\"></span>";
                        Modal_LessonNameID.Value = i.ToString();
                    }
                }
                Script += @"jQuery('#RemoveLessonNameConfirm').modal('hide');
                        jQuery(document).ready(function(){
                            jQuery('#LessonEdit').removeClass('fade');
                            jQuery('#LessonEdit').modal('show');
                            jQuery('.modal-backdrop:first').remove();
                            jQuery('#LessonEdit').addClass('fade');
                            jQuery('.modal-backdrop').addClass('fade');
                        });";
            }
            else
            {
                ddLessonName.InnerHtml = "Keine Fächer" + "<span class=\"caret\"></span>";
                Modal_LessonNameID.Value = "-1";
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
        protected void Logoutbutton_Click(object sender, EventArgs e)
        {
            StoredVars.Objects = new StoredVars();
            Response.Redirect("/default.aspx", true);
        }
        #region Tasks
        private void CopyLessoner(DateTime StartDate, DateTime EndDate, int WeekSpan, int Class, int weekindex, List<DateTime> weekbegins)
        {
            List<DateTime> InsertDates = new List<DateTime>();
            DateTime CurrentDate = StartDate;
            List<Lesson> TimeTableLessons = new List<Lesson>();
            List<DayClone> TimeTableDays = new List<DayClone>();
            while (CurrentDate <= EndDate)
            {
                InsertDates.Add(CurrentDate);
                CurrentDate = CurrentDate.AddDays(7 * WeekSpan);
            }

            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SQL.Statements.GetDayInformations;
                    cmd.Parameters.AddWithValue("@KlasseID", Class);
                    cmd.Parameters.AddWithValue("@Datum", weekbegins[weekindex]);
                    con.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DayClone d = new DayClone();
                            d.ID = Convert.ToInt32(reader["TagID"]);
                            d.FindetStatt = Convert.ToBoolean(reader["FindetStatt"]);
                            d.Information = reader["Information"].ToString();
                            d.DayNameID = Convert.ToInt32(reader["TagInfoID"]);
                            TimeTableDays.Add(d);
                        }
                    }
                    cmd.Parameters.Clear();
                    cmd.CommandText = SQL.Statements.GetLessonPerDay;
                    cmd.Parameters.AddWithValue("@TagID", -1);
                    for (int i = 0; i < TimeTableDays.Count; i++)
                    {
                        cmd.Parameters["@TagID"].Value = TimeTableDays[i].ID;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lesson l = new Lesson();
                                l.ID = Convert.ToInt32(reader["ID"]);
                                l.LehrerID = Convert.ToInt32(reader["LehrerID"]);
                                l.FachID = Convert.ToInt32(reader["FachID"]);
                                l.TagID = Convert.ToInt32(reader["TagID"]);
                                l.StundeBeginn = Convert.ToInt32(reader["Stunde_Beginn"]);
                                l.StundeEnde = Convert.ToInt32(reader["Stunde_Ende"]);
                                l.FachModID = Convert.ToInt32(reader["FachModID"]);
                                l.TagInfoID = TimeTableDays[i].DayNameID;
                                TimeTableLessons.Add(l);
                            }
                        }
                    }
                    for (int i = 0; i < InsertDates.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@KlasseID", Class);
                        cmd.Parameters.AddWithValue("@Datum", InsertDates[i]);
                        cmd.CommandText = SQL.Statements.CheckForLessoner;
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            cmd.CommandText = SQL.Statements.InsertEmptyLessoner;
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = SQL.Statements.GetLessonerID;
                        int LessonerID = Convert.ToInt32(cmd.ExecuteScalar());

                        cmd.CommandText = SQL.Statements.GetDayIDs;
                        cmd.Parameters.AddWithValue("@StundenplanID", LessonerID);
                        List<Day> days = new List<Day>();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Day d = new Day();
                                d.ID = Convert.ToInt32(reader["ID"]);
                                d.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);
                                days.Add(d);
                            }
                        }
                        cmd.CommandText = SQL.Statements.UpdateDay;
                        cmd.Parameters.AddWithValue("@FindetStatt", true);
                        cmd.Parameters.AddWithValue("@Information", "");
                        cmd.Parameters.AddWithValue("@ID", -1);
                        for (int j = 0; j < days.Count; j++)
                        {
                            for (int k = 0; k < TimeTableDays.Count; k++)
                            {
                                if (TimeTableDays[k].DayNameID == days[j].TagInfoID)
                                {
                                    cmd.Parameters["@FindetStatt"].Value = TimeTableDays[k].FindetStatt;
                                    cmd.Parameters["@Information"].Value = TimeTableDays[k].Information;
                                    cmd.Parameters["@ID"].Value = days[j].ID;
                                    cmd.ExecuteNonQuery();
                                    break;
                                }
                            }
                        }
                        cmd.Parameters.Clear();
                        cmd.CommandText = SQL.Statements.DeleteLessonsFromDay;
                        cmd.Parameters.AddWithValue("@TagID", -1);
                        for (int j = 0; j < days.Count; j++)
                        {
                            cmd.Parameters["@TagID"].Value = days[j].ID;
                            cmd.ExecuteNonQuery();
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LehrerID", -1);
                        cmd.Parameters.AddWithValue("@FachID", -1);
                        cmd.Parameters.AddWithValue("@TagID", -1);
                        cmd.Parameters.AddWithValue("@FachModID", -1);
                        cmd.Parameters.AddWithValue("@Stunde_Beginn", -1);
                        cmd.Parameters.AddWithValue("@Stunde_Ende", -1);
                        cmd.CommandText = SQL.Statements.InsertLesson;
                        for (int j = 0; j < TimeTableLessons.Count; j++)
                        {
                            for (int k = 0; k < days.Count; k++)
                            {
                                if (days[k].TagInfoID == TimeTableLessons[j].TagInfoID)
                                {
                                    cmd.Parameters["@LehrerID"].Value = TimeTableLessons[j].LehrerID;
                                    cmd.Parameters["@FachID"].Value = TimeTableLessons[j].FachID;
                                    cmd.Parameters["@TagID"].Value = days[k].ID;
                                    cmd.Parameters["@FachModID"].Value = TimeTableLessons[j].FachModID;
                                    cmd.Parameters["@Stunde_Beginn"].Value = TimeTableLessons[j].StundeBeginn;
                                    cmd.Parameters["@Stunde_Ende"].Value = TimeTableLessons[j].StundeEnde;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
        }
        #endregion
    }
}