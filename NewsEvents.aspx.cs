using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class NewsEvents : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  SetInitialRow();
                    initialize();
                    hdfilepath.Value = "no";
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                string script = "showMessage('" + message + "');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script, true);

            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if ((FileUpload1.HasFile) && (txtEventName.Text.Trim() != ""))
                {
                    //////////////////////////////////////////////////////////////////
                    System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                    int height = img.Height;
                    int width = img.Width;
                    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    if (size < 300)
                    {
                        string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/NewsEvents/FUJI/") + txtEventName.Text + extension);
                        SAVE(extension);
                        initialize();
                    }
                    /////////////////////////////////////////////////////////////////
                    else if (size > 300)
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "File size must not exceed 300kb";
                    }
                    //if (height > 250 || width > 400)
                    //{
                    //    lblmsg.Text = "Height and Width must not exceed 100px.";
                    //}

                    // string filepath = Path.GetFullPath(Server.MapPath("~/NewsEvents/"));
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter or select mandatory fields";
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        //protected void ValidateFileSize(object sender, ServerValidateEventArgs e)
        //{
        //    System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
        //    int height = img.Height;
        //    int width = img.Width;
        //    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
        //    if (size > 300)
        //    {
        //        CustomValidator1.ErrorMessage = "File size must not exceed 300 KB.";
        //        e.IsValid = false;
        //    }
        //    //if (height > 100 || width > 100)
        //    //{
        //    //    CustomValidator1.ErrorMessage = "Height and Width must not exceed 100px.";
        //    //    e.IsValid = false;
        //    //}
        //}
        public void SendEmail()
        {
            string username = Session["USERNAME"] as string;
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
            message.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailTo"].ToString()));
            message.Subject = "Approval request for News & Events in Confluence";
            message.IsBodyHtml = true; //to make message body as html  

            /////////////////////////////////
            string Body = "Dear Sir , <br /><br />";
            //Body = Body + "<html>< style >table, th, td {border: 1px solid black;}</ style >";
            //Body = Body + "< body >< table >< tr >< td > Maria Anders </ td >< td > Germany </ td ></ tr >< tr >" ;

            //Body = Body + "< td > Francisco Chang </ td >< td > Mexico </ td ></ tr ></ table ></ body ></ html > ";
            //border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 1 + " width = " + 200 + "
            Body = Body + "I am requesting approval for <b> New and Events  :'" + txtEventName.Text + "' </b>  details is registered in our confluence site.<br /><br /> Please Approve/Reject to proceed on this News and Events and also let us know for any assistance<br /><br />";

            Body = Body + "Please click the link below,<br />";
            Body = Body + "URL :- http://confluence:8085/ <br /><br />";
            ////Body = Body + "<table border=" + 1 + " cellpadding=" + 1 + " cellspacing=" + 0 + " width = " + 200 + "> <tr><td>Pass No:</td><td>" + "vinoth" + "</td></ tr>";
            //// Body = Body + "<table border=\"1\" style=\"width:200px; margin:2px;padding:0; border:1px solid #6cb5d9; border-collapse:collapse;\"> <tr><td>Pass No:</td><td>" + "vinoth" + "</td></ tr>";
            //// Body = Body + "<tr><td>Pass No:</td><td>" + "Kumar" + "</td></ tr>";
            ////Body = Body + "< tr >< td > Centro comercial Moctezuma</ td >< td > Francisco Chang </ td ></ tr >";
            //// Body = Body + "</table><br/>";
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

            //////}
            //Body = Body + "</tbody></table><br />";
            //Body = Body + " <b>Reach us:</b> https://www.google.com/maps/dir//Fujitec+India+Pvt+Ltd,+Fujitec+India+Pvt.+Ltd,+P+-+51,+8th+Avenue,+Mahindra+World+City,+Chennai,+Tamil+Nadu+603002/@12.7233955,80.0187177,17z/data=!4m8!4m7!1m0!1m5!1m1!1s0x3a52f9ffe77cfea3:0x1f42c8b89cf093f9!2m2!1d80.0209064!2d12.7233903" + "<br />";
            ////Body = Body + "<b>Note: </b><br />Kindly show your fully vacinated certificate at securtiy gate during your visit. <br />If you bring any gadgets inform it to security, please ensure whether it is updated in your visitor pass.<br /><br />";
            //Body = Body + "<b>Note: </b><br /> I. Kindly show your fully vacinated certificate at securtiy gate during your visit.<br />II. All visitors are required to adhere to COVID-19 protocols of temperature screening, maintaining physical distancing, wearing a face cover and frequently washing/sanitizing hands. <br />III. If you bring any gadgets inform it to security, please ensure whether it is updated in your visitor pass.Happy Visitor<br /><br />";
            Body = Body + "<b>Regards,</b><br /> <b>" + username + " </b><br /><b>Fujitec India Pvt. Ltd.</b><br />";
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
        private void SAVE(string extension)
        {
            try
            {
                // Save the  file server new path
                #region Save File
                string appBasePath = Server.MapPath("~/");
                string eventFolderPath = Path.Combine(appBasePath, "LandingPageResources/Events/");
                if (!Directory.Exists(eventFolderPath))
                {
                    Directory.CreateDirectory(eventFolderPath);
                }

                //var existingFiles = Directory.GetFiles(eventFolderPath);
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
                string photoFilename = $"{DateTime.Now:yyyyMMddHHmmssfff}-{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(eventFolderPath, photoFilename);
                string resolvedFilePath = Path.Combine("/", filePath.Replace(appBasePath, ""));

                FileUpload1.SaveAs(filePath);

                #endregion Save File

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    //string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    ////string filepath = Path.GetFullPath(Server.MapPath("~/NewsEvents/FUJI/"));
                    //string filepath = ConfigurationManager.AppSettings["News_Image"].Trim();
                    //FileUpload1.SaveAs(Server.MapPath("~/NewsEvents/FUJI/") + DateTime.Now.ToString("yyyyMMddHHmmss") + extension);
                    SqlCommand cmd = new SqlCommand("[SP_NewandEvent_Insert]", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EventName", SqlDbType.VarChar).Value = txtEventName.Text;
                    cmd.Parameters.Add("@FileNamepath", SqlDbType.VarChar).Value = resolvedFilePath;
                    cmd.Parameters.Add("@ApprovalStatus", SqlDbType.VarChar).Value = "MSC0005";
                    cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  

                    //cmd.Parameters.Add("@VISITORSTATUS", SqlDbType.VarChar).Value = "MSC0003";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        SendEmail();
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = txtEventName.Text + "  Saved successfully -- Sent to approval ";
                        //if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        //{
                        //    lblmsg.ForeColor = System.Drawing.Color.Green;
                        //    lblmsg.Text = "Employee Registered successfully";
                        //}
                        //else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                        //{                           
                        //    lblmsg.ForeColor = System.Drawing.Color.Green;
                        //    lblmsg.Text = "Employee Updated successfully";
                        //}
                        //else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                        //{
                        //    lblmsg.ForeColor = System.Drawing.Color.Red;
                        //    lblmsg.Text = "Error in registration";
                        //}
                    }

                }
            }
            catch (Exception ex)
            {
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

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";

                txtEventName.Text = gv.SelectedRow.Cells[2].Text;

            }
            catch (Exception ex)
            { }

        }


        public void initialize()
        {
            try
            {
                ////// string[] files = Directory.GetFiles(ConfigurationManager.AppSettings["FilePath"].ToString());
                //DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("FileName", typeof(string)));

                //dt.Columns.Add(new DataColumn("File_Path", typeof(string)));

                ////dt.Columns.Add(new DataColumn("View", typeof(string)));
                //DataRow dr = null;
                //dr = dt.NewRow();
                ////string path = ConfigurationManager.AppSettings["News_Image"].ToString();
                //string path = Server.MapPath(ConfigurationManager.AppSettings["News_Image"].ToString());
                //string[] fileEntries = Directory.GetFiles(path);

                //foreach (string fileName in fileEntries)
                //{
                //    dr["FileName"] = fileName.Remove(0, path.Length);
                //    dr["File_Path"] = fileName;
                //   // dr["View"] = "View";
                //    dt.Rows.Add(dr);
                //    dr = dt.NewRow();
                //}
                //ViewState["DGVAvailability"] = dt;
                //gv.DataSource = dt;
                //gv.DataBind();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_NewsandEventsPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["DGVAvailability"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                        //DataRow dr = ds.Tables[0].NewRow();
                        //dr.ItemArray = new object[] { 0, "--Select Department--" };
                        //ds.Tables[0].Rows.InsertAt(dr, 0);
                        ////ddDepartment.DataTextField = "DEPARTMENTNAME";
                        ////ddDepartment.DataValueField = "DEPARTMENTNAME";
                        //////ddDepartment.DataValueField = "DEPARTMENTID";
                        ////ddDepartment.DataSource = ds.Tables[0];
                        ////ddDepartment.DataBind();

                        //gv.DataSource = ds.Tables[1];
                        //gv.DataBind();
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
                gv.DataSource = (DataTable)ViewState["DGVAvailability"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

            try
            {
                txtEventName.Text = String.Empty;
                initialize();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string delfilepath = gv.Rows[e.RowIndex].Cells[3].Text;
                string ID = gv.Rows[e.RowIndex].Cells[1].Text;
                if (ID != String.Empty)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_NewsandEventsDelete", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            ViewState["DGVAvailability"] = ds.Tables[0];
                            gv.DataSource = ds.Tables[0];
                            gv.DataBind();

                        }
                    }
                }
                FileInfo file = new FileInfo(delfilepath);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                    lblmsg.Text = " file deleted successfully";
                    lblmsg.ForeColor = Color.Green;
                }
                else
                {
                    lblmsg.Text = " This file does not exists ";
                    lblmsg.ForeColor = Color.Red;
                }

                DataTable dt = new DataTable();
                dt = (DataTable)(ViewState["DGVAvailability"]);
                dt.Rows.RemoveAt(e.RowIndex);
                gv.DataSource = dt;
                gv.DataBind();
                initialize();

            }
            catch (Exception ex)
            {

            }
        }
        protected void GV_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Filename = e.Row.Cells[1].Text;
                string del = hdfilepath.Value.ToString();
                if (del == "delete")
                {
                    var filePath = Server.MapPath(ConfigurationManager.AppSettings["News_Image"].ToString() + Filename);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);

                    }
                    hdfilepath.Value = "none";
                }

            }

        }

        private void AddNewRowToGrid()
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["EventName"] = txtEventName.Text;
                drCurrentRow["FilePath"] = fileName;
                dtCurrentTable.Rows.Add(drCurrentRow);

                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtCurrentTable.Rows[i][1].ToString() == string.Empty)
                        dtCurrentTable.Rows[i].Delete();
                }
                dtCurrentTable.AcceptChanges();

                ViewState["CurrentTable"] = dtCurrentTable;


                //DGVADD.DataSource = dtCurrentTable;
                //DGVADD.DataBind();
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dt.Rows[i]["EventName"].ToString();// = txtRcptHdNo.Text;
                        dt.Rows[i]["FilePath"].ToString();// = ddlRcptDtlsItem.SelectedItem.Text;
                        //dt.Rows[i]["UOM"].ToString();// = txtRcptDtlsUOM.Text;
                        //dt.Rows[i]["Description"].ToString();// = txtRcptDtlsDescr.Text;
                        //dt.Rows[i]["Rate"].ToString();// = txtRcptDtlsRt.Text;
                        //dt.Rows[i]["Quatity"].ToString();// = txtRcptDtlsQty.Text;
                        //dt.Rows[i]["Amount"].ToString();// = txtRcptDtlsAmount.Text;
                    }
                }
            }
        }

        private void SetInitialRow()
        {

            DataTable datatable = new DataTable();

            datatable.Columns.Add("EventName", typeof(string));
            datatable.Columns.Add("FilePath", typeof(string));
            //datatable.Columns.Add("UOM", typeof(string));
            //datatable.Columns.Add("Description", typeof(string));
            //datatable.Columns.Add("Rate", typeof(string));
            //datatable.Columns.Add("Quatity", typeof(string));
            //datatable.Columns.Add("Amount", typeof(string));
            DataRow dr = null;

            dr = datatable.NewRow();

            dr["EventName"] = string.Empty;// txtRcptHdNo.Text;
            dr["FilePath"] = string.Empty;// ddlRcptDtlsItem.SelectedItem.Text;
            //dr["UOM"] = string.Empty;// txtRcptDtlsUOM.Text;
            //dr["Description"] = string.Empty;// txtR+cptDtlsDescr.Text;
            //dr["Rate"] = string.Empty;// txtRcptDtlsRt.Text;
            //dr["Quatity"] = string.Empty;// txtRcptDtlsQty.Text;
            //dr["Amount"] = string.Empty;// txtRcptDtlsAmount.Text;

            datatable.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = datatable;

            //DGVADD.DataSource = datatable;
            //DGVADD.DataBind();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((FileUpload1.HasFile) && (txtEventName.Text.Length > 0))
                {
                    AddNewRowToGrid();
                }
                //if ((FileUpload1.HasFile) && (txtEventName.Text.Length > 0))
                //{
                //    DataTable dttable = new DataTable();
                //    DataRow dr = null;
                //    dttable.Columns.Add(new DataColumn("EventNamePhoto", typeof(string)));

                //    dttable.Columns.Add(new DataColumn("FilePath", typeof(string)));

                //    dr = dttable.NewRow();

                //    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //    if (ViewState["CurrentTable"] != null)
                //    {
                //        DataTable dt = (DataTable)ViewState["CurrentTable"];
                //        if (dt.Rows.Count > 0)
                //        {
                //            for (int i = 0; i < dt.Rows.Count; i++)
                //            {
                //                dr = dt.NewRow();
                //                dt.Rows[i]["EventNamePhoto"] = txtRcptHdNo.Text;
                //                dt.Rows[i]["FilePath"] = txtRcptHdNo.Text;
                //            }
                //        }



                //        ////////////////
                //        dr["EventNamePhoto"] = txtEventName.Text;
                //        dr["FilePath"] = fileName + extension;
                //        dt.Rows.Add(dr);
                //        ViewState["CurrentTable"] = dt;
                //        DGVADD.DataSource = dt;
                //        DGVADD.DataBind();
                //    }
                //    else
                //    {
                //       DataTable dt = new DataTable();

                //        dt.Columns.Add(new DataColumn("EventNamePhoto", typeof(string)));

                //        dt.Columns.Add(new DataColumn("FilePath", typeof(string)));

                //        dr = dt.NewRow();


                //        ////////////////
                //        dr["EventNamePhoto"] = txtEventName.Text;
                //        dr["FilePath"] = fileName + extension;
                //        dt.Rows.Add(dr);

                //        DGVADD.DataSource = dt;
                //        DGVADD.DataBind();
                //    }
                //    ////////////////

                //    //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/NewsEvents/FUJI/") + txtEventName.Text + extension);
                //    //lblmsg.ForeColor = System.Drawing.Color.Green;
                //    //lblmsg.Text = fileName + "Saved successfully";
                //}
                //else
                //{
                //    lblmsg.ForeColor = System.Drawing.Color.Red;
                //    lblmsg.Text = "Please enter or select mandatory fields";
                //}
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddClear_Click(object sender, EventArgs e)
        {
            try
            {
                initialize();
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
    }
}