using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syncfusion.XlsIO.Implementation;
using System.Web.Services.Description;
using Syncfusion.JavaScript;
using System.Runtime.InteropServices.ComTypes;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class SchedulingScreen : System.Web.UI.Page
    {

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
                    ViewState["vArea"] = "";
                    //ViewState["Proj"] = "";
                    //ViewState["WrkUser"] = "";

                    ddStatus.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }
        //public static string[] GetAutoCompleteData(string prefix)
        //{
        //    // Perform database or any other data retrieval logic here
        //    // and return a list of matching values based on the prefix

        //    // Example data
        //    string[] autocompleteData = new string[]
        //    {
        //"Apple",
        //"Banana",
        //"Cherry",
        //"Grapes",
        //"Orange"
        //    };

        //    return autocompleteData.Where(item => item.StartsWith(prefix)).ToArray();
        //}
        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSSchedulingPageLoad", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { "Select Project", "--Select Project--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddProject.DataTextField = "ProjectName";
                        ddProject.DataValueField = "ProjectName";
                        ddProject.DataSource = ds.Tables[0];
                        ddProject.DataBind();
                        ViewState["Proj"] = ds.Tables[0];

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select User--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddAssignedUser.DataTextField = "Username";
                        ddAssignedUser.DataValueField = "Userid";
                        ddAssignedUser.DataSource = ds.Tables[1];
                        ddAssignedUser.DataBind();
                        ViewState["WrkUser"] = ds.Tables[1];

                        DataRow dr2 = ds.Tables[2].NewRow();
                        dr2.ItemArray = new object[] { 0, "Select Area" };
                        ds.Tables[2].Rows.InsertAt(dr2, 0);
                        ddArea.DataTextField = "AreaName";
                        ddArea.DataValueField = "ID";
                        ddArea.DataSource = ds.Tables[2];
                        ddArea.DataBind();

                        DataRow dr3 = ds.Tables[3].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select Task--" };
                        ds.Tables[3].Rows.InsertAt(dr3, 0);
                        ddTask.DataTextField = "TaskName";
                        ddTask.DataValueField = "ID";
                        ddTask.DataSource = ds.Tables[3];
                        ddTask.DataBind();

                        DataRow dr4 = ds.Tables[4].NewRow();
                        dr4.ItemArray = new object[] { 0, "--Select Task Type--" };
                        ds.Tables[4].Rows.InsertAt(dr4, 0);
                        ddTaskType.DataTextField = "TaskTypeName";
                        ddTaskType.DataValueField = "ID";
                        ddTaskType.DataSource = ds.Tables[4];
                        ddTaskType.DataBind();

                        DataRow dr5 = ds.Tables[5].NewRow();
                        dr5.ItemArray = new object[] { 0, "--Select Status--" };
                        ds.Tables[5].Rows.InsertAt(dr5, 0);
                        ddStatus.DataTextField = "StatusName";
                        ddStatus.DataValueField = "ID";
                        ddStatus.DataSource = ds.Tables[5];
                        ddStatus.DataBind();

                        ViewState["CurrentTable"] = ds.Tables[6];
                        gv.DataSource = ds.Tables[6];
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

        public void FetchSelectDetails(string ID, string actualdt)
        {
            try
            {
                lstProj.Items.Clear();
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_TSScheduling_Select", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                    if (!string.IsNullOrEmpty(actualdt))
                        cmd.Parameters.Add("@ActualFromdate", SqlDbType.Date).Value = Convert.ToDateTime(actualdt);
                    else
                        cmd.Parameters.Add("@ActualFromdate", SqlDbType.Date).Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        ViewState["Gridselect"] = ds.Tables[0];
                        if (ds.Tables[0].Rows[0]["UserId"].ToString().Length > 5)
                            ddAssignedUser.SelectedValue = ds.Tables[0].Rows[0]["UserId"].ToString().Substring(0, 6);
                        else
                            ddAssignedUser.SelectedValue = ds.Tables[0].Rows[0]["UserId"].ToString().Substring(0, 5);
                        txtFromDate.Text = ds.Tables[0].Rows[0]["Fromdate"].ToString();
                        txtTodate.Text = ds.Tables[0].Rows[0]["Todate"].ToString();
                        txtPlannedHours.Text = ds.Tables[0].Rows[0]["PlannedHours"].ToString();
                        //ddProject.SelectedValue 
                        string strlist = ds.Tables[0].Rows[0]["Project"].ToString();
                        string[] strarr = strlist.Split(',');
                        foreach (string str in strarr)
                        {
                            lstProj.Items.Add(str);
                                                       
                        }
                        txtProduct.Text = ds.Tables[0].Rows[0]["Product"].ToString();
                        txtCapacity.Text = ds.Tables[0].Rows[0]["capacity"].ToString();
                        txtSpeed.Text = ds.Tables[0].Rows[0]["speed"].ToString();
                        //ddArea.SelectedValue 
                        txtArea.Text = ds.Tables[0].Rows[0]["Area"].ToString();
                        ddTask.SelectedValue = ds.Tables[0].Rows[0]["Task"].ToString();
                        ddTaskType.SelectedValue = ds.Tables[0].Rows[0]["TaskType"].ToString();
                       // ddStatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
                        txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchuserWorkload()
        {
            try
            {
                string result = string.Empty;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_AllocateWork", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = (ddAssignedUser.SelectedValue);
                    //if (!string.IsNullOrEmpty(actualdt))
                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime(txtFromDate.Text);
                    //  else
                    cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(txtTodate.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        result = ds.Tables[0].Rows[0]["Code"].ToString();
                        ViewState["Result"] = result;
                        ViewState["AlcHours"] = ds.Tables[0].Rows[0]["AlcHours"].ToString();
                        lblmsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();
                        if (result == "2")
                        {
                            string val = ds.Tables[0].Rows[0]["AlcHours"].ToString();
                            if (val == "0")
                                txtPlannedHours.Text = "9";
                            else
                            {
                                txtPlannedHours.Text = val;
                                //txtPlannedHours.Enabled= false;
                            }
                           // ViewState["val"] = ds.Tables[0].Rows[0]["AlcHours"].ToString();
                            lblmsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                        }
                        // lblmsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();
                    }
                }
                // return result;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ddAssignedUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FetchEmployeedetails(ddMainSubConName.SelectedValue);
        }
        protected void ddArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                // string vArea = string.Empty;
                if (txtArea.Text.Contains(ddArea.SelectedItem.ToString()))
                {
                    lblmsg.Text = "Already this Area is added";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtArea.Text))
                    {
                        txtArea.Text = ddArea.SelectedItem.ToString();
                        //  vArea = ddArea.SelectedValue;
                        ViewState["vArea"] = ddArea.SelectedValue;
                    }
                    else
                    {
                        txtArea.Text = txtArea.Text + "," + ddArea.SelectedItem.ToString();
                        //  vArea = vArea + "," + ddArea.SelectedValue;
                        ViewState["vArea"] = ViewState["vArea"].ToString() + "," + ddArea.SelectedValue;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        protected void ddProj_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProduct.Text = "";
            txtSpeed.Text = "";
            txtCapacity.Text = "";
            lblmsg.Text = "";
            ////FetchEmployeedetails(ddMainSubConName.SelectedValue);
            //DataTable dt = (DataTable)ViewState["Proj"];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (ddProject.SelectedValue == dt.Rows[i]["ProjectName"].ToString())
            //    {
            //        txtProduct.Text = dt.Rows[i]["ProductName"].ToString();
            //        txtSpeed.Text = dt.Rows[i]["Speed"].ToString();
            //        txtCapacity.Text = dt.Rows[i]["Capacity (Kgs)"].ToString();
            //        //lstProj.Text = ddProject.SelectedValue.ToString();
            //        if (lstProj.Items.Count == 0)
            //        {
            //            ListItem selectedItem = ddProject.SelectedItem;
            //            // lstProj.Items.Add(selectedItem);
            //        }
            //        else
            //        {
            //            //foreach (ListItem item in lstProj.Items)
            //            //{
            //            //if (ddProject.SelectedItem.Text.Substring(0,8))
            //            if (lstProj.Items.Contains(ddProject.SelectedItem))
            //            {
            //                lblmsg.Text = "Already contains";
            //                lblmsg.ForeColor = System.Drawing.Color.Red;
            //            }
            //            else
            //            {
            //                int val = 0;
            //                var listBoxItems = lstProj.Items.Cast<object>().ToList();
            //                foreach (var vitem in listBoxItems) //foreach (var item in myListBox.Items)
            //                {
            //                    if (vitem.ToString().Substring(0, 8) == ddProject.SelectedValue.Substring(0, 8))
            //                    {
            //                        if (lstProj.Items.Contains(ddProject.SelectedItem))
            //                        {
            //                            lblmsg.Text = "Already contains";
            //                            lblmsg.ForeColor = System.Drawing.Color.Red;
            //                            val = 1;
            //                        }
            //                        //else
            //                        //{
            //                        //    ListItem selectedItem = ddProject.SelectedItem;
            //                        //    lstProj.Items.Add(selectedItem);
            //                        //}
            //                    }
            //                    else
            //                    {
            //                        val = 1;
            //                        lblmsg.Text = "Please select a unique ContractNo.";
            //                        lblmsg.ForeColor = System.Drawing.Color.Red;

            //                    }
            //                }
            //                if (val == 0)
            //                {
            //                    ListItem selectedItem = ddProject.SelectedItem;
            //                    // lstProj.Items.Add(selectedItem);
            //                }
            //            }
            //            //}
            //        }
            //        break;
            //    }
            //}
        }

        protected void GV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                ViewState["ID"] = gv.SelectedRow.Cells[1].Text;
                FetchSelectDetails(gv.SelectedRow.Cells[1].Text, "");
                //if (string.IsNullOrEmpty(gv.SelectedRow.Cells[15].Text.Trim()) || gv.SelectedRow.Cells[15].Text.Trim() == "&nbsp;")
                //    FetchSelectDetails(gv.SelectedRow.Cells[1].Text, DateTime.Now.ToShortDateString());

                //else
                //    FetchSelectDetails(gv.SelectedRow.Cells[1].Text, gv.SelectedRow.Cells[15].Text);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validation();
                string listpro = string.Empty;
                foreach (ListItem item in lstProj.Items)
                {
                    if (string.IsNullOrEmpty(listpro))
                        listpro = item.Text;
                    else
                        listpro = listpro + "," + item.Text;
                }
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"


                if (validt != "Error")
                {
                    //if(btn)

                    string workload = ViewState["Result"] as string;
                    if (string.IsNullOrEmpty(workload)) { workload = "0"; }
                    string hrs = ViewState["AlcHours"] as string;
                    if (string.IsNullOrEmpty(hrs)) { hrs = "0"; }
                    //if (hrs != string.Empty)
                    //{
                    //  //  decimal hours = Convert.ToDecimal(ViewState["AlcHours"] as string);

                    //}
                    
                    // decimal calhours = 
                    if ((int.Parse(workload) == 2) || (!string.IsNullOrEmpty(ViewState["ID"].ToString())))//&& (hours )
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("SP_TSScheduling_Insert", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (!string.IsNullOrEmpty(ViewState["ID"].ToString()))
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(ViewState["ID"].ToString());
                            else
                            {
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 0;
                                if (hrs != string.Empty)
                                {
                                    decimal alcHours = decimal.Parse(hrs); // Convert hrs to decimal

                                    if (alcHours >= decimal.Parse(txtPlannedHours.Text))
                                    {
                                        // Do nothing if alcHours is greater than or equal to txtPlannedHours.Text
                                    }
                                    else
                                    {
                                        txtPlannedHours.Text = alcHours.ToString(); // Set txtPlannedHours.Text to alcHours
                                    }
                                }
                            }
                            cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = (ddAssignedUser.SelectedValue);
                            cmd.Parameters.Add("@Fromdate", SqlDbType.DateTime).Value = txtFromDate.Text;
                            cmd.Parameters.Add("@Todate", SqlDbType.DateTime).Value = txtTodate.Text;
                            cmd.Parameters.Add("@PlannedHours", SqlDbType.VarChar).Value = txtPlannedHours.Text;// dtpDate.Text;
                            cmd.Parameters.Add("@Project", SqlDbType.VarChar).Value = listpro;// ddProject.SelectedValue;
                            cmd.Parameters.Add("@Product", SqlDbType.VarChar).Value = txtProduct.Text;
                            cmd.Parameters.Add("@capacity", SqlDbType.VarChar).Value = txtCapacity.Text;
                            cmd.Parameters.Add("@speed", SqlDbType.VarChar).Value = txtSpeed.Text;
                            cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = txtArea.Text;// ddArea.SelectedValue;
                            cmd.Parameters.Add("@Task", SqlDbType.VarChar).Value = ddTask.SelectedValue;
                            cmd.Parameters.Add("@TaskType", SqlDbType.VarChar).Value = ddTaskType.SelectedValue;
                            cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = ddStatus.SelectedValue;
                            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = txtRemarks.Text;
                            cmd.Parameters.Add("@Assignedby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@Createdby", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@AssigneduserId", SqlDbType.VarChar).Value = ddReassign.SelectedValue; //@ToDate date = null  

                            //cmd.Parameters.Add("", SqlDbType.VarChar).Value = ddCertstatus.SelectedValue;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    //FetchInitializeDetails();
                                    if (ds.Tables[0].Rows[0]["SCOPE_IDENTITY"].ToString() != "0")
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Green;
                                        lblmsg.Text = "Created successfully";
                                        ViewState["CurrentTable"] = ds.Tables[1];
                                        gv.DataSource = ds.Tables[1];
                                        gv.DataBind();
                                        btnSave.Enabled = false;
                                    }
                                    //else
                                    //{
                                    //    lblmsg.ForeColor = System.Drawing.Color.Green;
                                    //    lblmsg.Text = "This work was already allocated to Other user";// + ds.Tables[0].Rows[0]["UserID"].ToString();
                                    //    ViewState["CurrentTable"] = ds.Tables[1];
                                    //    gv.DataSource = ds.Tables[1];
                                    //    gv.DataBind();
                                    //}
                                    //Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                    ViewState["ID"] = "";
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
                                    ViewState["ID"] = "";
                                    btnSave.Enabled = false;
                                    ////Need to add function to get training details
                                    //getval();
                                    //sendmail();
                                }
                                else
                                {
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "This work was already allocated";// + ds.Tables[0].Rows[0]["UserID"].ToString();
                                    ViewState["CurrentTable"] = ds.Tables[1];
                                    gv.DataSource = ds.Tables[1];
                                    gv.DataBind();
                                }
                            }
                            //else
                            //{
                            //    //btnSave.Text = "Save";
                            //    //FetchInitializeDetails();
                            //    lblmsg.ForeColor = System.Drawing.Color.Red;
                            //    lblmsg.Text = "Work Load for this user crossed 9 hours.Already allocated work is " + ds.Tables[0].Rows[0]["Hours"].ToString() + " and now Planned hours is " + txtPlannedHours.Text;
                            //    ViewState["CurrentTable"] = ds.Tables[1];
                            //    gv.DataSource = ds.Tables[1];
                            //    gv.DataBind();
                            //    ViewState["ID"] = "";
                            //}
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtProjectFilter.Text.Length == 12)
                //{

                //    ListItem listItem = new ListItem(txtProjectFilter.Text, txtProjectFilter.Text);
                //    if (lstProj.Items.Contains(listItem))
                //    {
                //        lblmsg.Text = "Already contains";
                //    }


                //}
                //else if()
                //else
                //{
                //    //  lblmsg.Text = "Please Enter valid project number.";
                //    ListItem selectedItem = ddProject.SelectedItem;
                //    lstProj.Items.Add(selectedItem);
                //}


                //ListItem selectedItem = listItem;
                //lstProj.Items.Add(selectedItem);
                //lstProj.Items.Add(txtProjectFilter.Text);
                int val = 0;
                var listBoxItems = lstProj.Items.Cast<object>().ToList();
                foreach (var vitem in listBoxItems) //foreach (var item in myListBox.Items)
                {
                    if (vitem.ToString().Substring(0, 8) == ddProject.SelectedValue.Substring(0, 8))
                    {
                        if (lstProj.Items.Contains(ddProject.SelectedItem))
                        {
                            lblmsg.Text = "Already contains";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            val = 1;
                        }
                        //else
                        //{
                        //    ListItem selectedItem = ddProject.SelectedItem;
                        //    lstProj.Items.Add(selectedItem);
                        //}
                    }
                    else
                    {
                        val = 1;
                        lblmsg.Text = "Please select a unique ContractNo.";
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                if (val == 0)
                {
                    ListItem selectedItem = ddProject.SelectedItem;
                    lstProj.Items.Add(selectedItem);
                    DataTable dt = (DataTable)ViewState["Proj"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (ddProject.SelectedValue == dt.Rows[i]["ProjectName"].ToString())
                        {
                            txtProduct.Text = dt.Rows[i]["ProductName"].ToString();
                            txtSpeed.Text = dt.Rows[i]["Speed"].ToString();
                            txtCapacity.Text = dt.Rows[i]["Capacity (Kgs)"].ToString();
                            break;
                        }
                    }
                }


            }
            catch (Exception ex) { }
        }

        protected void DeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                lstProj.Items.RemoveAt(lstProj.Items.IndexOf(lstProj.SelectedItem));
            }
            catch (Exception ex) { }

        }

        protected void txtProjectFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtProjectFilter.Text;
            DataTable dt = (DataTable)ViewState["Proj"];
            DataTable filteredDataTable = GetFilteredOptions(filterText, dt);
            PopulateDropDownList(filteredDataTable);
        }
        protected void txtTodate_TextChanged(object sender, EventArgs e)
        {
            // Start and end dates
            if (txtFromDate.Text.Length > 4)
            {
                DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime endDate = Convert.ToDateTime(txtTodate.Text);

                // Calculate the number of days between the two dates
                TimeSpan duration = endDate - startDate;
                int numberOfDays = duration.Days;
                txtPlannedHours.Text = (numberOfDays * 9).ToString();
            }
        }

        private DataTable GetFilteredOptions(string filterText, DataTable dataTable)
        {
            DataRow[] filteredRows = dataTable.Select("ProjectName LIKE '%" + filterText + "%'");

            if (filteredRows.Length > 0)
            {
                DataTable filteredDataTable = dataTable.Clone();

                foreach (DataRow row in filteredRows)
                {
                    filteredDataTable.ImportRow(row);
                }

                return filteredDataTable;
            }
            else
            {
                return new DataTable();
            }
        }

        private void PopulateDropDownList(DataTable filteredDataTable)
        {
            ddProject.Items.Clear();
            foreach (DataRow row in filteredDataTable.Rows)
            {
                string text = row["ProjectName"].ToString(); // Replace "ColumnName" with the actual column name
                string value = row["ProjectName"].ToString(); // Replace "Value" with the actual column name

                ListItem item = new ListItem(text, value);
                ddProject.Items.Add(item);
            }
        }

        protected void txtAssignedFilter_TextChanged(object sender, EventArgs e)
        {
            string filterText = txtAssignedUser.Text;
            DataTable dt = (DataTable)ViewState["WrkUser"];
            DataTable filteredDataTable = GetFilteredUserOptions(filterText, dt);
            PopulateuserDropDownList(filteredDataTable);
        }

        private DataTable GetFilteredUserOptions(string filterText, DataTable dataTable)
        {
            //try
            //{
                DataRow[] filteredRows = dataTable.Select("Username LIKE '%" + filterText + "%'");

                if (filteredRows.Length > 0)
                {
                    DataTable filteredDataTable = dataTable.Clone();

                    foreach (DataRow row in filteredRows)
                    {
                        filteredDataTable.ImportRow(row);
                    }

                    return filteredDataTable;
                }
                else
                {
                    return new DataTable();
                }
            

        }

        private void PopulateuserDropDownList(DataTable filteredDataTable)
        {
            ddAssignedUser.Items.Clear();
            foreach (DataRow row in filteredDataTable.Rows)
            {
                string text = row["Username"].ToString(); // Replace "ColumnName" with the actual column name;
                string value = row["Userid"].ToString(); // Replace "Value" with the actual column name;
                ListItem item = new ListItem(text, value);
                ddAssignedUser.Items.Add(item);
            }
        }

        private void Validation()
        {
            try
            {
                lblmsg.Text = "";
                ViewState["Error"] = string.Empty;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                if ((ddAssignedUser.SelectedValue == "0"))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select User Name.";
                    ViewState["Error"] = "Error";
                }
                if ((txtFromDate.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Begin date should not be empty.";
                    ViewState["Error"] = "Error";
                }
                if (txtTodate.Text == string.Empty)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* End date should not be empty.";
                    ViewState["Error"] = "Error";
                }
                if (txtPlannedHours.Text == "0")
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please enter 'Planned hours' not be empty.";
                    ViewState["Error"] = "Error";
                }
                if (lstProj.Items.Count == 0)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Project.";
                    ViewState["Error"] = "Error";
                }
                if (txtArea.Text.Length < 2)
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Area.";
                    ViewState["Error"] = "Error";
                }
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
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                ddAssignedUser.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
                txtPlannedHours.Text = "0";
                ddProject.SelectedIndex = 0;
                txtProduct.Text = "";
                txtCapacity.Text = "0";
                txtSpeed.Text = "0";
                ddArea.SelectedIndex = 0;
                ddTask.SelectedIndex = 0;
                ddTaskType.SelectedIndex = 0;
                txtRemarks.Text = "";
                ddStatus.SelectedIndex = 1;
                ddReassign.SelectedIndex = 0;
                txtProjectFilter.Text = "";
                lstProj.Items.Clear();
                txtAssignedUser.Text = "";
                txtArea.Text = "";
                btnSave.Enabled = true;
                FetchInitializeDetails();
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddClear_Click(object sender, EventArgs e)
        {
            try
            {

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
                FromCal.SelectedDates.Clear();
                txtTodate.Text = "";
                txtPlannedHours.Text = "0".ToString();
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
                lblmsg.Text = "";
                txtTodate.Text = Tocal.SelectedDate.ToShortDateString();
                Tocal.Visible = false;
                Tocal.SelectedDates.Clear();

                if (txtFromDate.Text.Length > 8)
                {
                    DateTime date1 = Convert.ToDateTime(txtFromDate.Text);
                    DateTime date2 = Convert.ToDateTime(txtTodate.Text);
                    if (date1 > date2)
                    {
                        lblmsg.Text = "End date should be greater than or equal to start date";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        //txtPlannedHours.Text = "0";
                    }
                    else if (date1 == date2)
                    {
                        lblmsg.Text = "";
                        //  txtPlannedHours.Text = "9";
                    }
                }
                if (txtFromDate.Text.Length > 4)
                {
                    DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
                    DateTime endDate = Convert.ToDateTime(txtTodate.Text);
                    //Calculate the number of days between the two dates
                    int totalDays = (int)(endDate - startDate).TotalDays;
                    int businessDays = 0;

                    for (int i = 0; i <= totalDays; i++)
                    {
                        DateTime currentDate = startDate.AddDays(i);

                        if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                        {
                            businessDays++;
                        }
                    }
                    ////////////////////////
                    // TimeSpan duration = endDate - startDate;
                    int numberOfDays = businessDays;
                    txtPlannedHours.Text = (numberOfDays * 9).ToString();
                }
                FetchuserWorkload();
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
                // if (e.Day.Date < FromCal.SelectedDate)
                if (e.Day.Date < DateTime.Now.Date)
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
                if (e.Day.Date < DateTime.Now.Date)
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

    }
}