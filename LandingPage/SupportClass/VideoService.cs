using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public class VideoService
{
    private string connectionString;
    private readonly string videoDirectoryPath;
    private HttpContextBase httpContext;
    public VideoService(string connectionString, string videoDirectoryPath, HttpContextBase httpContext)
    {
        this.connectionString = connectionString;
        this.videoDirectoryPath = videoDirectoryPath;
        this.httpContext = httpContext;
    }

    public void ScanAndUpdateVideoFiles()
    {
        var list = GetAllVideoFiles(videoDirectoryPath);
        UpdateVideoDetails(list);
    }

    // Get all video files from a directory
    public List<string> GetAllVideoFiles(string directoryPath)
    {
        string[] videoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".mkv" };
        List<string> videoFiles = new List<string>();

        try
        {
            string appBasePath = HttpContext.Current.Server.MapPath("~/");
            string serverPath = Path.Combine(appBasePath, directoryPath);

            foreach (string extension in videoExtensions)
            {
                videoFiles.AddRange(Directory.GetFiles(serverPath, "*" + extension, SearchOption.AllDirectories));
            }
        }
        catch (Exception)
        {
            throw;
        }

        return videoFiles;
    }

    // Update VideoDetails table with video information 
    public void UpdateVideoDetails(List<string> videoPaths)
    {
        try
        {   // Logic to clear existing records in the VideoDetails table
            ClearVideoDetailsTable();

            string userId = httpContext.Session["USERID"] != null ? httpContext.Session["USERID"].ToString() : "Not Defined";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var videoPath in videoPaths)
                {
                    string appBasePath = HttpContext.Current.Server.MapPath("~/");

                    string videoName = Path.GetFileNameWithoutExtension(videoPath).ToUpper();

                    string resolvedFilePath = Path.Combine("/", videoPath.Replace(appBasePath, ""));

                    string query = "INSERT INTO VideoDetails (VideoName, VideoCategory, VideoPath, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy) " +
                                   "VALUES (@VideoName, @VideoCategory, @VideoPath, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy)";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Set parameters
                    command.Parameters.AddWithValue("@VideoName", videoName);
                    command.Parameters.AddWithValue("@VideoCategory", string.Empty);
                    command.Parameters.AddWithValue("@VideoPath", resolvedFilePath);
                    command.Parameters.AddWithValue("@CreatedBy", userId);
                    command.Parameters.AddWithValue("@ModifiedBy", userId);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void ClearVideoDetailsTable()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "TRUNCATE TABLE VideoDetails"; // SQL query to truncate the table
                SqlCommand command = new SqlCommand(query, connection);

                command.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}
