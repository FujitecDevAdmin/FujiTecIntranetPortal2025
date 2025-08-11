using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.MISReports
{
    public partial class WarehouseDashboard : System.Web.UI.Page
    {
        //dbconnectionLIVE
        string strDDUpload = ConfigurationManager.AppSettings["DELIVERYDASHBOARD"].ToString();
        string strcon = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
        bool b;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //try
                //{
                    if (!IsPostBack)
                    {
                        PgLoad();
                    }
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
                        lblUpload.Visible = false;
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
                        lblUpload.Visible = true;

                    }
                //}
                //catch (Exception ex)
                //{ }

            }
            catch (Exception ex)
            {

            }
        }

        private void tempload()
        {
            try
            {
                int[] dayscount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
                int[] week1 = { 1, 2, 3, 4, 5, 6, 7 };
                int[] week2 = { 8, 9, 10, 11, 12, 13, 14 };
                int[] week3 = { 15, 16, 17, 18, 19, 20, 21 };
                int[] week4 = { 22, 23, 24, 25, 26, 27, 28 };
                int[] week5 = { 29, 30, 31 };
                string POSTINGDATE = DateTime.Now.Day.ToString();
                if (int.Parse(POSTINGDATE) < 8)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        dayscount = dayscount.Where(val => val != i).ToArray();
                    }
                }
                else if ((int.Parse(POSTINGDATE) > 7) && (int.Parse(POSTINGDATE) < 15))
                {
                    for (int i = 8; i <= 14; i++)
                    {
                        dayscount = dayscount.Where(val => val != i).ToArray();
                    }
                }
                else if ((int.Parse(POSTINGDATE) > 14) && (int.Parse(POSTINGDATE) < 22))
                {
                    for (int i = 15; i <= 21; i++)
                    {
                        dayscount = dayscount.Where(val => val != i).ToArray();
                    }
                }
                else if ((int.Parse(POSTINGDATE) > 21) && (int.Parse(POSTINGDATE) < 29))
                {
                    for (int i = 22; i <= 28; i++)
                    {
                        dayscount = dayscount.Where(val => val != i).ToArray();
                    }
                }
                else
                {
                    for (int i = 28; i <= 31; i++)
                    {
                        dayscount = dayscount.Where(val => val != i).ToArray();
                    }
                }

                for (int k = 0; k < dayscount.Length; k++)
                {
                    int v = dayscount[k];
                    if (v.ToString().Length == 1)
                        POSTINGDATE = "D0" + v.ToString();
                    else
                        POSTINGDATE = "D" + v.ToString();
                    foreach (DataControlField col in gv.Columns)
                    {
                        if (col.HeaderText == POSTINGDATE)
                        {
                            col.Visible = false;
                            break;
                        }
                    }
                }

                //gv.Columns[0].Visible = false;
                //gv.Columns[7].Visible = false;

                //foreach (DataControlField col in gv.Columns)
                //{
                //    DataControlField col = gv.Columns;
                //    if (POSTINGDATE.Length == 1)
                //        POSTINGDATE = "D0" + POSTINGDATE;
                //    else
                //        POSTINGDATE = "D" + POSTINGDATE;

                //    if (col.HeaderText == POSTINGDATE)
                //    {
                //        col.Visible = false;
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

        }

        private void LoadReport()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["WAREHOUSE"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string proj = dt.Rows[i]["PROJECTNAME"].ToString();
                    string POSTINGDATE = dt.Rows[i]["POSTINGDATE"].ToString();
                    string receivedamount = dt.Rows[i]["RECEIVEDAMOUNT"].ToString();


                    DateTime dtm = Convert.ToDateTime(POSTINGDATE);
                    POSTINGDATE = dtm.Day.ToString();
                    if (POSTINGDATE.Length == 1)
                        POSTINGDATE = "D0" + POSTINGDATE;
                    else
                        POSTINGDATE = "D" + POSTINGDATE;

                    //if (Convert.ToDecimal(receivedamount) > 0)
                    //{
                       
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            SqlCommand cmd = new SqlCommand("SP_UPDATE_TBL_WAREHOUSEDASHBOARDUPLOAD", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@PROJECTNAME", SqlDbType.NVarChar).Value = proj;
                            cmd.Parameters.Add("@POSTINGDATE", SqlDbType.NVarChar).Value = POSTINGDATE;
                            cmd.Parameters.Add("@RECEIVEDAMOUNT", SqlDbType.NVarChar).Value = receivedamount;
                            SqlDataAdapter DA = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            con.Open();
                            DA.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //ViewState["DGV"] = ds.Tables[0];
                                //ViewState["DGVEx"] = ds.Tables[1];
                                //ViewState["TargetCostIN"] = ds.Tables[2].Rows[0]["TARGETCOST"].ToString();
                                //ViewState["TargetCostEX"] = ds.Tables[3].Rows[0]["TARGETCOST"].ToString();
                                //ViewState["DGVSalesClose"] = ds.Tables[4];
                            }
                        }
                    //}
                }

                //using (SqlConnection con = new SqlConnection(strcon))
                //{
                //    SqlCommand cmd = new SqlCommand("SP_WAREHOUSEDASHBOARDEXPORTIMPORTLOADGRID", con);
                //    cmd.CommandType = CommandType.StoredProcedure;

                //    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                //    DataSet ds = new DataSet();
                //    con.Open();
                //    DA.Fill(ds);
                //    //response.result = false;
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        ViewState["DGV"] = ds.Tables[0];
                //        ViewState["DGVEx"] = ds.Tables[1];
                //        ViewState["TargetCostIN"] = ds.Tables[2].Rows[0]["TARGETCOST"].ToString();
                //        ViewState["TargetCostEX"] = ds.Tables[3].Rows[0]["TARGETCOST"].ToString();
                //        ViewState["DGVSalesClose"] = ds.Tables[4];

                //    }
                //}

                string result = String.Empty;
                ///////////////////////////////////////////////
                if (rbtnType.SelectedValue == "DM")
                {

                    //DataTable dt = 
                    //gv.DataSource = (DataTable)ViewState["DGV"];
                    //gv.DataBind();
                    //result = ViewState["TargetCostIN"].ToString();
                    //gv.Rows[0].Cells[8].Text = result;
                    //gv.Rows[0].Font.Bold = true;

                    Response.Redirect("~/MISReports/DomesticWarehouseDashboard.aspx?Parameter=DM");


                }
                else
                {
                    //  DataTable dt =
                    //gv.DataSource = (DataTable)ViewState["DGVEx"];
                    //gv.DataBind();
                    //result = ViewState["TargetCostEX"].ToString();
                    //gv.Rows[0].Cells[8].Text = result;
                    //gv.Rows[0].Font.Bold = true;

                    Response.Redirect("~/MISReports/DomesticWarehouseDashboard.aspx?Parameter=EX");


                }

                //DataTable dtcolor = (DataTable)ViewState["DGVSalesClose"];
                //for (int bck = 0; bck < dtcolor.Rows.Count; bck++)
                //{
                //    // foreach (DataGridViewRow row in gv.Rows)
                //    for (int grd = 0; grd < gv.Rows.Count; grd++)
                //    {

                //        if (dtcolor.Rows[bck]["PROJECTNO"].ToString() == gv.Rows[grd].Cells[1].Text)
                //        {
                //            gv.Rows[grd].BackColor = System.Drawing.Color.Green;
                //        }
                //    }

                //}

                //////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void PgLoad()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_WarehouseConfluenceDashboard", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["WAREHOUSE"] = ds.Tables[0];

                        //gv.DataSource = ds.Tables[0];
                        // gv.DataBind();

                    }
                }


            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
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
                new DataColumn("PROJECTNO", typeof(string)),
                new DataColumn("BRANCHLOCATION", typeof(string)),
                //new DataColumn("LiftNumber", typeof(string)),

                new DataColumn("PROJECTNAME", typeof(string)),
                new DataColumn("PRODUCTTYPE", typeof(string)),
                new DataColumn("CAPACITY", typeof(string)),

                new DataColumn("SPEEDMPS", typeof(string)),
                new DataColumn("STOPS", typeof(string)),
                new DataColumn("TARGETUNITS", typeof(string)),

                new DataColumn("PERIODMONTH", typeof(string)),
                new DataColumn("PERIODYEAR", typeof(string)),
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
                                sqlBulkCopy.DestinationTableName = "dbo.TBL_WAREHOUSEDASHBOARDUPLOAD";
                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("PROJECTNO", "PROJECTNO");
                                sqlBulkCopy.ColumnMappings.Add("BRANCHLOCATION", "BRANCHLOCATION");
                                sqlBulkCopy.ColumnMappings.Add("PROJECTNAME", "PROJECTNAME");
                                sqlBulkCopy.ColumnMappings.Add("PRODUCTTYPE", "PRODUCTTYPE");
                                sqlBulkCopy.ColumnMappings.Add("CAPACITY", "CAPACITY");
                                sqlBulkCopy.ColumnMappings.Add("SPEEDMPS", "SPEEDMPS");
                                sqlBulkCopy.ColumnMappings.Add("STOPS", "STOPS");
                                sqlBulkCopy.ColumnMappings.Add("TARGETUNITS", "TARGETCOST");
                                sqlBulkCopy.ColumnMappings.Add("PERIODMONTH", "PERIODMONTH");
                                sqlBulkCopy.ColumnMappings.Add("PERIODYEAR", "PERIODYEAR");
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
                                    "SELECT COUNT(*) FROM dbo.TBL_WAREHOUSEDASHBOARDUPLOAD;", con);
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

        protected void GenerateTemplate_OnClick(object sender, EventArgs e)
        {
            try
            {
                //Label1.Text = "";
                string strURL = Server.MapPath("Template/WarehouseDashboard.xlsx");
                WebClient req = new WebClient();
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"WarehouseDashboard.xlsx" + "\"");
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

        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                //PgLoad();
                LoadReport();
                tempload();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void Clear_OnClick(object sender, EventArgs e)
        {

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

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (rbtnType.SelectedValue == "DM")
            //{

            //    //DataTable dt = 
            //    gv.DataSource = (DataTable)ViewState["DGV"];
            //    gv.DataBind();
            //    string result = ViewState["TargetCostIN"].ToString();
            //    gv.Rows[0].Cells[8].Text = result;
            //    gv.Rows[0].Font.Bold = true;


            //}
            //else
            //{
            //    //  DataTable dt =
            //    gv.DataSource = (DataTable)ViewState["DGVEx"];
            //    gv.DataBind();
            //    string result = ViewState["TargetCostEX"].ToString();
            //    gv.Rows[0].Cells[8].Text = result;
            //    gv.Rows[0].Font.Bold = true;


            //}

            DataTable dtcolor = (DataTable)ViewState["DGVSalesClose"];
            for (int bck = 0; bck < dtcolor.Rows.Count; bck++)
            {
                // foreach (DataGridViewRow row in gv.Rows)
                for (int grd = 0; grd < gv.Rows.Count; grd++)
                {

                    if (dtcolor.Rows[bck]["PROJECTNO"].ToString() == gv.Rows[grd].Cells[1].Text)
                    {
                        gv.Rows[grd].BackColor = System.Drawing.Color.Green;
                    }
                }

            }
            //string colcount = e.Row.Cells.Count.ToString();
            //for (int j = 0; j < int.Parse(colcount); j++)
            //{
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    gv.Columns[1].ItemStyle.Width = 75;
            //    gv.Columns[2].ItemStyle.Width = 75;
            //    gv.Columns[3].ItemStyle.Width = 50;
            //}
            // }
            //int indexOfColumn = int.Parse(DateTime.Now.Day.ToString());

            //if (e.Row.Cells.Count > indexOfColumn)
            //{
            //    e.Row.Cells[indexOfColumn].Visible = false;
            //}
            //for (int j = 1; j < gv.Rows.Count; j++)
            //{
            //    if (gv.Rows[j].Cells[0].Text.Contains())
            //}
            //int[] dayscount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            //int[] week1 = { 1, 2, 3, 4, 5, 6, 7 };
            //int[] week2 = { 8, 9, 10, 11, 12, 13, 14 };
            //int[] week3 = { 15, 16, 17, 18, 19, 20, 21 };
            //int[] week4 = { 22, 23, 24, 25, 26, 27, 28 };
            //int[] week5 = { 29, 30, 31 };
            //string POSTINGDATE = DateTime.Now.Day.ToString();
            //if (int.Parse(POSTINGDATE) < 8)
            //{
            //    for (int i = 1; i <= 7; i++)
            //    {
            //        dayscount = dayscount.Where(val => val != i).ToArray();
            //    }
            //}
            //else if ((int.Parse(POSTINGDATE) > 7) && (int.Parse(POSTINGDATE) < 15))
            //{
            //    for (int i = 8; i <= 14; i++)
            //    {
            //        dayscount = dayscount.Where(val => val != i).ToArray();
            //    }
            //}
            //else if ((int.Parse(POSTINGDATE) > 14) && (int.Parse(POSTINGDATE) < 22))
            //{
            //    for (int i = 15; i <= 21; i++)
            //    {
            //        dayscount = dayscount.Where(val => val != i).ToArray();
            //    }
            //}
            //else if ((int.Parse(POSTINGDATE) > 21) && (int.Parse(POSTINGDATE) < 29))
            //{
            //    for (int i = 22; i <= 28; i++)
            //    {
            //        dayscount = dayscount.Where(val => val != i).ToArray();
            //    }
            //}
            //else
            //{
            //    for (int i = 28; i <= 31; i++)
            //    {
            //        dayscount = dayscount.Where(val => val != i).ToArray();
            //    }
            //}


            //if (POSTINGDATE.Length == 1)
            //    POSTINGDATE = "D0" + POSTINGDATE;
            //else
            //    POSTINGDATE = "D" + POSTINGDATE;
            ////gv.Columns[0].Visible = false;
            ////gv.Columns[7].Visible = false;

            //foreach (DataControlField col in gv.Columns)
            //{
            //    if (col.HeaderText == POSTINGDATE)
            //    {
            //        col.Visible = false;
            //    }
            //}
        }

        protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnType.SelectedValue == "DM")
            {

                DataTable dt = (DataTable)ViewState["DGV"];
                if (dt!=null)
                {
                    gv.DataSource = dt;
                    gv.DataBind();
                    string result = ViewState["TargetCostIN"].ToString();
                    gv.Rows[0].Cells[8].Text = result;
                    gv.Rows[0].Font.Bold = true;
                }


            }
            else
            {
                DataTable dt = (DataTable)ViewState["DGV"];
                if (dt!=null)
                {
                    gv.DataSource = (DataTable)ViewState["DGVEx"];
                    gv.DataBind();
                    string result = ViewState["TargetCostEX"].ToString();
                    gv.Rows[0].Cells[8].Text = result;
                    gv.Rows[0].Font.Bold = true;
                }


            }
            DataTable dtcolor = (DataTable)ViewState["DGVSalesClose"];
            if (dtcolor != null)
            {
                for (int bck = 0; bck < dtcolor.Rows.Count; bck++)
                {
                    // foreach (DataGridViewRow row in gv.Rows)
                    for (int grd = 0; grd < gv.Rows.Count; grd++)
                    {

                        if (dtcolor.Rows[bck]["PROJECTNO"].ToString() == gv.Rows[grd].Cells[1].Text)
                        {
                            gv.Rows[grd].BackColor = System.Drawing.Color.Green;
                        }
                    }

                }
            }
        }
    }
}