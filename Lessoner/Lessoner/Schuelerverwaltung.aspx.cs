using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Lessoner
{
    public partial class Schuelerverwaltung : System.Web.UI.Page
    {
        #region Structs
        struct SelectedClass
        {
            public string Name;
            public int ID;
        }
        #endregion
        #region Get/Set Stuff
        private SelectedClass GetSelectedClass()
        {
            SelectedClass c = new SelectedClass();
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
            LoadStudentList();
        }
        private void AddClasses()
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.GetClasses;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool first = true;
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
                con.Close();
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
        private void LoadStudentList()
        {
            using (MySqlConnection con = new MySqlConnection("Server=127.0.0.1;Database=dbLessoner;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = SQL.Statements.GetStudentList;
                    cmd.Parameters.AddWithValue("@KlasseID", GetSelectedClass().ID);
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            StudentList.Controls.Add(BuildStudentRow(
                                Convert.ToInt32(reader["ID"]),
                                reader["Email"].ToString(),
                                reader["Vorname"].ToString(),
                                reader["Name"].ToString(),
                                reader["Strasse"].ToString(),
                                reader["Hausnummer"].ToString(),
                                reader["PLZ"].ToString(),
                                reader["Ort"].ToString(),
                                Convert.ToInt32(reader["KlasseID"]),
                                reader["KlassenName"].ToString()
                            ));
                        }
                    }
                    con.Close();
                }
            }
        }
        private TableRow BuildStudentRow(int ID, string Email, string Vorname, string Name, string Strasse, string HausNr, string PLZ, string Ort, int KlasseID, string KlasseName)
        {
            TableRow row = new TableRow();
            row.TableSection = TableRowSection.TableBody;
            TableCell CellVorname = new TableCell();
            Label lVorname = new Label();
            lVorname.Text = Vorname;
            CellVorname.Controls.Add(lVorname);
            row.Controls.Add(CellVorname);

            TableCell CellName = new TableCell();
            Label lName = new Label();
            lName.Text = Name;
            CellName.Controls.Add(lName);
            row.Controls.Add(CellName);

            TableCell CellStrasse = new TableCell();
            Label lStrasse = new Label();
            lStrasse.Text = Strasse;
            CellStrasse.Controls.Add(lStrasse);
            row.Controls.Add(CellStrasse);

            TableCell CellHausNr = new TableCell();
            Label lHausNr = new Label();
            lHausNr.Text = HausNr;
            CellHausNr.Controls.Add(lHausNr);
            row.Controls.Add(CellHausNr);

            TableCell CellPLZ = new TableCell();
            Label lPLZ = new Label();
            lPLZ.Text = PLZ;
            CellPLZ.Controls.Add(lPLZ);
            row.Controls.Add(CellPLZ);

            TableCell CellOrt = new TableCell();
            Label lOrt = new Label();
            lOrt.Text = Ort;
            CellOrt.Controls.Add(lOrt);
            row.Controls.Add(CellOrt);

            return row;
        }
    }
}