using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.MISReports
{
    public partial class DomesticWarehouseDashboard : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["dbconnectionLIVE"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PgLoad();
                    tempload();
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
                    string param = Request.QueryString["Parameter"].ToString();
                    string sp = string.Empty;
                    if (param == "DM")
                        sp = "SP_WAREHOUSEDASHBOARDEXPORTIMPORTLOADGRID";
                    else
                        sp = "SP_WAREHOUSEDASHBOARDEXPORTLOADGRID_MODFY";
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["DGV"] = ds.Tables[0];
                        // ViewState["DGVEx"] = ds.Tables[1];
                        ViewState["TargetCostIN"] = ds.Tables[1].Rows[0]["TARGETCOST"].ToString();
                        // ViewState["TargetCostEX"] = ds.Tables[3].Rows[0]["TARGETCOST"].ToString();
                        ViewState["DGVSalesClose"] = ds.Tables[2];
                        ViewState["Dayscount"] = ds.Tables[3];

                        txtTotalCostW1.Text = ds.Tables[0].Rows[0]["TotalCostW1"].ToString();
                        txtTotalCostW2.Text = ds.Tables[0].Rows[0]["TotalCostW2"].ToString();
                        txtTotalCostW3.Text = ds.Tables[0].Rows[0]["TotalCostW3"].ToString();
                        txtTotalCostW4.Text = ds.Tables[0].Rows[0]["TotalCostW4"].ToString();
                        txtTotalCostW5.Text = ds.Tables[0].Rows[0]["TotalCostW5"].ToString();

                        txtTotalCostTillW2.Text = ds.Tables[0].Rows[0]["TotalCostTillW2"].ToString();
                        txtTotalCostTillW3.Text = ds.Tables[0].Rows[0]["TotalCostTillW3"].ToString();
                        txtTotalCostTillW4.Text = ds.Tables[0].Rows[0]["TotalCostTillW4"].ToString();
                        txtTotal.Text = ds.Tables[0].Rows[0]["TotalCost"].ToString();

                    }
                }
               // string result = String.Empty;
                ///////////////////////////////////////////////
                //DataTable dt = 
                gv.DataSource = (DataTable)ViewState["DGV"];
                gv.DataBind();
                //result = ViewState["TargetCostIN"].ToString();
              //  gv.Rows[1].Cells[8].Text = result;

                DataTable dtcolor = (DataTable)ViewState["DGVSalesClose"];
                int w1 = 0, w2 = 0, w3 = 0, w4 = 0, w5 = 0;
                for (int bck = 0; bck < dtcolor.Rows.Count; bck++)
                {
                    // foreach (DataGridViewRow row in gv.Rows)
                   
                    if (dtcolor.Rows[bck]["NOOFUNITSPERWEEK"].ToString() == "Week1")
                    {
                        w1 = w1 + 1;
                        txtTotalUnitsW1.Text = w1.ToString();
                    }
                    else if (dtcolor.Rows[bck]["NOOFUNITSPERWEEK"].ToString() == "Week2")
                    {
                        w2 = w2 + 1;
                        txtTotalUnitsW2.Text = w2.ToString();
                    }
                    else if (dtcolor.Rows[bck]["NOOFUNITSPERWEEK"].ToString() == "Week3")
                    {
                        w3 = w3 + 1;
                        txtTotalUnitsW3.Text = w3.ToString();
                        txtTotalUnitsTillW3.Text = (w1 + w2 + w3).ToString();
                    }
                    else if (dtcolor.Rows[bck]["NOOFUNITSPERWEEK"].ToString() == "Week4")
                    {
                        w4 = w4 + 1;
                        txtTotalUnitsW4.Text = w4.ToString();
                        txtTotalUnitsTillW4.Text = (w1 + w2 + w3 + w4).ToString();
                    }
                    else if (dtcolor.Rows[bck]["NOOFUNITSPERWEEK"].ToString() == "Week5")
                    {
                        w5 = w5 + 1;
                        txtTotalUnitsW5.Text = w5.ToString();
                        txtTotalUnits.Text = (w1 + w2 + w3 + w4 + w5).ToString();
                    }
                    //for (int grd = 0; grd < gv.Rows.Count; grd++)
                    //{
                    //    if (dtcolor.Rows[bck]["PROJECTNO"].ToString().Trim() == gv.Rows[grd].Cells[1].Text.Trim())
                    //    {
                    //        gv.Rows[grd].BackColor = System.Drawing.Color.LightGreen;
                    //        break;
                    //    }
                    //}
                }
                int w_2 = 0, w_3 = 0, w_4 = 0, w_5 = 0;
                w_2 = int.Parse(txtTotalUnitsW1.Text) + int.Parse(txtTotalUnitsW2.Text);
                txtTotalUnitsTillW2.Text = w_2.ToString();
                w_3 = w_2 + int.Parse(txtTotalUnitsW3.Text);
                txtTotalUnitsTillW3.Text = w_3.ToString();
                w_4 = w_3 + int.Parse(txtTotalUnitsW4.Text);
                txtTotalUnitsTillW4.Text = w_4.ToString();
                w_5 = w_4 + int.Parse(txtTotalUnitsW5.Text);
                txtTotalUnits.Text = w_5.ToString();

                //////////////////////////////////////////////////////
                DataTable dt = (DataTable)ViewState["DGV"];
                DataRow dr1 = dt.NewRow();
                dr1["OPPORTUNITYNAME"] = "";
                dr1["PROJECTNO"] = "Total Units";
                dr1["BRANCH"] = "";
                dr1["PROJECTNAME"] = "";
                dr1["PRODUCTTYPE"] = "";
                dr1["CAPACITY"] = "";
                dr1["SPEEDMPS"] = "";
                dr1["STOPS"] = "0";
                dr1["TARGETCOST"] = "0";
                dr1["D01"] = 0; //"";
                dr1["D02"] = 0; //"";
                dr1["D03"] = 0; //"";
                dr1["D04"] = 0; //"";
                dr1["D05"] = 0; //"";
                dr1["D06"] = 0; //"";
                dr1["D07"] = 0; //"";
                dr1["D08"] = 0; //"";
                dr1["D09"] = 0; //"";
                dr1["D10"] = 0; //"";
                dr1["D11"] = 0; //"";
                dr1["D12"] = 0; //"";
                dr1["D13"] = 0; //"";
                dr1["D14"] = 0; //"";
                dr1["D15"] = 0; //"";
                dr1["D16"] = 0; //"";
                dr1["D17"] = 0; //"";
                dr1["D18"] = 0; //"";
                dr1["D19"] = 0; //"";
                dr1["D20"] = 0; //"";
                dr1["D21"] = 0; //"";
                dr1["D22"] = 0; //"";
                dr1["D23"] = 0; //"";
                dr1["D24"] = 0; //"";
                dr1["D25"] = 0; //"";
                dr1["D26"] = 0; //"";
                dr1["D27"] = 0; //"";
                dr1["D28"] = 0; //"";
                dr1["D29"] = 0; //"";
                dr1["D30"] = 0; //"";
                dr1["D31"] = 0; //"";
                DataTable dtv3 = (DataTable)ViewState["Dayscount"];
                for (int k = 0; k < dtv3.Rows.Count; k++)
                {
                    string POSTINGDATE = dtv3.Rows[k]["OrderSalseCloseDate"].ToString();
                    POSTINGDATE = Convert.ToDateTime(POSTINGDATE).Day.ToString();// DateTime.Now.Day.ToString();
                    if (POSTINGDATE.ToString().Length == 1)
                        POSTINGDATE = "D0" + POSTINGDATE.ToString();
                    else
                        POSTINGDATE = "D" + POSTINGDATE.ToString();

                    dr1[POSTINGDATE] = dtv3.Rows[k]["Salescount"].ToString();  //"";

                }

                dr1["TotalCostW1"] = txtTotalUnitsW1.Text;
                dr1["TotalCostW2"] = txtTotalUnitsW2.Text;
                dr1["TotalCostW3"] = txtTotalUnitsW3.Text;
                dr1["TotalCostW4"] = txtTotalUnitsW4.Text;
                dr1["TotalCostW5"] = txtTotalUnitsW5.Text;
                dr1["TotalCostTillW2"] = (int.Parse(txtTotalUnitsW1.Text) + int.Parse(txtTotalUnitsW2.Text)).ToString();
                dr1["TotalCostTillW3"] = (int.Parse(txtTotalUnitsW1.Text) + int.Parse(txtTotalUnitsW2.Text) + int.Parse(txtTotalUnitsW3.Text)).ToString();
                dr1["TotalCostTillW4"] = (int.Parse(txtTotalUnitsW1.Text) + int.Parse(txtTotalUnitsW2.Text) + int.Parse(txtTotalUnitsW3.Text) + int.Parse(txtTotalUnitsW4.Text)).ToString();
                dr1["TotalCost"] = (int.Parse(txtTotalUnitsW1.Text) + int.Parse(txtTotalUnitsW2.Text) + int.Parse(txtTotalUnitsW3.Text) + int.Parse(txtTotalUnitsW4.Text) + int.Parse(txtTotalUnitsW5.Text)).ToString();
                //dt.Rows.Add(dr1);
                dt.Rows.InsertAt(dr1, 0);
                gv.DataSource = (DataTable)ViewState["DGV"];
                gv.DataBind();
                //result = ViewState["TargetCostIN"].ToString();
                //gv.Rows[1].Cells[8].Text = result;
                gv.Rows[0].Font.Bold = true;
                gv.Rows[1].Font.Bold = true;
                //////////////////////////////////////////////////////
                //DataTable dtday = (DataTable)ViewState["Dayscount"];
                //int[] dayscount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
                //int[] week1 = { 1, 2, 3, 4, 5, 6, 7 };
                //int[] week2 = { 8, 9, 10, 11, 12, 13, 14 };
                //int[] week3 = { 15, 16, 17, 18, 19, 20, 21 };
                //int[] week4 = { 22, 23, 24, 25, 26, 27, 28 };
                //int[] week5 = { 29, 30, 31 };
                //string POSTINGDATE = DateTime.Now.Day.ToString();

                //if (week1.Contains(int.Parse(POSTINGDATE)))
                //{
                //    lblD1.Text = "D01";
                //    lblD2.Text = "D02";
                //    lblD3.Text = "D03";
                //    lblD4.Text = "D04";
                //    lblD5.Text = "D05";
                //    lblD6.Text = "D06";
                //    lblD7.Text = "D07";

                //    foreach (DataRow row in dtday.Rows)
                //    {
                //        if (week1[0].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD1.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[1].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD2.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[2].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD3.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[3].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD4.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[4].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD5.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[5].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD6.Text = row["Salescount"].ToString();
                //        }
                //        else if (week1[6].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD7.Text = row["Salescount"].ToString();
                //        }
                //    }


                //}
                //else if (week2.Contains(int.Parse(POSTINGDATE)))
                //{
                //    lblD1.Text = "D08";
                //    lblD2.Text = "D09";
                //    lblD3.Text = "D10";
                //    lblD4.Text = "D11";
                //    lblD5.Text = "D12";
                //    lblD6.Text = "D13";
                //    lblD7.Text = "D14";
                //    foreach (DataRow row in dtday.Rows)
                //    {
                //        if (week2[0].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD1.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[1].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD2.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[2].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD3.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[3].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD4.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[4].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD5.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[5].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD6.Text = row["Salescount"].ToString();
                //        }
                //        else if (week2[6].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD7.Text = row["Salescount"].ToString();
                //        }
                //    }
                //}
                //else if (week3.Contains(int.Parse(POSTINGDATE)))
                //{
                //    lblD1.Text = "D15";
                //    lblD2.Text = "D16";
                //    lblD3.Text = "D17";
                //    lblD4.Text = "D18";
                //    lblD5.Text = "D19";
                //    lblD6.Text = "D20";
                //    lblD7.Text = "D21";
                //    foreach (DataRow row in dtday.Rows)
                //    {
                //        if (week3[0].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD1.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[1].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD2.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[2].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD3.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[3].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD4.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[4].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD5.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[5].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD6.Text = row["Salescount"].ToString();
                //        }
                //        else if (week3[6].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD7.Text = row["Salescount"].ToString();
                //        }
                //    }
                //}
                //else if (week4.Contains(int.Parse(POSTINGDATE)))
                //{
                //    lblD1.Text = "D22";
                //    lblD2.Text = "D23";
                //    lblD3.Text = "D24";
                //    lblD4.Text = "D25";
                //    lblD5.Text = "D26";
                //    lblD6.Text = "D27";
                //    lblD7.Text = "D28";
                //    foreach (DataRow row in dtday.Rows)
                //    {
                //        if (week4[0].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD1.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[1].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD2.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[2].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD3.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[3].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD4.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[4].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD5.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[5].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD6.Text = row["Salescount"].ToString();
                //        }
                //        else if (week4[6].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD7.Text = row["Salescount"].ToString();
                //        }
                //    }
                //}
                //else if (week5.Contains(int.Parse(POSTINGDATE)))
                //{
                //    lblD1.Text = "D29";
                //    lblD2.Text = "D30";
                //    lblD3.Text = "D31";
                //    lblD4.Visible = false;
                //    lblD5.Visible = false;
                //    lblD6.Visible = false;
                //    lblD7.Visible = false;
                //    txtD4.Visible = false;
                //    txtD5.Visible = false;
                //    txtD6.Visible = false;
                //    txtD7.Visible = false;
                //    foreach (DataRow row in dtday.Rows)
                //    {
                //        if (week5[0].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD1.Text = row["Salescount"].ToString();
                //        }
                //        else if (week5[1].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD2.Text = row["Salescount"].ToString();
                //        }
                //        else if (week5[2].ToString() == row["ORSDAY"].ToString())
                //        {
                //            txtD3.Text = row["Salescount"].ToString();
                //        }

                //    }
                //}

                ///////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                // lblmsg.ForeColor = System.Drawing.Color.Red;
                // lblmsg.Text = ex.Message;
            }
        }
        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    for (int i = 29; i <= 31; i++)
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


            }
            catch (Exception ex)
            {

            }

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
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                //lblmsg.Text = ex.Message;
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
            //    //  DataTable dt = (DataTable)ViewState["DGV"];
            //    gv.DataSource = (DataTable)ViewState["DGVEx"];
            //    gv.DataBind();
            //    string result = ViewState["TargetCostEX"].ToString();
            //    gv.Rows[0].Cells[8].Text = result;
            //    gv.Rows[0].Font.Bold = true;


            //}
            DataTable dt = (DataTable)ViewState["DGV"];
            //gv.DataSource = (DataTable)ViewState["DGV"];
            //gv.DataBind();
            ////////////////////////vino//////////////////           
            DataTable dtcolor = (DataTable)ViewState["DGVSalesClose"];
            for (int bck = 0; bck < dtcolor.Rows.Count; bck++)
            {
                // foreach (DataGridViewRow row in gv.Rows)
                for (int grd = 0; grd < dt.Rows.Count; grd++)
                {
                    //if (dtcolor.Rows[bck]["PROJECTNO"].ToString() == "Grand Total")
                    //{
                    //    string result = ViewState["TargetCostIN"].ToString();
                    //    gv.Rows[1].Cells[8].Text = result;
                    //}
                    if (dtcolor.Rows[bck]["PROJECTNO"].ToString() == e.Row.Cells[1].Text)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    }

                }

            }
            /////////////////////////vino//////////////////
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

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/MISReports/WarehouseDashboard.aspx?");
            }
            catch(Exception ex)
            {

            }
        }
    }
}