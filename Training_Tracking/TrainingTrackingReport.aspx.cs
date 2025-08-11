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
using System.Transactions;
using ClosedXML.Excel;
using System.IO;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class TrainingTrackingReport : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FromCal.Visible = false;
                Tocal.Visible = false;
                FetchInitializeDetails();
                txtVendorID.Focus();

            }
        }
        protected void ddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("Sp_TrainingTracking_Certification_Report", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TT", SqlDbType.VarChar).Value = ddType.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                           

                            DataRow dr2 = ds.Tables[2].NewRow();
                            dr2.ItemArray = new object[] { 0, "--Select Training--" };
                            ds.Tables[2].Rows.InsertAt(dr2, 0);
                            ddTrainingModule.DataTextField = "TRAININGNAME";
                            ddTrainingModule.DataValueField = "TRAININGID";
                            ddTrainingModule.DataSource = ds.Tables[2];
                            ddTrainingModule.DataBind();
                            ViewState["tm"] = ds.Tables[2];

                        }
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = ex.Message;
                }
                //if (ddType.SelectedValue == "MSC0006")
                //{
                //    ddVendor.Enabled = false;
                //    txtVendorID.Enabled = false;
                //}
                //else
                //{
                //    txtVendorID.Enabled = true;
                //    ddVendor.Enabled = true;
                //    ddEmployee.Enabled = true;
                //    ddTrainingModule.Enabled = true;
                //}

                // txtsubconCompany.Text = ddVendorNo.selected
            }
            catch (Exception ex)
            {

            }
        }
        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Sp_TrainingTracking_Certification_Report", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TT", SqlDbType.VarChar).Value = ddType.SelectedValue;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select VendorId--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddVendor.DataTextField = "VendorName";
                        ddVendor.DataValueField = "VendorID";
                        ddVendor.DataSource = ds.Tables[0];
                        ddVendor.DataBind();
                        ViewState["ven"] = ds.Tables[0];

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Employee--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddEmployee.DataTextField = "Emp_Name";
                        ddEmployee.DataValueField = "Emp_Id";
                        ddEmployee.DataSource = ds.Tables[1];
                        ddEmployee.DataBind();
                        ViewState["emp"] = ds.Tables[1];

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Training--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddTrainingModule.DataTextField = "TRAININGNAME";
                        ddTrainingModule.DataValueField = "TRAININGID";
                        ddTrainingModule.DataSource = ds.Tables[2];
                        ddTrainingModule.DataBind();
                        ViewState["tm"] = ds.Tables[2];

                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void txtVendorID_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtVendorID.Text;
            DataTable dt = (DataTable)ViewState["ven"];
            DataTable filteredDataTable = GetFilteredOptions(filterText, dt, "VendorName");
            PopulateDropDownList(filteredDataTable, "VendorName", "VendorID");
        }

        private void PopulateDropDownList(DataTable filteredDataTable, string colname, string id)
        {
            ddVendor.Items.Clear();
            foreach (DataRow row in filteredDataTable.Rows)
            {
                string text = row[colname].ToString(); // Replace "ColumnName" with the actual column name
                string value = row[id].ToString(); // Replace "Value" with the actual column name

                ListItem item = new ListItem(text, value);
                ddVendor.Items.Add(item);
            }
        }

        private DataTable GetFilteredOptions(string filterText, DataTable dataTable, string fieldname)
        {
            DataRow[] filteredRows = dataTable.Select(fieldname + " LIKE '%" + filterText + "%'");

            if (filteredRows.Length > 0)
            {
                DataTable filteredDataTable = dataTable.Clone();

                foreach (DataRow row in filteredRows)
                {
                    filteredDataTable.ImportRow(row);
                }

                return filteredDataTable;
            }
            else
            {
                return new DataTable();
            }
        }

        protected void txtEmployee_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtEmployee.Text;
            DataTable dt = (DataTable)ViewState["emp"];
            DataTable filteredDataTable = GetFilteredOptions(filterText, dt, "Emp_Name");
            PopulateDropDownList(filteredDataTable, "Emp_Name", "Emp_Id");
        }

        protected void txtTM_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtEmployee.Text;
            DataTable dt = (DataTable)ViewState["tm"];
            DataTable filteredDataTable = GetFilteredOptions(filterText, dt, "TRAININGNAME");
            PopulateDropDownList(filteredDataTable, "TRAININGNAME", "TRAININGID");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DGV.DataSource = null;
                DGV.DataBind();
                Search();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                ddVendor.SelectedIndex = 0;
                txtVendorID.Text = "";
                ddEmployee.SelectedIndex = 0;
                txtEmployee.Text = "";
                ddTrainingModule.SelectedIndex = 0;
                txtTM.Text = "";
                ddType.SelectedIndex = 0;
                FetchInitializeDetails();
                txtVendorID.Focus();
                DGV.DataSource = null;
                DGV.DataBind();
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
                DGV.PageIndex = e.NewPageIndex;
                DGV.DataSource = (DataTable)ViewState["DGV"];
                DGV.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        public void Search()
        {
            try
            {
                lblmsg.Text = "";
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_TrainingTracking_Report", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar).Value = ddVendor.SelectedValue;
                        cmd.Parameters.Add("@subconEmployeename", SqlDbType.VarChar).Value = ddEmployee.SelectedValue;
                        cmd.Parameters.Add("@TM", SqlDbType.VarChar).Value = ddTrainingModule.SelectedValue;
                        cmd.Parameters.Add("@TT", SqlDbType.VarChar).Value = ddType.SelectedValue;
                        if (txtFromDate.Text.Length < 6)
                            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime("01-01-1900");
                        else
                            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = txtFromDate.Text;
                        if (txtTodate.Text.Length < 6)
                            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime("01-01-1900");
                        else
                            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = txtTodate.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ViewState["DGV"] = ds.Tables[0];
                                ///////////////////////////////////////////////////
                                DGV.DataSource = ds.Tables[0];
                                DGV.DataBind();
                                ///////////////////////////////////////////////////  
                                //int index = DGV.Rows.Count - 1;
                                //DGV.Rows[index].Font.Bold = true;
                                ///////////////////////////////////////////////////
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (FromCal.Visible)
                    FromCal.Visible = false;
                else
                    FromCal.Visible = true;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Tocal.Visible)
                    Tocal.Visible = false;
                else
                    Tocal.Visible = true;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void FromCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Text = FromCal.SelectedDate.ToShortDateString();
                FromCal.Visible = false;
                FromCal.SelectedDates.Clear();

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ToCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtTodate.Text = Tocal.SelectedDate.ToShortDateString();
                Tocal.Visible = false;
                Tocal.SelectedDates.Clear();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ToCalendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                if (e.Day.Date <= FromCal.SelectedDate)
                {
                    e.Day.IsSelectable = false;
                    e.Cell.ForeColor = System.Drawing.Color.Gray;
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void FromCalendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                //if (e.Day.Date < DateTime.Now.Date)
                //{
                //    e.Day.IsSelectable = false;
                //    e.Cell.ForeColor = System.Drawing.Color.Gray;
                //}
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
                DataTable dt = (DataTable)ViewState["DGV"];
                if (dt.Rows.Count > 1)
                    ExportDataTableToExcel(dt, "TrainingCertification.xlsx");
                else
                    lblmsg.Text = "No records to Download";
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

    }
}