using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.Master
{
    public partial class SkillGoalMapping : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["deletrecord"] = null;
                    FetchInitializeDetails();
                    ddDepartment.Focus();
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
                    SqlCommand cmd = new SqlCommand("SP_SkillGoalMappingPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Department--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddDepartment.DataTextField = "Departmentname";
                        ddDepartment.DataValueField = "DepartmentId";
                        ddDepartment.DataSource = ds.Tables[0];
                        ddDepartment.DataBind();

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Designation--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddDesignation.DataTextField = "Designation";
                        ddDesignation.DataValueField = "DesignationId";
                        ddDesignation.DataSource = ds.Tables[1];
                        ddDesignation.DataBind();

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Skill Id--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddSkillID.DataTextField = "ID";
                        ddSkillID.DataValueField = "ID";
                        ddSkillID.DataSource = ds.Tables[2];
                        ddSkillID.DataBind();

                        DataRow dr3 = ds.Tables[3].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select Skill Type--" };
                        ds.Tables[3].Rows.InsertAt(dr3, 0);
                        ddSkillname.DataTextField = "SkillDesc";
                        ddSkillname.DataValueField = "ID";
                        ddSkillname.DataSource = ds.Tables[3];
                        ddSkillname.DataBind();

                        SetInitialRow();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool val = false;
                if (gv.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gv.Rows)
                    {
                        if ((ddSkillID.SelectedItem.ToString() == row.Cells[1].Text))
                        {
                            //AddNewRowToGrid();
                            val = true;

                        }
                        else if (ddSkillID.SelectedItem.ToString() == 0.ToString())
                            val = true;

                    }
                    if (val == false)
                    {
                        AddNewRowToGrid();
                    }
                    else if (val == true)
                    {
                        val = false;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "It is already added in the list or please select valid skill ";
                    }
                }
                else
                    AddNewRowToGrid();



                ddSkillID.SelectedIndex = 0;
                ddSkillname.SelectedIndex = 0;

                // lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["SkillId"] = ddSkillID.SelectedValue;
                drCurrentRow["SkillName"] = ddSkillname.SelectedItem.Text;

                dtCurrentTable.Rows.Add(drCurrentRow);

                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtCurrentTable.Rows[i][1].ToString() == string.Empty)
                        dtCurrentTable.Rows[i].Delete();
                }
                dtCurrentTable.AcceptChanges();

                ViewState["CurrentTable"] = dtCurrentTable;


                gv.DataSource = dtCurrentTable;
                gv.DataBind();
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            // SetPreviousData();
        }
        private void SetPreviousData()
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowIndex++;

                    }

                }

            }

        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("SkillId", typeof(string)));
            dt.Columns.Add(new DataColumn("SkillName", typeof(string)));
            dr = dt.NewRow();
            dr["SkillId"] = "";
            dr["SkillName"] = "";
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
        }
        protected void btnTrainingClear_Click(object sender, EventArgs e)
        {

        }

        protected void ddSkillName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddSkillID.SelectedIndex = ddSkillname.SelectedIndex;
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddSkillID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddSkillname.SelectedIndex = ddSkillID.SelectedIndex;
            }
            catch (Exception ex)
            {

            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if ((ddDepartment.SelectedIndex != 0) & (ddDesignation.SelectedIndex != 0))
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)(ViewState["CurrentTable"]);
                    int count = dt.Rows.Count;
                    // ViewState["deletrecord"] = dt.Rows(e.RowIndex)
                    string v = gv.Rows[e.RowIndex].Cells[1].Text.ToString();
                    dt.Rows.RemoveAt(e.RowIndex);
                    gv.DataSource = dt;
                    gv.DataBind();
                    SkillDetaildel(v);
                    if (count <= 1)
                        SetInitialRow();

                }
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
                //  gv.DataSource = null; gv.DataBind();
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["GV"];
                gv.DataBind();
                //Clear();
            }
            catch (Exception ex)
            {

            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gv.Rows)
                    SkillDetail(row.Cells[1].Text, ViewState["designation"].ToString(), ViewState["department"].ToString());
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void SkillDetail(string SkillID, string Designationid, string Departmentid)
        {
            try
            {
                //if (b == true)
                //{
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Insert", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designationid;
                    cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = Departmentid;
                    cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                                                                                                               //cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            FetchInitializeDetails();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Skill mapped for " + Departmentid + " -- " + Designationid + " has been Created successfully";
                        }

                    }
                }
                //}
                //else
                //{
                //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                //    {
                //        SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Update", sqlConnection);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                //        cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designationid;
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataSet ds = new DataSet();
                //        da.Fill(ds);
                //        if (ds != null)
                //        {
                //            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                //            {
                //                FetchInitializeDetails();
                //                lblmsg.ForeColor = System.Drawing.Color.Green;
                //                lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                //            }

                //        }
                //    }
                //}
            }
            catch (Exception ex) { }
        }

        protected void SkillDetaildel(string SkillID)
        {
            try
            {
                //if (b == true)
                //{
                //using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                //{
                //    SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Insert", sqlConnection);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                //    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designationid;
                //    cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = Departmentid;
                //    cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                //    cmd.Parameters.Add("@modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                //                                                                                               //cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                //    DataSet ds = new DataSet();
                //    da.Fill(ds);
                //    if (ds != null)
                //    {
                //        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                //        {
                //            FetchInitializeDetails();
                //            lblmsg.ForeColor = System.Drawing.Color.Green;
                //            lblmsg.Text = "Skill mapped for " + Departmentid + " -- " + Designationid + " has been Created successfully";
                //        }

                //    }
                //}
                //}
                //else
                //{
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SkillMasterMapping_Update", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ViewState["designation"].ToString();
                    cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ViewState["department"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                       
                    }
                }
                //}
            }
            catch (Exception ex) { }
        }

        protected void ddDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["department"] = ddDepartment.SelectedValue;
                if ((ddDepartment.SelectedIndex != 0) && (ddDesignation.SelectedIndex != 0))
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillGridLoad", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ddDesignation.SelectedValue;
                        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = ds.Tables[0];
                                gv.DataSource = ds.Tables[0];
                                gv.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["designation"] = ddDesignation.SelectedValue;
                if ((ddDepartment.SelectedIndex != 0) && (ddDesignation.SelectedIndex != 0))
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillGridLoad", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = ddDesignation.SelectedValue;
                        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {                            
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ViewState["CurrentTable"] = ds.Tables[0];
                                gv.DataSource = ds.Tables[0];
                                gv.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}