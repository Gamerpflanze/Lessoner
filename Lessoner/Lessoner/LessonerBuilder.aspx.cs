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
    {//TODO: LoadLessoner mit Control Events sortieren
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
        }
        Dictionary<int, string> Teacher
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.Modal.Teacher;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.Modal.Teacher = value;
            }
        }
        Dictionary<int, string> LessonName
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.Modal.LessonName;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.Modal.LessonName = value;
            }
        }
        Dictionary<int, string> LessonModifier
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.Modal.LessonModifier;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.Modal.LessonModifier = value;
            }
        }
        List<Lesson> Lessons
        {
            get
            {
                return StoredVars.Objects.LessonerBuilder.Lessons;
            }
            set
            {
                StoredVars.Objects.LessonerBuilder.Lessons = value;
            }
        }
        //=================================================================

        //TODO: Optimisierungen
        protected void Page_Load(object sender, EventArgs e)
        {
            //EventHandler
            //this.PreRender += new System.EventHandler(this.LessonerBuilder_PreRender);
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
                LessonerBuilderCache.ClassSelector cl = SelectedTimeTable;
                cl.ClassID = -1;
                SelectedTimeTable = cl;
                using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
                {
                    using (MySqlCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        Teacher.Clear();
                        LessonName.Clear();
                        LessonModifier.Clear();
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
                        con.Close();
                    }
                }
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
                    con.Close();
                }
            }
            foreach (int i in Teacher.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                LinkButton TeacherLink = new LinkButton();

                TeacherLink.Attributes.Add("data-id", i.ToString());
                TeacherLink.Text = Teacher[i];
                li.Controls.Add(TeacherLink);
                TeacherLink.Click += new EventHandler(Teacher_Click);
                ulTeacher.Controls.Add(li);
            }
            foreach (int i in LessonName.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                LinkButton LessonNameLink = new LinkButton();

                LessonNameLink.Attributes.Add("data-id", i.ToString());
                LessonNameLink.Text = LessonName[i];
                li.Controls.Add(LessonNameLink);
                LessonNameLink.Click += new EventHandler(LessonName_Click);
                ulLessonNames.Controls.Add(li);
            }
            foreach (int i in LessonModifier.Keys)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                LinkButton LessonModLink = new LinkButton();

                LessonModLink.Attributes.Add("data-id", i.ToString());
                LessonModLink.Text = LessonModifier[i];
                li.Controls.Add(LessonModLink);
                LessonModLink.Click += new EventHandler(LessonMod_Click);
                ulLessonMod.Controls.Add(li);
            }
            InitialiseLessoner();
            lbtnOpenClassMenu.Attributes["data-id"] = SelectedTimeTable.ClassID.ToString();
            lbtnOpenClassMenu.Text = SelectedTimeTable.ClassName + "<span class=\"caret\"></span>";

            if (!Page.IsPostBack)
            {
                LoadLessoner();
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
            LoadLessoner();
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
        }
        protected void ClassSelect_Click(object sender, EventArgs e)
        {
            LinkButton ClassButton = sender as LinkButton;

            lbtnOpenClassMenu.Text = ClassButton.Text + "<span class=\"caret\"></span>";
            lbtnOpenClassMenu.Attributes["data-id"] = ClassButton.Attributes["data-id"];

            LessonerBuilderCache.ClassSelector c = SelectedTimeTable;
            c.ClassID = Convert.ToInt32(ClassButton.Attributes["data-id"]);
            c.ClassName = ClassButton.Text;
            SelectedTimeTable = c;

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
                        for (int i = 1; reader.Read(); i++)//LOL It works, we shall keep it!!!!!!!!!!!!!!!!!!!
                        {
                            (tbTimetable.Controls[0].Controls[Convert.ToInt32(reader["TagInfoID"])] as TableHeaderCell).Attributes.Add("data-id", reader["TagID"].ToString());
                            Day d = new Day();
                            d.TagInfoID = Convert.ToInt32(reader["TagInfoID"]);
                            (tbTimetable.Controls[0].Controls[i] as TableCell).Attributes.Add("data-takesplace", reader["FindetStatt"].ToString());
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
                                (tbTimetable.Controls[1].Controls[i] as TableCell).Attributes["data-infotype"] = "2";
                                (tbTimetable.Controls[1].Controls[i] as TableCell).Text = reader["Information"].ToString();
                                (tbTimetable.Controls[1].Controls[i] as TableCell).CssClass = "danger LessonerDayFree";
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteConfirmModalOpener", JavascriptCaller.OpenDeleteConfirmModal, true);
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

            ddTeacher.Attributes.Add("data-id", lesson.LehrerID.ToString());
            ddLessonName.Attributes.Add("data-id", lesson.FachID.ToString());
            ddLessonMod.Attributes.Add("data-id", lesson.FachModID.ToString());

            txtCountBegin.Text = lesson.StundeBeginn.ToString();
            txtCountEnd.Text = lesson.StundeEnde.ToString();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalOpener", JavascriptCaller.OpenLessonEditModal, true);
        }
        protected void AddLession_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            int Row = tbTimetable.Controls.IndexOf((((sender as Control).Parent as Control).Parent as TableCell).Parent as TableRow);
            int DayID = Convert.ToInt32((tbTimetable.Controls[0].Controls[tbTimetable.Controls[Row].Controls.IndexOf(((sender as Control).Parent as Control).Parent as TableCell)] as TableHeaderCell).Attributes["data-id"]);
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
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

            ddTeacher.Attributes.Add("data-id", lesson.LehrerID.ToString());
            ddLessonName.Attributes.Add("data-id", lesson.FachID.ToString());
            ddLessonMod.Attributes.Add("data-id", lesson.FachModID.ToString());

            txtCountBegin.Text = lesson.StundeBeginn.ToString();
            txtCountEnd.Text = lesson.StundeEnde.ToString();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalOpener", JavascriptCaller.OpenLessonEditModal, true);
        }
        private void KeepEditModalOpen()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalKeeper", JavascriptCaller.KeepEditModal, true);
        }
        private void HideEditModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalHider", JavascriptCaller.HideEditModal, true);
        }
        private void HideEditModalNoAbort()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalHiderNoAbort", JavascriptCaller.HideEditModalNoAbort, true);
        }
        private void KeepAbortModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AbortModalKeeper", JavascriptCaller.KeepAbortModalOpen, true);
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
        protected void Teacher_Click(object sender, EventArgs e)
        {
            ddTeacher.Attributes["data-id"] = (sender as LinkButton).Attributes["data-id"];
            ddTeacher.InnerHtml = (sender as LinkButton).Text + "<span class=\"caret\"></span>";
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
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.UpdateLesson;

                    cmd.Parameters.AddWithValue("@ID", btnApply.Attributes["data-lessonid"]);
                    cmd.Parameters.AddWithValue("@LehrerID", Convert.ToInt32(ddTeacher.Attributes["data-id"]));
                    cmd.Parameters.AddWithValue("@FachID", ddLessonName.Attributes["data-id"]);
                    cmd.Parameters.AddWithValue("@FachModID", ddLessonMod.Attributes["data-id"]);
                    cmd.Parameters.AddWithValue("@StundeBeginn", txtCountBegin.Text);
                    cmd.Parameters.AddWithValue("@StundeEnde", txtCountEnd.Text);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            KeepEditModalOpen();
            if (btn.ID == "btnApply")
            {
                HideEditModalNoAbort();
            }
            else
            {
                KeepAbortModal();
                HideEditModal();
            }

            LoadLessoner();
        }
        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteConfirmModalClode", JavascriptCaller.CloseDeleteConfirmModal, true);
        }
        protected void EditDay_Click(object sender, EventArgs e)
        {
            LoadLessoner();
            TableHeaderCell DayCell = (sender as Control).Parent as TableHeaderCell;
            chkTakesPlace.Checked = Convert.ToBoolean(DayCell.Attributes["data-takesplace"]);
            txtDayInfo.Text = (tbTimetable.Controls[1].Controls[tbTimetable.Controls[0].Controls.IndexOf(DayCell)] as TableCell).Text;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditDayOpener", JavascriptCaller.OpenEditDayModal, true);
            btnApplyDay.Attributes.Add("data-id", DayCell.Attributes["data-id"]);
        }
        protected void chkTakesPlace_CheckedChanged(object sender, EventArgs e)
        {
            LoadLessoner();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditDayModalKeeper", JavascriptCaller.KeepEditDayModal, true);
        }
        protected void ApplyDay_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using(MySqlCommand cmd = con.CreateCommand())
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditDayModalKeeper", JavascriptCaller.KeepEditDayModal, true);
            if ((sender as Control).ID == "btnApplyDay")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalCloser", JavascriptCaller.HideEditDayModal, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditDayModalCloserWithAbort", JavascriptCaller.HideEditDayModalWithAbort, true);
            }
        }
        //TODO: Den Kommentar hier drunter entfernen
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
        private void LessonerBuilder_PreRender(object sender, EventArgs e)
        {
            LoadLessoner();
        }
    }
}