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

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class TaskMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    txtTaskName.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_TSTaskMasterPageLoad", sqlConnection);
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

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Access--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddAccess.DataTextField = "AccessDesc";
                        ddAccess.DataValueField = "AccessCode";
                        ddAccess.DataSource = ds.Tables[2];
                        ddAccess.DataBind();
                        //ddAccess.DataTextField = "Accessname";
                        //ddAccess.DataValueField = "DepartmentId";
                        //ddAccess.DataSource = ds.Tables[2];
                        //ddAccess.DataBind();

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
                        SqlCommand cmd = new SqlCommand("SP_TSTaskMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TaskId", SqlDbType.VarChar).Value = txtTaskID.Text;
                        cmd.Parameters.Add("@TaskName", SqlDbType.VarChar).Value = txtTaskName.Text;
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        cmd.Parameters.Add("@Access", SqlDbType.VarChar).Value = ddAccess.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Task Master for " + txtTaskID.Text + " -- " + txtTaskName.Text + " has been Created successfully";

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Task Master For " + txtTaskID.Text + " -- " + txtTaskName.Text + " has been Updated  successfully";
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
                txtTaskID.Text = gv.SelectedRow.Cells[1].Text;
                txtTaskName.Text = gv.SelectedRow.Cells[2].Text;
                ddAccess.SelectedValue = gv.SelectedRow.Cells[3].Text;
                if ((gv.SelectedRow.Cells[3].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[3].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[4].Text;
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

                if ((txtTaskName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Task Name should not be empty ";
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
                txtTaskName.Text = "";
                txtTaskID.Text = "";
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {

            }
        }

    }
    
}