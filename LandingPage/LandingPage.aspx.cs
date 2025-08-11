using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace FujiTecIntranetPortal.LandingPage
{
    public partial class LandingPage : System.Web.UI.Page
    {
        string videosFolder = @"LandingPageResources\Videos\Live";

        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        LandingPageService landingPageService;

        VideoService videoService;

        LandingPageItems landingPageItems;

        List<EventSlideShowItem> eventSlideShowItems;

        List<NewsRendererItem> newsRendererItems;

        List<EmployeeSlideShowItem> employeeSlideShowItems;

        List<EmployeeContactItem> employeeContactItems;

        List<ProjectGalleryItem> projectGalleryItems;

        List<AwarenessSlideShowItem> awarenessSlideShowItems;

        List<QuotesContainerItem> quotesContainerItems;

        List<VideoContainerItem> videoContainerItems;

        List<QuickLinksContainerItem> quickLinksContainerItems;

        List<FlashNewsItem> flashNewsItem;

        public LandingPage()
        {
            landingPageService = new LandingPageService(strcon);
            landingPageItems = landingPageService.LoadLandingPageContent();

            HttpContextBase httpContext = HttpContext.Current != null ? new HttpContextWrapper(HttpContext.Current) : null;

            videoService = new VideoService(strcon, videosFolder, httpContext);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if it's the initial page load and the control is not null
            if (!IsPostBack)
            {
                ScanVideos();
                LoadItems();
                AssignItemsToHiddenFields();

                //ShowPopupButton.Attributes.Add("onclick", "showPopup(); return false;");
            }

        }
        public void ScanVideos()
        {
            videoService.ScanAndUpdateVideoFiles();
        }

        public void LoadItems()
        {

            // Fetch EventSlideShowItems
            eventSlideShowItems = GetItemsOfType<EventSlideShowItem>("EventSlideShow");

            // Fetch NewsRendererItems
            newsRendererItems = GetItemsOfType<NewsRendererItem>("NewsRenderer");

            // Fetch EmployeeSlideShowItems
            employeeSlideShowItems = GetItemsOfType<EmployeeSlideShowItem>("EmployeeSlideShow");

            // Fetch EmployeeContactItems
            employeeContactItems = GetItemsOfType<EmployeeContactItem>("EmployeeContact");

            // Fetch ProjectGalleryItems
            projectGalleryItems = GetItemsOfType<ProjectGalleryItem>("ProjectGallery");

            // Fetch AwarenessSlideShowItems
            awarenessSlideShowItems = GetItemsOfType<AwarenessSlideShowItem>("AwarenessSlideShow");

            // Fetch QuotesContainerItems
            quotesContainerItems = GetItemsOfType<QuotesContainerItem>("QuotesContainer");

            // Fetch VideoContainerItems
            videoContainerItems = GetItemsOfType<VideoContainerItem>("VideoContainer");

            // Fetch QuickLinksContainerItems
            quickLinksContainerItems = GetItemsOfType<QuickLinksContainerItem>("QuickLinksContainer");

            // Fetch FlashNewsItems
            flashNewsItem = GetItemsOfType<FlashNewsItem>("FlashNews");
          //  GetMarqueeContent();

        }

        public void AssignItemsToHiddenFields()
        {
            if (eventSlideShowItems != null && eventSlideShowItems.Count > 0)
            {
                string eventSlideShowJson = Newtonsoft.Json.JsonConvert.SerializeObject(eventSlideShowItems);
                eventSlideShowData.Value = eventSlideShowJson;
            }

            if (newsRendererItems != null && newsRendererItems.Count > 0)
            {
                string newsRendererJson = Newtonsoft.Json.JsonConvert.SerializeObject(newsRendererItems);
                newsRendererData.Value = newsRendererJson;
            }

            if (employeeSlideShowItems != null && employeeSlideShowItems.Count > 0)
            {
                string employeeSlideShowJson = Newtonsoft.Json.JsonConvert.SerializeObject(employeeSlideShowItems);
                employeeSlideShowData.Value = employeeSlideShowJson;
            }

            if (employeeContactItems != null && employeeContactItems.Count > 0)
            {
                string employeeContactJson = Newtonsoft.Json.JsonConvert.SerializeObject(employeeContactItems);
                employeeContactData.Value = employeeContactJson;
            }

            if (projectGalleryItems != null && projectGalleryItems.Count > 0)
            {
                string projectGalleryJson = Newtonsoft.Json.JsonConvert.SerializeObject(projectGalleryItems);
                projectGalleryData.Value = projectGalleryJson;
            }

            if (awarenessSlideShowItems != null && awarenessSlideShowItems.Count > 0)
            {
                string awarenessSlideShowJson = Newtonsoft.Json.JsonConvert.SerializeObject(awarenessSlideShowItems);
                awarenessSlideShowData.Value = awarenessSlideShowJson;
            }

            if (quotesContainerItems != null && quotesContainerItems.Count > 0)
            {
                string quotesContainerJson = Newtonsoft.Json.JsonConvert.SerializeObject(quotesContainerItems);
                quotesData.Value = quotesContainerJson;
            }

            if (videoContainerItems != null && videoContainerItems.Count > 0)
            {
                string videoContainerJson = Newtonsoft.Json.JsonConvert.SerializeObject(videoContainerItems);
                videoData.Value = videoContainerJson;
            }

            if (quickLinksContainerItems != null && quickLinksContainerItems.Count > 0)
            {
                string quickLinksContainerJson = Newtonsoft.Json.JsonConvert.SerializeObject(quickLinksContainerItems);
                quickLinksData.Value = quickLinksContainerJson;
            }

            if (flashNewsItem != null && flashNewsItem.Count > 0)
            {
                string flashNewsItemJson = Newtonsoft.Json.JsonConvert.SerializeObject(flashNewsItem);
                flashNewsData.Value = flashNewsItemJson;
            }
        }

        // Create a generic method to retrieve items of a specific type
        private List<T> GetItemsOfType<T>(string tableName)
        {
            List<T> items = new List<T>();

            if (landingPageItems != null && landingPageItems.TablesData != null)
            {
                TableData tableData = landingPageItems.TablesData.FirstOrDefault(data => data.TableName == tableName);
                if (tableData != null)
                {
                    items = tableData.TableItems.OfType<T>().ToList();
                }
            }

            return items;
        }

        protected string GetMarqueeContent()
        {
            if (flashNewsItem == null || flashNewsItem.Count == 0)
            {
                return ""; // Return empty string if the list is null or empty
            }


            StringBuilder content = new StringBuilder();

            if (flashNewsItem != null && flashNewsItem.Count > 0)
            {
                content.Append("<div style='margin-right: 20px;'>"); // Adding margin-bottom for spacing

                foreach (var news in flashNewsItem)
                {
                    content.Append("<strong style='color:red; background-color:yellow;'>").Append(news.Title).Append("</strong>").Append(" ");
                    content.Append("<span style='font-size: 20px; color: red;'>&#9888;</span>").Append(" ");
                    content.Append("<span>").Append(news.Description).Append("</span>");
                   // content.Append("good morning ").Append("vinoth ").Append("ready ");
                }

                content.Append("</div>");
            }

            return content.ToString();
        }

        // Function to truncate content to a specified length
        protected string TruncateContent(string content, int length)
        {
            if (!string.IsNullOrWhiteSpace(content) && content.Length > length)
            {
                return content.Substring(0, length) + "...";
            }
            return content;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Process form data when SubmitButton is clicked
            string fiveDigitEmployeeID = EmployeeIDTextBox.Text.Trim(); // int.TryParse(EmployeeIDTextBox.Text.Trim(), out int parsedID) ? parsedID.ToString("D5") : "00000";
            string password = PasswordTextBox.Text.Trim();

            // Perform necessary actions with the form data (e.g., save to database)
            // ...

            // Optionally, send a response message back or perform other actions
            //ScriptManager.RegisterStartupScript(this, GetType(), "hidePopupScript", "hidePopup();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alertScript", "alert('Form submitted successfully!');", true);

            try
            {
                Session["USERID"] = fiveDigitEmployeeID;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_APPLICATION_BSDON_USERLOGIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = fiveDigitEmployeeID;
                    cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@SESSIONID", SqlDbType.VarChar).Value = this.Session.SessionID;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    con.Open();
                    DA.Fill(Ds);
                    //response.result = false;
                    if (Ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                    {
                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            Session["VMSUSERNAME"] = Ds.Tables[1].Rows[0]["USERNAME"].ToString();
                            Session["VMSROLES"] = Ds.Tables[1].Rows[0]["ROLES"].ToString();
                            Session["VMSUSERID"] = fiveDigitEmployeeID;
                        }
                        if (Ds.Tables[2].Rows.Count > 0)
                        {
                            Session["MRBUSERNAME"] = Ds.Tables[2].Rows[0]["USERNAME"].ToString();
                            Session["MRBROLES"] = Ds.Tables[2].Rows[0]["ROLES"].ToString();
                            Session["MRBUSERID"] = fiveDigitEmployeeID;
                        }
                        if (Ds.Tables[3].Rows.Count > 0)
                        {
                            Session["EQPBRKUPUSERNAME"] = Ds.Tables[3].Rows[0]["USERNAME"].ToString();
                            Session["EQPBRKUPROLES"] = Ds.Tables[3].Rows[0]["ROLES"].ToString();
                            Session["EQPBRKUPUSERID"] = fiveDigitEmployeeID;
                            // Session["EQPBRKUP"] = true;
                        }
                        if (Ds.Tables[4].Rows.Count > 0)
                        {
                            Session["USERNAME"] = Ds.Tables[4].Rows[0]["USERNAME"].ToString();
                            Session["ROLES"] = Ds.Tables[4].Rows[0]["ROLES"].ToString();
                            Session["USERID"] = fiveDigitEmployeeID;
                            Session["Loc"] = Ds.Tables[4].Rows[0]["BranchID"].ToString();
                            // Session["EQPBRKUP"] = true;
                        }
                        Response.Redirect("~/Home.aspx",false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                    else
                    {
                        string script = "<script>showPopup();</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowPopupScript", script);

                        // Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Please enter valid username and password')</script>");
                        //LoginMessageLable.ForeColor = System.Drawing.Color.Red;

                        //  if ((txtUsername.Text != String.Empty) || (txtPassword.Text != String.Empty))
                        LoginMessageLable.Text = "Please enter correct login details";
                    }
                }

            }
            catch (Exception ex)
            {
                string script = "<script>showPopup();</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowPopupScript", script);

                //LoginMessageLable.ForeColor = System.Drawing.Color.Red;
                LoginMessageLable.Text = ex.Message;
            }
        }
    }
}


