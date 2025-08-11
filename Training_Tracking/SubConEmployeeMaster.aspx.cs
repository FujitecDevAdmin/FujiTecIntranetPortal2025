using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Document = iTextSharp.text.Document;
using Rectangle = iTextSharp.text.Rectangle;
using PageSize = iTextSharp.text.PageSize;
using Image = iTextSharp.text.Image;
using Paragraph = iTextSharp.text.Paragraph;
using PdfFont = iTextSharp.text.pdf.PdfFont;
using Font = iTextSharp.text.Font;
using Syncfusion.JavaScript.DataVisualization.Models.Diagram;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Mime;
using ClosedXML.Excel;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubConEmployeeMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        
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
                    SqlCommand cmd = new SqlCommand("SP_SubConEmployeeMasterPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Employee--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddEmployeeCategory.DataTextField = "EmployeeCategoryDesc";
                        ddEmployeeCategory.DataValueField = "EmployeeCategoryType";
                        ddEmployeeCategory.DataSource = ds.Tables[0];
                        ddEmployeeCategory.DataBind();

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select VendorId--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddVendorNo.DataTextField = "VendorName";
                        ddVendorNo.DataValueField = "ID";
                        ddVendorNo.DataSource = ds.Tables[1];
                        ddVendorNo.DataBind();

                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[3];
                        ddStatus.DataBind();

                        DataRow dr2 = ds.Tables[4].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select State--" };
                        ds.Tables[4].Rows.InsertAt(dr2, 0);
                        DDSTATE.DataTextField = "STATENAME";
                        DDSTATE.DataValueField = "STATECODE";
                        DDSTATE.DataSource = ds.Tables[4];
                        DDSTATE.DataBind();

                        ViewState["GV"] = ds.Tables[2];
                        gv.DataSource = ds.Tables[2];
                        gv.DataBind();
                        ViewState["export"] = ds.Tables[2];
                        ViewState["GV1"] = ds.Tables[5];
                        if (ds.Tables[6].Rows.Count > 0)
                            ViewState["email"] = ds.Tables[6].Rows[0]["EmailId"].ToString();
                        else
                            ViewState["email"] = string.Empty;
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
                    string fileName = string.Empty;
                    if (txtEmployeeID.Text.Length > 2 )
                    {
                        if (FileUpload1.HasFile && IsImage(FileUpload1.FileName))
                        {
                            fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);//txtEmployeeID.Text;//
                            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/PDFIDCARD/Photo/") + fileName);
                            ViewState["Photo"] = fileName;
                        }
                        else
                        {
                            fileName = ViewState["Photo"] as string; //Server.MapPath("~/PDFIDCARD/Photo/") + fileName;
                        }
                    }
                    else
                    {
                        fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);//txtEmployeeID.Text;//
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/PDFIDCARD/Photo/") + fileName);
                    }
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SubcontractEmployee_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Vendor_Id", SqlDbType.VarChar).Value = (ddVendorNo.SelectedValue);
                        cmd.Parameters.Add("@Emp_Id", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = ddEmployeeCategory.SelectedValue;
                        cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = txtEmployeeName.Text;// dtpDate.Text;
                        cmd.Parameters.Add("@ESINo", SqlDbType.VarChar).Value = txtESINo.Text;
                        cmd.Parameters.Add("@UNINo", SqlDbType.VarChar).Value = txtUNINO.Text;
                        cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar).Value = (txtPhoneNo.Text);
                        cmd.Parameters.Add("@Statecode", SqlDbType.VarChar).Value = (DDSTATE.SelectedValue);
                        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = (txtLocation.Text);
                        cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@BG", SqlDbType.VarChar).Value = txtBloodGroup.Text;
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        cmd.Parameters.Add("@Photo", SqlDbType.VarChar).Value = fileName;
                        //
                        DateTime? validUptoDate = string.IsNullOrEmpty(txtvalidDate.Text)? (DateTime?)null: DateTime.Parse(txtvalidDate.Text);
                        //
                        cmd.Parameters.Add("@ValidUptoDate", SqlDbType.Date).Value = validUptoDate;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Employee Master for " + txtEmployeeID.Text + " -- " + txtEmployeeName.Text + " Created successfully";
                                txtEmployeeID.Text = ds.Tables[0].Rows[0]["EMPID"].ToString();

                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Employee Master For " + txtEmployeeID.Text + " -- " + txtEmployeeName.Text + " Update  successfully";
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

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                //if ((txtEmployeeID.Text == string.Empty))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee ID should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                if ((txtEmployeeName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddEmployeeCategory.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Employee category";
                    ViewState["Error"] = "Error";
                }
                if ((txtLocation.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Location should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((DDSTATE.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* State should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddVendorNo.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Vendor";
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
                //testIdCard();
                lblmsg.Text = "";
                txtEmployeeID.Text = "";
                txtEmployeeName.Text = "";
                ddEmployeeCategory.SelectedIndex = 0;
                txtESINo.Text = "";
                txtUNINO.Text = "";
                txtPhoneNo.Text = "";
                ddVendorNo.SelectedIndex = 0;
                ddStatus.SelectedIndex = 0;
                DDSTATE.SelectedIndex = 0;
                txtLocation.Text = "";
                ddselect.SelectedIndex = 0;
                txtSearch.Text = "";
                txtsubconCompany.Text = "";
                txtBloodGroup.Text = "";
                txtvalidDate.Text = "";
                FetchInitializeDetails();
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
                DataTable dt = (DataTable)ViewState["export"];
                ExportDataTableToExcel(dt, "VendorMaster.xlsx");
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

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (ddselect.SelectedIndex != 0)
                    dt = (DataTable)ViewState["GVS1"];
                else
                    dt = (DataTable)ViewState["GV1"];
                gv.DataSource = dt;
                gv.DataBind();

                lblmsg.Text = "";
                txtEmployeeID.Text = gv.SelectedRow.Cells[1].Text;
                txtEmployeeName.Text = gv.SelectedRow.Cells[2].Text;
                ddEmployeeCategory.SelectedValue = gv.SelectedRow.Cells[3].Text;
                txtESINo.Text = gv.SelectedRow.Cells[5].Text;
                txtUNINO.Text = gv.SelectedRow.Cells[6].Text;
                txtPhoneNo.Text = gv.SelectedRow.Cells[7].Text;
                ddVendorNo.SelectedValue = gv.SelectedRow.Cells[8].Text;
                if ((gv.SelectedRow.Cells[10].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[10].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[10].Text;
                txtLocation.Text = gv.SelectedRow.Cells[12].Text;
                DDSTATE.SelectedValue = gv.SelectedRow.Cells[13].Text;
                txtBloodGroup.Text = gv.SelectedRow.Cells[15].Text;
                ViewState["Photo"] = gv.SelectedRow.Cells[16].Text;
                ViewState["DOJ"] = gv.SelectedRow.Cells[17].Text;
                ViewState["emailk"] = gv.SelectedRow.Cells[18].Text;
                string[] splitmodule = ddVendorNo.SelectedItem.ToString().Split('-');
                string txtcompany = splitmodule[0];
                txtsubconCompany.Text = txtcompany;
                // txtvalidDate.Text = gv.SelectedRow.Cells[19].Text;
                string val = gv.SelectedRow.Cells[19].Text;
                DateTime parsedDate;
                string[] formats = { "dd-MM-yyyy", "dd-MM-yyyy HH:mm:ss" };

                if (DateTime.TryParseExact(gv.SelectedRow.Cells[19].Text, formats, null, System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    txtvalidDate.Text = parsedDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtvalidDate.Text = "";
                }


                DataTable dt1 = new DataTable();
                dt1 = (DataTable)ViewState["GV"];
                gv.DataSource = dt1;
                gv.DataBind();

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void BindGrid()
        {
            //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_EmployeedetailSearch", sqlConnection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@selectConEmp", SqlDbType.VarChar).Value = ddselect.SelectedValue;
                cmd.Parameters.Add("@VENDORName", SqlDbType.VarChar).Value = txtSearch.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    ViewState["GVS"] = ds.Tables[0];
                    ViewState["GVS1"] = ds.Tables[1];
                    gv.DataSource = ds.Tables[0];
                    gv.DataBind();
                }
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

        //protected void printButton_Click(object sender, EventArgs e)
        //{
        //    // Print the web form
        //    PrintDocument pd = new PrintDocument();
        //    pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        //    pd.Print();
        //}
        //private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    // Set the dimensions of the bitmap to match the content you want to print
        //    Bitmap bmp = new Bitmap(800, 600);

        //    // Render the web control to the bitmap
        //    using (Graphics g = Graphics.FromImage(bmp))
        //    {
        //        Point location = webControl.PointToScreen(Point.Empty);
        //        g.CopyFromScreen(location, Point.Empty, bmp.Size);
        //    }

        //    // Draw the bitmap to the printer page
        //    e.Graphics.DrawImage(bmp, 0, 0);
        //}

        private bool IsImage(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return ext == ".jpg" || ext == ".jpeg";// || ext == ".png" || ext == ".gif";
        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && IsImage(FileUpload1.FileName))
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;
                decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                if (size > 100)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "File size should be less than 100kb";
                }


                //string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/EmployeePhoto/") + fileName);
                //string strname = FileUpload1.FileName.ToString();
                // FileUpload1.PostedFile.SaveAs(Server.MapPath("Emp_Image/") + strname);
                //FileUpload1.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["Emp_Image"].ToString()) + strname);
                //int imagefilelenth = FileUpload1.PostedFile.ContentLength;
                //byte[] imgarray = new byte[imagefilelenth];
                //HttpPostedFile image = FileUpload1.PostedFile;
                //image.InputStream.Read(imgarray, 0, imagefilelenth);
                //lblmsg.Text = FileUpload1.FileName.ToString();
                //Img = FileUpload1.PostedFile;
                //connection();
                //query = "Insert into ImageToDB (ImageName, Image) values (@Name, @Image)";
                //SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = TextBox1.Text;
                //com.Parameters.AddWithValue("@Image", SqlDbType.Image) Value = imgarray;
                //com.ExecuteNonQuery();
                //Label1.Visible = true;
                //Label1.Text = "Image Is Uploaded successfully";
                //imagebindGrid();
            }
            else
                lblmsg.Text = "Please select a valid image file '.jpg'";
        }

        public void FetchDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConEmployeeMasterPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Employee--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddEmployeeCategory.DataTextField = "EmployeeCategoryDesc";
                        ddEmployeeCategory.DataValueField = "EmployeeCategoryType";
                        ddEmployeeCategory.DataSource = ds.Tables[0];
                        ddEmployeeCategory.DataBind();

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select VendorId--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddVendorNo.DataTextField = "VendorName";
                        ddVendorNo.DataValueField = "ID";
                        ddVendorNo.DataSource = ds.Tables[1];
                        ddVendorNo.DataBind();

                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[3];
                        ddStatus.DataBind();

                        DataRow dr2 = ds.Tables[4].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select State--" };
                        ds.Tables[4].Rows.InsertAt(dr2, 0);
                        DDSTATE.DataTextField = "STATENAME";
                        DDSTATE.DataValueField = "STATECODE";
                        DDSTATE.DataSource = ds.Tables[4];
                        DDSTATE.DataBind();

                        ViewState["GV"] = ds.Tables[2];
                        gv.DataSource = ds.Tables[2];
                        gv.DataBind();

                        ViewState["GV1"] = ds.Tables[5];
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text.Length > 1)
                {
                    testIdCard();
                    sendmail();
                }
                else
                    lblmsg.Text = "Please select valid record from gridview to print.";
                //string Body = "Dear Sir/Madam,<br /><br />";
                //Body = Body + "This is to inform you about the pending Schedule status <br /><br />";
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////----------------------------
                //Body = Body + "<b>Branch Wise Summary - Pending units for GAD Request[Won to GAD Request]:</b><br />";
                ////Body = Body + "<b>Note:</b><b style=\"font-family:Calibri, sans-serif;font-weight:bold;color:red;\"> The below summary of cost exclude labour and DLP reversal in both forecast and actual.</b> <br /><br />";
                ////Body = Body + "<div>";
                //Body = Body + "<table border=\"0\" style=\"width:320px;height: 200px; margin:2px;padding: 20px; border:1px solid #6cb5d9; border-collapse:collapse;box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);\">";
                //Body = Body + "<thead style=\"margin:0;padding:0;background-color:#d9edf7;\">";
                //Body = Body + "<tr><td align=center colspan=2 style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;font-weight:bold;\">IDENTITY CARD</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri,Helvetica,Sans-serif; font-size:14px;font-weight:bold;\">Name</td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtLocation.Text + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Agency Name </td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + ddVendorNo.SelectedValue + "</td></tr>";
                //Body = Body + "<tr><td style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri,Helvetica,Sans-serif; font-size:14px;font-weight:bold;\">Detail</td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + ddEmployeeCategory.SelectedValue + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Contact Person </td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtsubconCompany.Text + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Contact No </td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtPhoneNo.Text + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Blood Group </td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtBloodGroup.Text + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Date Of Issue </td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + DateTime.Now.ToString() + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">ESI No</td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtESINo.Text.ToString() + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">UAN No</td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtUNINO.Text.ToString() + "</td></tr>";
                //Body = Body + "<tr><td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Valid Upto</td>";
                //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + DateTime.Now.ToString() + "</td></tr>";
                //Body = Body + "<tr><td align=Right colspan=2 style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Signature of Main Contractor</td></tr>";
                //Body = //Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtUNINO.Text.ToString() + "</td></tr>";
                ////Body = Body + "<td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif;font-size:14px;font-weight:bold;\">No.of Units</td>";
                //Body = Body + "</tr></thead><tbody style=\"margin:0;padding:0;background-color:#fff;\">";

                ////  string contractno = dt.Rows[0]["ContractNo"].ToString();
                ////for (int i = 0; i < dt3.Rows.Count; i++)
                ////{
                ////    Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + dt3.Rows[i]["Branch"].ToString() + "</td>";
                ////    Body = Body + "<td align=right style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif;font-size:14px;font-weight:bold;\">" + dt3.Rows[i]["ProjCount"].ToString() + "</td></tr>";
                ////}
                //Body = Body + "</tbody></table><br />";

                //Body = string.Empty;
                ////Body = @"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head><meta charset=\"UTF-8\">\r\n  <title>Identity Card</title>\r\n  <style>  \r\n.card {\r\n  width: 400px;\r\n  height: 310px;\r\n  border: 2px solid #ccc;\r\n  border-radius: 10px;\r\n  box-shadow: 5px 5px 10px #888888;\r\n  overflow: hidden;\r\n  display: flex;\r\n  font-family: 'Open Sans', sans-serif; \r\n}\r\n\r\n.card .left {\r\n  flex: 1; \r\n  padding: 10px;\r\n  display: flex;\r\n  flex-direction: column;\r\n  justify-content: flex-end;\r\n  \r\n}\r\n\r\n.card .left h2, .card .left p {\r\n  margin: 0;\r\n  padding: 0;\r\n  font-size: 15px; \r\n  margin-left:8px;\r\n\r\n}\r\n\r\n.card .left h2 {\r\n  font-weight: bold;\r\n}\r\n\r\n.card .left .detail {\r\n  font-weight: bold;\r\n  font-size: 15px; \r\n}\r\n\r\n\r\n.card .right {\r\n  flex: 1;\r\n  padding: 10px;\r\n  display: flex;\r\n  flex-direction: column;\r\n  justify-content: space-between;\r\n}\r\n\r\n.card .right h2, .card .right p {\r\n  margin: 0;\r\n  padding: 0;\r\n  font-size: 15px; \r\n}\r\n\r\n.card .right h2 {\r\n  font-weight: bold;\r\n}\r\n\r\n.card .right .signature {\r\n  align-self: flex-end;\r\n}\r\n\r\n\r\n.card img {\r\n margin-left:5px;\r\nmargin-top:5px;\r\nmargin-bottom:5px;\r\n  width: 150px;\r\n  height: 200px;\r\n  border: 1px solid Ash;\r\n  border-radius: 10px ;\r\n \r\n}\r\n .card .right .signature {\r\n      align-self: flex-end;\r\n    }\r\n\r\n  </style>\r\n</head>\r\n<body>\r\n  <div class=\"card\">\r\n    <div class=\"left\">\r\n      <img src=\"https://via.placeholder.com/150x150.png?text=Photo\" alt=\"Photo\">\r\n      <div>\r\n        <h2>Name:</h2>\r\n        <p>Vinoth</p>\r\n        <h2>Agency Name:</h2>\r\n        <p>Sensoft</p>\r\n        <h2>Detail:</h2>\r\n        <p >Technician</p>\r\n      </div>\r\n    </div>\r\n    <div class=\"right\">\r\n      <div>\r\n        <h2>Contact Person:</h2>\r\n        <p>ARUN</p>\r\n        <h2>Contact No:</h2>\r\n        <p>8934881112</p>\r\n        <h2>Blood Group:</h2>\r\n        <p>o+ve</p>\r\n      </div>\r\n      <div>\r\n        <h2>Date Of Issue:</h2>\r\n        <p>07-04-2023 16:13:42</p>\r\n        <h2>ESI No:</h2>\r\n        <p>5215958585</p>\r\n        <h2>UAN No:</h2>\r\n        <p>12342</p>\r\n        <h2>Valid Upto:</h2>\r\n        <p>07-04-2023 16:13:43</p>\r\n      </div>\r\n      <div>\r\n        <h2>Signature:</h2>\r\n        </br></br>\r\n        <p> </p>\r\n        </div>\r\n    </div>\r\n  </div>\r\n</body>\r\n</html>";

                ////StringBuilder html = new StringBuilder();

                ////html.Append("<!DOCTYPE html>");
                ////html.Append("<html lang=\"en\">");
                ////html.Append("<head>");
                ////html.Append("<meta charset=\"UTF-8\">");
                ////html.Append("<title>Identity Card</title>");
                //////html.Append("<style>");
                ////// CSS styles here
                //////html.Append(".card {width: 400px;height: 310px;border: 2px solid #ccc;border-radius: 10px;box-shadow: 5px 5px 10px #888888;overflow: hidden;display: flex;font-family: 'Open Sans', sans-serif; }");
                //////html.Append(".card .left {flex: 1;padding: 10px;display: flex;flex-direction: column;justify-content: flex-end;}");
                //////html.Append(".card .left h2, .card .left p {margin: 0;padding: 0;font-size: 15px;margin-left:8px;}");
                //////html.Append(".card .left h2 {font-weight: bold;}");
                //////html.Append(".card .left .detail {font-weight: bold;font-size: 15px;}");
                //////html.Append(".card .right { flex: 1;padding: 10px;display: flex;flex-direction: column;justify-content: space-between;}");
                //////html.Append(".card .right h2, .card .right p { margin: 0; padding: 0; font-size: 15px;}");
                //////html.Append(".card .right h2 { font-weight: bold;}");
                //////html.Append(".card .right .signature { align-self: flex-end;}");
                //////html.Append(".card img { margin-left:5px;margin-top:5px;margin-bottom:5px; width: 150px; height: 200px;  border: 1px solid Ash;  border-radius: 10px ;}");
                //////html.Append(".card .right .signature { align-self: flex-end;}");
                ////// CSS styles here
                //////html.Append("</style>");
                ////html.Append("</head>");
                ////html.Append("<body>");
                ////html.Append(" <div style=\"width: 400px; height: 310px; border: 2px solid #ccc; border-radius: 10px; box-shadow: 5px 5px 10px #888888; overflow: hidden; display: flex; font-family: 'Open Sans', sans-serif;\">");
                ////html.Append("<div style=\"flex: 1; padding: 10px; display: flex; flex-direction: column; justify-content: flex-end;\">");
                ////html.Append("<img src=\"https://via.placeholder.com/150x150.png?text=Photo\" alt=\"Photo\">");
                ////html.Append("<div>");
                ////html.Append("<h2>Name:</h2>");
                ////html.Append("<p>Vinoth</p>");
                ////html.Append("<h2>Agency Name:</h2>");
                ////html.Append("<p>Sensoft</p>");
                ////html.Append("<h2>Detail:</h2>");
                ////html.Append("<p>Technician</p>");
                ////html.Append("</div>");
                ////html.Append("</div>");
                ////html.Append("<div class=\"right\">");
                ////html.Append("<div>");
                ////html.Append("<h2>Contact Person:</h2>");
                ////html.Append("<p>ARUN</p>");
                ////html.Append("<h2>Contact No:</h2>");
                ////html.Append("<p>8934881112</p>");
                ////html.Append("<h2>Blood Group:</h2>");
                ////html.Append("<p>o+ve</p>");
                ////html.Append("</div>");
                ////html.Append("<div>");
                ////html.Append("<h2>Date Of Issue:</h2>");
                ////html.Append("<p>07-04-2023 16:13:42</p>");
                ////html.Append("<h2>ESI No:</h2>");
                ////html.Append("<p>5215958585</p>");
                ////html.Append("<h2>UAN No:</h2>");
                ////html.Append("<p>12342</p>");
                ////html.Append("<h2>Valid Upto:</h2>");
                ////html.Append("<p>07-04-2023 16:13:43</p>");
                ////html.Append("</div>");
                ////html.Append("<div>");
                ////html.Append("<h2>Signature:</h2>");
                ////html.Append("</br></br>");
                ////html.Append("<p> </p>");
                ////html.Append("</div>");
                ////html.Append("</div>");
                ////html.Append("</div>");
                ////html.Append("</body>");
                ////html.Append("</html>");

                ////                Body = @"<!DOCTYPE html>
                ////<html lang=""en"">
                ////<head>
                ////  <meta charset=""UTF-8"">
                ////  <title>Identity Card</title>
                ////</head>
                ////<body>
                ////  <div style=""width: 400px; height: 350px; border: 2px solid #ccc; border-radius: 10px; box-shadow: 5px 5px 10px #888888; overflow: hidden; display: flex; font-family: 'Open Sans', sans-serif;"">
                ////    <div style=""flex: 1; padding: 10px; display: flex; flex-direction: column; justify-content: flex-end;"">
                ////<div>     
                ////<img src=""https://via.placeholder.com/150x150.png?text=Photo"" alt=""Photo"" style=""margin-left: 5px; margin-top: 5px; margin-bottom: 5px; width: 100px; height: 150px; border: 1px solid Ash; border-radius: 10px;"">

                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; margin-left: 8px; font-weight: bold;"">Name:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">Vinoth</p>
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; margin-left: 8px; font-weight: bold;"">Agency Name:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">Sensoft</p>
                ////      <h2 style=""margin: 0; padding: 0; font-size: 15px; margin-left: 8px; font-weight: bold;"">Detail:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Technician</p>
                ////      </div>
                ////    </div>
                ////    <div style=""flex: 1; padding: 10px; display: flex; flex-direction: column; justify-content: space-between;"">
                ////      <div>  
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Contact Person:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">ARUN</p>
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Contact No:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">8934881112</p>
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Blood Group:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">o+ve</p>

                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Date Of Issue:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">07-04-2023 16:13:42</p>
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">ESI No:</h2>
                ////		<p style=""margin: 0; padding: 0; font-size: 15px;"">5215958585</p>
                ////		<h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">UAN No:</h2>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;"">12342</p>
                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Valid Upto:</h2>
                ////		<p style=""margin: 0; padding: 0; font-size: 15px;"">07-04-2023 16:13:43</p>

                ////        <h2 style=""margin: 0; padding: 0; font-size: 15px; font-weight: bold;"">Signature:</h2>
                ////        </br></br>
                ////        <p style=""margin: 0; padding: 0; font-size: 15px;""> </p>
                ////        </div>
                ////    </div>
                ////	</body>
                ////</html>";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //                    | SecurityProtocolType.Tls11
                //                    | SecurityProtocolType.Tls12;
                //using (MailMessage mail = new MailMessage())
                //{
                //    //mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "BookingListDetails.xlsx"));
                //    mail.From = new MailAddress("ftecin-notification@fujitec.co.in", "MIS Report");
                //    mail.To.Add("vinothkumar.s@in.fujitec.com");
                //    mail.CC.Add("vinothkumar.s@in.fujitec.com");
                //    mail.Bcc.Add("vinothkumar.s@in.fujitec.com");
                //    mail.Subject = "ID Card";// "Delivery Dashboard - Status as on Date : " + MonthYear;
                //    mail.Body = Body;
                //    mail.IsBodyHtml = true;
                //    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                //    {
                //        smtp.UseDefaultCredentials = false;
                //        smtp.Credentials = new System.Net.NetworkCredential("ftecin-notification@fujitec.co.in", "HelpDesk@20$22");
                //        smtp.EnableSsl = true;
                //        smtp.Send(mail);
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                if (ddselect.SelectedIndex != 0)
                    BindGrid();
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select Search Type";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddVendorNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string[] splitmodule = ddVendorNo.SelectedItem.ToString().Split('-');
                //string txtcompany = splitmodule[0];
                //txtsubconCompany.Text = txtcompany;
                //txtsubconCompany.Text= ddVendorNo.SelectedValue.ToString();
                txtsubconCompany.Text = ddVendorNo.SelectedItem.Text;
               // txtsubconCompany.Text = ddVendorNo.selected
            }
            catch (Exception ex)
            {

            }
        }
        
        private void sendmail()
        {
            try
            {
                string path = Server.MapPath("~/assets/images/fujitec_logo_J.jpg");
                LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
                Img.ContentId = "MyImage";
                // string image = "<iframe src = "https://fujitecindia1-my.sharepoint.com/personal/ftecin-notification_fujitec_co_in/_layouts/15/embed.aspx?UniqueId=e2d3213f-9cbb-4d30-b2e4-396f4ff78ced" width = "640" height = "360" frameborder = "0" scrolling = "no" allowfullscreen title = "fujitec_logo.png" ></ iframe >";//Server.MapPath("~/assets/images/fujitec_logo.png").ToString(); 
                ////----------------------------
                string Body = "Dear Sir/Madam,<br /><br />";
                Body = Body + "Current status of Employee Certification details,please find below<br/>";
                //Body = Body + "<b>Note:</b><b style=\"font-family:Calibri, sans-serif;font-weight:bold;color:red;\"> The below summary of cost exclude labour and DLP reversal in both forecast and actual.</b> <br /><br />";
                //Body = Body + "<div>";
                Body = Body + "<table border=\"0\" style=\"width:500px;height: 200px; margin:2px;padding: 20px; border:1px solid #6cb5d9; border-collapse:collapse;box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);\">";
                Body = Body + "<thead style=\"margin:0;padding:0;background-color:white;\">";                
                Body = Body + "<tr><td colspan=2 style=\"border: 1px solid black; padding: 10px;\">";
                Body = Body + "<img src=cid:MyImage  id='img' alt='' width='150px' height='30px'/>";
                //Body = Body + "<div id='outerdiv' style=\"width:200px; height:25px;overflow-x:hidden;\"><iframe src=\"https://fujitecindia1-my.sharepoint.com/personal/ftecin-notification_fujitec_co_in/_layouts/15/embed.aspx?UniqueId=e2d3213f-9cbb-4d30-b2e4-396f4ff78ced\" width=\"100px\" height=\"30px\" frameborder=\"0\" scrolling=\"no\" allowfullscreen title=\"fujitec_logo.png\"></iframe></div>";
                Body = Body + "</td ><td colspan = 4 align = center style =\"border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\"border: 1px solid black; padding: 10px;\">Name:" + txtEmployeeName.Text + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">ID:" + txtEmployeeID.Text + "</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\" border: 1px solid black; padding: 10px;\">Location:" + txtLocation.Text + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">Dept:" + string.Empty + "</td></tr>";
                Body = Body + "<tr><td colspan=2  style=\" border: 1px solid black; padding: 10px;\">SubContr.Name:" + txtsubconCompany.Text + "</td>";
                Body = Body + "<td colspan=4 style=\"border: 1px solid black; padding: 10px;\">Status:" + "Under Observation" + "</td></tr>";
                Body = Body + "<tr><td colspan=1 style=\"width:5%;border: 1px solid black; padding: 10px;\">S.No</td>";
                Body = Body + "<td colspan=1 style=\"width: 45 %; border: 1px solid black; padding: 10px; \">Process Details</td>";
                Body = Body + "<td colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Training Details</td>";
                Body = Body + " <td  colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Date</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px; \">Trained By</td><td style=\"border: 1px solid black; padding: 10px;\">Date</td><td style=\"border: 1px solid black; padding: 10px;\">Certified by</td></tr>";
                //Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Under Observation</td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                //Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                //  if (ddCertification.SelectedValue == "TCM000023")
                //{
                //Scaffholding
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">1</td><td style=\"border: 1px solid black; padding: 10px;\">Scaffolding entry / exit certification </td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td></tr>";
                // { Hoisting
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">2</td><td style=\"border: 1px solid black; padding: 10px;\">Hoisting</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td></tr>";
                //{CarTop
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">3</td><td style=\"border: 1px solid black; padding: 10px;\">Car Top Entry & Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td></tr>";
                //}PitEntry
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">4</td><td style=\"border: 1px solid black; padding: 10px;\">PitEntry Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td></tr>";
                //}Loto
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">5</td><td style=\"border: 1px solid black; padding: 10px;\">LOTO</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\">" + string.Empty + "</td></tr>";
                //}
                Body = Body + "</tr></thead><tbody style=\"margin:0;padding:0;background-color:#fff;\">";
                //}
                Body = Body + "</tbody></table><br />";

                ViewState["Preview"] = Body;
                //btnPreview.Enabled = true;
                AlternateView AV =
                AlternateView.CreateAlternateViewFromString(Body, null, MediaTypeNames.Text.Html);
                AV.LinkedResources.Add(Img);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                    | SecurityProtocolType.Tls11
                                    | SecurityProtocolType.Tls12;
                string filepath = Server.MapPath("~/PDFIDCARD/" + Regex.Replace(txtEmployeeID.Text, @"[^\w\d\s]", "") + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                
                using (MailMessage mail = new MailMessage())
                {
                    Attachment attachment = new Attachment(filepath);
                    mail.Attachments.Add(attachment);
                    string emailId = ViewState["email"] as string;
                     string emailIdk = ViewState["emailk"] as string;
                    if (string.IsNullOrEmpty(emailId))
                    {
                        emailId = ConfigurationManager.AppSettings["EmailToSafety"].ToString();
                    } 
                    if(string.IsNullOrEmpty(emailIdk))
                    {
                        emailIdk = ConfigurationManager.AppSettings["EmailToSafety"].ToString();
                    }
                    // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "BookingListDetails.xlsx"));
                    mail.From = new MailAddress("ftecin-notification@fujitec.co.in", "Safety Certification");
                    mail.To.Add(emailIdk);                  
                    mail.CC.Add(emailId);
                    mail.CC.Add(ConfigurationManager.AppSettings["EmailToSafety"].ToString());
                    //mail.Bcc.Add("vinothkumar.s@in.fujitec.com");
                    mail.Subject = "Under Observation Card";//"Delivery Dashboard - Status as on Date : " + MonthYear;
                    //mail.Body = Body;
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(AV);
                    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("ftecin-notification@fujitec.co.in", "HelpDesk@20$22");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex) { }
        }
        
        public void testIdCard()
        {
            try
            {
                IdCard idCard = new IdCard();
                idCard.Name = txtEmployeeName.Text;
                idCard.AgencyName = txtsubconCompany.Text;//ddVendorNo.SelectedItem.Text;
                idCard.Detail = ddEmployeeCategory.SelectedItem.Text;
                idCard.ContactPerson = txtsubconCompany.Text;
                idCard.ContactNo = txtPhoneNo.Text;
                idCard.BloodGroup = txtBloodGroup.Text;
                idCard.photopath = Server.MapPath("~/PDFIDCARD/Photo/") + ViewState["Photo"] as string;
                idCard.logo = Server.MapPath("~/assets/images/fujitec_logo.png");
                idCard.Location = txtLocation.Text;
                if (txtBloodGroup.Text.Contains("nbsp;"))
                {
                    // do something if the textbox contains the specified text
                    idCard.EsiNo = string.Empty;
                }
                else
                {
                    idCard.EsiNo = txtBloodGroup.Text;
                    // do something else if the textbox does not contain the specified text
                }
                DateTime currentDate = Convert.ToDateTime(ViewState["DOJ"].ToString());
                idCard.DateOfIssue = currentDate.ToString("dd-MM-yyyy");
                // idCard.DateOfIssue = currentDate.ToString("dd-MM-yyyy");
                if (txtESINo.Text.Contains("nbsp;"))
                {
                    // do something if the textbox contains the specified text
                    idCard.EsiNo = string.Empty;
                }
                else
                {
                    idCard.EsiNo = txtESINo.Text;
                    // do something else if the textbox does not contain the specified text
                }
                if (txtUNINO.Text.Contains("nbs"))
                {
                    // do something if the textbox contains the specified text
                    idCard.UanNo = string.Empty;
                }
                else
                {
                    idCard.UanNo = txtUNINO.Text;
                    // do something else if the textbox does not contain the specified text
                }
               // DateTime currentDate = Convert.ToDateTime(ViewState["DOJ"].ToString());
                DateTime nextDate = currentDate.AddMonths(12);
                //idCard.ValidUpto = nextDate.ToString("dd-MM-yyyy");
                if (txtvalidDate.Text.Length > 9)
                {
                    idCard.ValidUpto = Convert.ToDateTime(txtvalidDate.Text).ToString("dd-MM-yyyy");
                }
                else
                {
                    idCard.ValidUpto = nextDate.ToString("dd-MM-yyyy");
                }
                idCard.Signature = "";
                idCard.IDval = txtEmployeeID.Text;
                //idCard.GeneratePdf(@"D:\vinoth\Projects\FujiTecIntranetPortal\PDFIDCARD\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
                //string filepath = Server.MapPath("~/PDFIDCARD/" + Regex.Replace(txtEmployeeID.Text, @"[^\w\d\s]", "") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
                idCard.GeneratePdf(Server.MapPath("~/PDFIDCARD/" + Regex.Replace(txtEmployeeID.Text, @"[^\w\d\s]", "") + DateTime.Now.ToString("yyyyMMdd") + ".pdf"));
                //idCard.GeneratePdf1(Server.MapPath("~/PDFIDCARD/" + Regex.Replace(txtEmployeeID.Text, @"[^\w\d\s]", "") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf"));
                string filepath = Server.MapPath("~/PDFIDCARD/" + Regex.Replace(txtEmployeeID.Text, @"[^\w\d\s]", "") + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                string fileName = Path.GetFileName(filepath);

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(filepath);
                // Response.End();
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                //if (File.Exists(filepath))
                //{
                //    File.Delete(filepath);
                //    Console.WriteLine("File deleted successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("File does not exist.");
                //}
            }
            catch(Exception ex) { }
        }
    }

    class IdCard
    {
        public string Name { get; set; }
        public string AgencyName { get; set; }
        public string Detail { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string BloodGroup { get; set; }
        public string DateOfIssue { get; set; }
        public string EsiNo { get; set; }
        public string UanNo { get; set; }
        public string ValidUpto { get; set; }
        public string Signature { get; set; }
        public string IDval { get; set; }
        public string photopath { get; set; }
        public string logo { get; set; }
        public string Location { get; set; }
        public void GeneratePdf(string outputPath)
        {
            // Create a document
           // DateOfIssue = DateOfIssue.ToString("dd-MM-yyyy", new System.Globalization.CultureInfo("en-IN"));
            Document doc = new Document(new Rectangle(PageSize.A4));

            // Create a PdfWriter instance to write the PDF document
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

            // Open the document for writing
            doc.Open();


           // Font font = FontFactory.GetFont(FontFactory.HELVETICA, 30);
            var fontv = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);

            var fontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);
            //  new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.BaseColor.RED);
            //string m = 
            //////////////////////////////////////////////////////////////////////
            //// Add the content to the document
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 250f;
            table.LockedWidth = true;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            ////  Image Space
            //PdfPCell LogoSpace = new PdfPCell();
            //LogoSpace.Colspan = 2;
            //LogoSpace.Rowspan = 2;
            //LogoSpace.HorizontalAlignment = Element.ALIGN_CENTER;
            ////ContactPersonLable.BorderWidthLeft = 0;
            //// LogoSpace.BorderWidthRight = 0;
            ////ContactPersonLable.BorderWidthTop = 0;
            //LogoSpace.BorderWidthBottom = 0;
            //Image imagelogo = Image.GetInstance(logo);
            //imagelogo.ScaleToFit(100f, 100f);
            //imagelogo.Alignment = Element.ALIGN_CENTER;
            //LogoSpace.AddElement(imagelogo);
            //table.AddCell(LogoSpace);

            ////ContactPersonLable
            //Paragraph paragraph = new Paragraph($"{Location}", fontv);
            ////paragraph.Font = font;
            ////paragraph.Font.Color = new BaseColor(System.Drawing.Color.Red);
            //PdfPCell BranchLable = new PdfPCell(paragraph);
            //BranchLable.Colspan = 2;
            //BranchLable.Rowspan = 1;

            //// Set font weight and size for the text in the cell
            ////BranchLable.BorderWidthLeft = 0;
            ////  ContactPersonLable.BorderWidthRight = 0;
            //BranchLable.BorderWidthTop = 0;
            //BranchLable.BorderWidthBottom = 0;
            //BranchLable.HorizontalAlignment = Element.ALIGN_CENTER;

            //table.AddCell(BranchLable);
            /////////////////////////////////////////////////////////////////////////////////////////////
            //  ContactPersonValue
            ////  Paragraph paragraph = new Paragraph();
            //  PdfPCell BranchValue = new PdfPCell(new Paragraph($"{Location}", fontv));
            //  //PdfPCell ContactPersonValue = new PdfPCell(paragraph);

            //  //  paragraph.Font.Color = new BaseColor(System.Drawing.Color.Blue);
            //  BranchValue.Colspan = 1;
            //  BranchValue.Rowspan = 2;
            //  BranchValue.BorderWidthLeft = 0;
            //  //ContactPersonValue.BorderWidthRight = 0;
            //  BranchValue.BorderWidthTop = 0;
            //  BranchValue.BorderWidthBottom = 0;
            //  table.AddCell(BranchValue);
            Paragraph paragraph = new Paragraph();

            ////  IDLable
            //PdfPCell IDLbl = new PdfPCell(new Paragraph("IDCARD", fontv));
            //IDLbl.HorizontalAlignment = Element.ALIGN_CENTER;
            //IDLbl.Colspan = 1;
            //IDLbl.Rowspan = 2;
            //IDLbl.BorderWidthRight = 0;
            //IDLbl.BorderWidthTop = 0;
            //IDLbl.BorderWidthBottom = 0;
            //table.AddCell(IDLbl);

            //  Image Space
            PdfPCell ImageSpace = new PdfPCell();
            ImageSpace.Colspan = 3;
            ImageSpace.Rowspan = 8;
            //ContactPersonLable.BorderWidthLeft = 0;
           // ImageSpace.BorderWidthRight = 0;
            //ContactPersonLable.BorderWidthTop = 0;
            ImageSpace.BorderWidthBottom = 0;
            Image image = Image.GetInstance(photopath);
            image.ScaleToFit(100f, 100f);
            image.Alignment = Element.ALIGN_CENTER;
            ImageSpace.AddElement(image);
            table.AddCell(ImageSpace);

            //  IDLable
            PdfPCell IDLable = new PdfPCell(new Paragraph($"ID:", fontv));
            IDLable.Colspan = 1;
            IDLable.Rowspan = 1;
            IDLable.BorderWidthRight = 0;
            IDLable.BorderWidthTop = 0;
            IDLable.BorderWidthBottom = 0;
            table.AddCell(IDLable);
            //  IDValue
            PdfPCell IDValue = new PdfPCell(new Paragraph($"{IDval}", fontval));
            IDValue.Colspan = 1;
            IDValue.Rowspan = 1;
            IDValue.BorderWidthLeft = 0;
            IDValue.BorderWidthTop = 0;
            IDValue.BorderWidthBottom = 0;
            table.AddCell(IDValue);

            //  NameLable
            PdfPCell NameLable = new PdfPCell(new Paragraph($"Name:", fontv));
            NameLable.Colspan = 1;
            NameLable.Rowspan = 1;
            NameLable.BorderWidthRight = 0;
            NameLable.BorderWidthTop = 0;
            NameLable.BorderWidthBottom = 0;
            table.AddCell(NameLable);

            //  NameValue
            PdfPCell NameValue = new PdfPCell(new Paragraph($"{Name}", fontval));
            NameValue.Colspan = 1;
            NameValue.Rowspan = 1;
            NameValue.BorderWidthLeft = 0;
            NameValue.BorderWidthTop = 0;
            NameValue.BorderWidthBottom = 0;
            table.AddCell(NameValue);

            //  DetailLable
            PdfPCell DetailLable = new PdfPCell(new Paragraph($"Designation:", fontv));
            DetailLable.Colspan = 1;
            DetailLable.Rowspan = 1;
            DetailLable.BorderWidthRight = 0;
            DetailLable.BorderWidthTop = 0;
            DetailLable.BorderWidthBottom = 0;
            table.AddCell(DetailLable);

            //  DetailValue
            PdfPCell DetailValue = new PdfPCell(new Paragraph($"{Detail}", fontval));
            DetailValue.Colspan = 1;
            DetailValue.Rowspan = 1;
            DetailValue.BorderWidthLeft = 0;
            DetailValue.BorderWidthTop = 0;
            DetailValue.BorderWidthBottom = 0;
            table.AddCell(DetailValue);
            //  BranchNameLable
            PdfPCell BranchNameLable = new PdfPCell(new Paragraph($"Branch:", fontv));
            BranchNameLable.Colspan = 1;
            BranchNameLable.Rowspan = 1;
            BranchNameLable.BorderWidthRight = 0;
            BranchNameLable.BorderWidthTop = 0;
            BranchNameLable.BorderWidthBottom = 0;
            table.AddCell(BranchNameLable);

            //  AgencyNameValue
            PdfPCell BranchNameValue = new PdfPCell(new Paragraph($"{Location}", fontval));
            BranchNameValue.Colspan = 1;
            BranchNameValue.Rowspan = 1;
            BranchNameValue.BorderWidthLeft = 0;
            BranchNameValue.BorderWidthTop = 0;
            BranchNameValue.BorderWidthBottom = 0;
            table.AddCell(BranchNameValue);

            //  AgencyNameLable
            PdfPCell AgencyNameLable = new PdfPCell(new Paragraph($"Contractor Name:", fontv));
            AgencyNameLable.Colspan = 1;
            AgencyNameLable.Rowspan = 1;
            AgencyNameLable.BorderWidthRight = 0;
            AgencyNameLable.BorderWidthTop = 0;
            AgencyNameLable.BorderWidthBottom = 0;
            table.AddCell(AgencyNameLable);

            //  AgencyNameValue
            PdfPCell AgencyNameValue = new PdfPCell(new Paragraph($"{AgencyName}", fontval));
            AgencyNameValue.Colspan = 1;
            AgencyNameValue.Rowspan = 1;
            AgencyNameValue.BorderWidthLeft = 0;
            AgencyNameValue.BorderWidthTop = 0;
            AgencyNameValue.BorderWidthBottom = 0;
            table.AddCell(AgencyNameValue);
            

            //  ContactPersonLable
            // Paragraph paragraph = new Paragraph($"ContactPerson:", fontv);
            //paragraph.Font = font;
            //paragraph.Font.Color = new BaseColor(System.Drawing.Color.Red);
            PdfPCell ContactPersonLable = new PdfPCell(new Paragraph($"ContactPerson:", fontv));
           // PdfPCell ContactPersonLable = new PdfPCell(paragraph);
            ContactPersonLable.Colspan = 1;
            ContactPersonLable.Rowspan = 1;

            // Set font weight and size for the text in the cell
            //ContactPersonLable.BorderWidthLeft = 0;
            ContactPersonLable.BorderWidthRight = 0;
            ContactPersonLable.BorderWidthTop = 0;
            ContactPersonLable.BorderWidthBottom = 0;


            table.AddCell(ContactPersonLable);

            //  ContactPersonValue
            PdfPCell ContactPersonValue = new PdfPCell(new Paragraph($"{ContactPerson}", fontval));
            //PdfPCell ContactPersonValue = new PdfPCell(paragraph);

            paragraph.Font.Color = new BaseColor(System.Drawing.Color.Blue);
            ContactPersonValue.Colspan = 1;
            ContactPersonValue.Rowspan = 1;
            ContactPersonValue.BorderWidthLeft = 0;
            //ContactPersonValue.BorderWidthRight = 0;
            ContactPersonValue.BorderWidthTop = 0;
            ContactPersonValue.BorderWidthBottom = 0;
            table.AddCell(ContactPersonValue);

            //  ContactNoLable
            PdfPCell ContactNoLable = new PdfPCell(new Paragraph($"ContactNo:",fontv));
            ContactNoLable.Colspan = 1;
            ContactNoLable.Rowspan = 1;
            ContactNoLable.BorderWidthRight = 0;
            ContactNoLable.BorderWidthTop = 0;
            ContactNoLable.BorderWidthBottom = 0;
            table.AddCell(ContactNoLable);

            //  ContactNoValue
            PdfPCell ContactNoValue = new PdfPCell(new Paragraph($"{ContactNo}", fontval));
            //PdfPCell ContactNoValue = new PdfPCell(paragraph);

            //paragraph.Font.Color = new BaseColor(System.Drawing.Color.Blue);
            ContactNoValue.Colspan = 1;
            ContactNoValue.Rowspan = 1;
            ContactNoValue.BorderWidthLeft = 0;
            ContactNoValue.BorderWidthTop = 0;
            ContactNoValue.BorderWidthBottom = 0;
            table.AddCell(ContactNoValue);

            //  BloodGroupLable
            PdfPCell BloodGroupLable = new PdfPCell(new Paragraph($"BloodGroup:", fontv));
            BloodGroupLable.Colspan = 1;
            BloodGroupLable.Rowspan = 1;
            BloodGroupLable.BorderWidthRight = 0;
            BloodGroupLable.BorderWidthTop = 0;
            BloodGroupLable.BorderWidthBottom = 0;
            table.AddCell(BloodGroupLable);

            //  BloodGroupValue
            PdfPCell BloodGroupValue = new PdfPCell(new Paragraph($"{BloodGroup}", fontval));
            BloodGroupValue.Colspan = 1;
            BloodGroupValue.Rowspan = 1;
            BloodGroupValue.BorderWidthLeft = 0;
            BloodGroupValue.BorderWidthTop = 0;
            BloodGroupValue.BorderWidthBottom = 0;
            table.AddCell(BloodGroupValue);


            //  DateOfIssueLable
            PdfPCell DateOfIssueLable = new PdfPCell(new Paragraph($"DateOfIssue:", fontv));
            DateOfIssueLable.Colspan = 1;
            DateOfIssueLable.Rowspan = 1;
            DateOfIssueLable.BorderWidthRight = 0;
            DateOfIssueLable.BorderWidthTop = 0;
            DateOfIssueLable.BorderWidthBottom = 0;
            table.AddCell(DateOfIssueLable);

            //  DateOfIssueValue
            PdfPCell DateOfIssueValue = new PdfPCell(new Paragraph($"{DateOfIssue}", fontval));
            DateOfIssueValue.Colspan = 1;
            DateOfIssueValue.Rowspan = 1;
            DateOfIssueValue.BorderWidthLeft = 0;
            DateOfIssueValue.BorderWidthTop = 0;
            DateOfIssueValue.BorderWidthBottom = 0;
            table.AddCell(DateOfIssueValue);

            //  EsiNoLable
            PdfPCell EsiNoLable = new PdfPCell(new Paragraph($"ESINo:", fontv));
            EsiNoLable.Colspan = 1;
            EsiNoLable.Rowspan = 1;
            EsiNoLable.BorderWidthRight = 0;
            EsiNoLable.BorderWidthTop = 0;
            EsiNoLable.BorderWidthBottom = 0;
            table.AddCell(EsiNoLable);

            //  EsiNoValue
            PdfPCell EsiNoValue = new PdfPCell(new Paragraph($"{EsiNo}", fontval));
            EsiNoValue.Colspan = 1;
            EsiNoValue.Rowspan = 1;
            EsiNoValue.BorderWidthLeft = 0;
            EsiNoValue.BorderWidthTop = 0;
            EsiNoValue.BorderWidthBottom = 0;
            table.AddCell(EsiNoValue);
            ////  IDLable
            //PdfPCell IDLable = new PdfPCell(new Paragraph($"ID:", fontv));
            //IDLable.Colspan = 1;
            //IDLable.Rowspan = 1;
            //IDLable.BorderWidthRight = 0;
            //IDLable.BorderWidthTop = 0;
            //IDLable.BorderWidthBottom = 0;
            //table.AddCell(IDLable);
            //  UanNoLable
            PdfPCell UanNoLable = new PdfPCell(new Paragraph($"UANNo:", fontv));
            UanNoLable.Colspan = 1;
            UanNoLable.Rowspan = 1;
            UanNoLable.BorderWidthRight = 0;
            UanNoLable.BorderWidthTop = 0;
            UanNoLable.BorderWidthBottom = 0;
            table.AddCell(UanNoLable);
            ////  IDValue
            //PdfPCell IDValue = new PdfPCell(new Paragraph($"{IDval}", fontval));
            //IDValue.Colspan = 1;
            //IDValue.Rowspan = 1;
            //IDValue.BorderWidthRight= 0;
            //IDValue.BorderWidthTop = 0;
            //IDValue.BorderWidthBottom = 0;
            //table.AddCell(IDValue);

            //  UanNoValue
            PdfPCell UanNoValue = new PdfPCell(new Paragraph($"{UanNo}", fontval));
            UanNoValue.Colspan = 1;
            UanNoValue.Rowspan = 1;
            UanNoValue.BorderWidthLeft = 0;
            UanNoValue.BorderWidthTop = 0;
            UanNoValue.BorderWidthBottom = 0;
            table.AddCell(UanNoValue);

           

            //  ValidUptoLable
            PdfPCell ValidUptoLable = new PdfPCell(new Paragraph($"ValidUpto:", fontv));
            ValidUptoLable.Colspan = 1;
            ValidUptoLable.Rowspan = 1;
            ValidUptoLable.BorderWidthRight = 0;
            ValidUptoLable.BorderWidthTop = 0;
            //ValidUptoLable.BorderWidthBottom = 0;
            table.AddCell(ValidUptoLable);

              

            //  ValidUptoValue
            PdfPCell ValidUptoValue = new PdfPCell(new Paragraph($"{ValidUpto}", fontval));
            ValidUptoValue.Colspan = 1;
            ValidUptoValue.Rowspan = 1;
            ValidUptoValue.BorderWidthLeft = 0;
            ValidUptoValue.BorderWidthTop = 0;
           // ValidUptoValue.BorderWidthBottom = 0;
            table.AddCell(ValidUptoValue);

          

            ////  SignatureLable
            //PdfPCell SignatureLable = new PdfPCell(new Paragraph($"No signature is required."));
            //SignatureLable.Colspan = 2;
            //SignatureLable.Rowspan = 1;
            ////SignatureLable.BorderWidthRight = 0;
            ////SignatureLable.BorderWidthTop = 0;
            //////SignatureLable.BorderWidthBottom = 0;

            ////SignatureLable.BorderWidthLeft = 0;
            ////SignatureLable.BorderWidthTop = 0;
            //table.AddCell(SignatureLable);



            //  SignatureValue
            //PdfPCell SignatureValue = new PdfPCell(new Paragraph($""));
            //SignatureValue.Colspan = 1;
            //SignatureValue.Rowspan = 1;
            //SignatureValue.BorderWidthLeft = 0;
            //SignatureValue.BorderWidthTop = 0;
            //table.AddCell(SignatureValue);




            // Add the table to the document
            doc.Add(table);

            // Close the document
            doc.Close();
        }
            

        public void GeneratePdf2(string outputPath)
        {
            // Create a document
            Document doc = new Document(new Rectangle(PageSize.A4));

            // Create a PdfWriter instance to write the PDF document
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

            // Open the document for writing
            doc.Open();


            // Font font = FontFactory.GetFont(FontFactory.HELVETICA, 30);
            var fontv = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);

            var fontval = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10);
            //  new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.BaseColor.RED);
            //string m = 

            // Add the content to the document
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 400f;
            table.LockedWidth = true;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            //  Image Space
            PdfPCell ImageSpace = new PdfPCell();
            ImageSpace.Colspan = 1;
            ImageSpace.Rowspan = 10;
            Image image = Image.GetInstance(photopath);
            image.ScaleToFit(100f, 100f);
            ImageSpace.AddElement(image);
            table.AddCell(ImageSpace);

            //  ContactPersonLable
            Paragraph paragraph = new Paragraph($"ContactPerson:", fontv);
            //paragraph.Font = font;
            //paragraph.Font.Color = new BaseColor(System.Drawing.Color.Red);
            PdfPCell ContactPersonLable = new PdfPCell(paragraph);
            ContactPersonLable.Colspan = 1;
            ContactPersonLable.Rowspan = 1;

            // Set font weight and size for the text in the cell
            //  ContactPersonLable.BorderWidthLeft = 0;
            //  ContactPersonLable.BorderWidthRight = 0;
            // ContactPersonLable.BorderWidthTop = 0;
            ContactPersonLable.BorderWidthBottom = 0;


            table.AddCell(ContactPersonLable);

            //  ContactPersonValue
            PdfPCell ContactPersonValue = new PdfPCell(new Paragraph($"{ContactPerson}", fontval));
            //PdfPCell ContactPersonValue = new PdfPCell(paragraph);

            //  paragraph.Font.Color = new BaseColor(System.Drawing.Color.Blue);
            ContactPersonValue.Colspan = 1;
            ContactPersonValue.Rowspan = 1;
            //ContactPersonValue.BorderWidthLeft = 0;
            //ContactPersonValue.BorderWidthRight = 0;
            ContactPersonValue.BorderWidthTop = 0;
            ContactPersonValue.BorderWidthBottom = 0;
            table.AddCell(ContactPersonValue);

            //  ContactNoLable
            PdfPCell ContactNoLable = new PdfPCell(new Paragraph($"ContactNo:", fontv));
            ContactNoLable.Colspan = 1;
            ContactNoLable.Rowspan = 1;

            // ContactNoLable.BorderWidthTop = 0;
            ContactNoLable.BorderWidthBottom = 0;
            table.AddCell(ContactNoLable);

            //  ContactNoValue
            PdfPCell ContactNoValue = new PdfPCell(new Paragraph($"{ContactNo}", fontval));
            //PdfPCell ContactNoValue = new PdfPCell(paragraph);

            paragraph.Font.Color = new BaseColor(System.Drawing.Color.Blue);
            ContactNoValue.Colspan = 1;
            ContactNoValue.Rowspan = 1;

            ContactNoValue.BorderWidthTop = 0;
            ContactNoValue.BorderWidthBottom = 0;
            table.AddCell(ContactNoValue);

            //  BloodGroupLable
            PdfPCell BloodGroupLable = new PdfPCell(new Paragraph($"BloodGroup:", fontv));
            BloodGroupLable.Colspan = 1;
            BloodGroupLable.Rowspan = 1;

            BloodGroupLable.BorderWidthTop = 0;
            BloodGroupLable.BorderWidthBottom = 0;
            table.AddCell(BloodGroupLable);

            //  BloodGroupValue
            PdfPCell BloodGroupValue = new PdfPCell(new Paragraph($"{BloodGroup}", fontval));
            BloodGroupValue.Colspan = 1;
            BloodGroupValue.Rowspan = 1;

            BloodGroupValue.BorderWidthTop = 0;
            BloodGroupValue.BorderWidthBottom = 0;
            table.AddCell(BloodGroupValue);


            //  DateOfIssueLable
            PdfPCell DateOfIssueLable = new PdfPCell(new Paragraph($"DateOfIssue:", fontv));
            DateOfIssueLable.Colspan = 1;
            DateOfIssueLable.Rowspan = 1;

            // ContactNoValue.BorderWidthTop = 0;
            DateOfIssueLable.BorderWidthBottom = 0;
            table.AddCell(DateOfIssueLable);

            //  DateOfIssueValue
            PdfPCell DateOfIssueValue = new PdfPCell(new Paragraph($"{DateOfIssue}", fontval));
            DateOfIssueValue.Colspan = 1;
            DateOfIssueValue.Rowspan = 1;

            DateOfIssueValue.BorderWidthTop = 0;
            DateOfIssueValue.BorderWidthBottom = 0;
            table.AddCell(DateOfIssueValue);

            //  EsiNoLable
            PdfPCell EsiNoLable = new PdfPCell(new Paragraph($"ESINo:", fontv));
            EsiNoLable.Colspan = 1;
            EsiNoLable.Rowspan = 1;

            // ContactNoValue.BorderWidthTop = 0;
            EsiNoLable.BorderWidthBottom = 0;
            table.AddCell(EsiNoLable);

            //  EsiNoValue
            PdfPCell EsiNoValue = new PdfPCell(new Paragraph($"{EsiNo}", fontval));
            EsiNoValue.Colspan = 1;
            EsiNoValue.Rowspan = 1;

            EsiNoValue.BorderWidthTop = 0;
            EsiNoValue.BorderWidthBottom = 0;
            table.AddCell(EsiNoValue);
            //  IDLable
            PdfPCell IDLable = new PdfPCell(new Paragraph($"ID:", fontv));
            IDLable.Colspan = 1;
            IDLable.Rowspan = 1;

            //  ContactNoValue.BorderWidthTop = 0;
            IDLable.BorderWidthBottom = 0;
            table.AddCell(IDLable);
            //  UanNoLable
            PdfPCell UanNoLable = new PdfPCell(new Paragraph($"UANNo:", fontv));
            UanNoLable.Colspan = 1;
            UanNoLable.Rowspan = 1;

            UanNoLable.BorderWidthTop = 0;
            UanNoLable.BorderWidthBottom = 0;
            table.AddCell(UanNoLable);
            //  IDValue
            PdfPCell IDValue = new PdfPCell(new Paragraph($"{IDval}", fontval));
            IDValue.Colspan = 1;
            IDValue.Rowspan = 1;

            IDValue.BorderWidthTop = 0;
            IDValue.BorderWidthBottom = 0;
            table.AddCell(IDValue);

            //  UanNoValue
            PdfPCell UanNoValue = new PdfPCell(new Paragraph($"{UanNo}", fontval));
            UanNoValue.Colspan = 1;
            UanNoValue.Rowspan = 1;

            UanNoValue.BorderWidthTop = 0;
            UanNoValue.BorderWidthBottom = 0;
            table.AddCell(UanNoValue);

            //  NameLable
            PdfPCell NameLable = new PdfPCell(new Paragraph($"Name:", fontv));
            NameLable.Colspan = 1;
            NameLable.Rowspan = 1;

            // ContactNoValue.BorderWidthTop = 0;
            NameLable.BorderWidthBottom = 0;
            table.AddCell(NameLable);

            //  ValidUptoLable
            PdfPCell ValidUptoLable = new PdfPCell(new Paragraph($"ValidUpto:", fontv));
            ValidUptoLable.Colspan = 1;
            ValidUptoLable.Rowspan = 1;

            // ContactNoValue.BorderWidthTop = 0;
            ValidUptoLable.BorderWidthBottom = 0;
            table.AddCell(ValidUptoLable);

            //  NameValue
            PdfPCell NameValue = new PdfPCell(new Paragraph($"{Name}", fontval));
            NameValue.Colspan = 1;
            NameValue.Rowspan = 1;

            NameValue.BorderWidthTop = 0;
            NameValue.BorderWidthBottom = 0;
            table.AddCell(NameValue);

            //  ValidUptoValue
            PdfPCell ValidUptoValue = new PdfPCell(new Paragraph($"{ValidUpto}", fontval));
            ValidUptoValue.Colspan = 1;
            ValidUptoValue.Rowspan = 1;

            ValidUptoValue.BorderWidthTop = 0;
            ValidUptoValue.BorderWidthBottom = 0;
            table.AddCell(ValidUptoValue);

            //  AgencyNameLable
            PdfPCell AgencyNameLable = new PdfPCell(new Paragraph($"AgencyName:", fontv));
            AgencyNameLable.Colspan = 1;
            AgencyNameLable.Rowspan = 1;

            AgencyNameLable.BorderWidthTop = 0;
            AgencyNameLable.BorderWidthBottom = 0;
            table.AddCell(AgencyNameLable);

            //  SignatureLable
            PdfPCell SignatureLable = new PdfPCell(new Paragraph($"Signature:", fontv));
            SignatureLable.Colspan = 1;
            SignatureLable.Rowspan = 1;

            SignatureLable.BorderWidthTop = 0;
            SignatureLable.BorderWidthBottom = 0;
            table.AddCell(SignatureLable);

            //  AgencyNameValue
            PdfPCell AgencyNameValue = new PdfPCell(new Paragraph($"{AgencyName}", fontval));
            AgencyNameValue.Colspan = 1;
            AgencyNameValue.Rowspan = 1;

            AgencyNameValue.BorderWidthTop = 0;
            AgencyNameValue.BorderWidthBottom = 0;
            table.AddCell(AgencyNameValue);

            //  SignatureValue
            PdfPCell SignatureValue = new PdfPCell(new Paragraph($""));
            SignatureValue.Colspan = 1;
            SignatureValue.Rowspan = 3;
            SignatureValue.BorderWidthTop = 0;
            table.AddCell(SignatureValue);


            //  DetailLable
            PdfPCell DetailLable = new PdfPCell(new Paragraph($"Designation:", fontv));
            DetailLable.Colspan = 1;
            DetailLable.Rowspan = 1;

            DetailLable.BorderWidthTop = 0;
            DetailLable.BorderWidthBottom = 0;
            table.AddCell(DetailLable);

            //  DetailValue
            PdfPCell DetailValue = new PdfPCell(new Paragraph($"{Detail}", fontval));
            DetailValue.Colspan = 1;
            DetailValue.Rowspan = 1;

            DetailValue.BorderWidthTop = 0;
            // DetailValue.BorderWidthBottom = 0;
            table.AddCell(DetailValue);

            // Add the table to the document
            doc.Add(table);

            // Close the document
            doc.Close();
        }
        public void GeneratePdf1(string outputPath)
        {
            // Create a document
            Document doc = new Document(new Rectangle(PageSize.A4));

            // Create a PdfWriter instance to write the PDF document
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

            // Open the document for writing
            doc.Open();

            // Add the content to the document
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 400f;
            table.LockedWidth = true;
            table.SpacingBefore = 10f;
            table.SpacingAfter = 10f;

            //table.WidthPercentage = 100;
            //table.SetWidths(new float[] { 1f, 2f });

            // Add image to the card
            //PdfPCell imageCell = new PdfPCell(Image.GetInstance(@"D:\vinoth\Projects\FujiTecIntranetPortal\assets\images\EmptyPhotoFrame.jpg"));
            //imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //imageCell.Border = Rectangle.NO_BORDER;
            //table.AddCell(imageCell);

            //PdfPCell imageCell = new PdfPCell(Image.GetInstance(@"D:\vinoth\Projects\FujiTecIntranetPortal\assets\images\EmptyPhotoFrame.jpg"));
            //imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //imageCell.Border = Rectangle.NO_BORDER;
            //imageCell.Colspan = 2;
            //imageCell.Rowspan = 6;
            //table.AddCell(imageCell);

            PdfPCell imageCell = new PdfPCell(new Paragraph($""));
            imageCell.Colspan = 1;
            imageCell.Rowspan = 6;
            table.AddCell(imageCell);


            // Add Contact Person field
            PdfPCell ContactPersonLable = new PdfPCell(new Paragraph($"Contact Person : {ContactPerson}"));
            ContactPersonLable.Colspan = 1;
            ContactPersonLable.Rowspan = 1;
            table.AddCell(ContactPersonLable);

            //PdfPCell ContactPersonValue = new PdfPCell(new Paragraph($"{ContactPerson}"));
            //ContactPersonValue.Colspan = 1;
            //ContactPersonValue.Rowspan = 1;
            //table.AddCell(ContactPersonValue);

            // Add Contact No field
            PdfPCell ContactNoLable = new PdfPCell(new Paragraph($"Contact No : {ContactNo}"));
            ContactNoLable.Colspan = 1;
            ContactNoLable.Rowspan = 1;
            table.AddCell(ContactNoLable);

            //PdfPCell ContactNoValue = new PdfPCell(new Paragraph($"{ContactNo}"));
            //ContactNoValue.Colspan = 1;
            //ContactNoValue.Rowspan = 1;
            //table.AddCell(ContactNoValue);

            // Add Blood Group field
            PdfPCell BloodGroupLable = new PdfPCell(new Paragraph($"Blood Group : {BloodGroup}"));
            BloodGroupLable.Colspan = 1;
            BloodGroupLable.Rowspan = 1;
            table.AddCell(BloodGroupLable);

            //PdfPCell BloodGroupValue = new PdfPCell(new Paragraph($"{BloodGroup}"));
            //BloodGroupValue.Colspan = 1;
            //BloodGroupValue.Rowspan = 1;
            //table.AddCell(BloodGroupValue);

            // Add Date Of Issue field
            PdfPCell DateOfIssueLable = new PdfPCell(new Paragraph($"Date Of Issue : {DateOfIssue}"));
            DateOfIssueLable.Colspan = 1;
            DateOfIssueLable.Rowspan = 1;
            table.AddCell(DateOfIssueLable);

            //PdfPCell DateOfIssueValue = new PdfPCell(new Paragraph($"{DateOfIssue}"));
            //DateOfIssueValue.Colspan = 1;
            //DateOfIssueValue.Rowspan = 1;
            //table.AddCell(DateOfIssueValue);

            // Add EsiNo field
            PdfPCell EsiNoLable = new PdfPCell(new Paragraph($"ESI No : {EsiNo}"));
            EsiNoLable.Colspan = 1;
            EsiNoLable.Rowspan = 1;
            table.AddCell(EsiNoLable);

            //PdfPCell EsiNoValue = new PdfPCell(new Paragraph($"{EsiNo}"));
            //EsiNoValue.Colspan = 1;
            //EsiNoValue.Rowspan = 1;
            //table.AddCell(EsiNoValue);


            // Add UanNo field
            PdfPCell UanNoLable = new PdfPCell(new Paragraph($"UAN No : {UanNo}"));
            UanNoLable.Colspan = 1;
            UanNoLable.Rowspan = 1;
            table.AddCell(UanNoLable);

            //PdfPCell UanValue = new PdfPCell(new Paragraph($"{UanNo}"));
            //UanValue.Colspan = 1;
            //UanValue.Rowspan = 1;
            //table.AddCell(UanValue);

            // Add Name field
            PdfPCell NameLable = new PdfPCell(new Paragraph($"Name : {Name}"));
            NameLable.Colspan = 1;
            NameLable.Rowspan = 1;
            table.AddCell(NameLable);

            //PdfPCell NameValue = new PdfPCell(new Paragraph($"{Name}"));
            //NameValue.Colspan = 1;
            //NameValue.Rowspan = 1;
            //table.AddCell(NameValue);

            // Add Name field
            PdfPCell ValidUptoLable = new PdfPCell(new Paragraph($"ValidUpto : {ValidUpto}"));
            ValidUptoLable.Colspan = 1;
            ValidUptoLable.Rowspan = 1;
            table.AddCell(ValidUptoLable);

            //PdfPCell ValidUptoValue = new PdfPCell(new Paragraph($"{ValidUpto}"));
            //ValidUptoValue.Colspan = 1;
            //ValidUptoValue.Rowspan = 1;
            //table.AddCell(ValidUptoValue);


            // Add Agency Name field
            PdfPCell AgencyNameLable = new PdfPCell(new Paragraph($"Agency Name : {AgencyName}"));
            AgencyNameLable.Colspan = 1;
            AgencyNameLable.Rowspan = 1;
            table.AddCell(AgencyNameLable);

            //PdfPCell AgencyNameValue = new PdfPCell(new Paragraph($"{AgencyName}"));
            //AgencyNameValue.Colspan = 1;
            //AgencyNameValue.Rowspan = 1;
            //table.AddCell(AgencyNameValue);


            // Add Signature field
            PdfPCell SignatureLable = new PdfPCell(new Paragraph($"Signature : "));
            SignatureLable.Colspan = 1;
            SignatureLable.Rowspan = 2;
            table.AddCell(SignatureLable);

            //PdfPCell SignatureValue = new PdfPCell(new Paragraph($""));
            //SignatureValue.Colspan = 1;
            //SignatureValue.Rowspan = 2;
            //table.AddCell(SignatureValue);

            // Add Detail field
            PdfPCell DetailLable = new PdfPCell(new Paragraph($"Detail:{Detail}"));
            DetailLable.Colspan = 1;
            DetailLable.Rowspan = 1;
            table.AddCell(DetailLable);


            //PdfPCell DetailValue = new PdfPCell(new Paragraph($"{Detail}"));
            //DetailValue.Colspan = 1;
            //DetailValue.Rowspan = 1;
            //table.AddCell(DetailValue);






            //// Add personal details to the card
            //PdfPCell personalDetailsCell = new PdfPCell();
            //personalDetailsCell.Border = Rectangle.NO_BORDER;
            //personalDetailsCell.PaddingLeft = 10;
            //personalDetailsCell.PaddingBottom = 5;
            //personalDetailsCell.PaddingTop = 5;
            //personalDetailsCell.PaddingRight = 5;
            //personalDetailsCell.AddElement(new Paragraph($"Name : {Name}"));
            //personalDetailsCell.AddElement(new Paragraph("Agency Name: Sensoft"));
            //personalDetailsCell.AddElement(new Paragraph("Detail: Technician"));
            //table.AddCell(personalDetailsCell);

            //// Add contact details to the card
            //PdfPCell contactDetailsCell = new PdfPCell();
            //contactDetailsCell.Border = Rectangle.NO_BORDER;
            //contactDetailsCell.PaddingLeft = 10;
            //contactDetailsCell.PaddingBottom = 5;
            //contactDetailsCell.PaddingTop = 5;
            //contactDetailsCell.PaddingRight = 5;
            //contactDetailsCell.AddElement(new Paragraph("Contact Person: ARUN"));
            //contactDetailsCell.AddElement(new Paragraph("Contact No: 8934881112"));
            //contactDetailsCell.AddElement(new Paragraph("Blood Group: o+ve"));
            //table.AddCell(contactDetailsCell);

            //// Add other details to the card
            //PdfPCell otherDetailsCell = new PdfPCell();
            //otherDetailsCell.Border = Rectangle.NO_BORDER;
            //otherDetailsCell.PaddingLeft = 10;
            //otherDetailsCell.PaddingBottom = 5;
            //otherDetailsCell.PaddingTop = 5;
            //otherDetailsCell.PaddingRight = 5;
            //otherDetailsCell.AddElement(new Paragraph("Date Of Issue: 07-04-2023 16:13:42"));
            //otherDetailsCell.AddElement(new Paragraph("ESI No: 5215958585"));
            //otherDetailsCell.AddElement(new Paragraph("UAN No: 12342"));
            //otherDetailsCell.AddElement(new Paragraph("Valid Upto: 07-04-2023 16:13:43"));
            //otherDetailsCell.AddElement(new Paragraph("Signature"));
            //table.AddCell(otherDetailsCell);

            // Add the table to the document
            doc.Add(table);

            // Close the document
            doc.Close();
        }
    }
}