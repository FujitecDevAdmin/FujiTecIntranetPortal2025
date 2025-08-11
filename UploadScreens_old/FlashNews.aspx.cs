using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ConfulenceLandingPage.UploadScreens
{
    public partial class FlashNews : System.Web.UI.Page
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

            try
            {
                // Get values from the form controls
                string title = TextBoxTitle.Text.Trim();
                string description = TextBoxDescription.Text.Trim();
                DateTime? startDate = DateTime.TryParse(TextBoxStartDate.Text.Trim(), out var tempStartDate) ? tempStartDate : (DateTime?)null;
                DateTime? endDate = DateTime.TryParse(TextBoxEndDate.Text.Trim(), out var tempEndDate) ? tempEndDate : (DateTime?)null;
                string userId = Session["USERID"] != null ? Session["USERID"].ToString() : "Not Defined";


                if (string.IsNullOrEmpty(title))
                {
                    throw new Exception("Title cannot be empty.");
                }

                if (string.IsNullOrEmpty(description))
                {
                    throw new Exception("Description cannot be empty.");
                }

                if (startDate == DateTime.MinValue)
                {
                    throw new Exception("Start Date must be selected.");
                }

                if (endDate == DateTime.MinValue)
                {
                    throw new Exception("End Date must be selected.");
                }

                string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;


                // Insert data into the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO FlashNews (Title, [Description], PublishedDate, StartDate, EndDate, Active, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy)
                             VALUES (@Title, @Description, GETDATE(), @StartDate, @EndDate, 1, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", userId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userId);


                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Flash News added successfully!');</script>");

                TextBoxTitle.Text = string.Empty;
                TextBoxDescription.Text = string.Empty;
                TextBoxStartDate.Text = string.Empty;
                TextBoxEndDate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error adding Flash News: {ex.Message}');</script>");
            }

        }


    }
}