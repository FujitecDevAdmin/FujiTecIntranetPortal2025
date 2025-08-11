using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.MISReports
{
    public partial class InventoryDetailedReport : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnectionUAT"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FromCal.Visible = false;
                    Tocal.Visible = false;
                    FetchInitializeDetails();
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (FromCal.Visible)
                    FromCal.Visible = false;
                else
                    FromCal.Visible = true;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Tocal.Visible)
                    Tocal.Visible = false;
                else
                    Tocal.Visible = true;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void FromCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtFromDate.Text = FromCal.SelectedDate.ToShortDateString();
                FromCal.Visible = false;

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ToCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtTodate.Text = Tocal.SelectedDate.ToShortDateString();
                Tocal.Visible = false;

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ToCalendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                if (e.Day.Date <= FromCal.SelectedDate)
                {
                    e.Day.IsSelectable = false;
                    e.Cell.ForeColor = System.Drawing.Color.Gray;
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void FromCalendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                //if (e.Day.Date < DateTime.Now.Date)
                //{
                //    e.Day.IsSelectable = false;
                //    e.Cell.ForeColor = System.Drawing.Color.Gray;
                //}
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYDETAILREPORTPAGELOAD", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //ViewState["DPT"] = ds.Tables[1];
                                ////////////////////////////////////////////////////

                                DataRow ddl = ds.Tables[0].NewRow();
                                ddl.ItemArray = new object[] { 0, "--Select--" };
                                ds.Tables[0].Rows.InsertAt(ddl, 0);

                                ddWareHouse.DataTextField = "WAREHOUSENAME";
                                ddWareHouse.DataValueField = "WAREHOUSEID";
                                ddWareHouse.DataSource = ds.Tables[0];
                                ddWareHouse.DataBind();

                                //DataRow ddl1 = ds.Tables[1].NewRow();
                                //ddl1.ItemArray = new object[] { 0, "--Select--" };
                                //ds.Tables[1].Rows.InsertAt(ddl1, 0);

                                //ddPartNumber.DataTextField = "PARTDESCRIPTION";
                                //ddPartNumber.DataValueField = "DISPLAYNAME";
                                //ddPartNumber.DataSource = ds.Tables[1];
                                //ddPartNumber.DataBind();

                                //DataRow ddl21 = ds.Tables[2].NewRow();
                                //ddl21.ItemArray = new object[] { 0, "--Select--" };
                                //ds.Tables[2].Rows.InsertAt(ddl21, 0);

                                ddPartTypeName.DataTextField = "PARTTYPENAME";
                                ddPartTypeName.DataValueField = "PARTTYPENAME";
                                ddPartTypeName.DataSource = ds.Tables[1];
                                ddPartTypeName.DataBind();
                                ////////////////////////////////////////////////////
                                //ddMeetingRoomName.SelectedValue = ds.Tables[2].Rows[0]["ROOMCODE"].ToString();                            
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

        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["DGV"] != null && !ViewState["DGV"].Equals("-1"))
                {
                    ExportGridToExcel();
                }
                // ExportGridToExcel();
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please load data before export to excel";
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void ExportGridToExcel()
        {
            try
            {
                string strfilename = "";
                //if ((txtLiftNo.Text != String.Empty))
                //{
                //    strfilename = txtLiftNo.Text;
                //}
                //else
                //{
                //    if (txtProjectNo.Text != String.Empty)
                //        strfilename = txtProjectNo.Text;
                //    else
                //    {
                //        strfilename = "_";
                //    }
                //}
                DataTable dt = (DataTable)ViewState["DGV"];
                if (dt != null)
                {
                    // dt = city.GetAllCity();//your datatable
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        int m = 0;
                        foreach (var ws in wb.Worksheets)
                        {
                            ws.ColumnWidth = 20;
                            // ws.LastColumnUsed().Width = 60;

                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=MISInventory_Rpt" + strfilename.Trim() + ".xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            //   XLWorkbook.ColumnWidth                         
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if ((ddWareHouse.SelectedIndex > 0) && (txtFromDate.Text.Length > 0) && (txtTodate.Text.Length > 0))
                    Search();
                else
                    lblmsg.Text = "Please select the Mandatory Fields";
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
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {

                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSEARCH_Detail", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@MovingStatus", SqlDbType.VarChar).Value = ddMovementStatus.SelectedValue;
                        cmd.Parameters.Add("@WarehouseID", SqlDbType.VarChar).Value = ddWareHouse.SelectedValue;
                        cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime( txtFromDate.Text);
                        cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(txtTodate.Text);
                        cmd.Parameters.Add("@PartTypeName", SqlDbType.VarChar).Value = ddPartTypeName.SelectedValue;
                        cmd.Parameters.Add("@PartNumber", SqlDbType.VarChar).Value = ddPartNumber.SelectedValue;
                        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = ddMovementStatus.SelectedValue;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddWareHouse.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void DGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //string colcount = e.Row.Cells.Count.ToString();
                //for (int j = 0; j < int.Parse(colcount); j++)
                //{
                //    if (e.Row.RowType == DataControlRowType.DataRow)
                //    {
                //        e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                //        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                //        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //        e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;
                //    }
                //}
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ddPartTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("SP_MISINVENTORYDtlFetchPartDetails", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@MovingStatus", SqlDbType.VarChar).Value = ddMovementStatus.SelectedValue;
                    cmd.Parameters.Add("@PartTypeName", SqlDbType.VarChar).Value = ddPartTypeName.SelectedValue;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //ViewState["DGV"] = ds.Tables[0];
                            /////////////////////////////////////////////////////
                            //DGV.DataSource = ds.Tables[0];
                            //DGV.DataBind();
                            /////////////////////////////////////////////////////     
                            ///
                            DataRow ddl1 = ds.Tables[0].NewRow();
                            ddl1.ItemArray = new object[] { 0, "--Select--" };
                            ds.Tables[0].Rows.InsertAt(ddl1, 0);

                            ddPartNumber.DataTextField = "PARTDESCRIPTION";
                            ddPartNumber.DataValueField = "DISPLAYNAME";
                            ddPartNumber.DataSource = ds.Tables[0];
                            ddPartNumber.DataBind();
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
    }
}