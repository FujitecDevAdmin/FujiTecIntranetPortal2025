using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class Login : System.Web.UI.Page
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
                PgLoad();
                //BindGridviewData();
                
                Timer1.Enabled = true;
                //Timer1.Interval = 60000; //1 minute
                Timer1.Interval = 100;
                hdtimer.Value = "0";
                txtUsername.Focus();
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
                        Response.Redirect("~/Home.aspx");

                    }
                    else
                    {
                       // Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Please enter valid username and password')</script>");
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        
                      //  if ((txtUsername.Text != String.Empty) || (txtPassword.Text != String.Empty))
                            lblmsg.Text = "Please enter correct login details";
                        //else if (txtUsername.Text != String.Empty)
                        //    lblmsg.Text = "Please enter UserId";
                        //else if (txtPassword.Text != String.Empty)
                        //    lblmsg.Text = "Please enter Password";
                        //else if ((txtUsername.Text.Length == 0) || (txtPassword.Text.Length == 0))
                        //    lblmsg.Text = "Please enter Login details";

                        //string script = "alert(\"Hello!\");";
                        //ScriptManager.RegisterStartupScript(this, GetType(),
                        //                      "ServerControlScript", script, true);
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
                //  e.Row.Cells[1].
                // gvconverted.Font.Size = FontUnit.XLarge;

              //  this.gvconverted.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
               // this.gvconverted.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);

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

            if (count < dtview.Rows.Count)
                hdtimer.Value = count.ToString();
            else
                hdtimer.Value = "0";
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("Department", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            dt.Columns.Add("E-Mail", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Reporting To", typeof(string));
            dt.Columns.Add("Qualification", typeof(string));
            dt.Columns.Add("Experience", typeof(string));

            // PgLoad();

          //  DataTable dtview = new DataTable();
           // dtview = (DataTable)ViewState["GrdEmplWel"];

            DataRow dtrow = dt.NewRow();    // Create New Row
            //for()
            dtrow["Name"] = Name;             //Bind Data to Columns
            dtrow["Designation"] = Designation;
            dtrow["Department"] = Department;
            dtrow["Location"] = Location;
            dtrow["E-Mail"] = EMail;               //Bind Data to Columns
            dtrow["Mobile"] = Mobile;
            dtrow["Reporting To"] = ReportingTo;
            dtrow["Qualification"] = Qualification;
            dtrow["Experience"] = Experience;
            // dtrow["Experience"] = dtview.Rows[0]["Emp_Photo"].ToString();
            dt.Rows.Add(dtrow);
            img_Empphoto.ImageUrl = @"~\EmployeePhoto\" + photo.ToString();
            // gvnormal.DataSource = dt;
            // gvnormal.DataBind();
            gvconverted.DataSource = ConvertColumnsAsRows(dt);
            gvconverted.DataBind();
            gvconverted.HeaderRow.Visible = false;
            lstWelcomeMsg.Text = "";
            lstWelcomeMsg.Text =  $"\u2022 We are so excited to have you on our team! ..." + System.Environment.NewLine; 
            lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022 Your skills and talents will be a great addition to our project..." + Environment.NewLine;
            lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  On behalf of the whole department, welcome onboard!..." + Environment.NewLine;
            lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  Congratulations on joining our team!.." + Environment.NewLine;
            lstWelcomeMsg.Text = lstWelcomeMsg.Text + $"\u2022  I welcome you on behalf of management and hope you will enjoy working with us." + Environment.NewLine;
            lstWelcomeMsg.Font.Bold = true;
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

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            DataTable dtview = new DataTable();
            dtview = (DataTable)ViewState["GrdEmplWel"];
            // hdtimer.Value = "0";
            for (int i = int.Parse(hdtimer.Value.ToString()); i < dtview.Rows.Count; i++)
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

                int count = int.Parse(hdtimer.Value.ToString()) + 1;
                //Bind Data to Columns
                BindGridviewData(Name,  Designation,  Department,  Location,  EMail,  Mobile,  ReportingTo,  Qualification,  Experience,  photo, count);
                Timer1.Interval = 15000;
                break;
            }


        }

        protected void ImgbtnITSupport_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"http://itsupport.fujitecindia.com:8080/");
        }

        protected void ImgbtnVendora_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(@"http://scm.fujitecindia.com/buyer/users/login");
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
            Response.Redirect(@"https://www.gmail.com/");
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
            Response.Redirect(@"https://www.hrmantra.com/login.aspx");
        }
    }
}