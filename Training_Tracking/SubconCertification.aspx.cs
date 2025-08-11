using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ClosedXML.Excel;
using System.Net.NetworkInformation;
using DocumentFormat.OpenXml.Bibliography;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using static System.Net.WebRequestMethods;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;
using System.Net.Mime;
using System.Web.Services.Description;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubconCertification : System.Web.UI.Page
    {
        public string strScaffLoc = "", strScaffDept = "", strScaffTrainedby = "", strScaffCertifiedby = "", strScaffEmpname = "", strScaffIDcardno = "", strScaffCertification = "", strScaffTraining = "", StrScaffTrainingDate = "", StrScaffCDate = "", StrScaffstatus = "";
        public string strHostingLoc = "", strHostingdept = "", strHostingTrainedby = "", strHostingCertifiedby = "", strHostingEmpname = "", strHostingIDcardno = "", strHostingCertification = "", strHostingTraining = "", strHostingTrainingDate = "", strHostingCDate = "", strHostingstatus = "";
        public string strCarTopLoc = "", strCarTopdept = "", strCarTopTrainedby = "", strCarTopCertifiedby = "", strCarTopEmpname, strCarTopIDcardno = "", strCarTopCertification = "", strCarTopTraining = "", strCarTopTrainingDate = "", strCarTopCDate = "", strCarTopstatus = "";
        public string strPitEntryLoc = "", strPitEntryTopdept = "", strPitEntryTopTrainedby = "", strPitEntryTopCertifiedby = "", strPitEntryTopEmpname = "", strPitEntryTopIDcardno = "", strPitEntryTopCertification = "", strPitEntryTopTraining = "", strPitEntryTopTrainingDate = "", strPitEntryTopCDate = "", strPitEntrystatus = "";
        public string strLotoLoc = "", strLotodept = "", strLotoTrainedby = "", strLotoCertifiedby = "", strLotoTopEmpname = "", strLotoIDcardno = "", strLotoCertification = "", strLotoTraining = "", strLotoTrainingDate = "", strLotoCDate = "", strLotostatus = "";
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnPreview.Enabled = false;
                    FetchInitializeDetails();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }
        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConCertificateMasterPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select MainContractor--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddMainSubConName.DataTextField = "VendorName";
                        ddMainSubConName.DataValueField = "ID";
                        ddMainSubConName.DataSource = ds.Tables[0];
                        ddMainSubConName.DataBind();
                        ViewState["VendorE"] = ds.Tables[0];

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select SubCon-Employee--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddEmployeeName.DataTextField = "Emp_Name";
                        ddEmployeeName.DataValueField = "Emp_Id";
                        ddEmployeeName.DataSource = ds.Tables[1];
                        ddEmployeeName.DataBind();

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select SubCon-Certification--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddCertification.DataTextField = "TRAININGNAME";
                        ddCertification.DataValueField = "TRAININGID";
                        ddCertification.DataSource = ds.Tables[2];
                        ddCertification.DataBind();

                        DataRow dr3 = ds.Tables[3].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select SubCon-Training--" };
                        ds.Tables[3].Rows.InsertAt(dr3, 0);
                        ddTraining.DataTextField = "Stext";
                        ddTraining.DataValueField = "ID";
                        ddTraining.DataSource = ds.Tables[3];
                        ddTraining.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;


                if ((ddMainSubConName.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Sub-Contractor Name";
                    ViewState["Error"] = "Error";
                }
                if ((ddEmployeeName.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Employee Name";
                    ViewState["Error"] = "Error";
                }
                if ((txtIDCardNo.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Id card should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if (txtCertifiedBy.Text == string.Empty)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* CertifiedBy should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if (txtTrainedBy.Text == string.Empty)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* TrainedBy should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if (ddCertification.SelectedIndex == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select certification ";
                    ViewState["Error"] = "Error";
                }
                if (ddTraining.SelectedIndex == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* The training for the certification has not been completed yet.";
                    ViewState["Error"] = "Error";
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //lblmsg.Text = "0";
                Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                if (validt != "Error")
                {
                    //if(btn)
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SubconCertificationDetail_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (btnSave.Text == "Save")
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
                        else
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ViewState["ID"].ToString());
                        cmd.Parameters.Add("@SubconCompany", SqlDbType.VarChar).Value = (ddMainSubConName.SelectedValue);
                        cmd.Parameters.Add("@subconEmployeename", SqlDbType.VarChar).Value = ddEmployeeName.SelectedValue;
                        cmd.Parameters.Add("@IDCardNo", SqlDbType.VarChar).Value = txtIDCardNo.Text;
                        cmd.Parameters.Add("@Trainedby", SqlDbType.VarChar).Value = txtTrainedBy.Text;// dtpDate.Text;
                        cmd.Parameters.Add("@Certifiedby", SqlDbType.VarChar).Value = txtCertifiedBy.Text;
                        cmd.Parameters.Add("@Training", SqlDbType.VarChar).Value = ddTraining.SelectedValue;
                        cmd.Parameters.Add("@TrainingDate", SqlDbType.Date).Value = txtTrainingdate.Text;
                        cmd.Parameters.Add("@certification", SqlDbType.VarChar).Value = ddCertification.SelectedValue;
                        cmd.Parameters.Add("@certificationdate", SqlDbType.Date).Value = DateTime.Now.Date.ToString();
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  

                        cmd.Parameters.Add("@Certstatus", SqlDbType.VarChar).Value = ddCertstatus.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                //                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = " Created successfully";
                                ViewState["CurrentTable"] = ds.Tables[1];
                                gv.DataSource = ds.Tables[1];
                                gv.DataBind();
                                //Need to add function to get training details
                                getval();
                                sendmail();
                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                btnSave.Text = "Save";
                                //FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Update  successfully";
                                ViewState["CurrentTable"] = ds.Tables[1];
                                gv.DataSource = ds.Tables[1];
                                gv.DataBind();
                                //Need to add function to get training details
                                getval();
                                sendmail();
                            }
                            //            else //if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                            //            {
                            //                lblmsg.ForeColor = System.Drawing.Color.Red;
                            //                lblmsg.Text = "Error in registration";
                            //            }

                            //            //SendEmail();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                btnMail.Enabled = true;
                btnSave.Enabled = true;
                ddMainSubConName.SelectedIndex = 0;
                ddEmployeeName.SelectedIndex = 0;
                ddCertification.SelectedIndex = 0;
                txtIDCardNo.Text = string.Empty;
                txtCertifiedBy.Text = string.Empty;
                txtTrainedBy.Text = string.Empty;
                txtTrainingdate.Text = string.Empty;
                ddTraining.SelectedIndex = 0;
                txtTrainingdate.Text = string.Empty;
                gv.DataSource = null;
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                ViewState["ID"] = gv.SelectedRow.Cells[1].Text;
                ddMainSubConName.SelectedValue = gv.SelectedRow.Cells[2].Text;
                ddEmployeeName.SelectedValue = gv.SelectedRow.Cells[3].Text;
                txtIDCardNo.Text = gv.SelectedRow.Cells[5].Text;
                txtTrainedBy.Text = gv.SelectedRow.Cells[12].Text;
                txtCertifiedBy.Text = gv.SelectedRow.Cells[13].Text;
                ddCertification.SelectedValue = gv.SelectedRow.Cells[9].Text;
                ddTraining.SelectedValue = gv.SelectedRow.Cells[6].Text;
                txtTrainingdate.Text = gv.SelectedRow.Cells[8].Text;

                //  lblmsg.Text = gv.SelectedRow.Cells[16].Text;
                ddCertstatus.SelectedValue = gv.SelectedRow.Cells[16].Text;

            }
            catch (Exception ex)
            {

            }
        }
        public void FetchEmployeedetails(string contractorname)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FetchEmployeeDetails", sqlConnection);
                    cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = contractorname.Trim();
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select SubCon-Employee--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddEmployeeName.DataTextField = "Emp_Name";
                        ddEmployeeName.DataValueField = "Emp_Id";
                        ddEmployeeName.DataSource = ds.Tables[0];
                        ddEmployeeName.DataBind();

                        ViewState["Emp"] = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void ddMainSubConName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchEmployeedetails(ddMainSubConName.SelectedValue);
        }

        public void datediff()
        {
            try
            {
                string trainingdate = txtTrainingdate.Text;
                DateTime datecal = Convert.ToDateTime(trainingdate);
                DateTime currdate = DateTime.Now;
                int totalDays = (int)(currdate - datecal).TotalDays;
                if (totalDays < 30)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Atleast 30 days should be completed for Training and Certification";
                    btnSave.Enabled = false;
                    btnMail.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }
        protected void ddCertification_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dtc = (DataTable)ViewState["Training"];
                ddTraining.SelectedIndex = 0;
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow row in dtc.Rows)
                    {
                        if (ddCertification.SelectedValue == "TCM000023")
                        {
                            if (row["TRAININGID"].ToString() == "TCM000021")
                            {
                                ddTraining.SelectedValue = "TCM000021";
                                txtTrainingdate.Text = row["Actualdate"].ToString();
                                strScaffLoc = row["locationname"].ToString();
                                strScaffDept = row["Department"].ToString();

                            }
                            //else
                            //{
                            //    ddTraining.SelectedIndex = 0;
                            //    txtTrainingdate.Text = "";
                            //}
                        }
                        else if (ddCertification.SelectedValue == "TCM000024")
                        {
                            if (row["TRAININGID"].ToString() == "TCM000010")
                            {
                                ddTraining.SelectedValue = "TCM000010";
                                txtTrainingdate.Text = row["Actualdate"].ToString();
                                strHostingLoc = row["locationname"].ToString();
                                strHostingdept = row["Department"].ToString();
                                datediff();
                            }
                            // datediff();
                            //if(txtTrainingdate.Text)
                            //else
                            //{
                            //    ddTraining.SelectedIndex = 0;
                            //    txtTrainingdate.Text = "";
                            //}
                        }
                        else if (ddCertification.SelectedValue == "TCM000022")
                        {
                            if (row["TRAININGID"].ToString() == "TCM000030")
                            {
                                ddTraining.SelectedValue = "TCM000030";
                                txtTrainingdate.Text = row["Actualdate"].ToString();
                                strCarTopLoc = row["locationname"].ToString();
                                strCarTopdept = row["Department"].ToString();
                                datediff();
                            }

                            //else
                            //{
                            //    ddTraining.SelectedIndex = 0;
                            //    txtTrainingdate.Text = "";
                            //}
                        }
                        else if (ddCertification.SelectedValue == "TCM000026")
                        {
                            if (row["TRAININGID"].ToString() == "TCM000029")
                            {
                                ddTraining.SelectedValue = "TCM000029";
                                txtTrainingdate.Text = row["Actualdate"].ToString();
                                strPitEntryLoc = row["locationname"].ToString();
                                strPitEntryTopdept = row["Department"].ToString();
                                datediff();
                            }

                            //else
                            //{
                            //    ddTraining.SelectedIndex = 0;
                            //    txtTrainingdate.Text = "";
                            //}
                        }
                        else if (ddCertification.SelectedValue == "TCM000027")
                        {
                            if (row["TRAININGID"].ToString() == "TCM000028")
                            {
                                ddTraining.SelectedValue = "TCM000028";
                                txtTrainingdate.Text = row["Actualdate"].ToString();
                                strLotoLoc = row["locationname"].ToString();
                                strLotodept = row["Department"].ToString();
                                datediff();
                            }
                            //else
                            //{
                            //    ddTraining.SelectedIndex = 0;
                            //    txtTrainingdate.Text = "";
                            //}
                        }
                        //else if (ddCertification.SelectedValue == "TCM00")
                        //{
                        //    ddTraining.SelectedIndex = 0;
                        //}

                    }

                }

                if (ViewState["Dept"].ToString() == "Installation")
                {
                   // lblmsg.Text = string.Empty;
                    if (ddCertification.SelectedValue == "TCM000024")
                    {
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count == 0)
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'Scaffolding entry / exit'";
                            //foreach (DataRow row in dtc.Rows)
                            //{
                            //    if (row["TRAININGID"].ToString() != "TCM000023")
                            //    {
                            //        lblmsg.Text = "Please complete training and certification for 'Scaffolding entry / exit certification'";
                            //    }
                            //}
                        }
                    }
                    else if (ddCertification.SelectedValue == "TCM000022")
                    {
                        bool flg = false;
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                if ((row["certification"].ToString() == "TCM000024"))
                                {
                                    flg = true;
                                    datediff();
                                }

                            }
                            if (flg == false)
                            {
                                {
                                    ddCertification.SelectedIndex = 0;
                                    ddTraining.SelectedIndex = 0;
                                    txtTrainingdate.Text = "";
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Please complete training and certification for 'Hoisting'";
                                }
                            }
                        }
                        else
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'Scaffolding entry / exit and Hoisting'";
                        }
                    }
                    else if (ddCertification.SelectedValue == "TCM000026")
                    {
                        bool flg = false;
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                if ((row["certification"].ToString() == "TCM000022"))
                                {
                                    flg = true;
                                    datediff();
                                }

                            }
                            if (flg == false)
                            {
                                {
                                    ddCertification.SelectedIndex = 0;
                                    ddTraining.SelectedIndex = 0;
                                    txtTrainingdate.Text = "";
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Please complete training and certification for 'Car Top Entry and Exit'";
                                }
                            }
                        }
                        else
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'Scaffolding entry / exit and Hoisting'";
                        }
                    }
                    else if (ddCertification.SelectedValue == "TCM000027")
                    {
                        bool flg = false;
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                if ((row["certification"].ToString() == "TCM000026"))
                                {
                                    flg = true;
                                    datediff();
                                }

                            }
                            if (flg == false)
                            {
                                {
                                    ddCertification.SelectedIndex = 0;
                                    ddTraining.SelectedIndex = 0;
                                    txtTrainingdate.Text = "";
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Please complete training and certification for 'PitEntry Exit'";
                                }
                            }
                        }
                        else
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'Scaffolding entry / exit and Hoisting'";
                        }
                    }
                }
                else
                {
                    if (ddCertification.SelectedValue == "TCM000026")
                    {
                        bool flg = false;
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                if ((row["certification"].ToString() == "TCM000022"))
                                {
                                    flg = true;
                                    datediff();
                                }

                            }
                            if (flg == false)
                            {
                                {
                                    ddCertification.SelectedIndex = 0;
                                    ddTraining.SelectedIndex = 0;
                                    txtTrainingdate.Text = "";
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Please complete training and certification for 'Car Top Entry and Exit'";
                                }
                            }
                        }
                        else
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'Car Top Entry and Exit'";
                        }
                    }
                    else if (ddCertification.SelectedValue == "TCM000027")
                    {
                        bool flg = false;
                        DataTable dtv = (DataTable)ViewState["CurrentTable"];
                        if (dtv.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtv.Rows)
                            {
                                if ((row["certification"].ToString() == "TCM000026"))
                                {
                                    flg = true;
                                    datediff();
                                }

                            }
                            if (flg == false)
                            {
                                {
                                    ddCertification.SelectedIndex = 0;
                                    ddTraining.SelectedIndex = 0;
                                    txtTrainingdate.Text = "";
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Please complete training and certification for 'PitEntry Exit'";
                                }
                            }
                        }
                        else
                        {
                            ddCertification.SelectedIndex = 0;
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Please complete training and certification for 'PitEntry Exit'";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //FetchEmployeedetails(ddMainSubConName.SelectedValue);
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ddMainSubConName.SelectedIndex > 0) && (ddEmployeeName.SelectedIndex > 0))
                {
                    //lblmsg.Text = "Getval";
                    getval();
                    //lblmsg.Text = "GetSendmailval";
                    sendmail();
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select the Company name and Employee name";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ddMainSubConName.SelectedIndex > 0) && (ddEmployeeName.SelectedIndex > 0))
                {
                    //getval();
                    //sendmail();

                    //string prev = (string)ViewState["Preview"];
                    //string encodedText = HttpUtility.UrlEncode(prev);

                    string content = (string)ViewState["Preview"];



                    string content1 = "<!DOCTYPE html>";
                    content1 = content1 + "<html lang=\"en\">";
                    content1 = content1 + "<head>";
                    content1 = content1 + "<meta charset=\"UTF-8\">";
                    content1 = content1 + "<title>Identity Card</title>";
                    content1 = content1 + "</head>";
                    content1 = content1 + content;
                    content1 = content1 + "</body>";
                    content1 = content1 + "</html>";

                    System.IO.File.WriteAllText(@"D:\yoursite.htm", content1);
                    StringBuilder html = new StringBuilder();

                }
                else
                {
                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                    //lblmsg.Text = "Please select the Company name and Employee name";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void getval()
        {
            try
            {
                DataTable dtc = (DataTable)ViewState["CurrentTable"];

                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow row in dtc.Rows)
                    {
                        //if (ddCertification.SelectedValue == "TCM000023")
                        //{
                        if (row["TRAININGID"].ToString() == "TCM000021")
                        {
                            ddTraining.SelectedValue = "TCM000021";
                            txtTrainingdate.Text = row["Actualdate"].ToString();
                            strScaffLoc = row["locationname"].ToString();
                            strScaffDept = row["Department"].ToString();
                            strScaffCertification = row["certification"].ToString();
                            strScaffCertifiedby = row["Certifiedby"].ToString();
                            strScaffTraining = row["Training"].ToString();
                            StrScaffTrainingDate = row["Actualdate"].ToString();
                            strScaffTrainedby = row["Trainedby"].ToString();
                            StrScaffCDate = row["certificationdate"].ToString();
                            StrScaffstatus = row["Certstatus"].ToString();
                            // strScaffEmpname = row["EMPNAME"].ToString();
                            // strScaffIDcardno = row["IDCardNo"].ToString();
                            StrScaffTrainingDate = Convert.ToDateTime(StrScaffTrainingDate).ToString("dd-MM-yyyy");
                            StrScaffCDate = Convert.ToDateTime(StrScaffCDate).ToString("dd-MM-yyyy");
                            // strScaff = row["IDCardNo"].ToString();
                        }
                        else
                        {
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                        }
                        //}
                        //else if (ddCertification.SelectedValue == "TCM000024")
                        //{
                        if (row["TRAININGID"].ToString() == "TCM000010")
                        {
                            ddTraining.SelectedValue = "TCM000010";
                            txtTrainingdate.Text = row["Actualdate"].ToString();
                            strHostingLoc = row["locationname"].ToString();
                            strHostingdept = row["Department"].ToString();

                            strHostingCertification = row["certification"].ToString();
                            strHostingCertifiedby = row["Certifiedby"].ToString();
                            strHostingTraining = row["Training"].ToString();
                            strHostingTrainingDate = row["Actualdate"].ToString();
                            strHostingTrainedby = row["Trainedby"].ToString();
                            strHostingCDate = row["certificationdate"].ToString();
                            strHostingTrainingDate = Convert.ToDateTime(strHostingTrainingDate).ToString("dd-MM-yyyy");
                            strHostingCDate = Convert.ToDateTime(strHostingCDate).ToString("dd-MM-yyyy");
                            strHostingstatus = row["Certstatus"].ToString();
                        }
                        else
                        {
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                        }
                        //}
                        //else if (ddCertification.SelectedValue == "TCM000022")
                        //{
                        if (row["TRAININGID"].ToString() == "TCM000030")
                        {
                            ddTraining.SelectedValue = "TCM000030";
                            txtTrainingdate.Text = row["Actualdate"].ToString();
                            strCarTopLoc = row["locationname"].ToString();
                            strCarTopdept = row["Department"].ToString();

                            strCarTopCertification = row["certification"].ToString();
                            strCarTopCertifiedby = row["Certifiedby"].ToString();
                            strCarTopTraining = row["Training"].ToString();
                            strCarTopTrainingDate = row["Actualdate"].ToString();
                            strCarTopTrainedby = row["Trainedby"].ToString();
                            strCarTopCDate = row["certificationdate"].ToString();
                            strCarTopTrainingDate = Convert.ToDateTime(strCarTopTrainingDate).ToString("dd-MM-yyyy");
                            strCarTopCDate = Convert.ToDateTime(strCarTopCDate).ToString("dd-MM-yyyy");
                            strCarTopstatus = row["Certstatus"].ToString();
                        }
                        else
                        {
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                        }
                        //}
                        //else if (ddCertification.SelectedValue == "TCM000026")
                        //{
                        if (row["TRAININGID"].ToString() == "TCM000029")
                        {
                            ddTraining.SelectedValue = "TCM000029";
                            txtTrainingdate.Text = row["Actualdate"].ToString();
                            strPitEntryLoc = row["locationname"].ToString();
                            strPitEntryTopdept = row["Department"].ToString();

                            strPitEntryTopCertification = row["certification"].ToString();
                            strPitEntryTopCertifiedby = row["Certifiedby"].ToString();
                            strPitEntryTopTraining = row["Training"].ToString();
                            strPitEntryTopTrainingDate = row["Actualdate"].ToString();
                            strPitEntryTopTrainedby = row["Trainedby"].ToString();
                            strPitEntryTopCDate = row["certificationdate"].ToString();
                            strPitEntryTopTrainingDate = Convert.ToDateTime(strPitEntryTopTrainingDate).ToString("dd-MM-yyyy");
                            strPitEntryTopCDate = Convert.ToDateTime(strPitEntryTopCDate).ToString("dd-MM-yyyy");
                            strPitEntrystatus = row["Certstatus"].ToString();
                        }
                        else
                        {
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                        }
                        //}
                        //else if (ddCertification.SelectedValue == "TCM000027")
                        //{
                        if (row["TRAININGID"].ToString() == "TCM000028")
                        {
                            ddTraining.SelectedValue = "TCM000028";
                            txtTrainingdate.Text = row["Actualdate"].ToString();
                            strLotoLoc = row["locationname"].ToString();
                            strLotodept = row["Department"].ToString();

                            strLotoCertification = row["certification"].ToString();
                            strLotoCertifiedby = row["Certifiedby"].ToString();
                            strLotoTraining = row["Training"].ToString();
                            strLotoTrainingDate = row["Actualdate"].ToString();
                            strLotoTrainedby = row["Trainedby"].ToString();
                            strLotoCDate = row["certificationdate"].ToString();
                            strLotoTrainingDate = Convert.ToDateTime(strLotoTrainingDate).ToString("dd-MM-yyyy");
                            strLotoCDate = Convert.ToDateTime(strLotoCDate).ToString("dd-MM-yyyy");
                            strLotostatus = row["Certstatus"].ToString();
                        }
                        else
                        {
                            ddTraining.SelectedIndex = 0;
                            txtTrainingdate.Text = "";
                        }
                        // }

                        //rbtnagreement.SelectedValue = row["AgreementSigneInLive"].ToString();
                        //rbtnconStatus.SelectedValue = row["contractorstatus"].ToString();
                        //rbtnEmpstatus.SelectedValue = row["employeestatus"].ToString();
                        //ddNatureofWork.SelectedValue = row["NatureofWork"].ToString();
                        //ddDepartment.SelectedValue = row["Department"].ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchEmployeeTracking()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FetchEmployeeTrackingDetails", sqlConnection);
                    cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = ddMainSubConName.SelectedValue;
                    cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = ddEmployeeName.SelectedValue;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ViewState["Training"] = ds.Tables[3];
                            ViewState["TrainingHDR"] = ds.Tables[1];
                            //ViewState["CurrentTable"] = ds.Tables[2];
                            //gv.DataSource = ds.Tables[2];
                            //gv.DataBind();
                            ViewState["CurrentTable"] = ds.Tables[4];
                            gv.DataSource = ds.Tables[4];
                            gv.DataBind();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds.Tables[1].Rows)
                                {
                                    txtIDCardNo.Text = row["IDcardNo"].ToString();
                                    ViewState["Dept"] = row["Department"].ToString();
                                }

                            }

                            //if (ds.Tables[4].Rows.Count > 0)
                            //{

                            //}

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
        protected void ddEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FetchEmployeeTracking();

               
            }
            catch(Exception ex)
            {
                lblmsg.Text= ex.Message;
                lblmsg.ForeColor= System.Drawing.Color.Red;
            }
        }
        private void sendmail()
        {
            try
            {
                string path = Server.MapPath("~/assets/images/fujitec_logo_J.jpg");
                LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
                Img.ContentId = "MyImage";
                // string image = "<iframe src = "https://fujitecindia1-my.sharepoint.com/personal/ftecin-notification_fujitec_co_in/_layouts/15/embed.aspx?UniqueId=e2d3213f-9cbb-4d30-b2e4-396f4ff78ced" width = "640" height = "360" frameborder = "0" scrolling = "no" allowfullscreen title = "fujitec_logo.png" ></ iframe >";//Server.MapPath("~/assets/images/fujitec_logo.png").ToString(); 
                ////----------------------------
                string Body = "Dear Sir/Madam,<br /><br />";
                Body = Body + "Current status of Employee Certification details,please find below<br/>";
                //Body = Body + "<b>Note:</b><b style=\"font-family:Calibri, sans-serif;font-weight:bold;color:red;\"> The below summary of cost exclude labour and DLP reversal in both forecast and actual.</b> <br /><br />";
                //Body = Body + "<div>";
                Body = Body + "<table border=\"0\" style=\"width:500px;height: 200px; margin:2px;padding: 20px; border:1px solid #6cb5d9; border-collapse:collapse;box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);\">";
                if (ddCertification.SelectedValue == "TCM000023")
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#d9edf7;\">";
                }
                else if (ddCertification.SelectedValue == "TCM000024")
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#ffff00;\">";
                }
                else if (ddCertification.SelectedValue == "TCM000022")
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#00cc44;\">";
                }
                else if (ddCertification.SelectedValue == "TCM000026")
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#e0e0d1;\">";
                }
                else if (ddCertification.SelectedValue == "TCM000027")
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#ff9900;\">";
                }
                else
                {
                    Body = Body + "<thead style=\"margin:0;padding:0;background-color:#d9edf7;\">";
                }
                Body = Body + "<tr><td colspan=2 style=\"border: 1px solid black; padding: 10px;\">";
                Body = Body + "<img src=cid:MyImage  id='img' alt='' width='150px' height='30px'/>";
                //Body = Body + "<div id='outerdiv' style=\"width:200px; height:25px;overflow-x:hidden;\"><iframe src=\"https://fujitecindia1-my.sharepoint.com/personal/ftecin-notification_fujitec_co_in/_layouts/15/embed.aspx?UniqueId=e2d3213f-9cbb-4d30-b2e4-396f4ff78ced\" width=\"100px\" height=\"30px\" frameborder=\"0\" scrolling=\"no\" allowfullscreen title=\"fujitec_logo.png\"></iframe></div>";
                Body = Body + "</td ><td colspan = 4 align = center style =\"border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\"border: 1px solid black; padding: 10px;\">Name:" + ddEmployeeName.SelectedItem.Text + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">ID:" + txtIDCardNo.Text + "</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\" border: 1px solid black; padding: 10px;\">Location:" + strScaffLoc.ToString() + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">Dept:" + strScaffDept.ToString() + "</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\" border: 1px solid black; padding: 10px;\">SubContr.Name:" + ddMainSubConName.SelectedItem.Text.ToString() + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">Status:" + ddCertstatus.SelectedValue.ToString() + "</td></tr>";
                Body = Body + "<tr><td colspan=1 style=\"width:5%;border: 1px solid black; padding: 10px;\">S.No</td>";
                Body = Body + "<td colspan=1 style=\"width: 45 %; border: 1px solid black; padding: 10px; \">Process Details</td>";
                Body = Body + "<td colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Training Details</td>";
                Body = Body + " <td  colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Date</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px; \">Trained By</td><td style=\"border: 1px solid black; padding: 10px;\">Date</td><td style=\"border: 1px solid black; padding: 10px;\">Certified by</td></tr>";
                //Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Under Observation</td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                //  if (ddCertification.SelectedValue == "TCM000023")
                //{
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">1</td><td style=\"border: 1px solid black; padding: 10px;\">Scaffolding entry / exit certification </td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + StrScaffTrainingDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strScaffTrainedby.ToString() + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + StrScaffCDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strScaffCertifiedby.ToString() + "</td></tr>";
                // {
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">2</td><td style=\"border: 1px solid black; padding: 10px;\">Hoisting</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strHostingTrainingDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strHostingTrainedby.ToString() + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strHostingCDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strHostingCertifiedby.ToString() + "</td></tr>";
                //{
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">3</td><td style=\"border: 1px solid black; padding: 10px;\">Car Top Entry & Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strCarTopTrainingDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strCarTopTrainedby.ToString() + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strCarTopCDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strCarTopCertifiedby.ToString() + "</td></tr>";
                //}
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">4</td><td style=\"border: 1px solid black; padding: 10px;\">PitEntry Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strPitEntryTopTrainingDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strPitEntryTopTrainedby.ToString() + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strPitEntryTopCDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strPitEntryTopCertifiedby.ToString() + "</td></tr>";
                //}
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">5</td><td style=\"border: 1px solid black; padding: 10px;\">LOTO</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strLotoTrainingDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strLotoTrainedby.ToString() + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strLotoCDate + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + strLotoCertifiedby.ToString() + "</td></tr>";
                //}
                Body = Body + "</tr></thead><tbody style=\"margin:0;padding:0;background-color:#fff;\">";
                //}
                Body = Body + "</tbody></table><br />";

                ViewState["Preview"] = Body;
                btnPreview.Enabled = true;
                AlternateView AV =
               AlternateView.CreateAlternateViewFromString(Body, null, MediaTypeNames.Text.Html);
                AV.LinkedResources.Add(Img);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                    | SecurityProtocolType.Tls11
                                    | SecurityProtocolType.Tls12;
                using (MailMessage mail = new MailMessage())
                {
                    var vendorData = ViewState["VendorE"] as DataTable;
                    string emailId = string.Empty;
                    if (vendorData != null)
                    {
                        // Use LINQ to filter out the email ID for the selected vendor ID
                        var selectedVendorEmail = vendorData.AsEnumerable()
                                                            .Where(row => row.Field<string>("ID") == ddMainSubConName.SelectedValue)
                                                            .Select(row => row.Field<string>("EmailId"))
                                                            .FirstOrDefault();
                        emailId = selectedVendorEmail.ToString();
                    }
                        // ViewState["VendorE"] as string;
                    if (string.IsNullOrEmpty(emailId))
                    {
                        emailId = ConfigurationManager.AppSettings["EmailToSafety"].ToString();
                    }
                    //mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "BookingListDetails.xlsx"));
                    mail.From = new MailAddress("ftecin-notification@fujitec.co.in", "Safety Certification");
                    mail.To.Add(emailId);
                    mail.CC.Add(ConfigurationManager.AppSettings["EmailToSafety"].ToString());
                   //mail.Bcc.Add("vinothkumar.s@in.fujitec.com");
                    mail.Subject = "ID Card";// "Delivery Dashboard - Status as on Date : " + MonthYear;
                    //mail.Body = Body;
                    mail.IsBodyHtml = true;

                    mail.AlternateViews.Add(AV);
                    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("ftecin-notification@fujitec.co.in", "HelpDesk@20$22");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex) {
                lblmsg.Text = ex.Message;
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["CurrentTable"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


    }
}