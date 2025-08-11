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
    public partial class TNA : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEmployeeID.Text.Length == 5)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_TNAEmployeePageLoad", con);
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
                            txtloc.Text = ds.Tables[0].Rows[0]["LOCATIONNAME"].ToString();
                            lblDepartmentid.Text = ds.Tables[0].Rows[0]["DEPARTMENTID"].ToString();
                            lblDesignationid.Text = ds.Tables[0].Rows[0]["DesignationId"].ToString();
                            ///////////////////////////////////////////////////////
                            ViewState["GV"] = ds.Tables[1];
                            gv.DataSource = ds.Tables[1];
                            gv.DataBind();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ExportGridToExcel()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["GV"];
                dt.TableName = "TNA";
                ///////////////////////////////////////////////////
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
                            ws.ColumnWidth = 25;
                            // ws.LastColumnUsed().Width = 60;
                            //ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                            ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            //ws.Column = 10;

                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TNARequired " + DateTime.Now.ToShortDateString() + ".xlsx");
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmployeeID.Text = "";
                txtDepartment.Text = "";
                txtDesignationID.Text = "";
                txtEmployeeName.Text = "";
                txtloc.Text = "";
                gv.DataSource = null;
                gv.DataBind();
                gv.PageIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btndownloadexcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportGridToExcel();
            }
            catch (Exception ex)
            {

            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ExportGridToExcel();
        //    }
        //    catch (Exception ex)
        //    {

            //    }
            //}
    }
}