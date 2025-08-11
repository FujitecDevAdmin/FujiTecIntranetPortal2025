using ClosedXML.Excel;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class TimeSheetUploadSchedule : System.Web.UI.Page
    {

      // public string dbConnectionString = ConfigurationManager.ConnectionStrings["dbconnection1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateTemplate_OnClick(object sender, EventArgs e)
        {
            try
            {
                ////Label1.Text = "";
                //string strURL = Server.MapPath("Template/TimesheetSchedulerUpload.xlsm");
                //WebClient req = new WebClient();
                //Response.Clear();
                //Response.ClearContent();
                //Response.ClearHeaders();
                //Response.Buffer = true;
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("Content-Disposition", "attachment;filename=\"TimesheetSchedulerUpload.xlsm" + "\"");
                //byte[] data = req.DownloadData(strURL);
                //Response.BinaryWrite(data);
                //Response.End();

                string strFilePath = Server.MapPath("~/MISReports/Template/TimesheetSchedulerUpload.xlsm");

                if (System.IO.File.Exists(strFilePath))
                {
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=TimesheetSchedulerUpload.xlsm");

                    byte[] fileBytes = System.IO.File.ReadAllBytes(strFilePath);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    // Handle the case when the file doesn't exist or any other error.
                    Response.Write("The requested file does not exist.");
                }

            }
            catch (Exception ex)
            {
                //Label1.ForeColor = System.Drawing.Color.Red;
                //Label1.Text = ex.Message;
            }
        }

        protected void Upload(object sender, EventArgs e)
        {
            try
            {
                UploadExcelAndProcessData();
                
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;

            }
        }

        protected void UploadExcelAndProcessData()
        {
            if (FileUpload1.HasFile)
            {
                string filePath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(filePath);

                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (extension == ".xlsm")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Excelxlsm"].ConnectionString;
                    connectionString = string.Format(connectionString, filePath);

                    DataTable dtExcelData = ExtractExcelData(connectionString);

                    if (dtExcelData != null && dtExcelData.Rows.Count > 0)
                    {
                        string dbConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                        int rowsInserted = InsertDataToDatabase(dtExcelData, dbConnectionString);

                        lblmsg.ForeColor = System.Drawing.Color.DarkGreen;
                        lblmsg.Text = "Total number of rows uploaded: " + rowsInserted;
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "No data found in the Excel file.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select a valid .xlsm file.";
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please select an Excel file to upload.";
            }
        }

        //protected DataTable ExtractExcelData(string connectionString)
        //{
        //    DataTable dtExcelData = new DataTable();

        //    using (OleDbConnection excelCon = new OleDbConnection(connectionString))
        //    {
        //        excelCon.Open();
        //        string sheetName = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

        //        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", excelCon))
        //        {
        //            adapter.Fill(dtExcelData);
        //        }
        //    }

        //    return dtExcelData;
        //}

        protected DataTable ExtractExcelData(string connectionString)
        {
            DataTable dtExcelData = new DataTable();

            using (OleDbConnection excelCon = new OleDbConnection(connectionString))
            {
                excelCon.Open();

                DataTable schemaTable = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Find the sheet name that matches "SCHEDULER"
                string sheetName = schemaTable.AsEnumerable()
                    .Where(row => row.Field<string>("TABLE_NAME").EndsWith("SCHEDULER$"))
                    .Select(row => row.Field<string>("TABLE_NAME"))
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(sheetName))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", excelCon))
                    {
                        adapter.Fill(dtExcelData);
                    }
                }
            }

            return dtExcelData;
        }


        protected int InsertDataToDatabase(DataTable data, string connectionString)
        {
            int columnIndex = 0; // Replace with the correct column index

            foreach (DataRow row in data.Rows)
            {
                for (columnIndex = 0; columnIndex < 15; columnIndex++)
                {
                    string updatedValue = string.Empty;
                    string originalValue = row[columnIndex].ToString();
                    if (originalValue.Contains('~'))
                    {
                        string[] Value = originalValue.Split('~'); // Remove the "~" prefix
                        updatedValue = Value[1];
                        if (columnIndex >0)
                        {
                            int val = int.Parse(updatedValue);
                            updatedValue = val.ToString();
                        }
                    }
                    else
                        updatedValue = originalValue;
                    // Update the value in the DataTable
                    row[columnIndex] = updatedValue;
                }

            }

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlCon))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.Tbl_TS_Scheduling";

                    // Map the columns from Excel to the database table
                    sqlBulkCopy.ColumnMappings.Add("UserId", "UserId");
                    sqlBulkCopy.ColumnMappings.Add("TaskType", "TaskType");
                    sqlBulkCopy.ColumnMappings.Add("Task", "Task");
                    sqlBulkCopy.ColumnMappings.Add("AssigneduserId", "AssigneduserId");
                    sqlBulkCopy.ColumnMappings.Add("Project", "Project");
                    sqlBulkCopy.ColumnMappings.Add("Area", "Area");
                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                    sqlBulkCopy.ColumnMappings.Add("Product", "Product");
                    sqlBulkCopy.ColumnMappings.Add("capacity", "capacity");
                    sqlBulkCopy.ColumnMappings.Add("speed", "speed");
                    sqlBulkCopy.ColumnMappings.Add("Fromdate", "Fromdate");
                    sqlBulkCopy.ColumnMappings.Add("Todate", "Todate");
                    sqlBulkCopy.ColumnMappings.Add("PlannedHours", "PlannedHours");
                    sqlBulkCopy.ColumnMappings.Add("status", "status");
                    //
                    sqlBulkCopy.ColumnMappings.Add("Assignedby", "Assignedby");
                    // ...
                    sqlBulkCopy.WriteToServer(data);
                    return data.Rows.Count;
                }
            }
        }


        protected void Clear_OnClick(object sender, EventArgs e)
        {

            btnDwnldExl.Visible = false;
            //ddcalendarPeriod.SelectedIndex = 0;
            FileUpload1.Dispose();
            lblmsg.Text = "";
            //FileUpload1.Enabled

        }
        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ViewState["DGV"] != null && !ViewState["DGV"].Equals("-1"))
                //{
                //    ExportGridToExcel();
                //}
                ExportGridToExcel();
                //else
                //{
                //    lblmsg.ForeColor = System.Drawing.Color.Red;
                //    lblmsg.Text = "No data to export to excel";
                //}

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void dashboardload()
        {
            try
            {

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

                DataTable dt = (DataTable)ViewState["DGV"];
                if (dt != null)
                {
                    // dt = city.GetAllCity();//your datatable
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        int m = 0;
                        foreach (var ws in wb.Worksheets)
                        {
                            ws.ColumnWidth = 20;
                            // ws.LastColumnUsed().Width = 60;

                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + "Sample" + ".xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            //   XLWorkbook.ColumnWidth                         
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

    }
}