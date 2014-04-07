using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Lessoner.Data
{
    public partial class FileDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FileName= Request.QueryString["File"];
            string DownloadFileName="";
            string Path = "";
            using(MySqlConnection con = new MySqlConnection(SQL.Statements.ConnectionString))
            {
                using(MySqlCommand cmd = con.CreateCommand())
                {
                    con.Open();

                    cmd.CommandText = SQL.Statements.GetFile;
                    cmd.Parameters.AddWithValue("@FileName", FileName);
                    using(MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            DownloadFileName = reader["FileName"].ToString();
                            Path = reader["Path"].ToString();
                        }
                    }
                    
                    con.Close();
                }
            }

            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DownloadFileName);
            Response.Flush();
            Response.TransmitFile(Path);
            Response.End();
        }
    }
}