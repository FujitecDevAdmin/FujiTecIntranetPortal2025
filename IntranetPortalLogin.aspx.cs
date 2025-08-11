using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.IO;

namespace FujiTecIntranetPortal
{
    public partial class IntranetPortalLogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["USERNAME"] = null;
                Session["ROLES"] = null;
                Session["USERID"] = null;
                Session["DEPT"] = null;
                /////////
                Session["VMSUSERNAME"] = null;
                Session["VMSROLES"] = null;
                Session["VMSUSERID"] = null;
                Session["VMS"] = null;
                ////////////////
                Session["MRBUSERNAME"] = null;
                Session["MRBROLES"] = null;
                Session["MRBUSERID"] = null;
                Session["MRB"] = null;
                ////////////////
                Session["EQPBRKUPUSERNAME"] = null;
                Session["EQPBRKUPROLES"] = null;
                Session["EQPBRKUPUSERID"] = null;
                Session["EQPBRKUP"] = null;
                Session["USERID"] = null;
                ViewState["GrdEmplWel"] = null;
                Session["Loc"] = null;
                Session["TTAPPID"] = null;
                //Image2.ImageUrl = ConfigurationManager.AppSettings["Quote"].ToString();
                PgLoad();
                //BindGridviewData();

                //Timer1.Enabled = true;
                Timer2.Interval = 100;
                hdtimer.Value = "0";
                txtUsername.Focus();

                //imagefile();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session["USERID"] = txtUsername.Text;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_APPLICATION_BSDON_USERLOGIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = txtUsername.Text;
                    cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = txtPassword.Text;
                    cmd.Parameters.Add("@SESSIONID", SqlDbType.VarChar).Value = this.Session.SessionID; 
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    con.Open();
                    DA.Fill(Ds);
                    //response.result = false;
                    if (Ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                    {
                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            Session["VMSUSERNAME"] = Ds.Tables[1].Rows[0]["USERNAME"].ToString();
                            Session["VMSROLES"] = Ds.Tables[1].Rows[0]["ROLES"].ToString();
                            Session["VMSUSERID"] = txtUsername.Text;
                        }

                        if (Ds.Tables[2].Rows.Count > 0)
                        {
                            Session["MRBUSERNAME"] = Ds.Tables[2].Rows[0]["USERNAME"].ToString();
                            Session["MRBROLES"] = Ds.Tables[2].Rows[0]["ROLES"].ToString();
                            Session["MRBUSERID"] = txtUsername.Text;
                        }

                        if (Ds.Tables[3].Rows.Count > 0)
                        {
                            Session["EQPBRKUPUSERNAME"] = Ds.Tables[3].Rows[0]["USERNAME"].ToString();
                            Session["EQPBRKUPROLES"] = Ds.Tables[3].Rows[0]["ROLES"].ToString();
                            Session["EQPBRKUPUSERID"] = txtUsername.Text;
                            // Session["EQPBRKUP"] = true;
                        }
                        if (Ds.Tables[4].Rows.Count > 0)
                        {
                            Session["USERNAME"] = Ds.Tables[4].Rows[0]["USERNAME"].ToString();
                            Session["ROLES"] = Ds.Tables[4].Rows[0]["ROLES"].ToString();
                            Session["USERID"] = txtUsername.Text;
                            Session["Loc"] = Ds.Tables[4].Rows[0]["BranchID"].ToString();
                            // Session["EQPBRKUP"] = true;
                        }
                        Response.Redirect("~/Home.aspx");

                    }
                    else
                    {
                        // Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Please enter valid username and password')</script>");
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                        //  if ((txtUsername.Text != String.Empty) || (txtPassword.Text != String.Empty))
                        lblmsg.Text = "Please enter correct login details";
                    }
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void gvconverted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].CssClass = "gridcss";
                e.Row.Cells[1].CssClass = "gridcss1";

            }
            //this.gvconverted.DefaultCellStyle.Font = new Font("Tahoma", 15);

        }

        public DataTable ConvertColumnsAsRows(DataTable dt)
        {
            DataTable dtnew = new DataTable();
            //Convert all the rows to columns
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtnew.Columns.Add(Convert.ToString(i));
            }
            DataRow dr;
            // Convert All the Columns to Rows
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr = dtnew.NewRow();
                dr[0] = dt.Columns[j].ToString();
                for (int k = 1; k <= dt.Rows.Count; k++)
                    dr[k] = dt.Rows[k - 1][j];
                dtnew.Rows.Add(dr);
            }
            return dtnew;
        }
        // protected void BindGridviewData()
        protected void BindGridviewData(string Name, string Designation, string Department, string Location, string EMail, string Mobile, string ReportingTo, string Qualification, string Experience, string photo, int count)
        {
            DataTable dtview = new DataTable();
            dtview = (DataTable)ViewState["GrdEmplWel"];

            //if (count < dtview.Rows.Count)
            //    hdtimer.Value = count.ToString();
            //else
            //    hdtimer.Value = "0";
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
           // 
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("Location", typeof(string));
           // dt.Columns.Add("ReportingTo", typeof(string));
            //dt.Columns.Add("E-Mail", typeof(string));
            //dt.Columns.Add("Mobile", typeof(string));

            //dt.Columns.Add("Qualification", typeof(string));
            //dt.Columns.Add("Experience", typeof(string));

            // PgLoad();

            //  DataTable dtview = new DataTable();
            // dtview = (DataTable)ViewState["GrdEmplWel"];

            DataRow dtrow = dt.NewRow();    // Create New Row
            //for()
            dtrow["Name"] = Name;             //Bind Data to Columns
            //
            dtrow["Department"] = Department;
            dtrow["Designation"] = Designation;
            dtrow["Location"] = Location;
           // dtrow["ReportingTo"] = ReportingTo;
           // dtrow["E-Mail"] = EMail;               //Bind Data to Columns
            //dtrow["Mobile"] = Mobile;

            //dtrow["Qualification"] = Qualification;
            //dtrow["Experience"] = Experience;
            //// dtrow["Experience"] = dtview.Rows[0]["Emp_Photo"].ToString();
            dt.Rows.Add(dtrow);
            img_Empphoto.ImageUrl = @"~\EmployeePhoto\" + photo.ToString();
            // gvnormal.DataSource = dt;
            // gvnormal.DataBind();
            gvconverted.DataSource = ConvertColumnsAsRows(dt);
            gvconverted.DataBind();
            gvconverted.HeaderRow.Visible = false;
            //lstWelcomeMsg.Text = "";
            //lstWelcomeMsg.Text = $"\u2022 We are so excited to have you on our team! ..." + System.Environment.NewLine;
            //lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022 Your skills and talents will be a great addition to our project..." + Environment.NewLine;
            //lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  On behalf of the whole department, welcome onboard!..." + Environment.NewLine;
            //lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  Congratulations on joining our team!.." + Environment.NewLine;
            //lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  I welcome you on behalf of management and hope you will enjoy working with us." + Environment.NewLine;
            //lstWelcomeMsg.Font.Bold = true;
        }

        private void imagefile()
        {           

            // var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DataTable dtfilename = new DataTable();
            dtfilename.Columns.Add("ImageFileName", typeof(string));
            //DataRow dtrow = dtfilename.NewRow();
            var files = Directory.GetFiles(Server.MapPath("~/NewsEvents/FUJI/"));
            foreach (var f in files)
            {

                DataRow dtrow = dtfilename.NewRow();
                string fileName = Path.GetFileName(f);
                dtrow["ImageFileName"] = fileName;
                dtfilename.Rows.Add(dtrow);
            }
            ViewState["NEWSEVENTS"] = dtfilename;
        }

        private void PgLoad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_LoginPageload", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    con.Open();
                    DA.Fill(Ds);
                    //response.result = false;
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["GrdEmplWel"] = Ds.Tables[0];
                        ViewState["NEWSEVENTS"] = Ds.Tables[1];

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtview = new DataTable();
                dtview = (DataTable)ViewState["GrdEmplWel"];
                // hdtimer.Value = "0";
                if (ViewState["Griddata"] == null)
                {
                    ViewState["Griddata"] = 0;
                }
                int jv = (int)ViewState["Griddata"];
                for (int i = jv; i < dtview.Rows.Count; i++)
                {
                    string Name = dtview.Rows[i]["Emp_name"].ToString();             //Bind Data to Columns
                    string Designation = dtview.Rows[i]["Designation"].ToString();
                    string Department = dtview.Rows[i]["Department"].ToString();
                    string Location = dtview.Rows[i]["location"].ToString();
                    string EMail = dtview.Rows[i]["EmailId"].ToString();               //Bind Data to Columns
                    string Mobile = dtview.Rows[i]["Mobileno"].ToString();
                    string ReportingTo = dtview.Rows[i]["ReportingTo"].ToString();
                    string Qualification = dtview.Rows[i]["Qualification"].ToString();
                    string Experience = dtview.Rows[i]["PreviousExperience"].ToString();
                    string photo = dtview.Rows[i]["Emp_Photo"].ToString();

                    int count = (int)ViewState["Griddata"] + 1;
                    //Bind Data to Columns
                    BindGridviewData(Name, Designation, Department, Location, EMail, Mobile, ReportingTo, Qualification, Experience, photo, count);
                   // i = i + 1;
                    //Image3.ImageUrl = @"~/NewsEvents/" + i + ".jpg";
                    if (count < dtview.Rows.Count)
                        ViewState["Griddata"] = i;
                    else
                        ViewState["Griddata"] = 0;

                    Timer2.Interval = 10000;

                    //Timer1.Enabled = false;
                    //Timer2.Enabled = true;
                    //SetImageUrl(i);
                    //Timer2.Interval = 1000;
                    break;
                }
            }
            catch (Exception ex)
            {

            }
            //Random rand = new Random();
            //int i = rand.Next(1, 8);
            //Image3.ImageUrl = @"~/assets/images/" + i+".jpg";


        }

        protected void ImgbtnITSupport_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"http://itsupport.fujitecindia.com:8080/");
        }

        protected void ImgbtnVendora_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"https://scm.fujitecindia.com/buyer/users/login");
        }

        protected void ImgbtnFJP_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"http://home.intra.fujitec.com/");
        }

        protected void Imgbtnoffice365_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"https://www.office.com/");
        }

        protected void ImgbtnGoogleWorkspace_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"https://sites.google.com/a/jp.fujitec.com/googleapps-fujitec-global-site/");
        }

        protected void ImgbtnERP_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"http://fujitecerp/");
        }

        protected void ImgbtnCloud_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"https://app.cloudedi.io/#/Login");
        }

        protected void ImgbtnHRM_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"https://hrmantra.com/HRMGlobal/");
        }

        private void SetImageURLAuto()
        {
            try
            {
                if (ViewState["NEWSEVENTS"] != null)
                {
                    DataTable dtfilename = (DataTable)ViewState["NEWSEVENTS"];
                   // int jv = Convert.ToInt32(hdtimer.Value.ToString());
                    int jv = (int)ViewState["NewsEvents"];
                    for (int i = jv; i < dtfilename.Rows.Count; i++)
                    {

                        string Name = dtfilename.Rows[i]["ImageFileName"].ToString();
                        string Extension = dtfilename.Rows[i]["Extension"].ToString();
                        string FileNamepathNEWS = dtfilename.Rows[i]["FileNamepath"].ToString();
                       // Image3.ImageUrl = ConfigurationManager.AppSettings["News_Image"] + Name+ Extension;
                        Image3.ImageUrl = FileNamepathNEWS;
                        hdtimer.Value = i.ToString();
                       // Image3.AlternateText = Name;
                       //lblImageEvent.Text = Name.ToUpper().Substring(0, Name.Length - 4);
                        lblImageEvent.Text = Name.ToUpper();
                        break;
                    }
                    if (jv == dtfilename.Rows.Count)
                    {
                        ViewState["NewsEvents"] = null;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetImageUrl()
        {
            ////Random rand = new Random();
            ////int i = rand.Next(1, 6);
            ////Image3.ImageUrl = @"~/NewsEvents/" + i + ".jpg";
            // string path = @"~/NewsEvents/" + 1 + ".jpg";
            GVdata();
            if (ViewState["NewsEvents"] == null)
            {
                ViewState["NewsEvents"] = 0;
            }
            else
            {              

                int i = (int)ViewState["NewsEvents"];
                    i = i + 1;
                    ViewState["NewsEvents"] = i;
               
            }
                SetImageURLAuto();

            ////////////////////////////////////////////////////////
            //if (ViewState["NewsEvents"] == null)
            //{
            //    Image3.ImageUrl = @"~/NewsEvents/FUJI/" + 1 + ".jpg";
            //    ViewState["NewsEvents"] = 1;
            //}
            //else
            //{
            //    int i = (int)ViewState["NewsEvents"];
            //    if (i == 6)
            //    {
            //        Image3.ImageUrl = @"~/NewsEvents/FUJI/" + 1 + ".jpg";
            //        ViewState["NewsEvents"] = 1;
            //    }
            //    else
            //    {
            //        i = i + 1;
            //        Image3.ImageUrl = @"~/NewsEvents/FUJI/" + i + ".jpg";
            //        ViewState["NewsEvents"] = i;
            //    }
            //}
            ////////////////////////////////////////////////////////////////////
            // i = i + 1;
            //   Image3.ImageUrl = @"~/NewsEvents/" + imageno + ".jpg";
        }

        private void GVdata()
        {
            try
            {
                DataTable dtview = new DataTable();
                dtview = (DataTable)ViewState["GrdEmplWel"];
                // hdtimer.Value = "0";
                if (ViewState["Griddata"] == null)
                {
                    ViewState["Griddata"] = 0;
                }
                int jv = (int)ViewState["Griddata"];
                for (int i = jv; i < dtview.Rows.Count; i++)
                {
                    string Name = dtview.Rows[i]["Emp_name"].ToString();             //Bind Data to Columns
                    string Designation = dtview.Rows[i]["Designation"].ToString();
                    string Department = dtview.Rows[i]["Department"].ToString();
                    string Location = dtview.Rows[i]["location"].ToString();
                    string EMail = dtview.Rows[i]["EmailId"].ToString();               //Bind Data to Columns
                    string Mobile = dtview.Rows[i]["Mobileno"].ToString();
                    string ReportingTo = dtview.Rows[i]["ReportingTo"].ToString();
                    string Qualification = dtview.Rows[i]["Qualification"].ToString();
                    string Experience = dtview.Rows[i]["PreviousExperience"].ToString();
                    string photo = dtview.Rows[i]["Emp_Photo"].ToString();

                    int count = (int)ViewState["Griddata"] + 1;
                    //Bind Data to Columns
                    BindGridviewData(Name, Designation, Department, Location, EMail, Mobile, ReportingTo, Qualification, Experience, photo, count);
                    i = i + 1;
                    //Image3.ImageUrl = @"~/NewsEvents/" + i + ".jpg";
                    if (count < dtview.Rows.Count)
                        ViewState["Griddata"] = i;
                    else
                        ViewState["Griddata"] = 0;

                    //Timer1.Interval = 4000;

                    //Timer1.Enabled = false;
                    //Timer2.Enabled = true;
                    //SetImageUrl(i);
                    //Timer2.Interval = 1000;
                    break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void Timer2_Tick(object sender, EventArgs e)
        {
            SetImageUrl();
            //Timer2.Enabled = false;
            //Timer1.Enabled = true;
             Timer2.Interval = 10000;
        }
    }
}