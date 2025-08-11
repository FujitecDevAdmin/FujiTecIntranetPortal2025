using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class TaskTypeMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    txtTaskTypeName.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_TSTaskTypeMasterPageLoad", sqlConnection);
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

                        //DataRow dr2 = ds.Tables[2].NewRow();
                        //dr2.ItemArray = new object[] { 0, "--Select Access--" };
                        //ds.Tables[2].Rows.InsertAt(dr2, 0);
                        //ddAccess.DataTextField = "AccessDesc";
                        //ddAccess.DataValueField = "AccessCode";
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
                        SqlCommand cmd = new SqlCommand("SP_TSTaskTypeMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = txtTaskTypeID.Text;
                        cmd.Parameters.Add("@TaskTypeName", SqlDbType.VarChar).Value = txtTaskTypeName.Text;
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        // cmd.Parameters.Add("@Access", SqlDbType.VarChar).Value = ddAccess.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Task Type Master for " + txtTaskTypeID.Text + " -- " + txtTaskTypeName.Text + " has been Created successfully";

                                ShowMessageBox(true, "Operation completed successfully!");
                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Task Type Master For " + txtTaskTypeID.Text + " -- " + txtTaskTypeName.Text + " has been Updated  successfully";

                                ShowMessageBox(true, "Operation completed successfully!");
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
                ShowMessageBox(false, ex.Message);
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
                ShowMessageBox(false, ex.Message);
            }
        }

        protected void ShowMessageBox(bool isSuccess, string message)
        {
            string cssClass = isSuccess ? "alert-success" : "alert-danger";
            string alertIcon = isSuccess ? "fa-check-circle" : "fa-times-circle";

            string script = $@"
        <script>
            $(document).ready(function () {{
                var messageBox = $('<div class=""alert {cssClass} alert-dismissible"" role=""alert"">');
                messageBox.append('<i class=""fa {alertIcon} mr-2""></i>');
                messageBox.append('{message}');
                messageBox.append('<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>');
                $('body').append(messageBox);
            }});
        </script>";

            ClientScript.RegisterStartupScript(GetType(), "ShowMessageBox", script);
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtTaskTypeID.Text = gv.SelectedRow.Cells[1].Text;
                txtTaskTypeName.Text = gv.SelectedRow.Cells[2].Text;
               // ddAccess.SelectedValue = gv.SelectedRow.Cells[3].Text;
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

                if ((txtTaskTypeName.Text == string.Empty))
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
                txtTaskTypeName.Text = "";
                txtTaskTypeID.Text = "";
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {

            }
        }
    }
}