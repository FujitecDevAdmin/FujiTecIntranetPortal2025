using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.IO;
using ClosedXML.Excel;

namespace FujiTecIntranetPortal.MISReports
{
    public partial class InventoryReport : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public DataSet dtst;
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
                        SqlCommand cmd = new SqlCommand("SP_MISInventoryReportPageLoad", sqlConnection);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Loc", SqlDbType.VarChar).Value = Session["Loc"].ToString();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                // ViewState["DPT"] = ds.Tables[1];
                                ////////////////////////////////////////////////////

                                DataRow ddl = ds.Tables[0].NewRow();
                                ddl.ItemArray = new object[] { 0, "--Select--" };
                                ds.Tables[0].Rows.InsertAt(ddl, 0);

                                ddWareHouse.DataTextField = "WAREHOUSENAME";
                                ddWareHouse.DataValueField = "WAREHOUSEID";
                                ddWareHouse.DataSource = ds.Tables[0];
                                ddWareHouse.DataBind();

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
                dtst = new DataSet();

                DataTable dt = (DataTable)ViewState["DGV"];
                dt.TableName = "Summary";
                dtst.Tables.Add(dt);
                /////////////////////////////////////////////////

                string[] cars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                for (int i = 0; i < cars.Length; i++)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSExceldetail", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = cars[i];
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtCopy = ds.Tables[0].Copy();
                                dtCopy.TableName = "Movement Status -" + cars[i];
                                dtst.Tables.Add(dtCopy);
                               
                            }
                        }
                    }
                }
                ////////////////////////////////////////////////
                if (dt != null)
                {
                    // dt = city.GetAllCity();//your datatable
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt11 in dtst.Tables)
                        {
                            wb.Worksheets.Add(dt11);
                        }
                        int m = 0;
                        foreach (var ws in wb.Worksheets)
                        {
                            ws.ColumnWidth = 20;
                            // ws.LastColumnUsed().Width = 60;
                            ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

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

                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSEARCH_IP", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@MovingStatus", SqlDbType.VarChar).Value = ddMovementStatus.SelectedValue;
                        cmd.Parameters.Add("@WarehouseID", SqlDbType.VarChar).Value = ddWareHouse.SelectedValue;
                        cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = txtFromDate.Text;
                        cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = txtTodate.Text;
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
                                int index = DGV.Rows.Count - 1;
                                DGV.Rows[index].Font.Bold = true;
                                ///////////////////////////////////////////////////
                                // ViewState["InvRpt"] = ds.Tables[1];
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



        public void movementstatus()
        {
            try
            {
                dtst = new DataSet();
                string[] cars = { "A", "B", "C", "D", "E", "F", "G", "H" , "I", "J", };
                for (int i = 0; i < cars.Length; i++)
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSExceldetail", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = cars[i];
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dtst.Tables.Add(cars[i]+ds.Tables[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ddWareHouse.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
                DGV.DataSource = null;
                DGV.DataBind();
                ViewState["DGV"] = null;
                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //DataTable dt = (DataTable)ViewState["InvRpt"];
                ////dt = (DataTable)ViewState["InvRpt"];
                //DataRow[] dr = dt.Select("[MovementStatus]='A'");
                //DataTable dt1 = new DataTable();

                //if (dr.Length > 0)
                //{
                //    dt1 = dr.CopyToDataTable();
                //    DataTable dt2 = new DataTable();
                //    dt2 = dt1;
                //    string strfilename = "";
                //    if (dt2 != null)
                //    {
                //        // dt = city.GetAllCity();//your datatable
                //        using (XLWorkbook wb = new XLWorkbook())
                //        {
                //            wb.Worksheets.Add(dt2);
                //            int m = 0;
                //            foreach (var ws in wb.Worksheets)
                //            {
                //                ws.ColumnWidth = 20;
                //                // ws.LastColumnUsed().Width = 60;

                //            }

                //            Response.Clear();
                //            Response.Buffer = true;
                //            Response.Charset = "";
                //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //            Response.AddHeader("content-disposition", "attachment;filename=MISInventory_Rpt" + strfilename.Trim() + ".xlsx");

                //            using (MemoryStream MyMemoryStream = new MemoryStream())
                //            {
                //                //   XLWorkbook.ColumnWidth                         
                //                wb.SaveAs(MyMemoryStream);
                //                MyMemoryStream.WriteTo(Response.OutputStream);
                //                Response.Flush();
                //                Response.End();
                //            }
                //        }


                //    }
                //}
                string selectedval = "";
                if (DGV.SelectedRow.Cells[1].Text == "1")
                {
                    selectedval = "A";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "2")
                {
                    selectedval = "B";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "3")
                {
                    selectedval = "C";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "4")
                {
                    selectedval = "D";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "5")
                {
                    selectedval = "E";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "6")
                {
                    selectedval = "F";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "7")
                {
                    selectedval = "G";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "8")
                {
                    selectedval = "H";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "9")
                {
                    selectedval = "I";
                }
                else if (DGV.SelectedRow.Cells[1].Text == "10")
                {
                    selectedval = "J";
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SP_MISINVENTORYREPORTSExceldetail", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MovementStatus", SqlDbType.VarChar).Value = selectedval;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
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
                                DataTable dt = ds.Tables[0];
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

                        }

                    }
                    scope.Complete();
                }


            }
            catch (Exception ex)
            { }

        }

        protected void DGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string colcount = e.Row.Cells.Count.ToString();
                for (int j = 0; j < int.Parse(colcount); j++)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //Label lblTotalPrice = (Label)e.Row.FindControl("Salary");
            //    //lblTotalPrice.Text = m.ToString();
            //   // DGV.FooterRow.Cells[1].Text = "Grand Total";
            //    int sum = 0;
            //    for (int i = 0; i < DGV.Rows.Count; ++i)
            //    {
            //        sum += Convert.ToInt32(DGV.Rows[i].Cells[2].Text);
            //    }
            //    lblmsg.Text = sum.ToString();
            //}
        }
    }
}