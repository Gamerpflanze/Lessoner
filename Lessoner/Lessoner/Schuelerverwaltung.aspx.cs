using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Security.Cryptography;
using System.Text;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Lessoner
{
    public partial class Schuelerverwaltung : System.Web.UI.Page
    {
        #region Structs
        struct Class
        {
            public string Name;
            public int ID;
        }
        #endregion
        #region Get/Set Stuff
        private Class GetSelectedClass()
        {
            Class c = new Class();
            c.Name = ClassSelecter.Attributes["data-name"];
            c.ID = Convert.ToInt32(ClassSelecter.Attributes["data-id"]);
            return c;
        }
        private void SetSelectedClass(int ID, String Name)
        {
            ClassSelecter.Attributes["data-name"] = Name;
            ClassSelecter.Attributes["data-id"] = ID.ToString();
            OpenClassMenu.InnerHtml = Name + "<span class='caret'></span>";
        }
        #endregion
        #region Handler
        #endregion
        #region DBCOnnection
        MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            con.Open();
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
#if !DEBUG
                if (!StoredVars.Objects.Loggedin)
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
                if (!StoredVars.Objects.Rights["studentmanagement"]["permission"])
                {
                    Response.Clear();
                    Response.StatusCode = 403;
                    Response.End();
                    return;
                }
                if (!StoredVars.Objects.Rights["studentmanagement"]["editclass"])
                {
                    NewClassBtn.Style.Add("display", "none");
                    NewClassBtn.Attributes["onclick"] = "";
                    RenameClass.Style.Add("display", "none");
                    DeleteClass.Style.Add("display", "none");
                }
                if (!StoredVars.Objects.Rights["studentmanagement"]["editstudents"])
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CantEditSet", "CantEditSetter();", true);
                    AddStudent.Style.Add("display", "none");
                    SaveAll.Style.Add("display", "none");
                }
#endif
            if (StoredVars.Objects.Loggedin)
            {
                PageDropDown.Style.Add("display", "block");
                ReadyPageDropDown();
            }
            AddClasses(!Page.IsPostBack);
            BuildRightOptions();
            LoadStudentList();
        }
        private void AddClasses(bool SetNew)
        {
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.GetClasses;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    bool First = true;
                    while (reader.Read())
                    {
                        if(First&&SetNew)
                        {
                            SetSelectedClass(Convert.ToInt32(reader["ID"]), reader["Name"].ToString());
                            First = false;
                        }
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        LinkButton ClassLink = new LinkButton();

                        ClassLink.Attributes.Add("data-id", reader["ID"].ToString());
                        ClassLink.ID="Class"+reader["ID"].ToString();
                        ClassLink.Text = reader["Name"].ToString();
                        ClassLink.Click += new EventHandler(ClassSelect_Click);
                        ClassLink.OnClientClick = "ReadyClassChange();";
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(ClassLink);
                        li.Controls.Add(ClassLink);
                        ClassList.Controls.Add(li);
                    }
                }
            }
        }
        /*protected void AddEmptyStudent_Click(object sender, EventArgs e)
        {

        }*/
        protected void ClassSelect_Click(object sender, EventArgs e)
        {
            LinkButton l = sender as LinkButton;
            SetSelectedClass(Convert.ToInt32(l.Attributes["data-id"]), l.Text);
            LoadStudentList();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoadingIndicatorCloser", "jQuery('#LoadingModal').modal('hide');", true);
        }
        struct Student
        {
            public int ID;
            public int LoginID;
            public string Email;
            public string Vorname;
            public string Name;
            public string Strasse;
            public string Hausnummer;
            public string PLZ;
            public string Ort;
            public int KlasseID;
            public string KlasseName;
        }
        private void LoadStudentList()
        {
            int Count=StudentList.Controls.Count;
            for (int i = 1; i < Count; i++)
            {
                StudentList.Controls.RemoveAt(1);
            }
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.GetStudentList;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@KlasseID", GetSelectedClass().ID);
                List<Student> Students = new List<Student>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student s = new Student();

                        s.ID = Convert.ToInt32(reader["ID"]);
                        s.LoginID = Convert.ToInt32(reader["AnmeldungID"]);
                        s.Email = reader["Email"].ToString();
                        s.Vorname = reader["Vorname"].ToString();
                        s.Name = reader["Name"].ToString();
                        s.Strasse = reader["Strasse"].ToString();
                        s.Hausnummer = reader["Hausnummer"].ToString();
                        s.PLZ = reader["PLZ"].ToString();
                        s.Ort = reader["Ort"].ToString();
                        s.KlasseID = Convert.ToInt32(reader["KlasseID"]);
                        s.KlasseName = reader["KlassenName"].ToString();
                        Students.Add(s);
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = SQL.Statements.GetStudentRights;
                cmd.Parameters.AddWithValue("@SchuelerID", -1);
                foreach (Student s in Students)
                {
                    cmd.Parameters["@SchuelerID"].Value = s.ID;
                    string Rights = "";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["RechtGruppe"].ToString() == "login" && reader["RechtName"].ToString() == "isteacher")
                            {
                                continue;
                            }
                            Rights += Convert.ToInt32(reader["RechtWert"]).ToString();//Umweg damitt 1 oder 0 anstatt true oder false
                        }
                        StudentList.Controls.Add(BuildStudentRow(s.LoginID, s.Email, s.Vorname, s.Name, s.Strasse, s.Hausnummer, s.PLZ, s.Ort, s.KlasseID, s.KlasseName, Rights));
                    }
                }
            }
        }
        private TableRow BuildStudentRow(int ID, string Email, string Vorname, string Name, string Strasse, string HausNr, string PLZ, string Ort, int KlasseID, string KlasseName, string Rights)
        {
            TableRow row = new TableRow();
            row.Attributes.Add("data-rights", Rights);
            row.Attributes.Add("data-changed", "false");
            row.Attributes.Add("data-newstudent", "false");
            row.Attributes.Add("data-id", ID.ToString());

            row.TableSection = TableRowSection.TableBody;

            TableCell CellVorname = new TableCell();
            CellVorname.Attributes.Add("onClick", "EditStudent(this)");
            Label lVorname = new Label();
            lVorname.Text = Vorname;
            CellVorname.Controls.Add(lVorname);
            row.Controls.Add(CellVorname);

            TableCell CellName = new TableCell();
            CellName.Attributes.Add("onClick", "EditStudent(this)");
            Label lName = new Label();
            lName.Text = Name;
            CellName.Controls.Add(lName);
            row.Controls.Add(CellName);

            TableCell CellStrasse = new TableCell();
            CellStrasse.Attributes.Add("onClick", "EditStudent(this)");
            Label lStrasse = new Label();
            lStrasse.Text = Strasse;
            CellStrasse.Controls.Add(lStrasse);
            row.Controls.Add(CellStrasse);

            TableCell CellHausNr = new TableCell();
            CellHausNr.Attributes.Add("onClick", "EditStudent(this)");
            Label lHausNr = new Label();
            lHausNr.Text = HausNr;
            CellHausNr.Controls.Add(lHausNr);
            row.Controls.Add(CellHausNr);

            TableCell CellPLZ = new TableCell();
            CellPLZ.Attributes.Add("onClick", "EditStudent(this)");
            Label lPLZ = new Label();
            lPLZ.Text = PLZ;
            CellPLZ.Controls.Add(lPLZ);
            row.Controls.Add(CellPLZ);

            TableCell CellOrt = new TableCell();
            CellOrt.Attributes.Add("onClick", "EditStudent(this)");
            Label lOrt = new Label();
            lOrt.Text = Ort;
            CellOrt.Controls.Add(lOrt);
            row.Controls.Add(CellOrt);

            TableCell CellEmail = new TableCell();
            CellEmail.Attributes.Add("onClick", "EditStudent(this)");
            Label lEmail = new Label();
            lEmail.Text = Email;
            CellEmail.Controls.Add(lEmail);
            row.Controls.Add(CellEmail);
#if !DEBUG
            if (StoredVars.Objects.Rights["studentmanagement"]["deletestudent"])
#endif
            {
                TableCell CellDelete = new TableCell();
                CellDelete.Attributes.Add("data-ignoretransform", "true");
                HtmlButton DeleteButton = new HtmlButton();
                DeleteButton.Attributes.Add("onclick", "DeleteStudent(this)");
                DeleteButton.Attributes.Add("class", "btn btn-danger hidden-print");
                DeleteButton.Attributes.Add("data-id", ID.ToString());
                HtmlGenericControl DeleteSpan = new HtmlGenericControl("span");
                DeleteSpan.Attributes.Add("class", "glyphicon glyphicon-remove");
                DeleteButton.Controls.Add(DeleteSpan);
                CellDelete.Controls.Add(DeleteButton);
                row.Controls.Add(CellDelete);
            }
            return row;
        }
        private void BuildRightOptions()
        {
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.GetAllRights;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int Location = 0;
                    while (reader.Read())
                    {
                        if (reader["Group"].ToString() == "login" && reader["Name"].ToString() == "isteacher")
                        {
                            continue;
                        }
                        HtmlGenericControl Div = new HtmlGenericControl("div");
                        Div.Attributes.Add("class", "col-sm-6");
                        HtmlInputCheckBox check = new HtmlInputCheckBox();
                        HtmlGenericControl span = new HtmlGenericControl("span");
                        HtmlGenericControl label = new HtmlGenericControl("label");

                        //check.Attributes.Add("onchange", "RightChanged(this)");
                        span.Attributes.Add("data-location", Location.ToString());
                        check.ID = reader["ID"].ToString();
                        label.InnerText = reader["Beschreibung"].ToString();
                        label.Attributes.Add("for", reader["ID"].ToString());
                        span.Attributes.Add("onchange", "RightChanged(this)");
                        span.Controls.Add(check);
                        span.Controls.Add(label);
                        Div.Controls.Add(span);
                        StudentRights.Controls.Add(Div);
                        Location++;
                    }
                }
            }
        }
        /* 0=Success
         * 1=Nicht erlaubt
         * 2+[]=Fehler pro zeile
         */
        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static dynamic SaveStudent(dynamic StudentData, int ClassID)
        {
            dynamic ErrorArray = new dynamic[2];
            ErrorArray[0] = 2;
            ErrorArray[1] = new List<dynamic>();
#if !DEBUG
            if (!StoredVars.Objects.Rights["studentmanagement"]["editstudents"])
            {
                return 2;
            }
#endif
            using (MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        for (int i = 0; i < StudentData.Length; i++)
                        {
#if !DEBUG
                            if (Convert.ToBoolean(StudentData[i][1]))
#endif
                            {
                                if (Convert.ToBoolean(StudentData[i][0]))
                                {//Neuer Schüler
                                    byte[] Password;
                                    if (StudentData[i].Length != 5 || StudentData[i][3].Length != 13 || StudentData[i][4].Length != 7)//Daten auf richtiges format überprüfen
                                    {
                                        ErrorArray[1].Add(i);
                                        continue;
                                    }
                                    using (SHA1 hasher = SHA1.Create())
                                    {
                                        Password = hasher.ComputeHash(Encoding.UTF8.GetBytes(StudentData[i][4][3] + StudentData[i][4][4]));
                                    }
                                    cmd.CommandText = SQL.Statements.InsertNewLogin;
                                    cmd.Parameters.AddWithValue("@Email", StudentData[i][4][6]);
                                    cmd.Parameters.AddWithValue("@Password", Password);
                                    int LoginID = Convert.ToInt32(cmd.ExecuteScalar());
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.InsertNewStudent;
                                    cmd.Parameters.AddWithValue("@AnmeldungID", LoginID);
                                    cmd.Parameters.AddWithValue("@Vorname", StudentData[i][4][0]);
                                    cmd.Parameters.AddWithValue("@Name", StudentData[i][4][1]);
                                    cmd.Parameters.AddWithValue("@Strasse", StudentData[i][4][2]);
                                    cmd.Parameters.AddWithValue("@Hausnummer", StudentData[i][4][3]);
                                    cmd.Parameters.AddWithValue("@PLZ", StudentData[i][4][4]);
                                    cmd.Parameters.AddWithValue("@Ort", StudentData[i][4][5]);
                                    cmd.Parameters.AddWithValue("@KlasseID", ClassID);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.InsertNewRights;
                                    cmd.Parameters.AddWithValue("@AnmeldungID", LoginID);
                                    cmd.Parameters.AddWithValue("@Right1", 0);//da Kein Lehrer
                                    for (int j = 0; j < StudentData[i][3].Length; j++)
                                    {//                                                            V    Doesnt work another way              V 
                                        cmd.Parameters.AddWithValue("@Right" + (j + 2).ToString(), Convert.ToByte(StudentData[i][3][j] == '1'));
                                    }
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                                else
                                {
                                    //Aktualisieren
                                    //Änderungen
                                    if (StudentData[i].Length != 5 || StudentData[i][3].Length != 13 || StudentData[i][4].Length != 7)//Daten auf richtiges format überprüfen
                                    {
                                        ErrorArray[1].Add(i);
                                        continue;
                                    }
                                    cmd.CommandText = SQL.Statements.UpdateRights;      //Update Rechte
                                    cmd.Parameters.AddWithValue("@AnmeldungID", StudentData[i][2]);
                                    for (int j = 0; j < StudentData[i][3].Length; j++)
                                    {
                                        cmd.Parameters.AddWithValue("@Right" + (j + 1).ToString(), Convert.ToByte(StudentData[i][3][j] == '1'));
                                    }
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.UpdateStudent;     //Update Informationen
                                    cmd.Parameters.AddWithValue("@AnmeldungID", StudentData[i][2]);
                                    cmd.Parameters.AddWithValue("@Vorname", StudentData[i][4][0]);
                                    cmd.Parameters.AddWithValue("@Name", StudentData[i][4][1]);
                                    cmd.Parameters.AddWithValue("@Strasse", StudentData[i][4][2]);
                                    cmd.Parameters.AddWithValue("@Hausnummer", StudentData[i][4][3]);
                                    cmd.Parameters.AddWithValue("@PLZ", StudentData[i][4][4]);
                                    cmd.Parameters.AddWithValue("@Ort", StudentData[i][4][5]);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.UpdateLoginName;   //Loginname Updaten
                                    cmd.Parameters.Add("@Email", StudentData[i][4][6]);
                                    cmd.Parameters.AddWithValue("@AnmeldungID", StudentData[i][2]);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                            }
                        }
                        con.Close();
                    }
                    catch
                    {
                        //TODO:fehlerbehebung
                    }
                }
            }

            if (ErrorArray[1].Count > 0)
            {
                return ErrorArray;
            }
            else
            {
                return 0;
            }
        }

        protected void DeleteConfirmButton_Click(object sender, EventArgs e)
        {
            using(MySqlCommand cmd = con.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(DeleteTarget.Value));
                cmd.CommandText = SQL.Statements.DeleteLogin;
                cmd.ExecuteNonQuery();
            }
            LoadStudentList();
            CloseDeleteStudentModal();
        }
        private void CloseDeleteStudentModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoadingModalCloser", "jQuery('#DeleteConfirmModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
        }
        private void CloseNewClassModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NewClassModalCloser", "jQuery('#NewClassModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
        }
        private void CloseRenameClassModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RenameClassModalCloser", "jQuery('#RenameClassModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
        }
        private void CloseDeleteClassModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteClassModalCloser", "jQuery('#DeleteClassConfirmModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
        }
        protected void NewClassConfirm_Click(object sender, EventArgs e)
        {
            using(MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.InsertClass;
                cmd.Parameters.AddWithValue("@Name", NewClassName.Text);
                cmd.ExecuteNonQuery();
            }
            CloseNewClassModal();
            int Count = ClassList.Controls.Count;
            for (int i = 1; i < Count; i++)
            {
                ClassList.Controls.RemoveAt(1);
            }
            AddClasses(false);
        }
        protected void RenameClassConfirm_Click(object sender, EventArgs e)
        {
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@ID", GetSelectedClass().ID);
                cmd.Parameters.AddWithValue("@Name", RenameClassName.Text);
                cmd.CommandText = SQL.Statements.UpdateClass;
                cmd.ExecuteNonQuery();
            }
            Class c = GetSelectedClass();
            int Count = ClassList.Controls.Count;
            for (int i = 1; i < Count; i++ )
            {
                ClassList.Controls.RemoveAt(1);
            }
            SetSelectedClass(c.ID, RenameClassName.Text);
            LoadStudentList();
            CloseRenameClassModal();
            AddClasses(false);
        }
        protected void DeleteClassConfirmButton_Click(object sender, EventArgs e)
        {
            using(MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.DeleteClass;
                cmd.Parameters.AddWithValue("@ID", GetSelectedClass().ID);
                int iD = GetSelectedClass().ID;
                cmd.ExecuteNonQuery();
            }
            int Count = ClassList.Controls.Count;
            for (int i = 1; i < Count; i++)
            {
                ClassList.Controls.RemoveAt(1);
            }
            CloseDeleteClassModal();
            AddClasses(true);
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
    }
}