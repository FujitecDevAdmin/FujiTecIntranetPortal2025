using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.UploadScreens
{
    public partial class Awareness : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {

            }
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {

            if (FileUploadImages.HasFiles)
            {
                try
                {
                    string awarenessCategory = TextBoxAwarenessCategory.Text.Trim().ToUpper();

                    // Check if the awareness category name is null or empty
                    if (string.IsNullOrEmpty(awarenessCategory))
                    {
                        throw new Exception("Awareness category cannot be empty."); // You can customize the error message as needed
                    }

                    string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

                    string appBasePath = Server.MapPath("~/");
                    // Create a folder for the Awareness Category if it doesn't exist
                    string awarenessCategoryFolderPath = Path.Combine(appBasePath, "LandingPageResources/Awareness/" + awarenessCategory);
                    if (!Directory.Exists(awarenessCategoryFolderPath))
                    {
                        Directory.CreateDirectory(awarenessCategoryFolderPath);
                    }


                    foreach (HttpPostedFile uploadedFile in FileUploadImages.PostedFiles)
                    {
                        // Generate a unique file name using a Date time
                        string fileExtension = Path.GetExtension(uploadedFile.FileName);
                        string uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}-{Guid.NewGuid()}{fileExtension}";


                        string filePath = Path.Combine(awarenessCategoryFolderPath, uniqueFileName);
                        string resolvedFilePath = Path.Combine("/", filePath.Replace(appBasePath, ""));

                        var existingFiles = Directory.GetFiles(awarenessCategoryFolderPath);
                        foreach (var existingFile in existingFiles)
                        {
                            if (FilesAreIdentical(existingFile, uploadedFile.InputStream))
                            {
                                string existingFileName = Path.GetFileName(existingFile);
                                string uploadingFileName = uploadedFile.FileName;

                                // If identical file exists, inform the user and prevent upload
                                string alertMessage = $"File {uploadingFileName} has the same content as {existingFileName}. Please upload a different file.";
                                Response.Write($"<script>alert('{alertMessage}');</script>");
                                return; // Stop further processing
                            }
                        }

                        // Save the image to the specified path
                        uploadedFile.SaveAs(filePath);

                        string userId = Session["USERID"] != null ? Session["USERID"].ToString() : "Not Defined";

                        // Insert data into the database
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            string query = @"INSERT INTO AwarenessDetails (AwarenessCategory, ImagesPath, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
                                           VALUES (@AwarenessCategory, @ImagesPath, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@AwarenessCategory", awarenessCategory);
                            cmd.Parameters.AddWithValue("@ImagesPath", resolvedFilePath);
                            cmd.Parameters.AddWithValue("@CreatedBy", userId);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userId);

                            con.Open();
                            cmd.ExecuteNonQuery();

                            TextBoxAwarenessCategory.Text = string.Empty;
                            FileUploadImages.Dispose();

                        }
                    }

                    // Display success message or perform redirection
                    //Response.Write("<script>alert('Images uploaded successfully!');</script>");
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Images uploaded successfully!";
                        
                }
                catch (Exception ex)
                {
                    // Display error message
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text =  ex.Message;
                }
            }
            else
            {
                // Display message if no files are selected
                Response.Write("<script>alert('Please select image files to upload.');</script>");
            }

        }
        // Method to compare file content
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
    }
}