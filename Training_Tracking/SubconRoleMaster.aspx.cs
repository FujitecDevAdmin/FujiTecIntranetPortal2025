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

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubconRoleMaster : System.Web.UI.Page
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
                    SqlCommand cmd = new SqlCommand("SP_SubConRoleMasterPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[1].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Branch--" };
                        ds.Tables[1].Rows.InsertAt(dr, 0);
                        ddBranchID.DataTextField = "BranchName";
                        ddBranchID.DataValueField = "BranchId";
                        ddBranchID.DataSource = ds.Tables[1];
                        ddBranchID.DataBind();

                        DataRow dr1 = ds.Tables[2].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Role--" };
                        ds.Tables[2].Rows.InsertAt(dr1, 0);
                        ddApproverRole.DataTextField = "RoleDesc";
                        ddApproverRole.DataValueField = "RoleID";
                        ddApproverRole.DataSource = ds.Tables[2];
                        ddApproverRole.DataBind();

                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[3];
                        ddStatus.DataBind();
                        ViewState["GV"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
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
                        SqlCommand cmd = new SqlCommand("SP_SubConRoleMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@RoleId", SqlDbType.VarChar).Value = txtRoleID.Text;
                        cmd.Parameters.Add("@BranchID", SqlDbType.VarChar).Value = ddBranchID.SelectedValue;

                        cmd.Parameters.Add("@ApproverId", SqlDbType.VarChar).Value = txtApproverID.Text;
                        cmd.Parameters.Add("@ApproverRole", SqlDbType.VarChar).Value = ddApproverRole.Text;

                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  

                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "SubCon Role Master Created successfully";

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "SubCon Role Master Updated  successfully";
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
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtRoleID.Text = gv.SelectedRow.Cells[1].Text;
                ddBranchID.SelectedValue = gv.SelectedRow.Cells[2].Text;
                txtApproverID.Text = gv.SelectedRow.Cells[3].Text;
                ddApproverRole.SelectedValue= gv.SelectedRow.Cells[4].Text;
                ////  ddContractorType.SelectedValue = gddBranchID.SelectedValue = gv.SelectedRow.Cells[2].Text;v.SelectedRow.Cells[3].Text;
                //txtContact.Text = gv.SelectedRow.Cells[3].Text;
                //txtContactDesignation.Text = gv.SelectedRow.Cells[4].Text;
                //txtAddress.Text = gv.SelectedRow.Cells[5].Text.Trim();
                //txtEmailId.Text = gv.SelectedRow.Cells[6].Text.Trim();
                //txtMobileNo.Text = gv.SelectedRow.Cells[7].Text.Trim();
                //txtGSTNo.Text = gv.SelectedRow.Cells[8].Text.Trim();
                //txtPANNo.Text = gv.SelectedRow.Cells[9].Text.Trim();
                //txtOwnername.Text = gv.SelectedRow.Cells[10].Text.Trim();
                //txtInsurancePolicyNo.Text = gv.SelectedRow.Cells[11].Text;
                if ((gv.SelectedRow.Cells[5].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[5].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[5].Text;
            }
            catch (Exception ex)
            { }
        }
        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                if ((txtApproverID.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Approver ID should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddBranchID.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Branch Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddApproverRole.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Approver Role should not be empty ";
                    ViewState["Error"] = "Error";
                }

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
               // txtDepartmentName.Text = "";
               // txtDepartmentID.Text = "";
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {

            }
        }
    }
}