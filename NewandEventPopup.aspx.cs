using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class NewandEventPopup : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // img_Empphoto.ImageUrl = @"~\NewsEvents\FUJI\BRB.jpg";
            try
            {
                if (!IsPostBack)
                {
                    string ID = Request.QueryString["ID"].ToString();
                    string ApprovalFor = Request.QueryString["ApprovalFor"].ToString();
                    string Filepath = Request.QueryString["Filepath"];
                    txtEventName.Text = Request.QueryString["Eventname"].ToString();

                   // img_Empphoto.ImageUrl = @"~/NewsEvents/FUJI/" + txtEventName.Text + ".jpg";
                    img_Empphoto.ImageUrl = Filepath;
                    // PopupPageload(ApprovalFor, ID);
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
                ApproveNewsEvents(Request.QueryString["ApprovalFor"].ToString(), Request.QueryString["ID"].ToString(), "MSC0003");
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
                lblmsg.ForeColor = System.Drawing.Color.Red;
                if (txtRemarks.Text.Trim() != "")
                    ApproveNewsEvents(Request.QueryString["ApprovalFor"].ToString(), Request.QueryString["ID"].ToString(), "MSC0004");
                else
                    lblmsg.Text = "Please enter remark for Reject status.";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Write("<script>window.close();</" + "script>");
                //Response.End();
                Response.Redirect("~/ApprovalScreen.aspx");
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        private void PopupPageload(string ApprovalFor, string ID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_POPUPPAGELOAD", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                    cmd.Parameters.Add("@ApprovalFor", SqlDbType.VarChar).Value = ApprovalFor;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        txtEventName.Text = ds.Tables[0].Rows[0]["EventName"].ToString();
                        img_Empphoto.ImageUrl = @"~/NewsEvents/FUJI/" + txtEventName.Text + ".jpg";
                        //ds.Tables[0].Rows[0]["FileNamepath"].ToString() + txtEventName.Text + ".jpg";
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }
        private void ApproveNewsEvents(string ApprovalFor, string ID, string ApprovalStatus)
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
                        }
                        else
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Rejected Successfully";
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }
    }
}