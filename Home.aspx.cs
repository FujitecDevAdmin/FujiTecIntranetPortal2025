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
    public partial class Home : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Userid"] != null)
                {
                    hdUserId.Value = Server.UrlDecode(Request.QueryString["Userid"].ToString());
                    Initalize();
                }
                
            }
        }

        public void Initalize()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_HOME_PAGELOAD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = hdUserId.Value.ToString();
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    con.Open();
                    DA.Fill(Ds);
                    //response.result = false;
                    if (Ds != null)
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            Session["VMSUSERNAME"] = Ds.Tables[0].Rows[0]["USERNAME"].ToString();
                            Session["VMSROLES"] = Ds.Tables[0].Rows[0]["ROLES"].ToString();
                            Session["VMSUSERID"] = hdUserId.Value.ToString();
                        }

                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            Session["MRBUSERNAME"] = Ds.Tables[1].Rows[0]["USERNAME"].ToString();
                            Session["MRBROLES"] = Ds.Tables[1].Rows[0]["ROLES"].ToString();
                            Session["MRBUSERID"] = hdUserId.Value.ToString();
                        }

                        if (Ds.Tables[2].Rows.Count > 0)
                        {
                            Session["EQPBRKUPUSERNAME"] = Ds.Tables[2].Rows[0]["USERNAME"].ToString();
                            Session["EQPBRKUPROLES"] = Ds.Tables[2].Rows[0]["ROLES"].ToString();
                            Session["EQPBRKUPUSERID"] = hdUserId.Value.ToString();
                            // Session["EQPBRKUP"] = true;
                        }
                       // Response.Redirect("~/Test1.aspx");
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                
            }
        }

        protected void CP_Click(object sender, EventArgs e)
        {
            //string title = "Greetings";
            //string body = "Welcome to ASPSnippets.com";
            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
            try
            {
                  Response.Redirect("~/Admin/UserCreation.aspx?");

                //string queryString = "ChangePassword.aspx";
                //string newWin = "window.open('" + queryString + "');";
                //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            }
            catch { }
        }

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //DirectoryInfo d = new DirectoryInfo(Server.MapPath(ConfigurationManager.AppSettings["Form16"].ToString()));
        //        //FileInfo[] infos = d.GetFiles();
        //        //foreach (FileInfo f in infos)
        //        //{
        //        //    // File.Move(f.FullName, f.FullName.Replace("abc_", ""));
        //        //    File.Move(f.FullName, f.FullName.Replace(f.Name, f.Name.Substring(0, 5) + ".pdf"));
        //        //}


        //        string filePath = Server.MapPath(ConfigurationManager.AppSettings["Form16"].ToString() + Session["USERID"].ToString() + ".pdf");
        //        if (File.Exists(filePath))
        //        {
        //            Response.Clear();
        //            Response.ContentType = "application/octet-stream";
        //            Response.AppendHeader("content-disposition", "filename="
        //                + Session["USERID"].ToString() + ".pdf");
        //            Response.WriteFile(filePath);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
    }
}