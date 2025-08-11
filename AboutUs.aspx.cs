using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class AboutUs : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (!IsPostBack)
                {
                    initialize();
                }
            }
            catch (Exception ex)
            { }
        }
        //public void Initalize()
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strcon))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_HOME_PAGELOAD", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = hdUserId.Value.ToString();
        //            SqlDataAdapter DA = new SqlDataAdapter(cmd);
        //            DataSet Ds = new DataSet();
        //            con.Open();
        //            DA.Fill(Ds);
        //            //response.result = false;
        //            if (Ds != null)
        //            {
        //                if (Ds.Tables[0].Rows.Count > 0)
        //                {
        //                    Session["VMSUSERNAME"] = Ds.Tables[0].Rows[0]["USERNAME"].ToString();
        //                    Session["VMSROLES"] = Ds.Tables[0].Rows[0]["ROLES"].ToString();
        //                    Session["VMSUSERID"] = hdUserId.Value.ToString();
        //                }

        //                if (Ds.Tables[1].Rows.Count > 0)
        //                {
        //                    Session["MRBUSERNAME"] = Ds.Tables[1].Rows[0]["USERNAME"].ToString();
        //                    Session["MRBROLES"] = Ds.Tables[1].Rows[0]["ROLES"].ToString();
        //                    Session["MRBUSERID"] = hdUserId.Value.ToString();
        //                }

        //                if (Ds.Tables[2].Rows.Count > 0)
        //                {
        //                    Session["EQPBRKUPUSERNAME"] = Ds.Tables[2].Rows[0]["USERNAME"].ToString();
        //                    Session["EQPBRKUPROLES"] = Ds.Tables[2].Rows[0]["ROLES"].ToString();
        //                    Session["EQPBRKUPUSERID"] = hdUserId.Value.ToString();
        //                    // Session["EQPBRKUP"] = true;
        //                }
        //                // Response.Redirect("~/Test1.aspx");
        //            }
        //            else
        //            {

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        public void initialize()
        {
            try
            {
                //// string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["FilePath"].ToString());
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("FileName", typeof(string)));

                dt.Columns.Add(new DataColumn("File_Path", typeof(string)));

                dt.Columns.Add(new DataColumn("View", typeof(string)));
                DataRow dr = null;
                dr = dt.NewRow();
                string path = ConfigurationManager.AppSettings["FilePathContacts"].ToString();
                string[] fileEntries = Directory.GetFiles(path);

                foreach (string fileName in fileEntries)
                {
                    dr["FileName"] = fileName.Remove(0, path.Length + 1);
                    dr["File_Path"] = fileName;
                    dr["View"] = "View";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
                ViewState["DGVAvailability"] = dt;
                DGBooking.DataSource = dt;
                DGBooking.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void View_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int i = Convert.ToInt32(row.RowIndex);


                DataTable dt = (DataTable)(ViewState["DGVAvailability"]);

                string viewfilename = dt.Rows[i]["FileName"].ToString();
                // string v = "~/InductionFile/IT/" + viewfilename;

                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"400px\" controlsList=\"nodownload\">";
                //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                // ltEmbed.Text = string.Format(embed, ResolveUrl("~/InductionFile/IT/" + viewfilename));

                //embed = string.Empty;

                //embed= "< video oncontextmenu = \"return false;\" id = \"myVideo\" autoplay controls controlsList = \"nodownload\" >";

                //embed += "< source src = \""+v+"<?php echo $vid;?>\" type = \"video/mp4\" >";
                //embed += "</ video >";

                ltEmbed.Text = string.Format(embed, ResolveUrl("~/InductionFile/CONTACTS/" + viewfilename + "#toolbar=0"));
            }
            catch (Exception ex)
            {

            }
        }
    }
}