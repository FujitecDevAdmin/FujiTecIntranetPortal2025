using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class TrainingCategoryMaster : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FetchInitializeDetails();
                    ddlSelectTraining.Focus();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TrainingCategoryMasterPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {

                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[0];
                        ddStatus.DataBind();

                        DataRow dr = ds.Tables[1].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Training Type --" };
                        ds.Tables[1].Rows.InsertAt(dr, 0);
                        ddlSelectTraining.DataTextField = "TrainingCategoryDesc";
                        ddlSelectTraining.DataValueField = "TrainingCategoryType";
                        ddlSelectTraining.DataSource = ds.Tables[1];
                        ddlSelectTraining.DataBind();

                        //DataRow dr1 = ds.Tables[3].NewRow();
                        //dr1.ItemArray = new object[] { 0, "--Select Module Type --" };
                        //ds.Tables[3].Rows.InsertAt(dr1, 0);
                        //ddlTrainingCategory.DataTextField = "TRAININGNAME";
                        //ddlTrainingCategory.DataValueField = "TRAININGID";
                        //ddlTrainingCategory.DataSource = ds.Tables[3];
                        //ddlTrainingCategory.DataBind();

                        ViewState["GV"] = ds.Tables[2];
                        ViewState["GV1"] = ds.Tables[4];
                        gv.DataSource = ds.Tables[4];
                        gv.DataBind();
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
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string sp = string.Empty;
                        if (txtTrainingNo.Text != "")
                            sp = "SP_TRAININGCATEGORYMASTER_UPDATE";
                        else
                            sp = "SP_TRAININGCATEGORYMASTER_INSERT";
                        SqlCommand cmd = new SqlCommand(sp, sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@TRAININGCATEGORYCODE", SqlDbType.VarChar).Value = null;// ddlTrainingCategory.SelectedValue;
                        //if (ddlTrainingCategory.SelectedIndex != 0)
                        //    cmd.Parameters.Add("@TRAININGCATEGORYDesc", SqlDbType.VarChar).Value = ddlTrainingCategory.SelectedItem.ToString();
                        //else
                            cmd.Parameters.Add("@TRAININGCATEGORYDesc", SqlDbType.VarChar).Value = null;
                        cmd.Parameters.Add("@TRAININGNAME", SqlDbType.VarChar).Value = txtName.Text;
                        cmd.Parameters.Add("@TRAININGTYPE", SqlDbType.VarChar).Value = ddlSelectTraining.SelectedValue;
                        cmd.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@MODIFIEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  @TRAININGNO
                        cmd.Parameters.Add("@TRAININGNO", SqlDbType.VarChar).Value = txtTrainingNo.Text; //@ToDate date = null  @TRAININGNO
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                txtTrainingNo.Text = ds.Tables[0].Rows[0]["TM"].ToString();
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Master for " + ds.Tables[0].Rows[0]["TM"].ToString() + " -- " + txtName.Text + " Created/Updated successfully";
                            }
                            else //if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "ERROR")
                            {
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                lblmsg.Text = "Error in registration";
                            }
                            //SendEmail();
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

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                     


                if ((txtName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Training Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                if(ddlSelectTraining.SelectedIndex==0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Training Type";
                    ViewState["Error"] = "Error";
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            txtTrainingNo.Text = "";
         //   ddlTrainingCategory.SelectedIndex = 0;
            ddlSelectTraining.SelectedIndex = 0;
            ddStatus.SelectedIndex = 0;
            txtName.Text = "";
          //  ddlTrainingCategory.Enabled = true;
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["GV"];
                gv.DataSource = dt;
                gv.DataBind();

                lblmsg.Text = "";
                txtTrainingNo.Text = gv.SelectedRow.Cells[1].Text;
                txtName.Text = gv.SelectedRow.Cells[2].Text;
                ddlSelectTraining.SelectedValue = gv.SelectedRow.Cells[3].Text;
                //if ((gv.SelectedRow.Cells[5].Text.Trim() != ""))
                //    ddlTrainingCategory.SelectedValue = gv.SelectedRow.Cells[5].Text;
                if ((gv.SelectedRow.Cells[6].Text.Trim() == "MSC0001") || (gv.SelectedRow.Cells[6].Text.Trim() == "MSC0002"))
                    ddStatus.SelectedValue = gv.SelectedRow.Cells[6].Text;
                            

                DataTable dt1 = new DataTable();
                dt1 = (DataTable)ViewState["GV1"];
                gv.DataSource = dt1;
                gv.DataBind();
                //if (ddlSelectTraining.SelectedValue == "MSC0006")
                //{
                //    ddlTrainingCategory.Enabled = false;
                //}
                //else
                //{
                //    ddlTrainingCategory.Enabled = true;
                //}
            }
            catch (Exception ex)
            { }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["GV"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlSelectTraining_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlSelectTraining.SelectedValue == "MSC0006")
            //{
            //    ddlTrainingCategory.Enabled = false;
            //}
            //else
            //{
            //    ddlTrainingCategory.Enabled = true;

            //}
        }
    }
}