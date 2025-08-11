using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace FujiTecIntranetPortal.Master
{
    public partial class EmployeeMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public DataTable dtem = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    txtEmployeeID.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_EmployeeMasterPageLoad", sqlConnection);
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
                        ViewState["GV"] = ds.Tables[4];
                        gv.DataSource = ds.Tables[4];
                        gv.DataBind();

                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Department--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddDepartment.DataTextField = "Departmentname";
                        ddDepartment.DataValueField = "DepartmentId";
                        ddDepartment.DataSource = ds.Tables[0];
                        ddDepartment.DataBind();

                        DataRow dr1 = ds.Tables[2].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Designation--" };
                        ds.Tables[2].Rows.InsertAt(dr1, 0);
                        ddDesignation.DataTextField = "Designation";
                        ddDesignation.DataValueField = "DesignationId";
                        ddDesignation.DataSource = ds.Tables[2];
                        ddDesignation.DataBind();

                        DataRow dr2 = ds.Tables[3].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Location--" };
                        ds.Tables[3].Rows.InsertAt(dr2, 0);
                        ddLocation.DataTextField = "Locationname";
                        ddLocation.DataValueField = "LocationId";
                        ddLocation.DataSource = ds.Tables[3];
                        ddLocation.DataBind();

                        //if (ds.Tables[4].Rows.Count > 0)
                        //{
                        //    dtem = ds.Tables[4];
                        //}
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
                if (txtAge.Text == string.Empty)
                    txtAge.Text = 0.ToString();
                DateTime dob, doj;
                bool chValidityDOB = DateTime.TryParseExact(txtDOB.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob);
                //if (chValidityDOB == true)
                if(txtDOB.Text.Length<6)
                    txtDOB.Text = "01/01/2000";
                bool chValidityDOJ = DateTime.TryParseExact(txtDOJ.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out doj);
                //
                //if (chValidityDOJ == true)
                if (txtDOJ.Text.Length < 6)
                    txtDOJ.Text = "01/01/2000";

                if (validt != "Error")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_EmployeeMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                        cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = txtEmployeeName.Text;
                        cmd.Parameters.Add("@Age", SqlDbType.Int).Value = int.Parse(txtAge.Text);
                        cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = txtDOB.Text;
                        cmd.Parameters.Add("@DOJ", SqlDbType.DateTime).Value = txtDOJ.Text;
                        cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = ddGender.SelectedValue;
                        cmd.Parameters.Add("@DepartmentId", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                        cmd.Parameters.Add("@Departmentname", SqlDbType.VarChar).Value = ddDepartment.SelectedItem.Text;
                        cmd.Parameters.Add("@DesignationId", SqlDbType.VarChar).Value = ddDesignation.SelectedValue;
                        cmd.Parameters.Add("@DesignationName", SqlDbType.VarChar).Value = ddDesignation.SelectedItem.Text;
                        cmd.Parameters.Add("@LocationID", SqlDbType.VarChar).Value = ddLocation.SelectedValue;
                        cmd.Parameters.Add("@LocationName", SqlDbType.VarChar).Value = ddLocation.SelectedItem.Text;
                        cmd.Parameters.Add("@emailid", SqlDbType.VarChar).Value = txtEmailId.Text;
                        cmd.Parameters.Add("@Contactno", SqlDbType.VarChar).Value = txtPhoneNo.Text;
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
                                lblmsg.Text = "Employee details for " + txtEmployeeID.Text + " -- " + txtEmployeeName.Text + " has been Created successfully";
                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Employee details for " + txtEmployeeID.Text + " -- " + txtEmployeeName.Text + " has been Updated  successfully";
                            }
                            else //if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                            {
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                lblmsg.Text = "Error in registration";
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
                txtEmployeeID.Text = gv.SelectedRow.Cells[1].Text;
                dtem = (DataTable)ViewState["GV"];
                foreach (DataRow row in dtem.Rows)
                {
                    if (row["EMPID"].ToString() == txtEmployeeID.Text.ToString())
                    {
                        txtEmployeeID.Text = row["EMPID"].ToString();
                        txtEmployeeName.Text = row["EMP_NAME"].ToString();
                        txtAge.Text = row["AGE"].ToString();
                        txtDOB.Text = row["DOB"].ToString();
                        txtDOJ.Text = row["DOJ"].ToString();
                        ddGender.SelectedValue = row["Sex"].ToString();
                        ddDepartment.Text = row["DEPARTMENTID"].ToString();
                        txtEmailId.Text = row["EmailId"].ToString();
                        txtPhoneNo.Text = row["ContactNo"].ToString();
                        ddDesignation.Text = row["DesignationId"].ToString();
                        ddLocation.SelectedValue = row["LOCATIONID"].ToString();
                        txtDOB.Text = Convert.ToDateTime(txtDOB.Text).ToShortDateString();
                        txtDOJ.Text = Convert.ToDateTime(txtDOJ.Text).ToShortDateString();
                        break;
                    }
                }
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
                if ((txtEmployeeID.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee Id should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtEmployeeName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddDepartment.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please Select Department";
                    ViewState["Error"] = "Error";
                }
                if ((ddDesignation.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please Select Designation";
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
                txtEmployeeName.Text = "";
                txtEmployeeID.Text = "";
                txtAge.Text = "";
                txtDOB.Text = "";
                txtDOJ.Text = "";
                txtEmailId.Text = "";
                txtPhoneNo.Text = "";
                ddLocation.SelectedIndex = 0;
                ddDepartment.SelectedIndex = 0;
                ddDesignation.SelectedIndex = 0;
                ddStatus.SelectedIndex = 0;
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text.Length > 4)
                {
                    dtem = (DataTable)ViewState["GV"];
                    foreach (DataRow row in dtem.Rows)
                    {
                        if (row["EMPID"].ToString() == txtEmployeeID.Text.ToString())
                        {
                            txtEmployeeID.Text = row["EMPID"].ToString();
                            txtEmployeeName.Text = row["EMP_NAME"].ToString();
                            txtAge.Text = row["AGE"].ToString();
                            txtDOB.Text = row["DOB"].ToString();
                            txtDOJ.Text = row["DOJ"].ToString();
                            ddGender.SelectedValue = row["Sex"].ToString();
                            ddDepartment.Text = row["DEPARTMENTID"].ToString();
                            txtEmailId.Text = row["EmailId"].ToString();
                            txtPhoneNo.Text = row["ContactNo"].ToString();
                            //ddGender.SelectedValue = row["Sex"].ToString();
                            ddDesignation.Text = row["DesignationId"].ToString();
                            ddLocation.SelectedValue = row["LOCATIONID"].ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}