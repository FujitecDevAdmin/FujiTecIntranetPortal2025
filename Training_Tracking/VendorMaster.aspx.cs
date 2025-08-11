using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class VendorMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    txtVendorID.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_VendorMasterPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        //DataRow dr = ds.Tables[0].NewRow();
                        //dr.ItemArray = new object[] { 0, "--Select VendorId--" };
                        //ds.Tables[0].Rows.InsertAt(dr, 0);
                        //ddContractorType.DataTextField = "ContractorDesc";
                        //ddContractorType.DataValueField = "ContracorTypeCode";
                        //ddContractorType.DataSource = ds.Tables[0];
                        //ddContractorType.DataBind();

                        //DataRow dr1 = ds.Tables[1].NewRow();
                        //dr1.ItemArray = new object[] { 0, "--Select MainContractor--" };
                        //ds.Tables[1].Rows.InsertAt(dr1, 0);
                        //ddMainContractor.DataTextField = "Vendor Name";
                        //ddMainContractor.DataValueField = "ID";
                        //ddMainContractor.DataSource = ds.Tables[1];
                        //ddMainContractor.DataBind();

                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[3];
                        ddStatus.DataBind();

                        ViewState["GV"] = ds.Tables[2];
                        gv.DataSource = ds.Tables[2];
                        gv.DataBind();
                        ViewState["export"] = ds.Tables[2];
                    }
                }
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
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                if (validt != "Error")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_VendorMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Vendor_Id", SqlDbType.VarChar).Value = (txtVendorID.Text);
                        cmd.Parameters.Add("@Vendor_Name", SqlDbType.VarChar).Value = txtVendorName.Text;
                        cmd.Parameters.Add("@InsurancePolicyNo", SqlDbType.VarChar).Value = txtInsurancePolicyNo.Text;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = txtContact.Text;// dtpDate.Text;
                        cmd.Parameters.Add("@contactPersonDesignation", SqlDbType.VarChar).Value = txtContactDesignation.Text;
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = txtAddress.Text;
                        cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = txtEmailId.Text;
                        cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar).Value = (txtMobileNo.Text);
                        cmd.Parameters.Add("@GSTNo", SqlDbType.VarChar).Value = txtGSTNo.Text;
                        cmd.Parameters.Add("@PANNo", SqlDbType.VarChar).Value = txtPANNo.Text;
                        cmd.Parameters.Add("@OwnerName", SqlDbType.VarChar).Value = txtOwnername.Text;//ddMainContractor.SelectedValue;
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@BranchVendorcode", SqlDbType.VarChar).Value = txtBranchVendorCode.Text;
                        cmd.Parameters.Add("@ERPVendorId", SqlDbType.VarChar).Value = txtERPVendorCode.Text;
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                txtVendorID.Text = ds.Tables[0].Rows[0]["VendorID"].ToString();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Vendor Master for " + txtVendorID.Text + " -- " + txtVendorName.Text + " Created successfully";

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Vendor Master For " + txtVendorID.Text + " -- " + txtVendorName.Text + " Update  successfully";
                            }
                            else //if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                            {
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                lblmsg.Text = "Error in registration";
                            }

                            //SendEmail();
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
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["export"];
                ExportDataTableToExcel(dt, "VendorMaster.xlsx");
            }
            catch (Exception ex)
            {

            }
        }
        private void ExportDataTableToExcel(DataTable dt, string fileName)
        {
            using (var workbook = new XLWorkbook())
            {
                // Add DataTable as a worksheet
                var worksheet = workbook.Worksheets.Add(dt, "Sheet1");

                using (MemoryStream stream = new MemoryStream())
                {
                    // Save the workbook to the MemoryStream
                    workbook.SaveAs(stream);

                    // Set the position of the stream to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // Set the content type and headers for the response
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", $"attachment;filename={fileName}");

                    // Write the stream to the response
                    stream.CopyTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid()
        {
            //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_SubConVendorSearch", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@selectConEmp", SqlDbType.VarChar).Value = ddselect.SelectedValue;
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = txtSearch.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["GV"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtVendorID.Text = "";
                txtVendorName.Text = "";
                //ddContractorType.SelectedIndex = 0;
                txtContact.Text = "";
                txtContactDesignation.Text = "";
                txtAddress.Text = "";
                txtEmailId.Text = "";
                txtMobileNo.Text = "";
                txtGSTNo.Text = "";
                txtPANNo.Text = "";
                txtOwnername.Text = "";
                //ddMainContractor.SelectedIndex = 0;
                txtInsurancePolicyNo.Text = "";
                ddStatus.SelectedIndex = 0;
                txtSearch.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtVendorID.Text = gv.SelectedRow.Cells[1].Text;
                txtVendorName.Text = gv.SelectedRow.Cells[2].Text;
                //  ddContractorType.SelectedValue = gv.SelectedRow.Cells[3].Text;
                txtContact.Text = gv.SelectedRow.Cells[3].Text;
                txtContactDesignation.Text = gv.SelectedRow.Cells[4].Text;
                txtAddress.Text = gv.SelectedRow.Cells[5].Text.Trim();
                txtEmailId.Text = gv.SelectedRow.Cells[6].Text.Trim();
                txtMobileNo.Text = gv.SelectedRow.Cells[7].Text.Trim();
                txtGSTNo.Text = gv.SelectedRow.Cells[8].Text.Trim();
                txtPANNo.Text = gv.SelectedRow.Cells[9].Text.Trim();
                txtOwnername.Text = gv.SelectedRow.Cells[10].Text.Trim();
                txtInsurancePolicyNo.Text = gv.SelectedRow.Cells[11].Text;
                if ((gv.SelectedRow.Cells[12].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[12].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[12].Text;
            }
            catch (Exception ex)
            { }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["GV"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtGST_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGSTNo.Text.Length == 15)
                {
                    if (Regex.Match(txtGSTNo.Text, "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9]{1}[Zz]{1}[a-zA-Z]{1}").Success)
                    {

                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "GSTNO is not valid";
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
                //if ((txtMobileNo.Text == string.Empty) || (txtMobileNo.Text.Length < 10))
                //{
                //    lblmsg.Text = "* Mobile no should not be empty or it should be minimum 10 numbers " + Environment.NewLine;
                //    ViewState["Error"] = "Error";
                //}               


                if ((txtEmailId.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Email ID should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtVendorName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* SC Company Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtContact.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Contact Person should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtContactDesignation.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Contact Designation should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtAddress.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Address should not be empty ";
                    ViewState["Error"] = "Error";
                }
                //if (ddContractorType.SelectedValue == "MSC0007")
                //{
                //    if (ddMainContractor.SelectedValue == "0")
                //    {
                //        lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Main contractor ";
                //        ViewState["Error"] = "Error";
                //        ddMainContractor.Enabled = true;
                //    }                   
                //}
                //else if (ddContractorType.SelectedValue == "MSC0006")
                //{
                //    ddMainContractor.Enabled = false;
                //}
                if (Regex.Match(txtMobileNo.Text, "[0-9]{10}").Success)
                {
                }
                else
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Invalid Mobile no  ";// + Environment.NewLine;
                    ViewState["Error"] = "Error";
                }
                // }
                if (txtGSTNo.Text.Trim().Length >= 15)
                {
                    if (Regex.Match(txtGSTNo.Text.ToUpper().Trim(), "[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[Zz]{1}[0-9a-zA-Z]{1}").Success)
                    {
                        //if (Regex.Match(txtGSTNo.Text.ToUpper().Trim().Substring(0, 2), "[0-9]{2}").Success)
                        //{
                        //    if (Regex.Match(txtGSTNo.Text.ToUpper().Trim().Substring(2, 10), "[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}").Success)
                        //    {

                        //        if (Regex.Match(txtGSTNo.Text.ToUpper().Trim().Substring(12, 1), "[1-9]{1}").Success)
                        //        {
                        //            string vint = txtGSTNo.Text.ToUpper().Trim().Substring(13, 1);
                        //            if (Regex.Match(txtGSTNo.Text.ToUpper().Trim().Substring(13, 2), "[Zz]{1}[0-9a-zA-Z]{1}").Success)
                        //            {

                        //            }
                        //        }
                        //    }
                        //}

                        //DataTable dt = new DataTable();
                        //dt = (DataTable)ViewState["GV"];
                        //foreach(DataRow dr in dt.Rows)
                        //{
                        //    if (dr[].ToString() == "")
                        //}
                    }
                    else
                    {
                        ViewState["Error"] = "Error";
                        lblmsg.Text = lblmsg.Text + Environment.NewLine + "* GSTNO is not valid";
                    }
                }
                else
                {
                    txtGSTNo.Text = String.Empty;
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* GSTNO is not valid";
                }
                if (Regex.Match(txtPANNo.Text, "[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}").Success)
                {

                }
                else
                {
                    ViewState["Error"] = "Error";
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* PAN no is not valid";
                }

                //}
                //if (txtEmailId.Text != String.Empty)
                //{
                //    //    string email = txtEmailId.Text;
                //    //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //    //    Match match = regex.Match(email);
                //    //    if (match.Success)
                //    //        lblmsg.Text = "";
                //    //    else
                //    //    {
                //    //        // lblmsg.Text = 
                //    //        ViewState["Error"] = "Error";
                //    //        lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Invalid Email Address";
                //    //    }
                //}
                //else
                //{
                //    ViewState["Error"] = "Error";
                //}

            }
            catch (Exception ex)
            {

            }
        }
    }
}