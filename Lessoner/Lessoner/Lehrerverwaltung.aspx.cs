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
    public partial class Lehrerverwaltung : System.Web.UI.Page
    {
        #region Structs
        struct Class
        {
            public string Name;
            public int ID;
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
            if (StoredVars.Objects.Loggedin)
            {
                PageDropDown.Style.Add("display", "block");
                ReadyPageDropDown();
            }
            BuildRightOptions();
            LoadTeacherList();
        }
        /*protected void AddEmptyStudent_Click(object sender, EventArgs e)
        {

        }*/
        struct Teacher
        {
            public string Titel;
            public int ID;
            public int LoginID;
            public string Email;
            public string Vorname;
            public string Name;
            public string Strasse;
            public string Hausnummer;
            public string PLZ;
            public string Ort;
        }
        private void LoadTeacherList()
        {
            int Count = TeacherList.Controls.Count;
            for (int i = 1; i < Count; i++)
            {
                TeacherList.Controls.RemoveAt(1);
            }
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = SQL.Statements.GetTeacherList;
                cmd.Parameters.Clear();
                List<Teacher> Teachers = new List<Teacher>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Teacher t = new Teacher();

                        t.ID = Convert.ToInt32(reader["ID"]);
                        t.LoginID = Convert.ToInt32(reader["AnmeldungID"]);
                        t.Email = reader["Email"].ToString();
                        t.Vorname = reader["Vorname"].ToString();
                        t.Name = reader["Name"].ToString();
                        t.Strasse = reader["Strasse"].ToString();
                        t.Hausnummer = reader["Hausnummer"].ToString();
                        t.PLZ = reader["PLZ"].ToString();
                        t.Ort = reader["Ort"].ToString();
                        if(DBNull.Value.Equals(reader["Titel"]))
                        {
                            t.Titel = "";
                        }
                        else
                        {
                            t.Titel = reader["Titel"].ToString();
                        }
                        Teachers.Add(t);
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = SQL.Statements.GetTeacherRights;
                cmd.Parameters.AddWithValue("@LehrerID", -1);
                foreach (Teacher t in Teachers)
                {
                    cmd.Parameters["@LehrerID"].Value = t.ID;
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
                        TeacherList.Controls.Add(BuildStudentRow(t.LoginID, t.Email,t.Titel, t.Vorname, t.Name, t.Strasse, t.Hausnummer, t.PLZ, t.Ort, Rights));
                    }
                }
            }
        }
        private TableRow BuildStudentRow(int ID, string Email, string Title, string Vorname, string Name, string Strasse, string HausNr, string PLZ, string Ort, string Rights)
        {
            TableRow row = new TableRow();
            row.Attributes.Add("data-rights", Rights);
            row.Attributes.Add("data-changed", "false");
            row.Attributes.Add("data-newstudent", "false");
            row.Attributes.Add("data-id", ID.ToString());

            row.TableSection = TableRowSection.TableBody;

            TableCell CellTitle = new TableCell();
            CellTitle.Attributes.Add("onClick", "EditTeacher(this)");
            Label lTitle = new Label();
            lTitle.Text = Title;
            CellTitle.Controls.Add(lTitle);
            row.Controls.Add(CellTitle);

            TableCell CellVorname = new TableCell();
            CellVorname.Attributes.Add("onClick", "EditTeacher(this)");
            Label lVorname = new Label();
            lVorname.Text = Vorname;
            CellVorname.Controls.Add(lVorname);
            row.Controls.Add(CellVorname);

            TableCell CellName = new TableCell();
            CellName.Attributes.Add("onClick", "EditTeacher(this)");
            Label lName = new Label();
            lName.Text = Name;
            CellName.Controls.Add(lName);
            row.Controls.Add(CellName);

            TableCell CellStrasse = new TableCell();
            CellStrasse.Attributes.Add("onClick", "EditTeacher(this)");
            Label lStrasse = new Label();
            lStrasse.Text = Strasse;
            CellStrasse.Controls.Add(lStrasse);
            row.Controls.Add(CellStrasse);

            TableCell CellHausNr = new TableCell();
            CellHausNr.Attributes.Add("onClick", "EditTeacher(this)");
            Label lHausNr = new Label();
            lHausNr.Text = HausNr;
            CellHausNr.Controls.Add(lHausNr);
            row.Controls.Add(CellHausNr);

            TableCell CellPLZ = new TableCell();
            CellPLZ.Attributes.Add("onClick", "EditTeacher(this)");
            Label lPLZ = new Label();
            lPLZ.Text = PLZ;
            CellPLZ.Controls.Add(lPLZ);
            row.Controls.Add(CellPLZ);

            TableCell CellOrt = new TableCell();
            CellOrt.Attributes.Add("onClick", "EditTeacher(this)");
            Label lOrt = new Label();
            lOrt.Text = Ort;
            CellOrt.Controls.Add(lOrt);
            row.Controls.Add(CellOrt);

            TableCell CellEmail = new TableCell();
            CellEmail.Attributes.Add("onClick", "EditTeacher(this)");
            Label lEmail = new Label();
            lEmail.Text = Email;
            CellEmail.Controls.Add(lEmail);
            row.Controls.Add(CellEmail);

            TableCell CellDelete = new TableCell();
            CellDelete.Attributes.Add("data-ignoretransform", "true");
            HtmlButton DeleteButton = new HtmlButton();
            DeleteButton.Attributes.Add("onclick", "DeleteTeacher(this)");
            DeleteButton.Attributes.Add("class", "btn btn-danger");
            DeleteButton.Attributes.Add("data-id", ID.ToString());
            HtmlGenericControl DeleteSpan = new HtmlGenericControl("span");
            DeleteSpan.Attributes.Add("class", "glyphicon glyphicon-remove");
            DeleteButton.Controls.Add(DeleteSpan);
            CellDelete.Controls.Add(DeleteButton);
            row.Controls.Add(CellDelete);
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
                        TeacherRights.Controls.Add(Div);
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
        public static dynamic SaveTeacher(dynamic TeacherData)
        {
            dynamic ErrorArray = new dynamic[2];
            ErrorArray[0] = 2;
            ErrorArray[1] = new List<dynamic>();
#if !DEBUG
            if (!StoredVars.Objects.Loggedin || !StoredVars.Objects.Rights["studentmanagement"]["permission"])
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
                        for (int i = 0; i < TeacherData.Length; i++)
                        {
#if !DEBUG
                            if (Convert.ToBoolean(TeacherData[i][1]) && !StoredVars.Objects.Rights["studentmanagement"]["permission"])
#endif
                            {
                                if (Convert.ToBoolean(TeacherData[i][0]))
                                {//Neuer Schüler
                                    byte[] Password;
                                    if (TeacherData[i].Length != 5 || TeacherData[i][3].Length != 9 || TeacherData[i][4].Length != 8)//Daten auf richtiges format überprüfen
                                    {
                                        ErrorArray[1].Add(i);
                                        continue;
                                    }
                                    using (SHA1 hasher = SHA1.Create())
                                    {
                                        Password = hasher.ComputeHash(Encoding.UTF8.GetBytes(TeacherData[i][4][3] + TeacherData[i][4][4]));
                                    }
                                    cmd.CommandText = SQL.Statements.InsertNewLogin;
                                    cmd.Parameters.AddWithValue("@Email", TeacherData[i][4][6]);
                                    cmd.Parameters.AddWithValue("@Password", Password);
                                    int LoginID = Convert.ToInt32(cmd.ExecuteScalar());
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.InsertNewTeacher;
                                    cmd.Parameters.AddWithValue("@AnmeldungID", LoginID);
                                    cmd.Parameters.AddWithValue("@Titel", TeacherData[i][4][0]);
                                    cmd.Parameters.AddWithValue("@Vorname", TeacherData[i][4][1]);
                                    cmd.Parameters.AddWithValue("@Name", TeacherData[i][4][2]);
                                    cmd.Parameters.AddWithValue("@Strasse", TeacherData[i][4][3]);
                                    cmd.Parameters.AddWithValue("@Hausnummer", TeacherData[i][4][4]);
                                    cmd.Parameters.AddWithValue("@PLZ", TeacherData[i][4][5]);
                                    cmd.Parameters.AddWithValue("@Ort", TeacherData[i][4][6]);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.InsertNewRights;
                                    cmd.Parameters.AddWithValue("@AnmeldungID", LoginID);
                                    cmd.Parameters.AddWithValue("@Right1", 1);//da Lehrer
                                    for (int j = 0; j < TeacherData[i][3].Length; j++)
                                    {//                                                            V    Doesnt work another way              V 
                                        cmd.Parameters.AddWithValue("@Right" + (j + 2).ToString(), Convert.ToByte(TeacherData[i][3][j] == '1'));
                                    }
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                                else
                                {
                                    //Aktualisieren
                                    //Änderungen
                                    if (TeacherData[i].Length != 5 || TeacherData[i][3].Length != 9 || TeacherData[i][4].Length != 8)//Daten auf richtiges format überprüfen
                                    {
                                        ErrorArray[1].Add(i);
                                        continue;
                                    }
                                    cmd.CommandText = SQL.Statements.UpdateRights;      //Update Rechte
                                    cmd.Parameters.AddWithValue("@AnmeldungID", TeacherData[i][2]);
                                    for (int j = 0; j < TeacherData[i][3].Length; j++)
                                    {
                                        cmd.Parameters.AddWithValue("@Right" + (j + 1).ToString(), Convert.ToByte(TeacherData[i][3][j] == '1'));
                                    }
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.UpdateTeacher;     //Update Informationen
                                    cmd.Parameters.AddWithValue("@AnmeldungID", TeacherData[i][2]);
                                    cmd.Parameters.AddWithValue("@Titel", TeacherData[i][4][0]);
                                    cmd.Parameters.AddWithValue("@Vorname", TeacherData[i][4][1]);
                                    cmd.Parameters.AddWithValue("@Name", TeacherData[i][4][2]);
                                    cmd.Parameters.AddWithValue("@Strasse", TeacherData[i][4][3]);
                                    cmd.Parameters.AddWithValue("@Hausnummer", TeacherData[i][4][4]);
                                    cmd.Parameters.AddWithValue("@PLZ", TeacherData[i][4][5]);
                                    cmd.Parameters.AddWithValue("@Ort", TeacherData[i][4][6]);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = SQL.Statements.UpdateLoginName;   //Loginname Updaten
                                    cmd.Parameters.Add("@Email", TeacherData[i][4][6]);
                                    cmd.Parameters.AddWithValue("@AnmeldungID", TeacherData[i][2]);
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
            using (MySqlCommand cmd = con.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(DeleteTarget.Value));
                cmd.CommandText = SQL.Statements.DeleteLogin;
                cmd.ExecuteNonQuery();
            }
            LoadTeacherList();
            CloseDeleteTeacherModal();
        }
        private void CloseDeleteTeacherModal()
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LoadingModalCloser", "jQuery('#DeleteConfirmModal').modal('hide');jQuery('#LoadingModal').modal('hide');", true);
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
    }
}