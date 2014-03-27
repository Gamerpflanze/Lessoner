using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
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
#endif
            AddClasses();
            BuildRightOptions();
            LoadStudentList();
        }
        private void AddClasses()
        {
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.GetClasses;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        LinkButton ClassLink = new LinkButton();

                        ClassLink.Attributes.Add("data-id", reader["ID"].ToString());
                        ClassLink.Text = reader["Name"].ToString();
                        ClassLink.Click += new EventHandler(ClassSelect_Click);
                        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(ClassLink);
                        li.Controls.Add(ClassLink);
                        ClassList.Controls.Add(li);
                    }
                }
            }
        }
        protected void AddEmptyStudent_Click(object sender, EventArgs e)
        {

        }
        protected void ClassSelect_Click(object sender, EventArgs e)
        {
            LinkButton l = sender as LinkButton;
            SetSelectedClass(Convert.ToInt32(l.Attributes["data-id"]), l.Text);
            LoadStudentList();
        }
        struct Student
        {
            public int ID;
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
                        StudentList.Controls.Add(BuildStudentRow(s.ID, s.Email, s.Vorname, s.Name, s.Strasse, s.Hausnummer, s.PLZ, s.Ort, s.KlasseID, s.KlasseName, Rights));
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
                        CheckBox c = new CheckBox();
                        c.Attributes.Add("onchange", "RightChanged(this)");
                        c.Attributes.Add("data-location", Location.ToString());
                        c.Text = reader["Beschreibung"].ToString();
                        Div.Controls.Add(c);
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
        public static dynamic SaveStudent(dynamic StudentData)
        {
            dynamic ErrorArray = new dynamic[2];
            ErrorArray[0] = 2;
            ErrorArray[1] = new List<dynamic>();
            if (!StoredVars.Objects.Loggedin || !StoredVars.Objects.Rights["studentmanagement"]["permission"])
            {
                return 2;
            }
            using(MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using(MySqlCommand cmd = con.CreateCommand())
                {
                    for(int i = 0; i<StudentData.Length; i++)
                    {
                        if(StudentData[i][0])
                        {//Neuer Schüler

                        }
                        else
                        {//Aktualisieren
                            if (StudentData[i][1] && !StoredVars.Objects.Rights["studentmanagement"]["permission"])
                            {//Änderungen
                                if(StudentData[i].Length!=5||StudentData[i][3].Length!=9||StudentData[i][4].Length!=7)//Daten auf richtiges format überprüfen
                                {
                                    ErrorArray[1].Add(i);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}