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
    public partial class BSTimesheet : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchInitializeDetails(); // <-- Your method to load dropdowns
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnquiryNo.Text) || string.IsNullOrEmpty(txtProjectNo.Text) || string.IsNullOrEmpty(txtProjectName.Text) || ddTask.SelectedIndex == 0 || ddTaskType.SelectedIndex == 0 || ddStatus.SelectedIndex == 0 ||
                 ddWorkingEnvironment.SelectedIndex == 0 || string.IsNullOrEmpty(txtActualStart.Text) || string.IsNullOrEmpty(txtActualEnd.Text) || string.IsNullOrEmpty(txtActualHours.Text))
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please fill all required fields!";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_BSInsert_TSTimesheet", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Mandatory
                    cmd.Parameters.AddWithValue("@UserID", Session["USERID"]);
                    cmd.Parameters.AddWithValue("@ProjectNO", txtProjectNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@TaskID", Convert.ToInt32(ddTask.SelectedValue));
                    cmd.Parameters.AddWithValue("@TaskTypeID", Convert.ToInt32(ddTaskType.SelectedValue));

                    // Planned section
                    if (!string.IsNullOrEmpty(txtPlannedStart.Text))
                    {
                        cmd.Parameters.Add("@PlannedFromdate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtPlannedStart.Text);
                        cmd.Parameters.Add("@PlannedTodate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtPlannedEnd.Text);
                        cmd.Parameters.Add("@PlannedHours", SqlDbType.Float).Value = Convert.ToDouble(txtPlannedHours.Text);
                    }
                    else
                    {
                        cmd.Parameters.Add("@PlannedFromdate", SqlDbType.DateTime).Value = new DateTime(1900, 1, 1);
                        cmd.Parameters.Add("@PlannedTodate", SqlDbType.DateTime).Value = new DateTime(1900, 1, 1);
                        cmd.Parameters.Add("@PlannedHours", SqlDbType.Float).Value = 0.0;
                    }

                    // Actual fields
                    cmd.Parameters.AddWithValue("@ActualFromdate", Convert.ToDateTime(txtActualStart.Text));
                    cmd.Parameters.AddWithValue("@ActualTodate", Convert.ToDateTime(txtActualEnd.Text));
                    cmd.Parameters.AddWithValue("@ActualHours", Convert.ToDouble(txtActualHours.Text));

                    // Others
                    cmd.Parameters.AddWithValue("@StatusID", Convert.ToInt32(ddStatus.SelectedValue));
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["USERID"]);
                    cmd.Parameters.AddWithValue("@EnquiryNo", txtEnquiryNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProjectName", txtProjectName.Text.Trim());
                    cmd.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorkEnvmt", ddWorkingEnvironment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Checkedby", txtCheckedBy.Text.Trim());
                    // Reference ID (for planned task)
                    int referenceId = ViewState["ScheduleID"] != null ? Convert.ToInt32(ViewState["ScheduleID"]) : 0;
                    cmd.Parameters.AddWithValue("@ReferrenceID", referenceId);

                    // Timesheet ID (for unplanned update)
                    int timesheetId = ViewState["TimesheetID"] != null ? Convert.ToInt32(ViewState["TimesheetID"]) : 0;
                    cmd.Parameters.AddWithValue("@TimesheetID", timesheetId);

                    // Execute
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    // Refresh pending grid
                    btnPendingTask_Click(sender, e);

                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Timesheet saved successfully.";
                    


                    // Optional: Clear ViewState["TimesheetID"] to avoid update on next save
                    ViewState["TimesheetID"] = null;
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Error: " + ex.Message;
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all form inputs
            txtEnquiryNo.Text = "";
            txtProjectNo.Text = "";
            txtProjectName.Text = "";
            ddTask.SelectedIndex = 0;
            ddTaskType.SelectedIndex = 0;
            txtReferenceNo.Text = "";
            txtCheckedBy.Text = "";
            ddWorkingEnvironment.SelectedIndex = 0;
            txtPlannedStart.Text = "";
            txtPlannedEnd.Text = "";
            txtPlannedHours.Text = "0";
            txtRemarks.Text = "";
            txtActualStart.Text = "";
            txtActualEnd.Text = "";
            txtActualHours.Text = "0";
            ddStatus.SelectedIndex = 0;

            // Make fields read-only
            txtEnquiryNo.ReadOnly = false;
            txtProjectNo.ReadOnly = false;
            txtProjectName.ReadOnly = false;
            txtReferenceNo.ReadOnly = false;
            txtPlannedStart.ReadOnly = false;
            txtPlannedEnd.ReadOnly = false;
            txtPlannedHours.ReadOnly = false;

            ddTask.Enabled = true;
            ddTaskType.Enabled = true;
            lblmsg.Text = "";
            ViewState["ScheduleID"] = 0;
            ViewState["TimesheetID"] = 0;
        }
        protected void btnPendingTask_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "Button clicked";  // Debug log

            string userId = Session["USERID"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                lblmsg.Text = "User not logged in.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_BSPendingTasks_ByUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        gvPendingTasks.DataSource = ds.Tables[0];
                        gvPendingTasks.DataBind();
                        lblmsg.Text = "";  // Clear message
                    }
                    else
                    {
                        gvPendingTasks.DataSource = null;
                        gvPendingTasks.DataBind();
                        lblmsg.Text = "No pending tasks found.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Error: " + ex.Message;
            }
        }

        protected void gvPendingTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvPendingTasks.SelectedRow;
            int rowIndex = row.RowIndex;

            // Read DataKeys
            string taskId = gvPendingTasks.DataKeys[rowIndex]["TaskID"].ToString();
            string taskTypeId = gvPendingTasks.DataKeys[rowIndex]["TaskTypeID"].ToString();
            string statusId = gvPendingTasks.DataKeys[rowIndex]["StatusID"].ToString();

            // Read and store ScheduleID (Reference ID)
            //object scheduleIdObj = gvPendingTasks.DataKeys[rowIndex].Values["ScheduleID"];
            //object planneddate = gvPendingTasks.DataKeys[rowIndex].Values["Planned Start Date"];
            //if (scheduleIdObj != null && scheduleIdObj != DBNull.Value)
            //{
            //    ViewState["ScheduleID"] = Convert.ToInt32(scheduleIdObj);
            //}
            //else
            //{
            //    ViewState["ScheduleID"] = 0; // No ScheduleID available
            //}
            object scheduleIdObj = gvPendingTasks.DataKeys[rowIndex].Values["ScheduleID"];
            object plannedDateObj = gvPendingTasks.DataKeys[rowIndex].Values["PlannedStartDate"];
            DateTime plannedDate;

            if (plannedDateObj != null && DateTime.TryParse(plannedDateObj.ToString(), out plannedDate))
            {
                if (plannedDate == new DateTime(1900, 1, 1))
                {
                    // It is the default dummy date (1900-01-01)
                    // Treat this as unplanned or ignore
                    ViewState["TimesheetID"] = Convert.ToInt32(scheduleIdObj);
                }
                else
                {
                    // Valid planned date, handle accordingly
                    ViewState["ScheduleID"] = Convert.ToInt32(scheduleIdObj);
                }
            }
            else
            {
                // Null or invalid date
            }


            // Read visible data
            txtEnquiryNo.Text = row.Cells[2].Text;
            txtProjectNo.Text = row.Cells[3].Text;
            txtProjectName.Text = row.Cells[4].Text;
            txtReferenceNo.Text = row.Cells[7].Text;
            txtPlannedStart.Text = row.Cells[8].Text;
            txtPlannedEnd.Text = row.Cells[9].Text;
            txtPlannedHours.Text = row.Cells[10].Text;
            txtCheckedBy.Text = row.Cells[11].Text;
            // Set dropdowns by ID safely
            if (ddTask.Items.FindByValue(taskId) != null)
                ddTask.SelectedValue = taskId;

            if (ddTaskType.Items.FindByValue(taskTypeId) != null)
                ddTaskType.SelectedValue = taskTypeId;

            if (ddStatus.Items.FindByValue(statusId) != null)
                ddStatus.SelectedValue = statusId;

            // Make fields read-only
            txtEnquiryNo.ReadOnly = true;
            txtProjectNo.ReadOnly = true;
            txtProjectName.ReadOnly = true;
            txtReferenceNo.ReadOnly = true;
            txtPlannedStart.ReadOnly = true;
            txtPlannedEnd.ReadOnly = true;
            txtPlannedHours.ReadOnly = true;

            ddTask.Enabled = false;
            ddTaskType.Enabled = false;
        }




        protected void txtActualStart_TextChanged(object sender, EventArgs e)
        {
            // Optional: do something when the date is selected
            // Example: Validate or auto-fill another field
        }
        protected void txtActualEnd_TextChanged(object sender, EventArgs e)
        {
            // Optional: do something when the date is selected
            // Example: Validate or auto-fill another field
        }

    }
}