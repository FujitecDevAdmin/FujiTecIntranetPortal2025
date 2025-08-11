using ClosedXML.Excel;
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
    public partial class ClosedMargin : System.Web.UI.Page
    {
        string consString = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtup = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(consString))
                {
                    SqlCommand cmd = new SqlCommand("SP_CLOSEDMARGINPAGELOADCON", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt = ds.Tables[0];
                        dt1 = ds.Tables[1];
                        dtup = ds.Tables[2];
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnUpload_Onclick(object sender, EventArgs e)
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
                        dtExcelData.Columns.AddRange(new DataColumn[30] {
                new DataColumn("MONTHYEAR",typeof(string)),
                new DataColumn("LIFTNO", typeof(string)),
                new DataColumn("ORGANIZATION", typeof(string)),
                new DataColumn("BRANCH", typeof(string)),

                new DataColumn("DOMESTICEXPORT", typeof(string)),
                new DataColumn("EST_STD_ACT", typeof(string)),
                new DataColumn("PCVER", typeof(string)),

                new DataColumn("PROJECTNO", typeof(string)),
                new DataColumn("PROJECTNAME", typeof(string)),
                new DataColumn("SPECIFICATION", typeof(string)),

                new DataColumn("PRODUCT", typeof(string)),
                new DataColumn("COSTBUCKET", typeof(string)),
                new DataColumn("GMP", typeof(string)),

                new DataColumn("MATERIALDESCRIPTION_ENGG",typeof(string)),
                new DataColumn("MATERIALNO", typeof(string)),
                new DataColumn("KITTINGCODE", typeof(string)),

                new DataColumn("KITTINGDESCRIPTION",typeof(string)),
                new DataColumn("QTY", typeof(int)),
                new DataColumn("SUPPLYTYPE", typeof(string)),

                new DataColumn("UNITPRICE", typeof(decimal)),
                new DataColumn("TOTALPRICE", typeof(decimal)),
                new DataColumn("PRICEREFERENCE", typeof(string)),

                new DataColumn("MATERIALGROUPING", typeof(string)),
                new DataColumn("PRODUCTNAME", typeof(string)),
                new DataColumn("CAPACITY", typeof(string)),

                new DataColumn("SPEED_MPS", typeof(string)),
                new DataColumn("STOPS", typeof(string)),
                new DataColumn("CREATEDBY", typeof(string)),

                new DataColumn("MODIFIEDBY", typeof(string)),
                new DataColumn("CREATEDON", typeof(DateTime)),
               // new DataColumn("MODIFIEDON", typeof(DateTime)),
                });
                        //WHERE [LiftNo] IS NOT NULL
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] ", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();

                        consString = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.TBL_CLOSEDMARGINPLANUPLOAD";
                                //[OPTIONAL]: Map the Excel columns with that of the database table
                                sqlBulkCopy.ColumnMappings.Add("MONTHYEAR", "MONTHYEAR");
                                sqlBulkCopy.ColumnMappings.Add("LIFTNO", "LIFTNO");
                                sqlBulkCopy.ColumnMappings.Add("ORGANIZATION", "ORGANIZATION");
                                sqlBulkCopy.ColumnMappings.Add("BRANCH", "BRANCH");

                                sqlBulkCopy.ColumnMappings.Add("DOMESTICEXPORT", "DOMESTICEXPORT");
                                sqlBulkCopy.ColumnMappings.Add("EST_STD_ACT", "EST_STD_ACT");
                                sqlBulkCopy.ColumnMappings.Add("PCVER", "PCVER");
                                sqlBulkCopy.ColumnMappings.Add("PROJECTNO", "PROJECTNO");

                                sqlBulkCopy.ColumnMappings.Add("PROJECTNAME", "PROJECTNAME");
                                sqlBulkCopy.ColumnMappings.Add("SPECIFICATION", "SPECIFICATION");
                                sqlBulkCopy.ColumnMappings.Add("PRODUCT", "PRODUCT");

                                sqlBulkCopy.ColumnMappings.Add("COSTBUCKET", "COSTBUCKET");

                                sqlBulkCopy.ColumnMappings.Add("GMP", "GMP");
                                sqlBulkCopy.ColumnMappings.Add("MATERIALDESCRIPTION_ENGG", "MATERIALDESCRIPTION_ENGG");
                                sqlBulkCopy.ColumnMappings.Add("MATERIALNO", "MATERIALNO");
                                sqlBulkCopy.ColumnMappings.Add("KITTINGCODE", "KITTINGCODE");
                                sqlBulkCopy.ColumnMappings.Add("KITTINGDESCRIPTION", "KITTINGDESCRIPTION");

                                sqlBulkCopy.ColumnMappings.Add("QTY", "QTY");
                                sqlBulkCopy.ColumnMappings.Add("SUPPLYTYPE", "SUPPLYTYPE");
                                sqlBulkCopy.ColumnMappings.Add("UNITPRICE", "UNITPRICE");
                                sqlBulkCopy.ColumnMappings.Add("TOTALPRICE", "TOTALPRICE");

                                sqlBulkCopy.ColumnMappings.Add("PRICEREFERENCE", "PRICEREFERENCE");
                                sqlBulkCopy.ColumnMappings.Add("MATERIALGROUPING", "MATERIALGROUPING");
                                sqlBulkCopy.ColumnMappings.Add("PRODUCTNAME", "PRODUCTNAME");
                                sqlBulkCopy.ColumnMappings.Add("CAPACITY", "CAPACITY");

                                sqlBulkCopy.ColumnMappings.Add("SPEED_MPS", "SPEED_MPS");
                                sqlBulkCopy.ColumnMappings.Add("STOPS", "STOPS");
                                sqlBulkCopy.ColumnMappings.Add("CREATEDBY", "CREATEDBY");
                                sqlBulkCopy.ColumnMappings.Add("MODIFIEDBY", "MODIFIEDBY");
                                sqlBulkCopy.ColumnMappings.Add("CREATEDON", "CREATEDON");
                                //sqlBulkCopy.ColumnMappings.Add("MODIFIEDON", "MODIFIEDON");

                                con.Open();
                                // sqlBulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);
                                //  sqlBulkCopy.NotifyAfter = 1000;


                                // Perform an initial count on the destination table.
                                SqlCommand commandRowCount = new SqlCommand(
                                    "SELECT COUNT(*) FROM dbo.TBL_CLOSEDMARGINPLANUPLOAD;", con);
                                long countStart = System.Convert.ToInt32(
                                    commandRowCount.ExecuteScalar());
                                //Console.WriteLine("NotifyAfter Sample");

                                Console.WriteLine("Starting row count = {0}", countStart);

                                ///////////////////////////////////////////////////////////////////////
                                //string sql = "DELETE e FROM[dbo].TBL_CLOSEDMARGINPLANUPLOAD e INNER JOIN(SELECT * , RANK() OVER(PARTITION BY LIFTNO, MONTHYEAR OrdeR BY ID DESC) AS[RANK] FROM dbo.TBL_CLOSEDMARGINPLANUPLOAD) t ON e.LIFTNO = t.LIFTNO AND e.MONTHYEAR = t.MONTHYEAR and e.ID = t.ID WHERE[rank] > 1";
                                //// Perform an initial count on the destination table.
                                //commandRowCount = new SqlCommand(sql, con);
                                //commandRowCount.ExecuteNonQuery();
                                //Console.WriteLine("SINGAM DA");

                                ///////////////////////////////////////////////////////////////////////

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
            FileUpload1.Dispose();
            lblmsg.Text = "";
        }

        protected void GenerateTemplate_OnClick(object sender, EventArgs e)
        {
            try
            {
                //Label1.Text = "";
                string strURL = Server.MapPath("Template/ClosedMargin.xlsx");
                WebClient req = new WebClient();
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"ClosedMargin.xlsx" + "\"");
                byte[] data = req.DownloadData(strURL);
                Response.BinaryWrite(data);
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnActual_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(consString))
                {
                    SqlCommand cmd = new SqlCommand("SP_CLOSEDMARGINACTUAL", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3000;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dt = ds.Tables[0];
                            dt1 = ds.Tables[1];
                            dtup = ds.Tables[2];
                            lblmsg.ForeColor = System.Drawing.Color.DarkGreen;
                            lblmsg.Text = "Actual Data have been processed  !!!!";
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

        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                ExportGridToExcel();
            }
            catch(Exception ex)
            {

            }
        }
        private void ExportGridToExcel()
        {
            try
            {
                dtup.TableName = "ClosedMarginPlan";
                dt.TableName = "ClosedMarginActual";
                dt1.TableName = "ClosedMarginExpectedActual";

                if (dt != null)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dtup);
                        wb.Worksheets.Add(dt);
                        wb.Worksheets.Add(dt1);
                        //int m = 0;
                        foreach (var ws in wb.Worksheets)
                        {
                            ws.ColumnWidth = 20;
                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=ClosedMarginActualExpected" + DateTime.Now.ToString("d").Trim() + ".xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Downloaded Successfully";
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