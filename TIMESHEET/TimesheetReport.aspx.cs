using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class TimesheetReport : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public DataSet dtst;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FromCal.Visible = false;
                    Tocal.Visible = false;
                    FetchInitializeDetails();
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

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSReportPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString(); ;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {

                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { "Select Project", "--Select Project--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddProject.DataTextField = "ProjectName";
                        ddProject.DataValueField = "ProjectName";
                        ddProject.DataSource = ds.Tables[0];
                        ddProject.DataBind();
                        ViewState["Proj"] = ds.Tables[0];

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select User--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        dduser.DataTextField = "Username";
                        dduser.DataValueField = "Userid";
                        dduser.DataSource = ds.Tables[1];
                        dduser.DataBind();
                        ViewState["WrkUser"] = ds.Tables[1];

                        //DataRow dr2 = ds.Tables[2].NewRow();
                        //dr2.ItemArray = new object[] { 0, "Select Area" };
                        //ds.Tables[2].Rows.InsertAt(dr2, 0);
                        //ddArea.DataTextField = "AreaName";
                        //ddArea.DataValueField = "ID";
                        //ddArea.DataSource = ds.Tables[2];
                        //ddArea.DataBind();

                        DataRow dr3 = ds.Tables[3].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select Task--" };
                        ds.Tables[3].Rows.InsertAt(dr3, 0);
                        ddTask.DataTextField = "TaskName";
                        ddTask.DataValueField = "ID";
                        ddTask.DataSource = ds.Tables[3];
                        ddTask.DataBind();

                        DataRow dr4 = ds.Tables[4].NewRow();
                        dr4.ItemArray = new object[] { 0, "--Select Task Type--" };
                        ds.Tables[4].Rows.InsertAt(dr4, 0);
                        ddTaskType.DataTextField = "TaskTypeName";
                        ddTaskType.DataValueField = "ID";
                        ddTaskType.DataSource = ds.Tables[4];
                        ddTaskType.DataBind();

                        DataRow dr5 = ds.Tables[5].NewRow();
                        dr5.ItemArray = new object[] { 0, "--Select Status--" };
                        ds.Tables[5].Rows.InsertAt(dr5, 0);
                        ddStatus.DataTextField = "StatusName";
                        ddStatus.DataValueField = "ID";
                        ddStatus.DataSource = ds.Tables[5];
                        ddStatus.DataBind();

                        //ViewState["CurrentTable"] = ds.Tables[6];
                        //gv.DataSource = ds.Tables[6];
                        //gv.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["DGV"] != null && !ViewState["DGV"].Equals("-1"))
                {
                    ExportGridToExcel();
                }
                // ExportGridToExcel();
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please load data before export to excel";
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void ExportGridToExcel()
        {
            try
            {
                string strfilename = "";
               
                dtst = new DataSet();               

                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = (DataTable)ViewState["DGV"];
                    if (dt != null)
                    {
                        dt.TableName = "Timesheet";
                        dtst.Tables.Add(dt);

                        foreach (DataTable dt11 in dtst.Tables)
                        {
                            var worksheet = wb.Worksheets.Add(dt11);

                            // Set center alignment for the header row
                            worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        foreach (var ws in wb.Worksheets)
                        {
                            // Set center alignment for all columns' header cells
                            foreach (var col in ws.Columns())
                            {
                                col.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }

                            ws.ColumnWidth = 20;
                            ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        }
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TimeSheetReport" + strfilename.Trim() + ".xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //if ((ddWareHouse.SelectedIndex > 0) && (txtFromDate.Text.Length > 0) && (txtTodate.Text.Length > 0))
                Search();
                //else
                //    lblmsg.Text = "Please select the Mandatory Fields";
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
                        SqlCommand cmd = new SqlCommand("SP_TimesheetReport", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                       // cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = "";
                        cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = dduser.SelectedValue;
                        if (ddProject.SelectedValue == "Select Project")
                            cmd.Parameters.Add("@project", SqlDbType.VarChar).Value = "";
                        else
                            cmd.Parameters.Add("@project", SqlDbType.VarChar).Value = ddProject.SelectedValue;
                        if (ddTaskType.SelectedValue == "0")
                            cmd.Parameters.Add("@TaskType", SqlDbType.VarChar).Value = "";
                        else
                            cmd.Parameters.Add("@TaskType", SqlDbType.VarChar).Value = ddTaskType.SelectedValue;
                        if (ddTask.SelectedValue == "0")
                            cmd.Parameters.Add("@Task", SqlDbType.VarChar).Value = "";
                        else
                            cmd.Parameters.Add("@Task", SqlDbType.VarChar).Value = ddTask.SelectedValue;
                        if (txtFromDate.Text.Length < 6)
                            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime("01-01-1900");
                        else
                            cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = txtFromDate.Text;
                        if (txtTodate.Text.Length < 6)
                            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime("01-01-1900");
                        else
                            cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = txtTodate.Text;
                        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = txtArea.Text;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        cmd.Parameters.Add("@Assignedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); 
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtAssignedUser.Text = "";
                txtProjectFilter.Text = "";
                ddProject.SelectedIndex = 0;
                ddTask.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
                DGV.DataSource = null;
                DGV.DataBind();
                ViewState["DGV"] = null;
                lblmsg.Text = "";
                FetchInitializeDetails();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                //    {
                //        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSExceldetail", sqlConnection);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = selectedval;
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataSet ds = new DataSet();
                //        da.Fill(ds);
                //        if (ds != null)
                //        {
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                string strfilename = "";
                //                //if ((txtLiftNo.Text != String.Empty))
                //                //{
                //                //    strfilename = txtLiftNo.Text;
                //                //}
                //                //else
                //                //{
                //                //    if (txtProjectNo.Text != String.Empty)
                //                //        strfilename = txtProjectNo.Text;
                //                //    else
                //                //    {
                //                //        strfilename = "_";
                //                //    }
                //                //}
                //                DataTable dt = ds.Tables[0];
                //                if (dt != null)
                //                {
                //                    // dt = city.GetAllCity();//your datatable
                //                    using (XLWorkbook wb = new XLWorkbook())
                //                    {
                //                        wb.Worksheets.Add(dt);
                //                        int m = 0;
                //                        foreach (var ws in wb.Worksheets)
                //                        {
                //                            ws.ColumnWidth = 20;
                //                            // ws.LastColumnUsed().Width = 60;

                //                        }

                //                        Response.Clear();
                //                        Response.Buffer = true;
                //                        Response.Charset = "";
                //                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //                        Response.AddHeader("content-disposition", "attachment;filename=MISInventory_Rpt" + strfilename.Trim() + ".xlsx");

                //                        using (MemoryStream MyMemoryStream = new MemoryStream())
                //                        {
                //                            //   XLWorkbook.ColumnWidth                         
                //                            wb.SaveAs(MyMemoryStream);
                //                            MyMemoryStream.WriteTo(Response.OutputStream);
                //                            Response.Flush();
                //                            Response.End();
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    scope.Complete();
                //}
            }
            catch (Exception ex)
            { }

        }
        protected void txtProjectFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtProjectFilter.Text;
            DataTable dt = (DataTable)ViewState["Proj"];
            DataTable filteredDataTable = GetFilteredOptions(filterText, dt);
            PopulateDropDownList(filteredDataTable);
        }

        private DataTable GetFilteredOptions(string filterText, DataTable dataTable)
        {
            DataRow[] filteredRows = dataTable.Select("ProjectName LIKE '%" + filterText + "%'");

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

        private void PopulateDropDownList(DataTable filteredDataTable)
        {
            ddProject.Items.Clear();
            foreach (DataRow row in filteredDataTable.Rows)
            {
                string text = row["ProjectName"].ToString(); // Replace "ColumnName" with the actual column name
                string value = row["ProjectName"].ToString(); // Replace "Value" with the actual column name

                ListItem item = new ListItem(text, value);
                ddProject.Items.Add(item);
            }
        }

        protected void txtAssignedFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtAssignedUser.Text;
            DataTable dt = (DataTable)ViewState["WrkUser"];
            DataTable filteredDataTable = GetFilteredUserOptions(filterText, dt);
            PopulateuserDropDownList(filteredDataTable);
        }

        private DataTable GetFilteredUserOptions(string filterText, DataTable dataTable)
        {
            DataRow[] filteredRows = dataTable.Select("Username LIKE '%" + filterText + "%'");

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

        private void PopulateuserDropDownList(DataTable filteredDataTable)
        {
            dduser.Items.Clear();
            foreach (DataRow row in filteredDataTable.Rows)
            {
                string text = row["Username"].ToString(); // Replace "ColumnName" with the actual column name;
                string value = row["Userid"].ToString(); // Replace "Value" with the actual column name;
                ListItem item = new ListItem(text, value);
                dduser.Items.Add(item);
            }
        }
        //protected void DGV_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        string colcount = e.Row.Cells.Count.ToString();
        //        for (int j = 0; j < int.Parse(colcount); j++)
        //        {
        //            if (e.Row.RowType == DataControlRowType.DataRow)
        //            {
        //                e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Right;
        //                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        //                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //                e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblmsg.ForeColor = System.Drawing.Color.Red;
        //        lblmsg.Text = ex.Message;
        //    }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //Label lblTotalPrice = (Label)e.Row.FindControl("Salary");
        //    //lblTotalPrice.Text = m.ToString();
        //   // DGV.FooterRow.Cells[1].Text = "Grand Total";
        //    int sum = 0;
        //    for (int i = 0; i < DGV.Rows.Count; ++i)
        //    {
        //        sum += Convert.ToInt32(DGV.Rows[i].Cells[2].Text);
        //    }
        //    lblmsg.Text = sum.ToString();
        //}
        // }
    }
}