using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.SkillAssesment
{
    public partial class SkillAssessmentReport : System.Web.UI.Page
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
                    SqlCommand cmd = new SqlCommand("SP_SkillReportPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Skill Set --" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddSkillSet.DataTextField = "SkillDesc";
                        ddSkillSet.DataValueField = "ID";
                        ddSkillSet.DataSource = ds.Tables[0];
                        ddSkillSet.DataBind();
                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Department--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddDepartment.DataTextField = "Departmentname";
                        ddDepartment.DataValueField = "DepartmentId";
                        ddDepartment.DataSource = ds.Tables[1];
                        ddDepartment.DataBind();
                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Designation--" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddDesignation.DataTextField = "Designation";
                        ddDesignation.DataValueField = "DesignationId";
                        ddDesignation.DataSource = ds.Tables[2];
                        ddDesignation.DataBind();
                        DataRow dr3 = ds.Tables[3].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select Location--" };
                        ds.Tables[3].Rows.InsertAt(dr3, 0);
                        ddLocation.DataTextField = "Locationname";
                        ddLocation.DataValueField = "LocationId";
                        ddLocation.DataSource = ds.Tables[3];
                        ddLocation.DataBind();
                        //ViewState["GV"] = ds.Tables[0];
                        //gv.DataSource = ds.Tables[2];
                        //gv.DataBind();                      
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //  Clear();
                // if (txtEmployeeID.Text.Length == 5)
                //  {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SkillGenerateReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmpId", SqlDbType.VarChar).Value = txtEmployeeID.Text;
                    cmd.Parameters.Add("@DeptID", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;
                    cmd.Parameters.Add("@DesgnID", SqlDbType.VarChar).Value = ddDesignation.SelectedValue;
                    cmd.Parameters.Add("@loc", SqlDbType.VarChar).Value = ddLocation.SelectedValue;
                    cmd.Parameters.Add("@skillid", SqlDbType.VarChar).Value = ddSkillSet.SelectedValue;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Emp"] = ds.Tables[0];
                        //////////////////////////////////////////////////////
                        ExportGridToExcel();

                    }
                }
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void ExportGridToExcel()
        {
            try
            {
                //string strfilename = "";

                //dtst = new DataSet();

                DataTable dt = (DataTable)ViewState["Emp"];
                dt.TableName = "TNA";
                //dtst.Tables.Add(dt);
                ///////////////////////////////////////////////////

                //string[] cars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                //for (int i = 0; i < cars.Length; i++)
                //{
                //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                //    {
                //        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSExceldetail", sqlConnection);
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = cars[i];
                //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                //        DataSet ds = new DataSet();
                //        da.Fill(ds);
                //        if (ds != null)
                //        {
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                DataTable dtCopy = ds.Tables[0].Copy();
                //                dtCopy.TableName = "Movement Status -" + cars[i];
                //                dtst.Tables.Add(dtCopy);

                //            }
                //        }
                //    }
                //}
                //////////////////////////////////////////////////
                if (dt != null)
                {
                    // dt = city.GetAllCity();//your datatable
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        // foreach (DataTable dt11 in dtst.Tables)
                        //  {
                        wb.Worksheets.Add(dt);
                        // }
                        int m = 0;
                        foreach (var ws in wb.Worksheets)
                        {
                            ws.ColumnWidth = 15;
                            // ws.LastColumnUsed().Width = 60;
                            //ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                            ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            //ws.Column = 10;

                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TNAReport " + DateTime.Now.ToShortDateString() + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmployeeID.Text = "";
                ddDepartment.SelectedIndex = 0;
                ddDesignation.SelectedIndex = 0;
                ddLocation.SelectedIndex = 0;
                ddSkillSet.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }
    }
}