using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Master
{
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    txtDepartmentName.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_DepartmentMasterPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {                      
                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[1];
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
                        SqlCommand cmd = new SqlCommand("SP_DepartmentMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = txtDepartmentID.Text;
                        cmd.Parameters.Add("@Departmentname", SqlDbType.VarChar).Value = txtDepartmentName.Text;
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
                                lblmsg.Text = "Department Master for " + txtDepartmentID.Text + " -- " + txtDepartmentName.Text + " has been Created successfully";

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Department Master For " + txtDepartmentID.Text + " -- " + txtDepartmentName.Text + " has been Updated  successfully";
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
                txtDepartmentID.Text = gv.SelectedRow.Cells[1].Text;
                txtDepartmentName.Text = gv.SelectedRow.Cells[2].Text;
                ////  ddContractorType.SelectedValue = gv.SelectedRow.Cells[3].Text;
                //txtContact.Text = gv.SelectedRow.Cells[3].Text;
                //txtContactDesignation.Text = gv.SelectedRow.Cells[4].Text;
                //txtAddress.Text = gv.SelectedRow.Cells[5].Text.Trim();
                //txtEmailId.Text = gv.SelectedRow.Cells[6].Text.Trim();
                //txtMobileNo.Text = gv.SelectedRow.Cells[7].Text.Trim();
                //txtGSTNo.Text = gv.SelectedRow.Cells[8].Text.Trim();
                //txtPANNo.Text = gv.SelectedRow.Cells[9].Text.Trim();
                //txtOwnername.Text = gv.SelectedRow.Cells[10].Text.Trim();
                //txtInsurancePolicyNo.Text = gv.SelectedRow.Cells[11].Text;
                if ((gv.SelectedRow.Cells[3].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[3].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[3].Text;
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
                
                if ((txtDepartmentName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Department Name should not be empty ";
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
                txtDepartmentName.Text = "";
                txtDepartmentID.Text = "";
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {

            }
        }
    }
}