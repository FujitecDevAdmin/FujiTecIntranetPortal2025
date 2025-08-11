using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class EmployeeRegistration : System.Web.UI.Page
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
            { }
        }
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

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_PageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Department--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddDepartment.DataTextField = "DEPARTMENTNAME";
                        ddDepartment.DataValueField = "DEPARTMENTNAME";
                        ddDepartment.DataSource = ds.Tables[0];
                        ddDepartment.DataBind();
                        ViewState["GV"] = ds.Tables[1];
                        gv.DataSource = ds.Tables[1];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";

                if ((txtEmployeeID.Text.Length > 3) && (txtEmployeeName.Text.Length > 0) && (txtDesignation.Text.Length > 0) && (txtEmailId.Text.Length > 0) && (txtReportingTo.Text.Length > 0) && (ddDepartment.SelectedIndex > 0) && (txtMobileNo.Text.Length > 0))
                {
                    //else
                    //{
                    if (FileUpload1.HasFile)
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
                        else if (size < 100)
                        {

                            // Save the  file server new path
                            #region Save File
                            string appBasePath = Server.MapPath("~/");
                            string employeesFolderPath = Path.Combine(appBasePath, "LandingPageResources/Employees/");
                            if (!Directory.Exists(employeesFolderPath))
                            {
                                Directory.CreateDirectory(employeesFolderPath);
                            }

                            //var existingFiles = Directory.GetFiles(employeesFolderPath);
                            //foreach (var existingFile in existingFiles)
                            //{
                            //    if (FilesAreIdentical(existingFile, FileUpload1.PostedFile.InputStream))
                            //    {
                            //        string existingFileName = Path.GetFileName(existingFile);
                            //        string uploadingFileName = FileUpload1.FileName;

                            //        // If identical file exists, inform the user and prevent upload
                            //        string alertMessage = $"File {uploadingFileName} has the same content as {existingFileName}. Please upload a different file.";
                            //        Response.Write($"<script>alert('{alertMessage}');</script>");
                            //        return; // Stop further processing
                            //    }
                            //}

                            string fileExtension = Path.GetExtension(FileUpload1.FileName);
                            string photoFilename = $"{txtEmployeeID.Text.Trim().Replace(" ", "_")}" +
                                $"_{System.Text.RegularExpressions.Regex.Replace(txtEmployeeName.Text.Trim(), @"\s+", " ")}".Replace(" ", "_") + $"{fileExtension}";
                            string filePath = Path.Combine(employeesFolderPath, photoFilename);
                            string resolvedFilePath = Path.Combine("/", filePath.Replace(appBasePath, ""));

                            FileUpload1.SaveAs(filePath);

                            #endregion Save File


                            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd = new SqlCommand("SP_EMP_Insert", sqlConnection);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@Emp_Id", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                                cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = txtEmployeeName.Text;
                                cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = txtDesignation.Text;// dtpDate.Text;
                                cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddDepartment.Text;
                                cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = txtLocation.Text;
                                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = txtEmailId.Text;
                                cmd.Parameters.Add("@Mobileno", SqlDbType.VarChar).Value = txtMobileNo.Text;
                                cmd.Parameters.Add("@ReportingTo", SqlDbType.VarChar).Value = txtReportingTo.Text;
                                cmd.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = txtQualification.Text;
                                cmd.Parameters.Add("@PreviousExperience", SqlDbType.VarChar).Value = txtExperience.Text;
                                cmd.Parameters.Add("@Emp_Photo", SqlDbType.VarChar).Value = resolvedFilePath;
                                cmd.Parameters.Add("@Emp_Message", SqlDbType.VarChar).Value = txtWelcomeMsg.Text;
                                cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                                cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                if (ds != null)
                                {
                                    if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Green;
                                        lblmsg.Text = "Employee Registered successfully";

                                    }
                                    else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Green;
                                        lblmsg.Text = "Employee Updated successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Red;
                                        lblmsg.Text = "Error in registration";
                                    }

                                    // SendEmail();
                                }

                            }

                        }
                        //////////////////////
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Please select the Employee photo";
                    }
                    // }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter the mandatory fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please enter the mandatory fields";
            }
        }
        private bool FilesAreIdentical(string filePath, Stream fileStream)
        {
            using (var existingFile = File.OpenRead(filePath))
            using (var newFile = fileStream)
            {
                int fileByte;
                do
                {
                    fileByte = existingFile.ReadByte();
                    if (fileByte != newFile.ReadByte())
                    {
                        return false;
                    }
                } while (fileByte != -1);

                return true;
            }
        }

        public void SendEmail()
        {
            string username = Session["USERNAME"] as string;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
            message.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailTo"].ToString()));
            message.Subject = "Approval request for Our New Buddies in Confluence";
            message.IsBodyHtml = true; //to make message body as html  

            /////////////////////////////////
            string Body = "Dear Sir , <br />";
            //Body = Body + "<html>< style >table, th, td {border: 1px solid black;}</ style >";
            //Body = Body + "< body >< table >< tr >< td > Maria Anders </ td >< td > Germany </ td ></ tr >< tr >" ;
            //Body = Body + "< td > Francisco Chang </ td >< td > Mexico </ td ></ tr ></ table ></ body ></ html > ";
            //border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 200 + "
            Body = Body + "I am requesting approval for <b> Our New Buddies : Mr/Ms.'" + txtEmployeeName.Text + "--" + txtEmployeeID.Text + "' </b> details is registered in our confluence site.<br /><br /> Please Approve/Reject to proceed on Our New Buddies and also let us know for any assistance<br /><br /><br />";
            Body = Body + "Please click the link below,<br />";
            Body = Body + "URL :- http://confluence:8085/ <br /><br />";
            ////Body = Body + "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 0 + " width = " + 200 + "> <tr><td>Pass No:</td><td>" + "vinoth" + "</td></ tr>";
            //// Body = Body + "<table border=\"1\" style=\"width:200px; margin:2px;padding:0; border:1px solid #6cb5d9; border-collapse:collapse;\"> <tr><td>Pass No:</td><td>" + "vinoth" + "</td></ tr>";
            //// Body = Body + "<tr><td>Pass No:</td><td>" + "Kumar" + "</td></ tr>";
            ////Body = Body + "< tr >< td > Centro comercial Moctezuma</ td >< td > Francisco Chang </ td ></ tr >";
            //// Body = Body + "</table><br/>";
            ///
            //Body = Body + "<table border=\"0\" style=\"width:400px; margin:2px;padding:0; border:1px solid #6cb5d9; border-collapse:collapse;\">";
            ////Body = Body + "<thead style=\"margin:0;padding:0;background-color:#d9edf7;\">";
            ////Body = Body + "<tr><td style=\"width:30%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;font-weight:bold;\">Film Title</td>";
            ////Body = Body + "<td style=\"width:30%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;font-weight:bold;\">Asset ID</td>";
            ////Body = Body + "<td style=\"width:40%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;font-weight:bold;\">Film Base</td>";
            ////Body = Body + "<td style=\"width:40%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;font-weight:bold;\">No of Reels</td>";
            ////Body = Body + "</tr></thead><tbody style=\"margin:0;padding:0;background-color:#fff;\">";
            //////for (int i = 0; i < dt.Rows.Count; i++)
            //////{
            //Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;\">" + "Pass No:" + "</td>";
            ////    Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + "AS001" + "</td>";
            ////    Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + "VP" + "</td>";
            //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + ViewState["PassNo"].ToString() + "</td></tr>";
            //Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;\">" + "Visitor Name:" + "</td>";
            //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + txtVisitorName.Text + "</td></tr>";
            //Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;\">" + "Visting Date:" + "</td>";
            //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + txtAppointmentDate.Text + "</td></tr>";
            //Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;\">" + "Purpose Of Visit:" + "</td>";
            //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + txtPOV.Text + "</td></tr>";
            //Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif; font-size:14px;\">" + "Mobile No:" + "</td>";
            //Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Arial,Helvetica,Sans-serif;font-size:14px;\">" + txtMobileNo.Text + "</td></tr>";
            Body = Body + "<b>Regards,</b><br /> <b>" + username + " </b><br /><b>Fujitec India Pvt. Ltd.</b><br />";

            //////}
            //Body = Body + "</tbody></table><br />";
            //Body = Body + " <b>Reach us:</b> https://www.google.com/maps/dir//Fujitec+India+Pvt+Ltd,+Fujitec+India+Pvt.+Ltd,+P+-+51,+8th+Avenue,+Mahindra+World+City,+Chennai,+Tamil+Nadu+603002/@12.7233955,80.0187177,17z/data=!4m8!4m7!1m0!1m5!1m1!1s0x3a52f9ffe77cfea3:0x1f42c8b89cf093f9!2m2!1d80.0209064!2d12.7233903" + "<br />";
            ////Body = Body + "<b>Note: </b><br />Kindly show your fully vacinated certificate at securtiy gate during your visit. <br />If you bring any gadgets inform it to security, please ensure whether it is updated in your visitor pass.<br /><br />";
            //Body = Body + "<b>Note: </b><br /> I. Kindly show your fully vacinated certificate at securtiy gate during your visit.<br />II. All visitors are required to adhere to COVID-19 protocols of temperature screening, maintaining physical distancing, wearing a face cover and frequently washing/sanitizing hands. <br />III. If you bring any gadgets inform it to security, please ensure whether it is updated in your visitor pass.Happy Visitor<br /><br />";
            //Body = Body + "<b>Regards,</b><br /><b>Fujitec India Pvt. Ltd.</b><br />";
            //< iframe src = "https://www.google.com/maps/d/embed?mid=1nxxfsdOOs2x2HPEsNV-YlJDLFzM&hl=en&ehbc=2E312F" width = "640" height = "480" ></ iframe >
            /////////////////////////////////            
            message.Body = Body;
            smtp.Port = 587;
            smtp.Host = "smtp.office365.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"].ToString(), ConfigurationManager.AppSettings["EmailPwd"].ToString());
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtEmployeeID.Text = gv.SelectedRow.Cells[1].Text;
                txtEmployeeName.Text = gv.SelectedRow.Cells[2].Text;
                txtDesignation.Text = gv.SelectedRow.Cells[3].Text;
                ddDepartment.Text = gv.SelectedRow.Cells[4].Text;
                txtLocation.Text = gv.SelectedRow.Cells[5].Text;
                txtEmailId.Text = gv.SelectedRow.Cells[6].Text;
                txtMobileNo.Text = gv.SelectedRow.Cells[7].Text;
                txtReportingTo.Text = gv.SelectedRow.Cells[8].Text;
                txtQualification.Text = gv.SelectedRow.Cells[9].Text;
                txtExperience.Text = gv.SelectedRow.Cells[10].Text;


                //ddMeetingRoomName.Text = DGV.SelectedRow.Cells[2].Text;
                //ddBuildingName.SelectedValue = DGV.SelectedRow.Cells[3].Text;
                //ddFloor.SelectedValue=DGV.SelectedRow.Cells[4].Text;


                //using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                //{
                //    SqlCommand cmd = new SqlCommand("SP_MEETINGROOMMASTER_GRIDSELECT", sqlConnection);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);
                //    if (ds != null)
                //    {
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            //ddMeetingRoomName.Text = ds.Tables[0].Rows[0]["MEETINGROOMNAME"].ToString();
                //            //ddBuildingName.SelectedValue = ds.Tables[0].Rows[0]["BUILDINGNAME"].ToString();
                //            //ddFloor.SelectedValue = ds.Tables[0].Rows[0]["FLOORNO"].ToString();
                //            //txtNofPerson.Text = ds.Tables[0].Rows[0]["NOOFPERSON"].ToString();
                //            //ddStatus.SelectedValue = ds.Tables[0].Rows[0]["MEETINGROOMSTATUS"].ToString();

                //            //chckCamera.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["CAMERA"]);
                //            //chckMike.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MIKE"].ToString());
                //            //chckSpeaker.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["SPEAKER"].ToString());
                //            //chckMonitor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["MONITOR"].ToString());
                //            //chckProjector.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["PROJECTOR"].ToString());

                //        }

                //    }

                //}
            }
            catch (Exception ex)
            { }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtEmployeeID.Text = "";
                txtEmployeeName.Text = "";
                txtDesignation.Text = "";
                ddDepartment.SelectedIndex = 0;
                txtLocation.Text = "";
                txtEmailId.Text = "";
                txtMobileNo.Text = "";
                txtReportingTo.Text = "";
                txtQualification.Text = "";
                txtExperience.Text = "";
                txtWelcomeMsg.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
    }
}