using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.SkillAssesment
{
    public partial class SkillAssessment : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  FetchInitializeDetails();
                    //txtEmployeeID.Focus();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        public void Clear()
        {
            try
            {
                //txtEmployeeID.Text = "";
                txtDepartment.Text = "";
                txtDesignationID.Text = "";
                txtEmployeeName.Text = "";
                txtDate.Text = "";
                txtRemark.Text = "";
                gv.DataSource = null;
                gv.DataBind();
                gv.PageIndex = 0;
                lblmsg.Text = "";
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
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["GV"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                // Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                string Id = string.Empty;
                if (validt != "Error")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillAssessmentInsert", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                        cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar).Value = lblDepartmentid.Text;
                        cmd.Parameters.Add("@DesignationID", SqlDbType.VarChar).Value = lblDesignationid.Text;
                        cmd.Parameters.Add("@Remark", SqlDbType.VarChar).Value = txtRemark.Text;
                        cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                // FetchInitializeDetails();
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Skill Assesment for " + txtEmployeeID.Text + " -- " + txtEmployeeName.Text + " has been Created successfully";

                            }

                        }
                        string text = String.Empty;
                        DataTable gvdt = (DataTable)ViewState["GV"];
                        for (int i = 0; i < gvdt.Rows.Count; i++)
                        {
                            //foreach (GridViewRow row in gv.Rows)
                            //{
                            //    for (int j = 0; j < gv.Columns.Count; j++)
                            //    {
                            //String header = gv.Columns[i].HeaderText;
                            //String cellText = row.Cells[j];
                            var ddl = gv.Rows[i].FindControl("ddlSkilllevel") as DropDownList;
                            text = gv.Rows.Count.ToString();
                            string value = ddl.SelectedItem.Value;
                            //  if (gvdt.Rows[i]["ID"].ToString() == gv.Rows[i].Cells[1].ToString())
                            SkillDetail(gvdt.Rows[i]["ID"].ToString(), value.ToString());

                            //    }
                            //}
                            //if (gv.Rows[i][])
                        }
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        // lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                        //lblmsg.Text = "Employee skill Details saved Successfully";
                    }

                }
                //Clear();
                // lblmsg.Text = "Employee skill Details saved Successfully";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void SkillDetail(string SkillID, string Skilllevel)
        {
            try
            {
                btnSave.Enabled = true;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SkillAssessmentDetailInsert", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                    cmd.Parameters.Add("@Skillid", SqlDbType.VarChar).Value = SkillID;
                    cmd.Parameters.Add("@Skilllevel", SqlDbType.VarChar).Value = Skilllevel; //@ToDate date = null
                    cmd.Parameters.Add("@createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            //FetchInitializeDetails();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            // lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                            lblmsg.Text = "Employee skill Details saved Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                        {
                            //FetchInitializeDetails();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            // lblmsg.Text = "Skill Master details for " + txtSkillID.Text + " -- " + txtSkillName.Text + " has been Created/Update successfully";
                            lblmsg.Text = "Employee skill Details already saved";

                        }

                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmployeeID.Text = "";
                Clear();
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Clear();
                if (txtEmployeeID.Text.Length == 5)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_SkillEmployeePageLoad", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                        SqlDataAdapter DA = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        con.Open();
                        DA.Fill(ds);
                        //response.result = false;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ViewState["Emp"] = ds.Tables[0];
                            //////////////////////////////////////////////////////
                            txtEmployeeName.Text = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();
                            txtDepartment.Text = ds.Tables[0].Rows[0]["DEPARTMENTNAME"].ToString();
                            txtDesignationID.Text = ds.Tables[0].Rows[0]["DesignationDesc"].ToString();
                            txtDate.Text = ds.Tables[0].Rows[0]["TodaysDate"].ToString();
                            lblDepartmentid.Text = ds.Tables[0].Rows[0]["DEPARTMENTID"].ToString();
                            lblDesignationid.Text = ds.Tables[0].Rows[0]["DesignationId"].ToString();
                            txtRemark.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                            ///////////////////////////////////////////////////////
                            ViewState["GV"] = ds.Tables[1];
                            gv.DataSource = ds.Tables[1];
                            gv.DataBind();

                            // DataTable gvdt = (DataTable)ViewState["GV"];
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                var ddl = gv.Rows[i].FindControl("ddlSkilllevel") as DropDownList;
                                //ddl.SelectedItem.Text = ds.Tables[1].Rows[i]["Skilllevel"].ToString();
                                ddl.SelectedValue = ds.Tables[1].Rows[i]["Skilllevel"].ToString();

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Skilltype = e.Row.Cells[2].Text;

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (Skilltype == "Knowledge")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00ff19");
                    }
                    if (Skilltype == "Skill")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00ff99");//#ff9900
                    }
                    if (Skilltype == "Attitude")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00e6ff");//#00ff19
                    }
                }
            }
        }

        protected void PreviewPopUp(object sender, EventArgs e)
        {
            getGridInfo();
            string url = "SkillAssessmentPopup.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=750,left=200,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        private void getGridInfo()
        {

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new System.Data.DataColumn("SNo", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Type", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Activities", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Skilllevel", typeof(String)));

            int cnt = 1;
            foreach (GridViewRow row in gv.Rows)
            {
                //Label SNo = (Label)row.FindControl("SNO");
                //Label SkillTypeDesc = (Label)row.FindControl("SkillTypeDesc");
                //Label SkillDesc = (Label)row.FindControl("SkillDesc");
                //Label TotalPrice = (Label)row.FindControl("LBLTotal");
                int SNo = cnt++;
                string SkillTypeDesc = row.Cells[2].Text;
                string SkillDesc = row.Cells[3].Text;

                var ddl = row.FindControl("ddlSkilllevel") as DropDownList;
                //text = gv.Rows.Count.ToString();
                string value = ddl.SelectedItem.Value;

                dr = dt.NewRow();
                dr[0] = SNo;
                dr[1] = SkillTypeDesc;
                dr[2] = SkillDesc;
                dr[3] = value;
                dt.Rows.Add(dr);
            }

            Session["SGV"] = dt;
            // Response.Redirect("Admin/Default.aspx");
        }


    }
}