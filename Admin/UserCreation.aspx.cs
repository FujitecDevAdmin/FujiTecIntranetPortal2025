using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Admin
{
    public partial class UserCreation : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                    PgLoad();
                
            }
            catch (Exception ex)
            {

            }
        }
        private void PgLoad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_UserPageLoad", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Session["USERID"].ToString(); 
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Users"] = ds.Tables[0];
                        //////////////////////////////////////////////////////

                        DataRow ddl = ds.Tables[0].NewRow();
                        ddl.ItemArray = new object[] { 0, "--Select Branch--" };
                        ds.Tables[0].Rows.InsertAt(ddl, 0);

                        ddBranch.DataTextField = "Branchdesc";
                        ddBranch.DataValueField = "BranchID";
                        ddBranch.DataSource = ds.Tables[0];
                        ddBranch.DataBind();

                        ViewState["DGV"] = ds.Tables[1];
                        gv.DataSource = ds.Tables[1];
                        gv.DataBind();

                        //////////////////////////////////////////////////////
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            if (!(Session["USERID"].ToString().Trim() == "02021" || Session["USERID"].ToString().Trim() == "01259" || Session["USERID"].ToString().Trim() == "00450" || Session["USERID"].ToString().Trim() == "03380"))
                            {
                                txtUserName.Text = ds.Tables[1].Rows[0]["USERNAME"].ToString();
                                txtPassword.Text = ds.Tables[1].Rows[0]["USERPASSWORD"].ToString();
                                ddBranch.SelectedValue = ds.Tables[1].Rows[0]["BRANCHID"].ToString();
                                txtEmailId.Text = ds.Tables[1].Rows[0]["EMAILID"].ToString();
                                txtMobileNo.Text = ds.Tables[1].Rows[0]["MOBILENO"].ToString();
                                ddstatus.SelectedValue = ds.Tables[1].Rows[0]["STATUSACTIVE"].ToString();
                                txtPassword.Attributes["value"] = ds.Tables[1].Rows[0]["USERPASSWORD"].ToString();
                                txtConfirmPassword.Attributes["value"] = ds.Tables[1].Rows[0]["USERPASSWORD"].ToString();
                                txtUserID.Text = Session["USERID"].ToString().Trim();
                                txtUserID.Enabled = false;
                            }
                            else
                            {
                                txtUserID.Enabled = true;
                            }
                        }
                        //////////////////////////////////////////////////////
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void SaveRec()
        {
            try
            {
                lblmsg.Text = "";
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_USERCREATION_INSERT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = txtUserID.Text;
                    cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = txtUserName.Text;
                    cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = txtPassword.Text;
                    cmd.Parameters.Add("@BRANCHID", SqlDbType.VarChar).Value = ddBranch.SelectedValue;
                    cmd.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtEmailId.Text;
                    cmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = txtMobileNo.Text;
                    cmd.Parameters.Add("@STATUSACTIVE", SqlDbType.VarChar).Value = ddstatus.SelectedValue;
                    cmd.Parameters.Add("@CREATEDBY", SqlDbType.Int).Value = Session["USERID"].ToString();
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            PgLoad();
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "New User Created Successfully";

                        }
                        else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                        {
                            PgLoad();
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "User Update Successfully";

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                if (validt != "Error")
                {
                    SaveRec();
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                if ((txtUserID.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* User ID should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtUserName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* User Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((ddBranch.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select User branch";
                    ViewState["Error"] = "Error";
                }
                if ((txtPassword.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Password should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if ((txtPassword.Text != txtConfirmPassword.Text))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Password and Confirm password should be same";
                    ViewState["Error"] = "Error";
                }
                //if ((ddstatus.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select User status";
                //    ViewState["Error"] = "Error";
                //}


            }
            catch (Exception ex)
            {

            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserID.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                txtEmailId.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                ddBranch.SelectedIndex= 0;
                ddstatus.SelectedIndex= 0;
                lblmsg.Text=string.Empty;
                txtConfirmPassword.Attributes["value"] = string.Empty;
                txtPassword.Attributes["value"] = string.Empty;
            }
            catch(Exception ex)
            {

            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtUserID.Text = gv.SelectedRow.Cells[1].Text;
                txtUserName.Text = gv.SelectedRow.Cells[2].Text;
                //txtPassword.Text= gv.SelectedRow.Cells[3].Text;

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = (DataTable)ViewState["DGV"];
            gv.DataBind();
        }

        protected void txtUserID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUserID.Text.Length == 5)
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        SqlCommand cmd = new SqlCommand("SP_UserIDLoad", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userId", SqlDbType.VarChar).Value = txtUserID.Text;
                        SqlDataAdapter DA = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        con.Open();
                        DA.Fill(ds);
                        //response.result = false;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                           // ViewState["Users"] = ds.Tables[0];
                            //////////////////////////////////////////////////////
                            txtUserName.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                            txtPassword.Text = ds.Tables[0].Rows[0]["USERPASSWORD"].ToString();
                            ddBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHID"].ToString();
                            txtEmailId.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                            txtMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                            ddstatus.SelectedValue = ds.Tables[0].Rows[0]["STATUSACTIVE"].ToString();
                            txtPassword.Attributes["value"] = ds.Tables[0].Rows[0]["USERPASSWORD"].ToString();
                            txtConfirmPassword.Attributes["value"] = ds.Tables[0].Rows[0]["USERPASSWORD"].ToString();
                            //////////////////////////////////////////////////////
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        //protected void ddBranch_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //ViewState["Pass"] = txtPassword.Text;
        //        //txtPassword.Text = ViewState["Pass"].ToString();
        //    }
        //    catch { }
        //}
    }
}