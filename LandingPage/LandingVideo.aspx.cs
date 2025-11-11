using System;
using System.Web;

namespace FujiTecIntranetPortal
{
    public partial class LandingVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user has already viewed the landing video in this session
            bool hasViewedLandingVideo = Session["LandingVideoLoaded"] != null;

            if (!IsPostBack)
            {
                // If user has already viewed the video, redirect to landing page
                if (hasViewedLandingVideo)
                {
                    Response.Redirect("~/LandingPage/LandingPage.aspx");
                    return;
                }

                // Set session flag to indicate user has viewed the landing video
                Session["LandingVideoLoaded"] = true;
                hasViewedLandingVideo = true;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void SetSessionValue(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string GetSessionValue(string key)
        {
            var value = HttpContext.Current.Session[key];
            return value != null ? value.ToString() : null;
        }

    }
}