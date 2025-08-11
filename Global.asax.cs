using System;

namespace FujiTecIntranetPortal
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

            if (Session["USERID"] != null)
            {
                //Redirect to Welcome Page if Session is not null    
                // Response.Redirect("Welcome.aspx");
                //Session["ssnid"] = Session.SessionID;

            }
            else
            {
                //Redirect to Login Page if Session is null & Expires     
                //Response.Redirect("~/Login.aspx");
                Response.Redirect("~/LandingPage/LandingPage.aspx");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}