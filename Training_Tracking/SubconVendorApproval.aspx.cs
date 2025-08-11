using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubconVendorApproval : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    //txtEmployeeID.Focus();
                }
            }
            catch (Exception ex)
            {
                //lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubConApprovalPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["GV"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                //lblmsg.Text = ex.Message;
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = (int)ViewState["rowindex"];

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["GV"];
            string ID = dt.Rows[i]["ID"].ToString();
            string CompanyNm = dt.Rows[i]["Company name"].ToString();
            string Branchname = dt.Rows[i]["Branch name"].ToString();
            string Appstatus = dt.Rows[i]["Approval Status"].ToString();
            string ApproverName = dt.Rows[i]["ApproverName"].ToString();
            Session["TTAPPID"] = ID;
          //  Session["Approver"] = ApproverName;
            string url = "Training_Tracking/SubConRegistration.aspx?ID=" + ID + "&Approver=" + ApproverName;
            // string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=300,top=100,resizable=yes');";
            // ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Response.Redirect("~/" + url);
        }
        //protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        //{
        //}
        //protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //}

        protected void gv_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ViewState["rowindex"] = Convert.ToInt32(e.CommandArgument);
                }
            }
            catch (Exception ex)
            {
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                //lblmsg.Text = ex.Message;
            }
        }
    }
}