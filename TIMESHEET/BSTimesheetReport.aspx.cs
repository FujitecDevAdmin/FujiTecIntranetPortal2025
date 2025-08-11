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
using System.IO;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class BSTimesheetReport : System.Web.UI.Page
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchInitializeDetails();
                //BindTimesheetReport(); // Load all data initially, or you can leave it empty if you want
            }
        }

        public void FetchInitializeDetails()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_LoadDropdownDataForBusinessTimesheet", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // 1. Task Dropdown
                        ddTask.DataSource = reader;
                        ddTask.DataTextField = "TaskName";
                        ddTask.DataValueField = "ID";
                        ddTask.DataBind();
                        ddTask.Items.Insert(0, new ListItem("-- Select Task --", ""));

                        // Move to next result set
                        reader.NextResult();

                        // 2. Task Type Dropdown
                        ddTaskType.DataSource = reader;
                        ddTaskType.DataTextField = "TaskTypeName";
                        ddTaskType.DataValueField = "ID";
                        ddTaskType.DataBind();
                        ddTaskType.Items.Insert(0, new ListItem("-- Select Task Type --", ""));

                        // Move to next result set
                        reader.NextResult();

                        // 3. Status Dropdown
                        ddStatus.DataSource = reader;
                        ddStatus.DataTextField = "StatusName";
                        ddStatus.DataValueField = "ID";
                        ddStatus.DataBind();
                        ddStatus.Items.Insert(0, new ListItem("-- Select Status --", ""));
                    }
                }
            }

        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            txtEnquiryNo.Text = string.Empty;

            ddTask.ClearSelection();
            ddTaskType.ClearSelection();
            ddStatus.ClearSelection();
            txtAssignedBy.Text = string.Empty;
            // Clear Grid
            gvTimesheetReport.DataSource = null;
            gvTimesheetReport.DataBind();

            // Optionally rebind your grid to show all records or blank it
            // BindGrid(); // Uncomment if you have a grid refresh method
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindTimesheetReport();
        }

        private void BindTimesheetReport()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbconnection1"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetBSTimesheetReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", string.IsNullOrEmpty(txtUserId.Text) ? (object)DBNull.Value : txtUserId.Text);
                    cmd.Parameters.AddWithValue("@FromDate", string.IsNullOrEmpty(txtFromDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtFromDate.Text));
                    cmd.Parameters.AddWithValue("@ToDate", string.IsNullOrEmpty(txtToDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtToDate.Text));
                    cmd.Parameters.AddWithValue("@EnquiryNo", string.IsNullOrEmpty(txtEnquiryNo.Text) ? (object)DBNull.Value : txtEnquiryNo.Text);
                    cmd.Parameters.AddWithValue("@TaskID", string.IsNullOrEmpty(ddTask.SelectedValue) ? (object)DBNull.Value : ddTask.SelectedValue);
                    cmd.Parameters.AddWithValue("@TaskTypeID", string.IsNullOrEmpty(ddTaskType.SelectedValue) ? (object)DBNull.Value : ddTaskType.SelectedValue);
                    cmd.Parameters.AddWithValue("@StatusID", string.IsNullOrEmpty(ddStatus.SelectedValue) ? (object)DBNull.Value : ddStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@AssignedBy", string.IsNullOrEmpty(txtAssignedBy.Text) ? (object)DBNull.Value : txtAssignedBy.Text);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvTimesheetReport.DataSource = dt;
                        gvTimesheetReport.DataBind();
                    }
                }
            }
        }
        protected void gvTimesheetReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTimesheetReport.PageIndex = e.NewPageIndex;
            BindTimesheetReport(); // Replace with your actual method that binds the GridView
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=TimesheetReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // Disable paging to export all data
                    gvTimesheetReport.AllowPaging = false;

                    // Reload all data if needed
                    BindTimesheetReport(); // Make sure this reloads all current filters

                    gvTimesheetReport.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        // Required for GridView export
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for Export to Excel
        }

    }
}
