using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class ApprovalScreen : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  SetInitialRow();
                    initialize();
                    // hdfilepath.Value = "no";
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        public void initialize()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_ApprovalPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["DGVAvailability"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();

                        //GridView1.DataSource = ds.Tables[0];
                        //GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void ApproveNewsEvents(string ApprovalFor, string ID, string ApprovalStatus,string Eventname)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_Approval", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ApprovalFor", SqlDbType.VarChar).Value = ApprovalFor;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                    cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = ApprovalStatus;
                    cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarks.Text;
                    cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"] as string;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ApprovalStatus == "MSC0003")
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Approved Successfully";
                            //SendEmail("Approved", Eventname);
                        }
                        else
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Rejected Successfully";
                            //SendEmail("Rejected", Eventname);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }

        public void SendEmail(string Eventstatus,string EventName)
        {
            string username = Session["USERNAME"] as string;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
            message.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailTo"].ToString()));
            message.Subject = Eventstatus + " request for " + EventName.Trim() + "in confluence" ;
            message.IsBodyHtml = true; //to make message body as html  

            /////////////////////////////////
            string Body = "Dear Sir , <br /><br />";
            Body = Body + Eventstatus + " request for  < b> New and Events  :'" + EventName + "' </b>  details is registered in our confluence site.<br />";

            Body = Body + "Please click the link below,<br />";
            Body = Body + "URL :- http://confluence:8085/ <br /><br />";
            Body = Body + "<b>Regards,</b><br /> <b>" + username + " </b><br /><b>Fujitec India Pvt. Ltd.</b><br />";
            /////////////////////////////////            
            message.Body = Body;
            smtp.Port = 587;
            smtp.Host = "smtp.office365.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"].ToString(), ConfigurationManager.AppSettings["EmailPwd"].ToString());
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = (int)ViewState["rowindex"];

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DGVAvailability"];
                string ID = dt.Rows[i]["ID"].ToString();
                string ApprovalFor = dt.Rows[i]["NEWSANDEVENTS"].ToString();
                string Filepath = dt.Rows[i]["FileNamepath"].ToString();
                string Eventname = dt.Rows[i]["EVENTNAME"].ToString();
                //ViewState["statusevent"]
                popup(ID, ApprovalFor, Filepath, Eventname);

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void GV_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // if (e.CommandName != "SendMail") return;
            // int id = Convert.ToInt32(e.CommandArgument);
            // do something

            //   if(e.com)
        }

        protected void gv_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ViewState["rowindex"] = Convert.ToInt32(e.CommandArgument);
                }
            }
            catch(Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void ToggleCheckState(bool checkState)
        {
            // Iterate through the Products.Rows property
            try
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    // Access the CheckBox
                    CheckBox cb = (CheckBox)row.FindControl("Chckselect");
                    if (cb != null)
                        cb.Checked = checkState;
                }
            }
            catch(Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleCheckState(true);
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }

        private void popup(string ID, string ApprovalFor, string Filepath, string Eventname)
        {
            try
            {
                if (ApprovalFor == "APPROVAL FOR NEWS AND EVENTS")
                {
                    string url = "NewandEventPopup.aspx?ID=" + ID + "&ApprovalFor=" + ApprovalFor + "&Eventname=" + Eventname.TrimEnd().Trim() + "&Filepath=" + Filepath;
                    // string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=300,top=100,resizable=yes');";
                    // ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                      Response.Redirect("~/" + url);
                    //Response.Redirect("~/NewandEventPopup.aspx?ID=27&ApprovalFor=APPROVAL FOR NEWS AND EVENTS&Eventname=Speech by MD's and Mr. M.Balasubramanian - Director HR in Installation Process & Orientation Training Program &Filepath=~\\NewsEvents\\FUJI\\20220620083806.jpg");
                }
                else if (ApprovalFor == "APPROVAL FOR EMPLOYEE")
                {
                    string url = "EmployeeRegistrationPopup.aspx?ID=" + ID + "&ApprovalFor=" + ApprovalFor + "&Eventname=" + Eventname + "&Filepath=" + Filepath;
                    // string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=300,top=100,resizable=yes');";
                    //  ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    Response.Redirect("~/" + url);
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnUnCheckAll_Click(object sender, EventArgs e)
        {
            ToggleCheckState(false);
        }
        private void ApproveReject(string ApprovalStatus)
        {
            // Iterate through the Products.Rows property
            try
            {
                int i = 0; bool selflag = false;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DGVAvailability"];
                foreach (GridViewRow row in gv.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkRow = (row.Cells[4].FindControl("Chckselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string ID = dt.Rows[i]["ID"].ToString();
                            if (ID.Length < 5)
                                ID = "0" + ID;
                            string ApprovalFor = dt.Rows[i]["NEWSANDEVENTS"].ToString();
                            //string country = (row.Cells[2].FindControl("lblCountry") as Label).Text;
                            ApproveNewsEvents(ApprovalFor, ID, ApprovalStatus, dt.Rows[i]["EVENTNAME"].ToString());
                            //initialize();
                            selflag = true;
                        }
                        i++;
                    }
                }
                initialize();
                if (selflag == false)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select any record to Approve or Reject";
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                ApproveReject("MSC0003");
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRemarks.Text.Trim() != "")
                    ApproveReject("MSC0004");
                else
                    lblmsg.Text = "Please enter remark for Reject status.";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                initialize();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
    }
}