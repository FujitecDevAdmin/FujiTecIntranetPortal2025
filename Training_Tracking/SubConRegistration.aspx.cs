using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Transactions;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubConRegistration : System.Web.UI.Page
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    ddBranchID.Focus();
                    ViewState["ApproverName"] = "";
                    ViewState["Error"] = string.Empty;
                    // if (Session["TTAPPID"].ToString() != null)
                    if (Request.QueryString.Count > 0)
                    {
                        txtID.Text = Request.QueryString["ID"].ToString();
                        ViewState["ApproverName"] = Request.QueryString["Approver"].ToString();
                        if (txtID.Text.Length > 5)
                            Initialize();
                    }
                    if (txtID.Text.Length > 5)
                    {
                        //if (ViewState["ApproverName"].ToString() == "MSC0042")
                        //{
                        //    //btnSave.Enabled = true;
                        //    btnApprove.Enabled = true;
                        //}
                        //else
                        btnApprove.Enabled = true;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnReject.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (txtID.Text.Length > 5)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                }
                // lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConRegistrationMasterPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Branch--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddBranchID.DataTextField = "BranchName";
                        ddBranchID.DataValueField = "BranchId";
                        ddBranchID.DataSource = ds.Tables[0];
                        ddBranchID.DataBind();

                        //DataRow dr1 = ds.Tables[2].NewRow();
                        //dr1.ItemArray = new object[] { 0, "--Select Role--" };
                        //ds.Tables[2].Rows.InsertAt(dr1, 0);
                        //ddApproverRole.DataTextField = "RoleDesc";
                        //ddApproverRole.DataValueField = "RoleID";
                        //ddApproverRole.DataSource = ds.Tables[2];
                        //ddApproverRole.DataBind();

                        //ddStatus.DataTextField = "StatusDesc";
                        //ddStatus.DataValueField = "StatusCode";
                        //ddStatus.DataSource = ds.Tables[3];
                        //ddStatus.DataBind();
                        //ViewState["GV"] = ds.Tables[0];
                        //gv.DataSource = ds.Tables[0];
                        //gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ViewState["ApproverName"].ToString()))
                {
                    if (ViewState["ApproverName"].ToString() == "MSC0042")
                    {
                        ViewState["Error"] = "";
                        // btnSave.Text = "Update";
                        if ((dd12.SelectedIndex == 0))
                        {

                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* PPE Issued ";
                            ViewState["Error"] = "Error";
                        }
                        if ((dd13.SelectedIndex == 0))
                        {

                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Safety Induction Given ";
                            ViewState["Error"] = "Error";
                        }
                        if ((dd14.SelectedIndex == 0))
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* ID Card Issued ";
                            ViewState["Error"] = "Error";
                        }
                    }
                    else if (ViewState["ApproverName"].ToString() == "MSC0047")
                    {
                        if (!string.IsNullOrEmpty((txtVendorID.Text)))
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Vendor ID from ERP need to update";
                            ViewState["Error"] = "Error";
                        }
                    }
                }
                //using (TransactionScope scope = new TransactionScope())
                //{
                if (ViewState["Error"].ToString() != "Error")
                {
                    string sp = ""; string email = string.Empty;
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd;
                        sp = "SP_TBL_SUBCONREGDET_UPDATE";
                        cmd = new SqlCommand(sp, sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                        //cmd.Parameters.Add("@Companyname", SqlDbType.VarChar).Value = txtSCCompanyName.Text;
                        //cmd.Parameters.Add("@VendorID", SqlDbType.VarChar).Value = string.Empty;
                        //cmd.Parameters.Add("@ChecklistID1", SqlDbType.VarChar).Value = dd1.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID2", SqlDbType.VarChar).Value = dd2.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID3", SqlDbType.VarChar).Value = dd3.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID4", SqlDbType.VarChar).Value = dd4.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID5", SqlDbType.VarChar).Value = dd5.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID6", SqlDbType.VarChar).Value = dd6.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID7", SqlDbType.VarChar).Value = dd7.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID8", SqlDbType.VarChar).Value = dd8.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID9", SqlDbType.VarChar).Value = dd9.SelectedValue;
                        //cmd.Parameters.Add("@ChecklistID10", SqlDbType.VarChar).Value = dd10.SelectedValue;

                        //cmd.Parameters.Add("@ChecklistID11", SqlDbType.VarChar).Value = dd11.SelectedValue;
                        cmd.Parameters.Add("@ChecklistID12", SqlDbType.VarChar).Value = dd12.SelectedValue;
                        cmd.Parameters.Add("@ChecklistID13", SqlDbType.VarChar).Value = dd13.SelectedValue;
                        cmd.Parameters.Add("@ChecklistID14", SqlDbType.VarChar).Value = dd14.SelectedValue;

                        cmd.Parameters.Add("@Remark1", SqlDbType.VarChar).Value = txt1.Text;
                        cmd.Parameters.Add("@Remark2", SqlDbType.VarChar).Value = txt2.Text;
                        cmd.Parameters.Add("@Remark3", SqlDbType.VarChar).Value = txt3.Text;
                        cmd.Parameters.Add("@Remark4", SqlDbType.VarChar).Value = txt4.Text;
                        cmd.Parameters.Add("@Remark5", SqlDbType.VarChar).Value = txt5.Text;

                        cmd.Parameters.Add("@Remark6", SqlDbType.VarChar).Value = txt6.Text;
                        cmd.Parameters.Add("@Remark7", SqlDbType.VarChar).Value = txt7.Text;
                        cmd.Parameters.Add("@Remark8", SqlDbType.VarChar).Value = txt8.Text;
                        cmd.Parameters.Add("@Remark9", SqlDbType.VarChar).Value = txt9.Text;
                        cmd.Parameters.Add("@Remark10", SqlDbType.VarChar).Value = txt10.Text;

                        cmd.Parameters.Add("@Remark11", SqlDbType.VarChar).Value = txt11.Text;
                        cmd.Parameters.Add("@Remark12", SqlDbType.VarChar).Value = txt12.Text;
                        cmd.Parameters.Add("@Remark13", SqlDbType.VarChar).Value = txt13.Text;
                        cmd.Parameters.Add("@Remark14", SqlDbType.VarChar).Value = txt14.Text;

                        cmd.Parameters.Add("@vendorId", SqlDbType.VarChar).Value = txtVendorID.Text;
                        //cmd.Parameters.Add("@Modifiedon", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                ViewState["TrainingID"] = ds.Tables[0].Rows[0]["TM"].ToString();
                                txtID.Text = ds.Tables[0].Rows[0]["TM"].ToString();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Sub Con Registration Updated Successfully";
                                btnUpdate.Enabled = false;
                            }
                        }
                    }
                }
                //    scope.Complete();
                //    //SendEmail(email);
                //}
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();

                if (ViewState["Error"].ToString() != "Error")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string sp = ""; string email = string.Empty;
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            // string Update = "";// ViewState["Update"] as string;
                            SqlCommand cmd;
                            //if (btnSave.Text == "Update")
                            //{
                            //    sp = "SP_TBL_SUBCONREG_HDR_UPDATE";
                            //    cmd = new SqlCommand(sp, sqlConnection);
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                            //    //cmd.Parameters.Add("@Companyname", SqlDbType.VarChar).Value = txtSCCompanyName.Text;
                            //    //cmd.Parameters.Add("@VendorID", SqlDbType.VarChar).Value = string.Empty;
                            //    //cmd.Parameters.Add("@ChecklistID1", SqlDbType.VarChar).Value = dd1.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID2", SqlDbType.VarChar).Value = dd2.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID3", SqlDbType.VarChar).Value = dd3.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID4", SqlDbType.VarChar).Value = dd4.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID5", SqlDbType.VarChar).Value = dd5.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID6", SqlDbType.VarChar).Value = dd6.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID7", SqlDbType.VarChar).Value = dd7.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID8", SqlDbType.VarChar).Value = dd8.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID9", SqlDbType.VarChar).Value = dd9.SelectedValue;
                            //    //cmd.Parameters.Add("@ChecklistID10", SqlDbType.VarChar).Value = dd10.SelectedValue;

                            //    //cmd.Parameters.Add("@ChecklistID11", SqlDbType.VarChar).Value = dd11.SelectedValue;
                            //    cmd.Parameters.Add("@ChecklistID12", SqlDbType.VarChar).Value = dd12.SelectedValue;
                            //    cmd.Parameters.Add("@ChecklistID13", SqlDbType.VarChar).Value = dd13.SelectedValue;
                            //    cmd.Parameters.Add("@ChecklistID14", SqlDbType.VarChar).Value = dd14.SelectedValue;

                            //    //cmd.Parameters.Add("@Remark1", SqlDbType.VarChar).Value = txt1.Text;
                            //    //cmd.Parameters.Add("@Remark2", SqlDbType.VarChar).Value = txt2.Text;
                            //    //cmd.Parameters.Add("@Remark3", SqlDbType.VarChar).Value = txt3.Text;
                            //    //cmd.Parameters.Add("@Remark4", SqlDbType.VarChar).Value = txt4.Text;
                            //    //cmd.Parameters.Add("@Remark5", SqlDbType.VarChar).Value = txt5.Text;

                            //    //cmd.Parameters.Add("@Remark6", SqlDbType.VarChar).Value = txt6.Text;
                            //    //cmd.Parameters.Add("@Remark7", SqlDbType.VarChar).Value = txt7.Text;
                            //    //cmd.Parameters.Add("@Remark8", SqlDbType.VarChar).Value = txt8.Text;
                            //    //cmd.Parameters.Add("@Remark9", SqlDbType.VarChar).Value = txt9.Text;
                            //    //cmd.Parameters.Add("@Remark10", SqlDbType.VarChar).Value = txt10.Text;

                            //    //cmd.Parameters.Add("@Remark11", SqlDbType.VarChar).Value = txt11.Text;
                            //    cmd.Parameters.Add("@Remark12", SqlDbType.VarChar).Value = txt12.Text;
                            //    cmd.Parameters.Add("@Remark13", SqlDbType.VarChar).Value = txt13.Text;
                            //    cmd.Parameters.Add("@Remark14", SqlDbType.VarChar).Value = txt14.Text;

                            //    //cmd.Parameters.Add("@Createdon", SqlDbType.DateTime).Value = DateTime.Now;
                            //    //cmd.Parameters.Add("@Modifiedon", SqlDbType.DateTime).Value = DateTime.Now;
                            //    cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            //}
                            //else
                            //{
                            sp = "SP_TBL_SUBCONREG_HDR_INSERT";

                            cmd = new SqlCommand(sp, sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = ddBranchID.SelectedValue;
                            cmd.Parameters.Add("@Companyname", SqlDbType.VarChar).Value = txtSCCompanyName.Text;
                            cmd.Parameters.Add("@VendorID", SqlDbType.VarChar).Value = string.Empty;
                            cmd.Parameters.Add("@ChecklistID1", SqlDbType.VarChar).Value = dd1.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID2", SqlDbType.VarChar).Value = dd2.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID3", SqlDbType.VarChar).Value = dd3.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID4", SqlDbType.VarChar).Value = dd4.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID5", SqlDbType.VarChar).Value = dd5.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID6", SqlDbType.VarChar).Value = dd6.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID7", SqlDbType.VarChar).Value = dd7.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID8", SqlDbType.VarChar).Value = dd8.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID9", SqlDbType.VarChar).Value = dd9.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID10", SqlDbType.VarChar).Value = dd10.SelectedValue;

                            cmd.Parameters.Add("@ChecklistID11", SqlDbType.VarChar).Value = dd11.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID12", SqlDbType.VarChar).Value = "-1";//dd12.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID13", SqlDbType.VarChar).Value = "-1"; //dd13.SelectedValue;
                            cmd.Parameters.Add("@ChecklistID14", SqlDbType.VarChar).Value = "-1";// dd14.SelectedValue;

                            cmd.Parameters.Add("@Remark1", SqlDbType.VarChar).Value = txt1.Text;
                            cmd.Parameters.Add("@Remark2", SqlDbType.VarChar).Value = txt2.Text;
                            cmd.Parameters.Add("@Remark3", SqlDbType.VarChar).Value = txt3.Text;
                            cmd.Parameters.Add("@Remark4", SqlDbType.VarChar).Value = txt4.Text;
                            cmd.Parameters.Add("@Remark5", SqlDbType.VarChar).Value = txt5.Text;

                            cmd.Parameters.Add("@Remark6", SqlDbType.VarChar).Value = txt6.Text;
                            cmd.Parameters.Add("@Remark7", SqlDbType.VarChar).Value = txt7.Text;
                            cmd.Parameters.Add("@Remark8", SqlDbType.VarChar).Value = txt8.Text;
                            cmd.Parameters.Add("@Remark9", SqlDbType.VarChar).Value = txt9.Text;
                            cmd.Parameters.Add("@Remark10", SqlDbType.VarChar).Value = txt10.Text;

                            cmd.Parameters.Add("@Remark11", SqlDbType.VarChar).Value = txt11.Text;
                            cmd.Parameters.Add("@Remark12", SqlDbType.VarChar).Value = txt12.Text;
                            cmd.Parameters.Add("@Remark13", SqlDbType.VarChar).Value = txt13.Text;
                            cmd.Parameters.Add("@Remark14", SqlDbType.VarChar).Value = txt14.Text;

                            cmd.Parameters.Add("@Createdon", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@Modifiedon", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            //}
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    ViewState["TrainingID"] = ds.Tables[0].Rows[0]["TM"].ToString();
                                    txtID.Text = ds.Tables[0].Rows[0]["TM"].ToString();
                                }
                            }
                        }


                        //for (int i = 0; i < gv.Rows.Count; i++)
                        //{
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("SP_TBL_SUBCONREG_DET_INSERT", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = ViewState["TrainingID"].ToString();
                            cmd.Parameters.Add("@ApproverID", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = "MSC0003";
                            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            //cmd.Parameters.Add("@createdon", SqlDbType.DateTime).Value = DateTime.Now();
                            //cmd.Parameters.Add("@modifiedon", SqlDbType.DateTime).Value = Session["USERID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = "Sub Con Registration record Created Successfully";
                                    email = ds.Tables[1].Rows[0]["EmailId"].ToString();
                                    btnSave.Enabled = false;

                                }
                            }
                            // scope.Complete();
                        }
                        //btnSave.Enabled = false;
                        // }
                        scope.Complete();
                        SendEmail(email);
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }
        
        //protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        //(DataTable)ViewState["DPT"];
        //        gv.PageIndex = e.NewPageIndex;
        //        gv.DataSource = (DataTable)ViewState["GV"];
        //        gv.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lblmsg.Text = "";
        //        txtRoleID.Text = gv.SelectedRow.Cells[1].Text;
        //        ddBranchID.SelectedValue = gv.SelectedRow.Cells[2].Text;
        //        txtApproverID.Text = gv.SelectedRow.Cells[3].Text;
        //        ddApproverRole.SelectedValue = gv.SelectedRow.Cells[4].Text;
        //        ////  ddContractorType.SelectedValue = gddBranchID.SelectedValue = gv.SelectedRow.Cells[2].Text;v.SelectedRow.Cells[3].Text;
        //        //txtContact.Text = gv.SelectedRow.Cells[3].Text;
        //        //txtContactDesignation.Text = gv.SelectedRow.Cells[4].Text;
        //        //txtAddress.Text = gv.SelectedRow.Cells[5].Text.Trim();
        //        //txtEmailId.Text = gv.SelectedRow.Cells[6].Text.Trim();
        //        //txtMobileNo.Text = gv.SelectedRow.Cells[7].Text.Trim();
        //        //txtGSTNo.Text = gv.SelectedRow.Cells[8].Text.Trim();
        //        //txtPANNo.Text = gv.SelectedRow.Cells[9].Text.Trim();
        //        //txtOwnername.Text = gv.SelectedRow.Cells[10].Text.Trim();
        //        //txtInsurancePolicyNo.Text = gv.SelectedRow.Cells[11].Text;
        //        if ((gv.SelectedRow.Cells[5].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[5].Text.Trim() == "MSC0002"))
        //            ddStatus.SelectedValue = gv.SelectedRow.Cells[5].Text;
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        private void SendMail1(string EmailTo, string Status)
        {
            string username = Session["USERNAME"] as string;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString(), "Vendor " + Status.ToString());
            string[] Tomaillist = EmailTo.Split(new char[] { ';' });
            if (Tomaillist.Length > 0)
            {
                foreach (string tomail in Tomaillist)
                {
                    message.To.Add(new MailAddress(tomail.ToString()));
                }
            }
            else
            {
                string emailId = ConfigurationManager.AppSettings["EmailToSafety"].ToString();
                message.To.Add(new MailAddress(emailId.ToString()));
            }
            message.Subject = "Vendor:" + txtSCCompanyName.Text + "--" + txtID.Text + "--" + Status + "  successfully";
            message.IsBodyHtml = true; //to make message body as html  

            /////////////////////////////////
            string Body = "Dear Sir , <br /><br />";
            Body = Body + "We are pleased to inform you that, '" + txtSCCompanyName.Text + "' </b> has been '" + Status + "' as a Fujitec Vendor. <br /><br />";
            Body = Body + "Please click the link below,<br />";
            Body = Body + "URL :- http://confluence:8085/ <br /><br />";
            Body = Body + "<b>Regards,</b><br /> <b>" + username + " </b><br /><b>Fujitec India Pvt. Ltd.</b><br />";
            message.Body = Body;
            smtp.Port = 587;
            smtp.Host = "smtp.office365.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"].ToString(), ConfigurationManager.AppSettings["EmailPwd"].ToString());
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public void SendEmail(string EmailTo)
        {
            string username = Session["USERNAME"] as string;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString(), "Vendor Approval");
            message.To.Add(new MailAddress(EmailTo.ToString()));
            message.Subject = "Approval request for Vendor:" + txtSCCompanyName.Text + "--" + txtID.Text;
            message.IsBodyHtml = true; //to make message body as html  

            /////////////////////////////////
            string Body = "Dear Sir , <br /><br />";
            Body = Body + "This is to inform you that, approval required for SubContractor :  '" + txtSCCompanyName.Text + "' </b> , details are registered in our confluence portal.<br /><br /> Please Approve/Reject to proceed on this SubContractor.<br /><br />";

            Body = Body + "Please click the link below,<br />";
            Body = Body + "URL :- http://confluence:8085/ <br /><br />";
            Body = Body + "<b>Regards,</b><br /> <b>" + username + " </b><br /><b>Fujitec India Pvt. Ltd.</b><br />";
            message.Body = Body;
            smtp.Port = 587;
            smtp.Host = "smtp.office365.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"].ToString(), ConfigurationManager.AppSettings["EmailPwd"].ToString());
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                //if ((txtSCCompanyName.Text == string.Empty))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Company Name should not be empty ";
                //    ViewState["Error"] = "Error";
                //}

                //if ((ddBranchID.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Branch ID should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((ddBranchID.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Branch Name should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd1.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Subcontractor Prequalication Form ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd2.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Subcontractor Registration  Form ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd3.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Agreement of Subcontractor ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd4.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Pan Card Copy ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd5.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Bank Details or Cancelled Cheque ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd6.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Self Declaration Or GST ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd7.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Bio Data ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd8.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Appointment Letter ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd9.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* ESI Form ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd10.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* PF Form ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd11.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Nagarik Suraksha Form ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd12.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* PPE Issued ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd13.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Safety Induction Given ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((dd14.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* ID Card Issued ";
                //    ViewState["Error"] = "Error";
                //}

            }
            catch (Exception ex)
            {

            }
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                FetchInitializeDetails();
                btnSave.Enabled = true;
                btnUpdate.Enabled = true;
                txtID.Text = string.Empty;
                txtSCCompanyName.Text = string.Empty;
                //  txtVendorID.Text = string.Empty;
                dd1.SelectedIndex = 0;
                dd2.SelectedIndex = 0;
                dd3.SelectedIndex = 0;
                dd4.SelectedIndex = 0;
                dd5.SelectedIndex = 0;
                dd6.SelectedIndex = 0;
                dd7.SelectedIndex = 0;
                dd8.SelectedIndex = 0;
                dd9.SelectedIndex = 0;
                dd10.SelectedIndex = 0;
                dd11.SelectedIndex = 0;
                dd12.SelectedIndex = 0;
                dd13.SelectedIndex = 0;
                dd14.SelectedIndex = 0;
                txt1.Text = string.Empty;
                txt2.Text = string.Empty;
                txt3.Text = string.Empty;
                txt4.Text = string.Empty;
                txt5.Text = string.Empty;
                txt6.Text = string.Empty;
                txt7.Text = string.Empty;
                txt8.Text = string.Empty;
                txt9.Text = string.Empty;
                txt10.Text = string.Empty;
                txt11.Text = string.Empty;
                txt12.Text = string.Empty;
                txt13.Text = string.Empty;
                txt14.Text = string.Empty;
            }
            catch (Exception ex)
            {

            }
        }

        public void Initialize()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConRegApprvlLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = txtID.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ddBranchID.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString();
                        txtSCCompanyName.Text = ds.Tables[0].Rows[0]["Companyname"].ToString();
                        dd1.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID1"].ToString();
                        dd2.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID2"].ToString();
                        dd3.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID3"].ToString();
                        dd4.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID4"].ToString();
                        dd5.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID5"].ToString();
                        dd6.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID6"].ToString();
                        dd7.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID7"].ToString();
                        dd8.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID8"].ToString();
                        dd9.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID9"].ToString();
                        dd10.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID10"].ToString();
                        dd11.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID11"].ToString();
                        dd12.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID12"].ToString();
                        dd13.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID13"].ToString();
                        dd14.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID14"].ToString();
                        txt1.Text = ds.Tables[0].Rows[0]["Remark1"].ToString();
                        txt2.Text = ds.Tables[0].Rows[0]["Remark2"].ToString();
                        txt3.Text = ds.Tables[0].Rows[0]["Remark3"].ToString();
                        txt4.Text = ds.Tables[0].Rows[0]["Remark4"].ToString();
                        txt5.Text = ds.Tables[0].Rows[0]["Remark5"].ToString();
                        txt6.Text = ds.Tables[0].Rows[0]["Remark6"].ToString();
                        txt7.Text = ds.Tables[0].Rows[0]["Remark7"].ToString();
                        txt8.Text = ds.Tables[0].Rows[0]["Remark8"].ToString();
                        txt9.Text = ds.Tables[0].Rows[0]["Remark9"].ToString();
                        txt10.Text = ds.Tables[0].Rows[0]["Remark10"].ToString();
                        txt11.Text = ds.Tables[0].Rows[0]["Remark11"].ToString();
                        txt12.Text = ds.Tables[0].Rows[0]["Remark12"].ToString();
                        txt13.Text = ds.Tables[0].Rows[0]["Remark13"].ToString();
                        txt14.Text = ds.Tables[0].Rows[0]["Remark14"].ToString();

                        if (ds.Tables[1].Rows.Count > 0)
                        {

                            int i = 0;
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                if ("MSC0040" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl1.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0041" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl2.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0042" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl3.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0043" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl4.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0044" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl5.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0045" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl6.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0046" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl7.BackColor = System.Drawing.Color.LightGreen;
                                }
                                else if ("MSC0047" == ds.Tables[1].Rows[i]["ApproverName"].ToString())
                                {
                                    txtlvl8.BackColor = System.Drawing.Color.LightGreen;
                                }
                                i++;
                            }
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                string email = string.Empty;
                string status = string.Empty;
                ViewState["Error"] = "";
                if (!string.IsNullOrEmpty(ViewState["ApproverName"].ToString()))
                {
                    if (ViewState["ApproverName"].ToString() == "MSC0042")
                    {
                        if ((dd12.SelectedIndex == 0))
                        {

                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* PPE Issued ";
                            ViewState["Error"] = "Error";
                        }
                        if ((dd13.SelectedIndex == 0))
                        {

                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Safety Induction Given ";
                            ViewState["Error"] = "Error";
                        }
                        if ((dd14.SelectedIndex == 0))
                        {

                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = lblmsg.Text + Environment.NewLine + "* ID Card Issued ";
                            ViewState["Error"] = "Error";
                        }
                    }
                }
                if (ViewState["Error"].ToString() != "Error")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string sp = "";

                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {

                            SqlCommand cmd = new SqlCommand("SP_TBL_SUBCONREG_APPROVALDET_INSERT", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = txtID.Text;
                            cmd.Parameters.Add("@ApproverID", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = "MSC0003";
                            cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = "Requesting to proceed for vendor approval";
                                    //email = ds.Tables[1].Rows[0]["EmailId"].ToString();
                                    int i = 0;
                                    foreach (DataRow row in ds.Tables[1].Rows)
                                    {
                                        if (i == 0)
                                            email = ds.Tables[1].Rows[0]["EmailId"].ToString();
                                        else
                                            email = email + ";" + row["EmailId"].ToString();

                                        i++;
                                    }
                                    status = ds.Tables[1].Rows[0]["status"].ToString();
                                }
                            }
                            // scope.Complete();
                        }
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                        // }
                        scope.Complete();
                        if (status == "Approved")
                            SendMail1(email, "Approved");
                        else
                            SendEmail(email);
                    }
                }

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
                string email = string.Empty;
                string status = string.Empty;
                using (TransactionScope scope = new TransactionScope())
                {
                    string sp = "";

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {

                        SqlCommand cmd = new SqlCommand("SP_TBL_SUBCONREG_REGDET_INSERT", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = txtID.Text;
                        cmd.Parameters.Add("@ApproverID", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = "MSC0004";
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Rejected Successfully";
                                //email = ds.Tables[1].Rows[0]["EmailId"].ToString();
                                int i = 0;
                                foreach (DataRow row in ds.Tables[1].Rows)
                                {
                                    if (i == 0)
                                        email = ds.Tables[1].Rows[0]["EmailId"].ToString();
                                    else
                                        email = email + ";" + row["EmailId"].ToString();

                                    i++;
                                }
                                status = ds.Tables[1].Rows[0]["status"].ToString();
                            }
                        }
                        // scope.Complete();
                    }
                    btnReject.Enabled = false;
                    btnApprove.Enabled = false;
                    // }
                    scope.Complete();
                    if (status == "Rejected")
                        SendMail1(email, "Rejected");
                    //else
                    //    SendEmail(email);
                }


            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConRegApprvlLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = ViewState["TrainingID"].ToString();
                    cmd.Parameters.Add("@ApproverID", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ddBranchID.SelectedValue = ds.Tables[0].Rows[0]["BranchID"].ToString();
                        txtSCCompanyName.Text = ds.Tables[0].Rows[0]["Companyname"].ToString();
                        dd1.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID1"].ToString();
                        dd2.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID2"].ToString();
                        dd3.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID3"].ToString();
                        dd4.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID4"].ToString();
                        dd5.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID5"].ToString();
                        dd6.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID6"].ToString();
                        dd7.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID7"].ToString();
                        dd8.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID8"].ToString();
                        dd9.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID9"].ToString();
                        dd10.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID10"].ToString();
                        dd11.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID11"].ToString();
                        dd12.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID12"].ToString();
                        dd13.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID13"].ToString();
                        dd14.SelectedValue = ds.Tables[0].Rows[0]["ChecklistID14"].ToString();

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