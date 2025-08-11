using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class SubConRegReport : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtSubconRegID.Text.Length > 7))
                    Search();
                //else
                //    lblmsg.Text = "Please select the Mandatory Fields";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                txtSubconRegID.Text= string.Empty;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
                DGV.PageIndex = e.NewPageIndex;
                DGV.DataSource = (DataTable)ViewState["DGV"];
                DGV.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        public void Search()
        {
            try
            {
                lblmsg.Text = "";
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SubConReportRegStatus", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@SubConRegHDRID", SqlDbType.VarChar).Value = txtSubconRegID.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ViewState["DGV"] = ds.Tables[0];
                                ///////////////////////////////////////////////////
                                DGV.DataSource = ds.Tables[0];
                                DGV.DataBind();
                                ///////////////////////////////////////////////////  
                                //int index = DGV.Rows.Count - 1;
                                //DGV.Rows[index].Font.Bold = true;
                                ///////////////////////////////////////////////////
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

    }
}