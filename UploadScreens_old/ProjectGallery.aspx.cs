using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConfulenceLandingPage.UploadScreens
{
    public partial class ProjectGallery : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        DataTable ProjectDetails;


        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (!IsPostBack)
            {
                //BindProjectDetails(); // Call method to bind data on initial load
            }

        }

        // Method to bind project details to the DataGrid
        private void BindProjectDetails()
        {
            // Fetch data from your database or another source
            DataTable dt = GetDataFromDatabase(); // Implement this method to retrieve your data

            if (dt != null && dt.Rows.Count > 0)
            {
                //ProjectDetailsGrid.DataSource = dt;
                //ProjectDetailsGrid.DataBind();
            }
        }

        protected void ProjectDetailsGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "PreviewImage")
            {
                string imagePath = e.CommandArgument.ToString();

                // Register the script to execute on the client side
                string script = $"openFullScreen('{imagePath}', 'Preview Image')";
                ScriptManager.RegisterStartupScript(this, GetType(), "PreviewImageScript", script, true);
            }
            else if (e.CommandName == "DeleteProject")
            {
                string projectName = e.CommandArgument.ToString();
                DeleteProject(projectName);
                BindProjectDetails(); // Rebind after deletion
            }
        }

        private void DeleteProject(string projectName)
        {
            // Implement deletion logic based on your requirements
            // Example: Delete the project with the specified name from the database   

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "DELETE FROM ProjectDetails WHERE ProjectName = @ProjectName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProjectName", projectName);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Method to simulate fetching data from the database
        private DataTable GetDataFromDatabase()
        {
            ProjectDetails = new DataTable();

            string query = "SELECT ID, ProjectName, ProjectLocation, ProjectDescription, ImagesPath FROM ProjectDetails Order By ID Desc";

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(ProjectDetails);
                }
                catch (SqlException ex)
                {
                    // Handle exceptions here
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return ProjectDetails;
        }

        protected void ButtonUpload_Click(object sender, EventArgs e)
        {

            if (FileUploadImages.HasFiles)
            {
                try
                {
                    string projectName = TextBoxProjectName.Text.Trim().ToUpper();
                    string projectLocation = TextBoxProjectLocation.Text.Trim().ToUpper();
                    string projectDescription = TextBoxProjectDescription.Text.Trim().ToUpper();

                    // Check if the project name is null or empty
                    if (string.IsNullOrEmpty(projectName))
                    {
                        throw new Exception("Project name cannot be empty."); // You can customize the error message as needed
                    }

                    string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

                    string appBasePath = Server.MapPath("~/");
                    // Create a folder for the project if it doesn't exist
                    string projectFolderPath = Path.Combine(appBasePath, "LandingPageResources/Projects/" + projectName);
                    if (!Directory.Exists(projectFolderPath))
                    {
                        Directory.CreateDirectory(projectFolderPath);
                    }


                    foreach (HttpPostedFile uploadedFile in FileUploadImages.PostedFiles)
                    {
                        // Generate a unique file name using a GUID
                        string fileExtension = Path.GetExtension(uploadedFile.FileName);
                        string uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}-{Guid.NewGuid()}{fileExtension}";

                        string filePath = Path.Combine(projectFolderPath, uniqueFileName);
                        string resolvedFilePath = Path.Combine("/", filePath.Replace(appBasePath, ""));

                        var existingFiles = Directory.GetFiles(projectFolderPath);
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
                            string query = @"INSERT INTO ProjectDetails (ProjectName, ProjectLocation, ProjectDescription, ImagesPath, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
                                           VALUES (@ProjectName, @ProjectLocation, @ProjectDescription, @ImagesPath, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@ProjectName", projectName);
                            cmd.Parameters.AddWithValue("@ProjectLocation", projectLocation);
                            cmd.Parameters.AddWithValue("@ProjectDescription", projectDescription);
                            cmd.Parameters.AddWithValue("@ImagesPath", resolvedFilePath);
                            cmd.Parameters.AddWithValue("@CreatedBy", userId);
                            cmd.Parameters.AddWithValue("@ModifiedBy", userId);

                            con.Open();
                            cmd.ExecuteNonQuery();

                            TextBoxProjectName.Text = string.Empty;
                            TextBoxProjectLocation.Text = string.Empty;
                            TextBoxProjectDescription.Text = string.Empty;
                            FileUploadImages.Dispose();

                        }
                    }

                    // Display success message or perform redirection
                    Response.Write("<script>alert('Images uploaded successfully!');</script>");
                }
                catch (Exception ex)
                {
                    // Display error message
                    Response.Write("<script>alert('Error uploading images: " + ex.Message + "');</script>");
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
