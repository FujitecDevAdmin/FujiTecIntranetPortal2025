using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Master
{
    public partial class CategoryMaster : System.Web.UI.Page
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
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SkillMasterPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ddStatus.DataTextField = "StatusDesc";
                        ddStatus.DataValueField = "StatusCode";
                        ddStatus.DataSource = ds.Tables[1];
                        ddStatus.DataBind();
                        ViewState["GV"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();

                        //DataRow dr = ds.Tables[0].NewRow();
                        //dr.ItemArray = new object[] { 0, "--Select Department--" };
                        //ds.Tables[0].Rows.InsertAt(dr, 0);
                        //ddDepartment.DataTextField = "Departmentname";
                        //ddDepartment.DataValueField = "DepartmentId";
                        //ddDepartment.DataSource = ds.Tables[0];
                        //ddDepartment.DataBind();

                        //DataRow dr1 = ds.Tables[2].NewRow();
                        //dr1.ItemArray = new object[] { 0, "--Select Designation--" };
                        //ds.Tables[2].Rows.InsertAt(dr1, 0);
                        //ddDesignation.DataTextField = "Designation";
                        //ddDesignation.DataValueField = "DesignationId";
                        //ddDesignation.DataSource = ds.Tables[2];
                        //ddDesignation.DataBind();

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Skill Type--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddSkillType.DataTextField = "Metadatadescription";
                        ddSkillType.DataValueField = "Metasubcode";
                        ddSkillType.DataSource = ds.Tables[2];
                        ddSkillType.DataBind();

                        ////if (ds.Tables[4].Rows.Count > 0)
                        ////{
                        ////    dtem = ds.Tables[4];
                        ////}
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
                btnSave.Enabled = false;
                Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"

                string Id = string.Empty;
                if (validt != "Error")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillMaster_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = txtSkillID.Text;
                        cmd.Parameters.Add("@SkillName", SqlDbType.VarChar).Value = txtSkillName.Text;
                        cmd.Parameters.Add("@SkillType", SqlDbType.VarChar).Value = ddSkillType.SelectedValue;
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created successfully";
                                Id = ds.Tables[1].Rows[0]["ID"].ToString();
                                txtSkillID.Text = Id;
                            }
                            else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Updated  successfully";
                                Id = txtSkillID.Text;

                            }
                        }
                    }
                    //for (int i = 0; i < cbl_Ds1.Items.Count; i++)
                    //{
                    //    if (cbl_Ds1.Items[i].Selected)
                    //        SkillDetail(Id, cbl_Ds1.Items[i].Value.ToString(), true);
                    //    else
                    //        SkillDetail(Id, cbl_Ds1.Items[i].Value.ToString(), false);
                    //}
                    //for (int i = 0; i < cbl_Ds2.Items.Count; i++)
                    //{
                    //    if (cbl_Ds2.Items[i].Selected)
                    //        SkillDetail(Id, cbl_Ds2.Items[i].Value.ToString(), true);
                    //    else
                    //        SkillDetail(Id, cbl_Ds2.Items[i].Value.ToString(), false);
                    //}
                    //for (int i = 0; i < cbl_Ds3.Items.Count; i++)
                    //{
                    //    if (cbl_Ds3.Items[i].Selected)
                    //        SkillDetail(Id, cbl_Ds3.Items[i].Value.ToString(), true);
                    //    else
                    //        SkillDetail(Id, cbl_Ds3.Items[i].Value.ToString(), false);
                    //}

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void SkillDetail(string SkillID, string Designationid, bool b)
        {
            try
            {
                if (b == true)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Insert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                        cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designationid;
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                        cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                            }

                        }
                    }
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Update", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                        cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designationid;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                            }

                        }
                    }
                }
            }
            catch (Exception ex) { }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
              //  gv.DataSource = null; gv.DataBind();
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["GV"];
                gv.DataBind();
               // Clear();
            }
            catch (Exception ex)
            {

            }
        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // Clear();
                lblmsg.Text = "";
                txtSkillID.Text = gv.SelectedRow.Cells[1].Text;
                txtSkillName.Text = gv.SelectedRow.Cells[2].Text;
                //if(gv.SelectedRow.Cells[3].Text.Length == 0)
                ddSkillType.SelectedValue = gv.SelectedRow.Cells[3].Text;
                DataTable dtem = (DataTable)ViewState["GV"];
                btnSave.Enabled = true;
                //foreach (DataRow row in dtem.Rows)
                //{
                //    //if (row["ID"].ToString() == txtSkillID.Text.ToString())
                //    //{
                //    //    txtSkillID.Text = row["ID"].ToString();
                //    //    txtSkillName.Text = row["Skill"].ToString();
                //    //for (int i = 0; i < cbl_Ds1.Items.Count; i++)
                //    //{
                //    if (txtSkillID.Text == row["ID"].ToString())
                //    {
                //        string chkbox1 = row["DesignationID"].ToString();
                //        //row.ItemArray[3].ToString();
                //        if (chkbox1 == "3")
                //        {
                //            cbl_Ds1.Items[0].Selected = true;
                //        }
                //        else if (chkbox1 == "4")
                //        {
                //            cbl_Ds1.Items[1].Selected = true;
                //        }
                //        else if (chkbox1 == "5")
                //        {
                //            cbl_Ds1.Items[2].Selected = true;
                //        }
                //        else if (chkbox1 == "6")
                //        {
                //            cbl_Ds1.Items[3].Selected = true;
                //        }
                //        //}
                //        //for (int i = 0; i < cbl_Ds2.Items.Count; i++)
                //        //{

                //        // string chkbox1 = row["DesignationID"].ToString();
                //        //row.ItemArray[3].ToString();
                //        else if (chkbox1 == "7")
                //        {
                //            cbl_Ds2.Items[0].Selected = true;
                //        }
                //        else if (chkbox1 == "8")
                //        {
                //            cbl_Ds2.Items[1].Selected = true;
                //        }
                //        else if (chkbox1 == "9")
                //        {
                //            cbl_Ds2.Items[2].Selected = true;
                //        }
                //        else if (chkbox1 == "10")
                //        {
                //            cbl_Ds2.Items[3].Selected = true;
                //        }
                //        //}
                //        //for (int i = 0; i < cbl_Ds3.Items.Count; i++)
                //        //{
                //        //  string chkbox1 = row["DesignationID"].ToString();
                //        //row.ItemArray[3].ToString();
                //        else if (chkbox1 == "11")
                //        {
                //            cbl_Ds3.Items[0].Selected = true;
                //        }
                //        else if (chkbox1 == "12")
                //        {
                //            cbl_Ds3.Items[1].Selected = true;
                //        }
                //    }
                //    // }
                //    //if ((row["statusAI"].ToString().Trim() == "MSC0001") || (row["statusAI"].ToString().Trim() == "MSC0002"))
                //    //    ddStatus.SelectedValue = row["statusAI"].ToString().Trim();
                //    //break;

                //    //}
                //}
            }
            catch (Exception ex)
            { }
        }

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                //if ((txtEmployeeID.Text == string.Empty))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Employee Id should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                if ((txtSkillName.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* skill Name should not be empty ";
                    ViewState["Error"] = "Error";
                }
                //if ((ddDepartment.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please Select Department";
                //    ViewState["Error"] = "Error";
                //}
                //if ((ddDesignation.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please Select Designation";
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
                Clear();
            }
            catch (Exception ex)
            {

            }
        }
        private void Clear()
        {
            try
            {
                lblmsg.Text = "";
                btnSave.Enabled = true;
                txtSkillID.Text = "";
                txtSkillName.Text = "";
                ddStatus.SelectedIndex = 0;
                //cbl_Ds1.ClearSelection();
                //cbl_Ds2.ClearSelection();
                //cbl_Ds3.ClearSelection();
                gv.DataSource = null;gv.DataBind();
                FetchInitializeDetails();
            }
            catch (Exception ex) { }
        }
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtSkillName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}