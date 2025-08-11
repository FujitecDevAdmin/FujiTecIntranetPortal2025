using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.MISReports
{
    public partial class DashboardDeliveryEquipment : System.Web.UI.Page
    {
        //dbconnectionLIVE
        string strDDUpload = ConfigurationManager.AppSettings["DELIVERYDASHBOARD"].ToString();
        string strcon = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
        bool b;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    PgLoad();

                string userid = Session["USERID"] as string;
                b = strDDUpload.Contains(userid);
                if (b == false)
                {
                    btnUpload.Enabled = false;
                    btnGenerate.Enabled = false;
                    FileUpload1.Enabled = false;
                    btnDwnldExl.Enabled = false;
                    btnUpload.Visible = false;
                    btnGenerate.Visible = false;
                    FileUpload1.Visible = false;
                    btnDwnldExl.Visible = false;
                }
                else
                {
                    btnUpload.Enabled = true;
                    btnGenerate.Enabled = true;
                    FileUpload1.Enabled = true;
                    btnDwnldExl.Enabled = true;
                    btnUpload.Visible = true;
                    btnGenerate.Visible = true;
                    FileUpload1.Visible = true;
                    btnDwnldExl.Visible = true;

                }



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
                    SqlCommand cmd = new SqlCommand("DashboardDeliveryPageload", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //ViewState["Users"] = ds.Tables[0];
                        //////////////////////////////////////////////////////

                        //DataRow ddl = ds.Tables[0].NewRow();
                        //ddl.ItemArray = new object[] { 0, "--Select--" };
                        //ds.Tables[0].Rows.InsertAt(ddl, 0);

                        DataRow ddl1 = ds.Tables[0].NewRow();
                        ddl1.ItemArray = new object[] { 0, "--Select--" };
                        ds.Tables[0].Rows.InsertAt(ddl1, 0);

                        ddcalendarPeriod.DataTextField = "CalendarPeriodName";
                        ddcalendarPeriod.DataValueField = "calendarperiodid";
                        ddcalendarPeriod.DataSource = ds.Tables[0];
                        ddcalendarPeriod.DataBind();

                        string st = DateTime.Now.ToString("MMM");
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //bool contain = strDDUpload.Contains(st);
                            bool bstrcontain = ds.Tables[0].Rows[i]["CalendarPeriodName"].ToString().Contains(st);
                            if (bstrcontain == true)
                            {
                                ddcalendarPeriod.SelectedValue = ds.Tables[0].Rows[i]["calendarperiodid"].ToString();
                            }
                        }
                        //ViewState["DGV"] = ds.Tables[1];
                        //gv.DataSource = ds.Tables[1];
                        //gv.DataBind();

                        //DataRow ddl1 = ds.Tables[1].NewRow();
                        //ddl1.ItemArray = new object[] { 0, "--Select--" };
                        //ds.Tables[1].Rows.InsertAt(ddl1, 0);

                        //DropDownList1.DataTextField = "DISPLAYNAME";
                        //DropDownList1.DataValueField = "PARENTID";
                        //DropDownList1.DataSource = ds.Tables[1];
                        //DropDownList1.DataBind();


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

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["DGV"];
                gv.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void GenerateTemplate_OnClick(object sender, EventArgs e)
        {
            try
            {
                //Label1.Text = "";
                string strURL = Server.MapPath("Template/DashboardDelivery.xlsx");
                WebClient req = new WebClient();
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"DashboardDelivery.xlsx" + "\"");
                byte[] data = req.DownloadData(strURL);
                Response.BinaryWrite(data);
                Response.End();
            }
            catch (Exception ex)
            {
                //Label1.ForeColor = System.Drawing.Color.Red;
                //Label1.Text = ex.Message;
            }
        }

        protected void Upload(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select the excel file to upload";
                }
                //Upload and save the file
                else
                {
                    string filepath = Server.MapPath("~/Files/");
                    System.IO.DirectoryInfo di = new DirectoryInfo(filepath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(excelPath);

                    string conString = string.Empty;
                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03
                            conString = ConfigurationManager.ConnectionStrings["Excelxls"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 or higher
                            conString = ConfigurationManager.ConnectionStrings["Excelxlsx"].ConnectionString;
                            break;

                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                        dtExcelData.Columns.AddRange(new DataColumn[10] {
                new DataColumn("REGION", typeof(string)),
                new DataColumn("PLANYEAR", typeof(string)),
                //new DataColumn("LiftNumber", typeof(string)),

                new DataColumn("PLANMONTH", typeof(string)),
                new DataColumn("PLANNEDUNITS", typeof(int)),
                new DataColumn("PLANNNEDREVENUE", typeof(string)),

                new DataColumn("CREATEDON", typeof(string)),
                new DataColumn("MODIFIEDON", typeof(string)),
                new DataColumn("CREATEDBY", typeof(string)),

                new DataColumn("MODIFIEDBY", typeof(string)),
                new DataColumn("EQPINST", typeof(string)),
              //  new DataColumn("MATERIALDESCRIPTION_ENGG", typeof(string)),

              //   new DataColumn("MATERIALNO", typeof(string)),
              //  new DataColumn("QTY", typeof(int)),
              //  new DataColumn("SUPPLYTYPE", typeof(string)),

              //  new DataColumn("UnitPrice", typeof(decimal)),
              //  new DataColumn("TotalPrice", typeof(decimal)),
              ////  new DataColumn("Costfrom", typeof(string)),

              //  new DataColumn("MATERIALDESCRIPTION", typeof(string)),

              //  new DataColumn("Remarks", typeof(string)),
              //  new DataColumn("STATUS", typeof(string)),

                });
                        //WHERE [LiftNo] IS NOT NULL
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] ", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();

                        string consString = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.DASHBOARDDELIVERYPLANUPLOAD";
                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("REGION", "REGION");
                                sqlBulkCopy.ColumnMappings.Add("PLANYEAR", "PLANYEAR");
                                sqlBulkCopy.ColumnMappings.Add("PLANMONTH", "PLANMONTH");
                                sqlBulkCopy.ColumnMappings.Add("PLANNEDUNITS", "PLANNEDUNITS");
                                sqlBulkCopy.ColumnMappings.Add("PLANNNEDREVENUE", "PLANNNEDREVENUE");
                                sqlBulkCopy.ColumnMappings.Add("CREATEDON", "CREATEDON");
                                sqlBulkCopy.ColumnMappings.Add("MODIFIEDON", "MODIFIEDON");
                                sqlBulkCopy.ColumnMappings.Add("CREATEDBY", "CREATEDBY");
                                sqlBulkCopy.ColumnMappings.Add("MODIFIEDBY", "MODIFIEDBY");
                                sqlBulkCopy.ColumnMappings.Add("EQPINST", "EQPINST");
                                //sqlBulkCopy.ColumnMappings.Add("GMP", "GMP");
                                //sqlBulkCopy.ColumnMappings.Add("MATERIALDESCRIPTION_ENGG", "MATERIALDESCRIPTION_ENGG");
                                //sqlBulkCopy.ColumnMappings.Add("MATERIALNO", "MATERIALNO");
                                //sqlBulkCopy.ColumnMappings.Add("QTY", "QTY");
                                //sqlBulkCopy.ColumnMappings.Add("SUPPLYTYPE", "SUPPLYTYPE");
                                //sqlBulkCopy.ColumnMappings.Add("UnitPrice", "UnitPrice");
                                //sqlBulkCopy.ColumnMappings.Add("TotalPrice", "TotalPrice");
                                //// sqlBulkCopy.ColumnMappings.Add("Costfrom", "Costfrom");
                                //sqlBulkCopy.ColumnMappings.Add("MATERIALDESCRIPTION", "MATERIALDESCRIPTION");
                                //sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");
                                //sqlBulkCopy.ColumnMappings.Add("STATUS", "STATUS");
                                con.Open();
                                // sqlBulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);
                                //  sqlBulkCopy.NotifyAfter = 1000;


                                // Perform an initial count on the destination table.
                                SqlCommand commandRowCount = new SqlCommand(
                                    "SELECT COUNT(*) FROM dbo.DASHBOARDDELIVERYPLANUPLOAD;", con);
                                long countStart = System.Convert.ToInt32(
                                    commandRowCount.ExecuteScalar());
                                Console.WriteLine("NotifyAfter Sample");
                                Console.WriteLine("Starting row count = {0}", countStart);




                                sqlBulkCopy.WriteToServer(dtExcelData);


                                long countEnd = System.Convert.ToInt32(commandRowCount.ExecuteScalar());
                                //Console.WriteLine("Ending row count = {0}", countEnd);
                                // Console.WriteLine("{0} rows were added.", countEnd - countStart);
                                long count = countEnd - countStart;
                                lblmsg.ForeColor = System.Drawing.Color.DarkGreen;
                                lblmsg.Text = "Total No of rows uploaded " + count.ToString() + " !!!!";
                                // Console.WriteLine("Press Enter to finish.");
                                // Console.ReadLine();
                                con.Close();
                            }
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

        protected void Clear_OnClick(object sender, EventArgs e)
        {
            DDType.SelectedIndex = 0;
            btnDwnldExl.Visible = false;
            //ddcalendarPeriod.SelectedIndex = 0;
            FileUpload1.Dispose();
            lblmsg.Text = "";
            //FileUpload1.Enabled
            gv.DataSource = null;
            gv.DataBind();
            if (b == false)
            {
                btnUpload.Enabled = false;
                btnGenerate.Enabled = false;
                FileUpload1.Enabled = false;
                btnDwnldExl.Enabled = false;
            }
            else
            {
                btnUpload.Enabled = true;
                btnGenerate.Enabled = true;
                FileUpload1.Enabled = true;
                btnDwnldExl.Enabled = true;

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
                    lblmsg.Text = "No data to export to excel";
                }

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void dashboardload()
        {
            try
            {
                if (DDType.SelectedIndex != 0)
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        SqlCommand cmd = new SqlCommand("SP_POCREPORTEQUIPMENTINSTALLATION", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@period", SqlDbType.VarChar).Value = ddcalendarPeriod.SelectedValue;
                        cmd.Parameters.Add("@DType", SqlDbType.VarChar).Value = DDType.SelectedValue;
                        SqlDataAdapter DA = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        con.Open();
                        DA.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (DDType.SelectedValue == "EQP")
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() != "ERROR")
                                {
                                    ViewState["DGV"] = ds.Tables[1];
                                    ///////////////////////////////////////////////////
                                    ViewState["DGV"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    ///////////////////////////////////////////////////  
                                    int index = gv.Rows.Count - 1;
                                    gv.Rows[index].Font.Bold = true;
                                }
                            }
                            else if (DDType.SelectedValue == "INST")
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() != "ERROR")
                                {
                                    ViewState["DGV"] = ds.Tables[1];
                                    //////////////////////////////////////////////////////
                                    ViewState["DGV"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    ///////////////////////////////////////////////////  
                                    int index = gv.Rows.Count - 1;
                                    gv.Rows[index].Font.Bold = true;
                                }
                            }

                            btnUpload.Enabled = false;
                            btnGenerate.Enabled = false;
                            FileUpload1.Enabled = false;
                            btnDwnldExl.Visible = true;
                            ///////////////////////////////////////////////////
                            lblmsg.Text = "";
                        }
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select Dashboard Type";
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
                //string strfilename = "";
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
                        Response.AddHeader("content-disposition", "attachment;filename=" + DDType.Text.Trim() + ".xlsx");

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
        
        protected void ddcalendarPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDType.SelectedIndex != 0)
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        SqlCommand cmd = new SqlCommand("SP_POCREPORTEQUIPMENTINSTALLATION", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@period", SqlDbType.VarChar).Value = ddcalendarPeriod.SelectedValue;
                        cmd.Parameters.Add("@DType", SqlDbType.VarChar).Value = DDType.SelectedValue;
                        SqlDataAdapter DA = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        con.Open();
                        DA.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (DDType.SelectedValue == "EQP")
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() != "ERROR")
                                {
                                    ViewState["DGV"] = ds.Tables[1];
                                    //////////////////////////////////////////////////////
                                    ViewState["DGV"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    ///////////////////////////////////////////////////  
                                    int index = gv.Rows.Count - 1;
                                    gv.Rows[index].Font.Bold = true;
                                }
                            }
                            else if (DDType.SelectedValue == "INST")
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() != "ERROR")
                                {
                                    ViewState["DGV"] = ds.Tables[1];
                                    //////////////////////////////////////////////////////
                                    ViewState["DGV"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    ///////////////////////////////////////////////////  
                                    int index = gv.Rows.Count - 1;
                                    gv.Rows[index].Font.Bold = true;
                                }
                            }

                            btnUpload.Enabled = false;
                            btnGenerate.Enabled = false;
                            FileUpload1.Enabled = false;
                            ///////////////////////////////////////////////////
                            lblmsg.Text = "";
                        }
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select Dashboard Type";
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string colcount = e.Row.Cells.Count.ToString();
                for (int j = 0; j < int.Parse(colcount); j++)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                        //  e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                        //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                        //e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;

                        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                        //e.Row.Cells[2].Text = String.Format("{0:C2}", Convert.ToDecimal(e.Row.Cells[2].Text));
                        gv.Columns[j].HeaderStyle.ForeColor = Color.White;
                        //e.Row.Cells[0].Text= string.Format("   " + e.Row.Cells[0].Text, e.Row.Cells[0].Text);
                    }
                }
                // gv.Columns[0].HeaderCell.Style.ForeColor = Color.White;
                //gv.Columns[0].HeaderStyle.ForeColor = Color.White;
                //gv.Columns[1].HeaderStyle.ForeColor = Color.White;

            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void DDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDType.SelectedIndex > 0)
            {
                //ddcalendarPeriod.SelectedIndex = 0;
                lblmsg.Text = "";
                if (b == false)
                {
                    btnUpload.Enabled = false;
                    btnGenerate.Enabled = false;
                    FileUpload1.Enabled = false;
                    btnDwnldExl.Enabled = false;
                }
                else
                {
                    btnUpload.Enabled = false;
                    btnGenerate.Enabled = false;
                    FileUpload1.Enabled = false;
                    btnDwnldExl.Enabled = true;

                }
                dashboardload();
            }
        }
    }
}