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

namespace FujiTecIntranetPortal
{
    public partial class ServiceEmailPowerBISetup : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            FetchInitializeDetails();
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_EmailPowerbipageload", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["GV"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // lblmsg.ForeColor = System.Drawing.Color.Red;
                // lblmsg.Text = ex.Message;
            }
        }

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                //if ((txtEmployeeID.Text == string.Empty))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee ID should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                if ((txtProjectCode.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Project Code should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtProjectDescription.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Project Description should not be empty";
                    ViewState["Error"] = "Error";
                }
                if ((txtToEmail.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* ToEmail should not be empty ";
                    ViewState["Error"] = "Error";
                }
                //if ((DDSTATE.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* State should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((ddVendorNo.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Vendor";
                //    ViewState["Error"] = "Error";
                //}

            }
            catch (Exception ex)
            {

            }
        }

        private void Clear()
        {
            lblmsg.Text = "";
            txtProjectCode.Text = "";
            txtProjectDescription.Text = "";
            txtCCEmail.Text = "";
            txtToEmail.Text = "";
            txtBCCEmail.Text = "";
            //ddselect.SelectedIndex = 0;
            //txtSearch.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //testIdCard();
               Clear();

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
                lblmsg.Text = "";
                Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                if (validt != "Error")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_PowerBIEmail_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = txtProjectCode.Text;
                        cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = txtProjectDescription.Text;
                        cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = txtToEmail.Text;
                        cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = txtCCEmail.Text;// dtpDate.Text;
                        cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = txtBCCEmail.Text;
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                // FetchInitializeDetails();
                                Clear();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Project Code Created successfully";
                               // txtEmployeeID.Text = ds.Tables[0].Rows[0]["EMPID"].ToString();
                                

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                // FetchInitializeDetails();
                                Clear();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Project Code Update  successfully";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                if (ddselect.SelectedIndex != 0)
                    BindGrid();
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select Search Type";
                }
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
                SqlCommand cmd = new SqlCommand("SP_PowerBIEmail_Search", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;
                if (ddselect.SelectedIndex == 1)
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = txtSearch.Text;
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = "";// dtpDate.Text;
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = "";
                }
                else if (ddselect.SelectedIndex == 2)
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = txtSearch.Text;
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = "";// dtpDate.Text;
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = "";
                }
                else if (ddselect.SelectedIndex == 3)
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = txtSearch.Text;
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = "";// dtpDate.Text;
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = "";
                }
                else if (ddselect.SelectedIndex == 4)
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = txtSearch.Text;
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = "";
                }
                else if (ddselect.SelectedIndex == 5)
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = txtSearch.Text;
                }
                else
                {
                    cmd.Parameters.Add("@ProjectCode", SqlDbType.NVarChar).Value = "";
                    cmd.Parameters.Add("@ProjectDescription", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ToEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@CcEmails", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@BccEmails", SqlDbType.VarChar).Value = "";
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    ViewState["GV"] = ds.Tables[0];
                    gv.DataSource = ds.Tables[0];
                    gv.DataBind();
                }
            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt = (DataTable)ViewState["GV"];
                //gv.DataSource = dt;
                //gv.DataBind();

                //lblmsg.Text = "";
                //txtEmployeeID.Text = gv.SelectedRow.Cells[1].Text;
                //txtEmployeeName.Text = gv.SelectedRow.Cells[2].Text;
                //ddEmployeeCategory.SelectedValue = gv.SelectedRow.Cells[3].Text;
                //txtESINo.Text = gv.SelectedRow.Cells[5].Text;
                //txtUNINO.Text = gv.SelectedRow.Cells[6].Text;
                //txtPhoneNo.Text = gv.SelectedRow.Cells[7].Text;
                //ddVendorNo.SelectedValue = gv.SelectedRow.Cells[8].Text;
                //if ((gv.SelectedRow.Cells[10].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[10].Text.Trim() == "MSC0002"))
                //    ddStatus.SelectedValue = gv.SelectedRow.Cells[10].Text;
                //txtLocation.Text = gv.SelectedRow.Cells[12].Text;
                //DDSTATE.SelectedValue = gv.SelectedRow.Cells[13].Text;
                //txtBloodGroup.Text = gv.SelectedRow.Cells[15].Text;
                //ViewState["Photo"] = gv.SelectedRow.Cells[16].Text;
                //ViewState["DOJ"] = gv.SelectedRow.Cells[17].Text;
                //ViewState["Emailid"] = gv.SelectedRow.Cells[18].Text;
                //string[] splitmodule = ddVendorNo.SelectedItem.ToString().Split('-');
                //string txtcompany = splitmodule[0];
                //txtsubconCompany.Text = txtcompany;
                txtProjectCode.Text = gv.SelectedRow.Cells[2].Text;
                txtProjectDescription.Text = gv.SelectedRow.Cells[3].Text;
                txtToEmail.Text = gv.SelectedRow.Cells[4].Text;
                txtCCEmail.Text = gv.SelectedRow.Cells[5].Text;
                txtBCCEmail.Text = gv.SelectedRow.Cells[6].Text;
                //DataTable dt1 = new DataTable();
                //dt1 = (DataTable)ViewState["GV"];
                //gv.DataSource = dt1;
                //gv.DataBind();

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ddVendorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string[] splitmodule = ddVendorNo.SelectedItem.ToString().Split('-');
                //string txtcompany = splitmodule[0];
                //txtsubconCompany.Text = txtcompany;
                //txtsubconCompany.Text= ddVendorNo.SelectedValue.ToString();
                //txtsubconCompany.Text = ddVendorNo.SelectedItem.Text;
                // txtsubconCompany.Text = ddVendorNo.selected
            }
            catch (Exception ex)
            {

            }
        }

    }
}