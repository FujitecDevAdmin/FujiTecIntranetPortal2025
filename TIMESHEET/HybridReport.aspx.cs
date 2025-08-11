using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class HybridReport : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDefaultDateRange();
                LoadDepartments(); // optional: loads dropdown list
                btnExport.Enabled = false;
            }
        }
        private void LoadDepartments()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_hyb_GetDepartments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlDepartment.DataSource = dr;
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentID";
                ddlDepartment.DataBind();

                ddlDepartment.Items.Insert(0, new ListItem("--All--", ""));
            }
        }

        private void SetDefaultDateRange()
        {
            DateTime now = DateTime.Now;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            txtFromDate.Text = firstDay.ToString("yyyy-MM-dd");
            txtToDate.Text = lastDay.ToString("yyyy-MM-dd");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Reset date fields to current month start/end
            SetDefaultDateRange();

            // Reset department dropdown
            ddlDepartment.SelectedIndex = 0;

            // Clear grid
            gvReport.DataSource = null;
            gvReport.DataBind();
            btnExport.Enabled = false;
            // Optional: Clear any messages
            //  lblMessage.Text = "";
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SmartWorkspaceReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // Optional: style header row
                gvReport.HeaderRow.Style.Add("background-color", "#FFFFFF");
                foreach (TableCell cell in gvReport.HeaderRow.Cells)
                {
                    cell.Style["background-color"] = "#f2f2f2";
                }

                gvReport.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for Export to Excel to work
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = DateTime.Parse(txtFromDate.Text);
                DateTime toDate = DateTime.Parse(txtToDate.Text);
                int? departmentId = null;

                if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
                    departmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_hyb_BookingSummaryReport", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);

                        if (departmentId.HasValue)
                            cmd.Parameters.AddWithValue("@DepartmentID", departmentId.Value);
                        else
                            cmd.Parameters.AddWithValue("@DepartmentID", DBNull.Value);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                gvReport.DataSource = dt;
                gvReport.DataBind();
                btnExport.Enabled = true;
            }
            catch (Exception ex) {  }
        }

    }
}