using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class BusinessSupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Upload(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                lblmsg.Text = "Please select an Excel file to upload.";
                return;
            }

            string fileExt = Path.GetExtension(FileUpload1.FileName);
            if (fileExt != ".xlsx" && fileExt != ".xls")
            {
                lblmsg.Text = "Only Excel files (.xlsx, .xls) are allowed.";
                return;
            }

            try
            {
                using (var stream = new MemoryStream(FileUpload1.FileBytes))
                using (var workbook = new ClosedXML.Excel.XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Skip header + data type row

                    foreach (var row in rows)
                    {
                        string task = row.Cell(1).GetString();
                        int taskType = int.Parse(row.Cell(2).GetValue<string>());
                        string enquiryNo = row.Cell(3).GetString();
                        string project = row.Cell(4).GetString();
                        string projectName = row.Cell(5).GetString();
                        string referenceNo = row.Cell(6).GetString();
                        string assignedUserId = row.Cell(7).GetString();
                        string checkedBy = row.Cell(8).GetString();
                        int status = int.Parse(row.Cell(9).GetValue<string>());
                        DateTime fromDate = row.Cell(10).GetDateTime();
                        DateTime toDate = row.Cell(11).GetDateTime();
                        string assignedBy = row.Cell(12).GetString();
                        float Plannedhours = float.Parse(row.Cell(13).GetValue<string>());

                        SaveToDatabase(task, taskType, enquiryNo, project, projectName, referenceNo,
                                       assignedUserId, checkedBy, status, fromDate, toDate, assignedBy, Plannedhours);
                    }
                    lblmsg.Text = "Data uploaded successfully!";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Error: " + ex.Message;
            }
        }

        private void SaveToDatabase(string task, int taskType, string enquiryNo, string project, string projectName, string referenceNo, string assignedUserId, string checkedBy, int status,DateTime fromDate, DateTime toDate, string assignedBy,float Plannedhours)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbconnection1"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Save_BS_ExcelUpload", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Task", task);
                    cmd.Parameters.AddWithValue("@TaskType", taskType);
                    cmd.Parameters.AddWithValue("@EnquiryNo", enquiryNo);
                    cmd.Parameters.AddWithValue("@Project", project);
                    cmd.Parameters.AddWithValue("@ProjectName", projectName);
                    cmd.Parameters.AddWithValue("@ReferenceNo", referenceNo);
                    cmd.Parameters.AddWithValue("@AssigneduserId", assignedUserId);
                    cmd.Parameters.AddWithValue("@Checkedby", checkedBy);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Fromdate", fromDate);
                    cmd.Parameters.AddWithValue("@Todate", toDate);
                    cmd.Parameters.AddWithValue("@Assignedby", assignedBy);
                    cmd.Parameters.AddWithValue("@Plannedhours", Plannedhours);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void GenerateTemplate_OnClick(object sender, EventArgs e)
        {
            using (var workbook = new XLWorkbook())
            {
                // Sheet 1: TimesheetTemplate
                var worksheet = workbook.Worksheets.Add("TimesheetTemplate");

                worksheet.Cell(1, 1).Value = "Task";
                worksheet.Cell(1, 2).Value = "TaskType";
                worksheet.Cell(1, 3).Value = "EnquiryNo";
                worksheet.Cell(1, 4).Value = "Project";
                worksheet.Cell(1, 5).Value = "ProjectName";
                worksheet.Cell(1, 6).Value = "ReferenceNo";
                worksheet.Cell(1, 7).Value = "AssigneduserId";
                worksheet.Cell(1, 8).Value = "Checkedby";
                worksheet.Cell(1, 9).Value = "status";
                worksheet.Cell(1, 10).Value = "Fromdate";
                worksheet.Cell(1, 11).Value = "Todate";
                worksheet.Cell(1, 12).Value = "Assignedby";
                worksheet.Cell(1, 13).Value = "PlannedHours";

                worksheet.Column(6).Style.NumberFormat.SetFormat("@");
                worksheet.Column(7).Style.NumberFormat.SetFormat("@");
                worksheet.Column(8).Style.NumberFormat.SetFormat("@");
                worksheet.Column(12).Style.NumberFormat.SetFormat("@");
                // Set column format for Fromdate (J) and Todate (K)
                worksheet.Column(10).Style.DateFormat.Format = "dd-MM-yyyy";
                worksheet.Column(11).Style.DateFormat.Format = "dd-MM-yyyy";
                var headerRange = worksheet.Range("A1:M1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

                worksheet.Columns().AdjustToContents();

                // Sheet 2: MasterData
                var masterSheet = workbook.Worksheets.Add("MasterData");
                int currentRow = 1;

                // --- Task List ---
                masterSheet.Cell(currentRow, 1).Value = "ID";
                masterSheet.Cell(currentRow, 2).Value = "TaskName";
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                currentRow++;

                var taskList = new Dictionary<int, string>
                {
                    {1073, "Estimation"},
                    {1074, "Technical Inquiry"},
                    {1075, "Scheduling"},
                    {1076, "Order Booking"},
                    {1077, "Cost Control"},
                    {1078, "Layout Coordination"},
                    {1079, "FPT"},
                    {1080, "Supply Service"},
                };

                foreach (var task in taskList)
                {
                    masterSheet.Cell(currentRow, 1).Value = task.Key;
                    masterSheet.Cell(currentRow, 2).Value = task.Value;
                    currentRow++;
                }

                currentRow += 2; // Gap

                // --- TaskType List ---
                masterSheet.Cell(currentRow, 1).Value = "ID";
                masterSheet.Cell(currentRow, 2).Value = "TaskTypeName";
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightBlue;
                currentRow++;

                var taskTypeList = new Dictionary<int, string>
                {
                    {1007, "Domestic"},
                    {1008, "Export"},
                    {1009, "MOD / FOD / SERVICE"},
                    {1010, "New Order Booking"},
                    {1011, "Variation Order"},
                    {1012, "Revalidation"},
                    {1013, "Reports"},
                    {1014, "RFM"},
                    {1015, "Forecast"},
                    {1016, "Backlog Updation"},
                    {1017, "New GAD Request"},
                    {1018, "Rev GAD Request"},
                    {1019, "S Prodcut GAD"},
                    {1020, "Standard Cost Working"},
                    {1021, "Variance Analysis"},
                    {1022, "Meeting"},
                    {1023, "Escalator"},
                    {1024, "Fujitec Express"},
                    {1025, "Tracker Updation"},
                };

                foreach (var taskType in taskTypeList)
                {
                    masterSheet.Cell(currentRow, 1).Value = taskType.Key;
                    masterSheet.Cell(currentRow, 2).Value = taskType.Value;
                    currentRow++;
                }

                currentRow += 2; // Gap

                // --- Status List ---
                masterSheet.Cell(currentRow, 1).Value = "ID";
                masterSheet.Cell(currentRow, 2).Value = "StatusName";
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                masterSheet.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightCoral;
                currentRow++;

                var statusList = new Dictionary<int, string>
                {
                    {1, "Inprogress"},
                    {2, "Completed"},
                    {3, "Not yet started"},
                    {5, "On Hold"},
                };

                foreach (var status in statusList)
                {
                    masterSheet.Cell(currentRow, 1).Value = status.Key;
                    masterSheet.Cell(currentRow, 2).Value = status.Value;
                    currentRow++;
                }

                masterSheet.Columns().AdjustToContents();

                // Export
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    byte[] bytes = stream.ToArray();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=TimesheetTemplate.xlsx");
                    Response.BinaryWrite(bytes);
                    Response.End();
                }
            }
        }


        // 3. Download last uploaded file
        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Uploads/LastUploaded.xlsx");
            if (File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("Content-Disposition", "attachment; filename=LastUploaded.xlsx");
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                lblmsg.Text = "No file available to download.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        // 4. Clear message
        protected void Clear_OnClick(object sender, EventArgs e)
        {
            lblmsg.Text = string.Empty;
        }
    }
}