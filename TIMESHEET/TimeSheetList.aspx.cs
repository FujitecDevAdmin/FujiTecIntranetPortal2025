using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlTypes;
using ClosedXML.Excel;
using System.IO;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class TimeSheetList : System.Web.UI.Page
    {
        public DataSet dtst;
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FromCal.Visible = false;
                    Tocal.Visible = false;
                    FetchInitializeDetails();
                    ViewState["ID"] = "";
                    ViewState["TSID"] = "";
                    ViewState["Totalhours"] = "0";
                    ddStatus.SelectedIndex = 1;
                    //txtActualStart.Enabled= false;
                    //txtActualEnd.Enabled= false;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnDwnldExl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["CurrentTable"] != null && !ViewState["CurrentTable"].Equals("-1"))
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
               
                dtst = new DataSet();
                //CurrentTable
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                dt.TableName = "Timesheet";
                dtst.Tables.Add(dt);
                /////////////////////////////////////////////////

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
                            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Center column headers

                            ws.ColumnWidth = 20;
                            // ws.LastColumnUsed().Width = 60;
                            ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                        }

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=TimeSheetReport" + strfilename.Trim() + ".xlsx");

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
        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSTimeSheetPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["Proj"] = ds.Tables[5];
                        DataRow dr = ds.Tables[5].NewRow();
                        dr.ItemArray = new object[] { "Select Project", "--Select Project--" };
                        ds.Tables[5].Rows.InsertAt(dr, 0);
                        //ddProject.DataTextField = "ProjectName";
                        //ddProject.DataValueField = "ProjectName";
                        //ddProject.DataSource = ds.Tables[5];
                        //ddProject.DataBind();

                        //DataRow dr1 = ds.Tables[1].NewRow();
                        //dr1.ItemArray = new object[] { 0, "--Select User--" };
                        //ds.Tables[1].Rows.InsertAt(dr1, 0);
                        //ddAssignedUser.DataTextField = "Username";
                        //ddAssignedUser.DataValueField = "Userid";
                        //ddAssignedUser.DataSource = ds.Tables[1];
                        //ddAssignedUser.DataBind();

                        DataRow dr2 = ds.Tables[0].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select Area--" };
                        ds.Tables[0].Rows.InsertAt(dr2, 0);
                        //ddArea.DataTextField = "AreaName";
                        //ddArea.DataValueField = "ID";
                        //ddArea.DataSource = ds.Tables[0];
                        //ddArea.DataBind();

                        DataRow dr3 = ds.Tables[1].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select Task--" };
                        ds.Tables[1].Rows.InsertAt(dr3, 0);
                        ddTask.DataTextField = "TaskName";
                        ddTask.DataValueField = "ID";
                        ddTask.DataSource = ds.Tables[1];
                        ddTask.DataBind();

                        DataRow dr4 = ds.Tables[2].NewRow();
                        dr4.ItemArray = new object[] { 0, "--Select Task Type--" };
                        ds.Tables[2].Rows.InsertAt(dr4, 0);
                        ddTaskType.DataTextField = "TaskTypeName";
                        ddTaskType.DataValueField = "ID";
                        ddTaskType.DataSource = ds.Tables[2];
                        ddTaskType.DataBind();

                        DataRow dr5 = ds.Tables[3].NewRow();
                        dr5.ItemArray = new object[] { 0, "--Select Status--" };
                        ds.Tables[3].Rows.InsertAt(dr5, 0);
                        ddStatus.DataTextField = "StatusName";
                        ddStatus.DataValueField = "ID";
                        ddStatus.DataSource = ds.Tables[3];
                        ddStatus.DataBind();

                        ViewState["CurrentTable"] = ds.Tables[4];
                        gv.DataSource = ds.Tables[4];
                        gv.DataBind();


                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string valtasktype = ddTaskType.SelectedItem.ToString();//Others
                if ("Others" != ddTaskType.SelectedItem.ToString())
                {
                    Validation();
                    string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                    if (validt != "Error")
                    {
                        //if(btn)
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("SP_TSTimesheet_Insert", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (!string.IsNullOrEmpty(ViewState["ID"].ToString()))
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ViewState["ID"].ToString());
                            else
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["USERID"].ToString();

                            if (!string.IsNullOrEmpty(txtFromDate.Text.ToString()))
                           //if(txtFromDate.Enabled== false)
                            {
                                cmd.Parameters.Add("@PlannedFromdate", SqlDbType.DateTime).Value = txtFromDate.Text;
                                cmd.Parameters.Add("@PlannedTodate", SqlDbType.DateTime).Value = txtTodate.Text;
                                cmd.Parameters.Add("@PlannedHours", SqlDbType.VarChar).Value = txtPlannedHours.Text;
                            }// dtpDate.Text;
                            else
                            {
                                cmd.Parameters.Add("@PlannedFromdate", SqlDbType.DateTime).Value = "1900-01-01"; 
                                cmd.Parameters.Add("@PlannedTodate", SqlDbType.DateTime).Value = "1900-01-01"; 
                                cmd.Parameters.Add("@PlannedHours", SqlDbType.VarChar).Value = "0";
                            }
                            cmd.Parameters.Add("@Project", SqlDbType.VarChar).Value = txtproject.Text;//ddProject.SelectedValue;
                            cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = txtProduct.Text;
                            cmd.Parameters.Add("@capacity", SqlDbType.VarChar).Value = txtCapacity.Text;
                            cmd.Parameters.Add("@speed", SqlDbType.VarChar).Value = txtSpeed.Text;
                            cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = txtArea.Text;
                            cmd.Parameters.Add("@Task", SqlDbType.VarChar).Value = ddTask.SelectedValue;
                            cmd.Parameters.Add("@TaskType", SqlDbType.VarChar).Value = ddTaskType.SelectedValue;
                            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                            cmd.Parameters.Add("@ActualFromdate", SqlDbType.DateTime).Value = txtActualStart.Text;
                            cmd.Parameters.Add("@ActualTodate", SqlDbType.DateTime).Value = txtActualEnd.Text;
                            cmd.Parameters.Add("@ActualHours", SqlDbType.VarChar).Value = txtActualHours.Text;// dtpDate.Text;
                            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarks.Text;
                            cmd.Parameters.Add("@WorkEnvt", SqlDbType.VarChar).Value = ddWorkingEnvironment.SelectedValue;
                            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            //cmd.Parameters.Add("@Modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                            if (!string.IsNullOrEmpty(ViewState["TSID"].ToString()))
                                cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = int.Parse(ViewState["TSID"].ToString());
                            else
                                cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = 0;
                            //cmd.Parameters.Add("", SqlDbType.VarChar).Value = ddCertstatus.SelectedValue;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    //FetchInitializeDetails();
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = " Created successfully";
                                    ViewState["CurrentTable"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    clear();
                                    btnSave.Enabled = false;
                                    //Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                }
                                else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                                {
                                    btnSave.Text = "Save";
                                    //FetchInitializeDetails();
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = "Update  successfully";
                                    ViewState["CurrentTable"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    //ViewState["ID"] = "";
                                    clear();
                                    btnSave.Enabled = false;
                                    ////Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                }
                            }

                        }
                    }
                }
                else
                {
                    //////////////////////////////////
                    if (txtActualStart.Text.Length > 4)
                    {
                        DateTime startDate = Convert.ToDateTime(txtActualStart.Text);
                        DateTime endDate = Convert.ToDateTime(txtActualEnd.Text);
                        //Calculate the number of days between the two dates
                        int totalDays = (int)(endDate - startDate).TotalDays;
                        int businessDays = 0;

                        for (int i = 0; i <= totalDays; i++)
                        {
                            DateTime currentDate = startDate.AddDays(i);
                            //if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                            if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                            {
                                businessDays++;
                            }
                        }
                        ////////////////////////
                        // TimeSpan duration = endDate - startDate;
                        int numberOfDays = businessDays;
                        //txtActualHours.Text = (numberOfDays * 9).ToString();
                        ViewState["Totalhours"] = txtActualHours.Text;
                    }
                    /////////////////////////////////////////////////////////////////
                    string input = txtActualHours.Text;
                    bool isValid = int.TryParse(input, out int intValue) || float.TryParse(input, out float floatValue);
                    string acthours = txtActualHours.Text.ToString();
                    float valfloathrs = float.Parse(acthours);
                    string calhrs = ViewState["Totalhours"].ToString();
                    float valcalhrs = float.Parse(calhrs);
                    if ((txtActualHours.Text == string.Empty) || (isValid == false) || (valfloathrs > valcalhrs) )
                    {
                        lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please enter valid Actual hours";
                        ViewState["Error"] = "Error";
                    }
                    else
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("SP_TSTimesheet_Insert", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (!string.IsNullOrEmpty(ViewState["ID"].ToString()))
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ViewState["ID"].ToString());
                            else
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@PlannedFromdate", SqlDbType.DateTime).Value = "1900-01-01";
                            cmd.Parameters.Add("@PlannedTodate", SqlDbType.DateTime).Value = "1900-01-01";
                            cmd.Parameters.Add("@PlannedHours", SqlDbType.VarChar).Value = "0";// dtpDate.Text;
                            cmd.Parameters.Add("@Project", SqlDbType.VarChar).Value = "";//ddProject.SelectedValue;
                            cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@capacity", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@speed", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = "";
                            cmd.Parameters.Add("@Task", SqlDbType.VarChar).Value = ddTask.SelectedValue;
                            cmd.Parameters.Add("@TaskType", SqlDbType.VarChar).Value = ddTaskType.SelectedValue;
                            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                            cmd.Parameters.Add("@ActualFromdate", SqlDbType.DateTime).Value = txtActualStart.Text;
                            cmd.Parameters.Add("@ActualTodate", SqlDbType.DateTime).Value = txtActualEnd.Text;
                            cmd.Parameters.Add("@ActualHours", SqlDbType.VarChar).Value = txtActualHours.Text;// dtpDate.Text;
                            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarks.Text;
                            //cmd.Parameters.Add("@Assignedby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@WorkEnvt", SqlDbType.VarChar).Value = ddWorkingEnvironment.SelectedValue;
                            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            //cmd.Parameters.Add("@Modifiedby", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  
                            if (!string.IsNullOrEmpty(ViewState["TSID"].ToString()))
                                cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = int.Parse(ViewState["TSID"].ToString());
                            else
                                cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = 0;
                            //cmd.Parameters.Add("", SqlDbType.VarChar).Value = ddCertstatus.SelectedValue;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    //FetchInitializeDetails();
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = " Created successfully";
                                    ViewState["CurrentTable"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    clear();
                                    btnSave.Enabled = false;
                                    //Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                }
                                else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                                {
                                    btnSave.Text = "Save";
                                    //FetchInitializeDetails();
                                    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    lblmsg.Text = "Update  successfully";
                                    ViewState["CurrentTable"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                    //ViewState["ID"] = "";
                                    clear();
                                    btnSave.Enabled = false;
                                    ////Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                }
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
        protected void btnTodayTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSTimeSheetToday", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@TS", SqlDbType.Int).Value = 2;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["CurrentTable"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnAllTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSTimeSheetToday", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@TS", SqlDbType.Int).Value = 1;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["CurrentTable"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnPendingTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSTimeSheetToday", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    cmd.Parameters.Add("@TS", SqlDbType.Int).Value = 3;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["CurrentTable"] = ds.Tables[0];
                        gv.DataSource = ds.Tables[0];
                        gv.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;

                //if ((txtFromDate.Text == string.Empty))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Begin date should not be empty";
                //    ViewState["Error"] = "Error";
                //}
                //if (txtTodate.Text == string.Empty)
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* End date should not be empty";
                //    ViewState["Error"] = "Error";
                //}
                //if (txtPlannedHours.Text == "0")
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please enter 'Planned hours' not be empty";
                //    ViewState["Error"] = "Error";
                //}
                if (ddStatus.SelectedIndex == 3)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please change status";
                    ViewState["Error"] = "Error";
                }
                //if (ddArea.SelectedIndex == 0)
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Area";
                //    ViewState["Error"] = "Error";
                //}
                if (ddTask.SelectedIndex == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Task";
                    ViewState["Error"] = "Error";
                }
                if (ddTaskType.SelectedIndex == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Task Type";
                    ViewState["Error"] = "Error";
                }
                if (ddStatus.SelectedIndex == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Status";
                    ViewState["Error"] = "Error";
                }
                if (txtActualStart.Text == string.Empty)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Actual start date";
                    ViewState["Error"] = "Error";
                }
                else if (Convert.ToDateTime(txtActualStart.Text) < Convert.ToDateTime(txtFromDate.Text))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Actual start date should not be less than planned date";
                    ViewState["Error"] = "Error";
                }
                if (txtActualEnd.Text == string.Empty)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Actual end date";
                    ViewState["Error"] = "Error";
                }
                if ((txtActualHours.Text == string.Empty) || (int.Parse(txtActualHours.Text) == 0) || (int.Parse(txtActualHours.Text) > 10))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Actual hours should be greater than 0 and less than 10";
                    ViewState["Error"] = "Error";
                }
                string input = txtActualHours.Text;
                bool isValid = int.TryParse(input, out int intValue) || float.TryParse(input, out float floatValue);

                if (isValid)
                {
                    // The input is a valid integer or float
                    // Perform further actions here
                }
                else
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Actual hours should be in this format 1 or 1.25 or 1.5 or 1.75";
                    ViewState["Error"] = "Error";
                }

               // string input = txtActualHours.Text;
               // bool isValid = int.TryParse(input, out int intValue) || float.TryParse(input, out float floatValue);
                string acthours = txtActualHours.Text.ToString();
                float valfloathrs = float.Parse(acthours);
                string calhrs = ViewState["Totalhours"].ToString();
                float valcalhrs = float.Parse(calhrs);
                //if ((valfloathrs <= valcalhrs))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please enter valid Actual hours";
                //    ViewState["Error"] = "Error";
                //}

            }
            catch (Exception ex)
            {

            }
        }
        public void clear()
        {
            try
            {
                lblmsg.Text = "";
                txtFromDate.Text = "";
                txtTodate.Text = "";
                txtPlannedHours.Text = "0";
                txtproject.Text = "";
                txtProduct.Text = "";
                txtCapacity.Text = "0";
                txtSpeed.Text = "0";
                txtArea.Text = "";
                ddTask.SelectedIndex = 0;
                ddTaskType.SelectedIndex = 0;
                txtRemarks.Text = "";
                ddStatus.SelectedIndex = 1;
                txtActualStart.Text = "";
                txtActualEnd.Text = "";
                txtActualHours.Text = "0";
                ViewState["ID"] = "";
                ViewState["TSID"] = "";
                txtproject.Enabled = true;
                txtProduct.Enabled = true;
                txtArea.Enabled = true;
                ddTask.Enabled = true;
                txtPlannedHours.Enabled = true;
                txtFromDate.Enabled = true;
                txtTodate.Enabled = true;
                txtCapacity.Enabled = true;
                txtSpeed.Enabled = true;
                ddTaskType.Enabled = true;
                ViewState["Totalhours"] = "0";
                btnSave.Enabled = true;
                ddWorkingEnvironment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                FetchInitializeDetails();
                //lblmsg.Text = "";
                ////ddAssignedUser.SelectedIndex = 0;
                //txtFromDate.Text = "";
                //txtTodate.Text = "";
                //txtPlannedHours.Text = "0";
                //ddProject.SelectedIndex = 0;
                //txtProduct.Text = "";
                //txtCapacity.Text = "0";
                //txtSpeed.Text = "0";
                //ddArea.SelectedIndex = 0;
                //ddTask.SelectedIndex = 0;
                //ddTaskType.SelectedIndex = 0;
                //txtRemarks.Text = "";
                //ddStatus.SelectedIndex = 0;
                //txtActualStart.Text = "";
                //txtActualEnd.Text = "";
                //txtActualHours.Text = "0";
            }
            catch (Exception ex)
            {

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
                txtActualStart.Text = FromCal.SelectedDate.ToShortDateString();
                FromCal.SelectedDates.Clear();
                if ((ddTaskType.SelectedItem.ToString() != "Others") && (txtFromDate.Text.Length > 6))
                {
                    if (ViewState["Actualstartdate"].ToString().Length > 6)
                    {
                        if (Convert.ToDateTime(FromCal.SelectedDate.ToShortDateString()) < Convert.ToDateTime(ViewState["Actualstartdate"].ToString()))
                        {
                            txtActualStart.Text = DateTime.Now.ToShortDateString();
                            lblmsg.Text = "Actual start date should not be lesser than earlier start date";
                        }
                    }
                }
                //txtActualEnd.Text = FromCal.SelectedDate.ToShortDateString();
                FromCal.Visible = false;
                txtActualEnd.Text = "";
                txtActualHours.Text = "0";                
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
                txtActualEnd.Text = Tocal.SelectedDate.ToShortDateString();
                Tocal.Visible = false;
                Tocal.SelectedDates.Clear();
                if (txtActualStart.Text.Length > 8)
                {
                    DateTime date1 = Convert.ToDateTime(txtActualStart.Text);
                    DateTime date2 = Convert.ToDateTime(txtActualEnd.Text);
                    if (date1 > date2)
                    {
                        lblmsg.Text = "Actual End date is greater than or equal Actual start date";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        //txtPlannedHours.Text = "9";
                    }
                    //else if (date1 == date2)
                    //{
                    //    txtPlannedHours.Text = "9";
                    //}
                }

                if (txtActualStart.Text.Length > 4)
                {
                    DateTime startDate = Convert.ToDateTime(txtActualStart.Text);
                    DateTime endDate = Convert.ToDateTime(txtActualEnd.Text);
                    //Calculate the number of days between the two dates
                    int totalDays = (int)(endDate - startDate).TotalDays;
                    int businessDays = 0;

                    for (int i = 0; i <= totalDays; i++)
                    {
                        DateTime currentDate = startDate.AddDays(i);

                        //if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                        if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            businessDays++;
                        }
                    }
                    ////////////////////////
                    // TimeSpan duration = endDate - startDate;
                    int numberOfDays = businessDays;
                    txtActualHours.Text = (numberOfDays * 9).ToString();
                    ViewState["Totalhours"] = txtActualHours.Text;
                }

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
                if (e.Day.Date > DateTime.Now.Date)
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
        protected void ddTaskTypeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string vArea = "Others";
                if (vArea.Contains(ddTaskType.SelectedItem.ToString()))
                {
                    //lblmsg.Text = "";
                    txtproject.Enabled = false;
                    txtProduct.Enabled = false;
                    txtArea.Enabled = false;
                    //ddTask.Enabled = false;
                    txtPlannedHours.Enabled = false;
                    txtFromDate.Enabled = false;
                    txtTodate.Enabled = false;
                    txtCapacity.Enabled = false;
                    txtSpeed.Enabled = false;
                }
                //else
                //{
                //    if (string.IsNullOrEmpty(txtArea.Text))
                //    {
                //        txtArea.Text = ddArea.SelectedItem.ToString();
                //        //  vArea = ddArea.SelectedValue;
                //        ViewState["vArea"] = ddArea.SelectedValue;
                //    }
                //    else
                //    {
                //        txtArea.Text = txtArea.Text + "," + ddArea.SelectedItem.ToString();
                //        //  vArea = vArea + "," + ddArea.SelectedValue;
                //        ViewState["vArea"] = ViewState["vArea"].ToString() + "," + ddArea.SelectedValue;
                //    }
                //}

            }
            catch (Exception ex)
            {

            }
        }
        protected void FromCalendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                if (e.Day.Date > DateTime.Now.Date)
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
        public void FetchSelectDetails(string ID, string actualdt)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSTimesheet_Select", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    if (!string.IsNullOrEmpty(actualdt))
                        cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = actualdt;
                    else
                        cmd.Parameters.Add("@TSID", SqlDbType.Int).Value = 0;//Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["Gridselect"] = ds.Tables[0];
                        //ddAssignedUser.SelectedValue = ds.Tables[0].Rows[0]["UserId"].ToString();
                        txtFromDate.Text = ds.Tables[0].Rows[0]["Fromdate"].ToString();
                        txtTodate.Text = ds.Tables[0].Rows[0]["Todate"].ToString();
                        txtPlannedHours.Text = ds.Tables[0].Rows[0]["PlannedHours"].ToString();
                        // ddProject.SelectedValue
                        txtproject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                        txtProduct.Text = ds.Tables[0].Rows[0]["Product"].ToString();
                        txtCapacity.Text = ds.Tables[0].Rows[0]["capacity"].ToString();
                        txtSpeed.Text = ds.Tables[0].Rows[0]["speed"].ToString();
                        // ddArea.SelectedValue
                        txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                        ddTask.SelectedValue = ds.Tables[0].Rows[0]["Task"].ToString();
                        ddTaskType.SelectedValue = ds.Tables[0].Rows[0]["TaskType"].ToString();
                        ddStatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
                        if(ds.Tables[0].Rows[0]["status"].ToString() == "2")
                        {
                            btnSave.Enabled = false;
                        }
                        txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                        if (ds.Tables[0].Rows[0]["ActualFromdate"].ToString().Length > 5)
                        {
                            txtActualStart.Text = ds.Tables[0].Rows[0]["ActualFromdate"].ToString();
                            txtActualEnd.Text = ds.Tables[0].Rows[0]["ActualTodate"].ToString();
                        }
                        else
                        {
                            txtActualStart.Text = DateTime.Now.ToShortDateString().ToString();// ds.Tables[0].Rows[0]["ActualFromdate"].ToString();
                            txtActualEnd.Text = DateTime.Now.ToShortDateString().ToString();//ds.Tables[0].Rows[0]["ActualTodate"].ToString();
                        }
                        txtActualHours.Text = ds.Tables[0].Rows[0]["ActualHours"].ToString();
                        ViewState["Actualstartdate"] = ds.Tables[0].Rows[0]["ActualFromdate"].ToString();
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
            try
            {
                ViewState["ID"] = gv.SelectedRow.Cells[1].Text;
                ViewState["TSID"] = gv.SelectedRow.Cells[2].Text;
                //if (string.IsNullOrEmpty(gv.SelectedRow.Cells[15].Text.Trim()) || gv.SelectedRow.Cells[15].Text.Trim() == "&nbsp;")
                FetchSelectDetails(gv.SelectedRow.Cells[1].Text, gv.SelectedRow.Cells[2].Text);
                txtproject.Enabled = false;
                txtProduct.Enabled = false;
                txtCapacity.Enabled = false;
                txtSpeed.Enabled = false;
                txtArea.Enabled = false;
                ddTaskType.Enabled = false;
                ddTask.Enabled = false;
                txtFromDate.Enabled = false;
                txtTodate.Enabled = false;
                txtPlannedHours.Enabled = false;

                //else
                //FetchSelectDetails(gv.SelectedRow.Cells[1].Text, gv.SelectedRow.Cells[15].Text);
            }
            catch (Exception ex)
            {

            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //(DataTable)ViewState["DPT"];
                gv.PageIndex = e.NewPageIndex;
                gv.DataSource = (DataTable)ViewState["CurrentTable"];
                gv.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}