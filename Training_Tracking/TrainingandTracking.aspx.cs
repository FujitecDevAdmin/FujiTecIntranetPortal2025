using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using System.Net;
using System.Net.Mail;

namespace FujiTecIntranetPortal.Training_Tracking
{
    public partial class TrainingandTracking : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Calendar1.Visible = false;
                    Calendar2.Visible = false;
                    SetInitialRow();
                    FetchInitializeDetails();
                    ViewState["Update"] = "";
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
                    SqlCommand cmd = new SqlCommand("SP_TrianingTrackingMasterPageLoad", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select MainContractor--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddMainSubConName.DataTextField = "VendorName";
                        ddMainSubConName.DataValueField = "ID";
                        ddMainSubConName.DataSource = ds.Tables[0];
                        ddMainSubConName.DataBind();

                        DataRow dr1 = ds.Tables[1].NewRow();
                        dr1.ItemArray = new object[] { 0, "--Select Category--" };
                        ds.Tables[1].Rows.InsertAt(dr1, 0);
                        ddcategory.DataTextField = "DDCATEGORYDESC";
                        ddcategory.DataValueField = "DDCATEGORYCODE";
                        ddcategory.DataSource = ds.Tables[1];
                        ddcategory.DataBind();

                        DataRow dr2 = ds.Tables[3].NewRow();
                        dr2.ItemArray = new object[] { 0, "--Select SubCon-Employee--" };
                        ds.Tables[3].Rows.InsertAt(dr2, 0);
                        ddEmployeeName.DataTextField = "Emp_Name";
                        ddEmployeeName.DataValueField = "Emp_Id";
                        ddEmployeeName.DataSource = ds.Tables[3];
                        ddEmployeeName.DataBind();


                        DataRow dr3 = ds.Tables[4].NewRow();
                        dr3.ItemArray = new object[] { 0, "--Select State--" };
                        ds.Tables[4].Rows.InsertAt(dr3, 0);
                        ddState.DataTextField = "STATENAME";
                        ddState.DataValueField = "STATECODE";
                        ddState.DataSource = ds.Tables[4];
                        ddState.DataBind();

                        //ViewState["GV"] = ds.Tables[2];
                        //gv.DataSource = ds.Tables[2];
                        //gv.DataBind();
                        DataRow dr4 = ds.Tables[2].NewRow();
                        dr4.ItemArray = new object[] { 0, "--Select Training Type--" };
                        ds.Tables[2].Rows.InsertAt(dr4, 0);
                        ddTrainingName.DataTextField = "TRAININGNAME";
                        ddTrainingName.DataValueField = "TRAININGID";
                        ddTrainingName.DataSource = ds.Tables[2];
                        ddTrainingName.DataBind();

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
                Validation();
                string validt = ViewState["Error"] as string; //ViewState["Error"] = "Error"
                if (validt != "Error")
                {
                    Save();
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

                string rbtnagree, rbtnconstat, rbtnempstat = string.Empty;
                rbtnagree = rbtnagreement.SelectedValue.ToString();
                rbtnconstat = rbtnconStatus.SelectedValue.ToString();
                rbtnempstat = rbtnEmpstatus.SelectedValue.ToString();

                if ((ddMainSubConName.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Sub-Contractor Name";
                    ViewState["Error"] = "Error";
                }

                if ((ddEmployeeName.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Employee Name";
                    ViewState["Error"] = "Error";
                }

                if ((ddDepartment.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Department";
                    ViewState["Error"] = "Error";
                }

                if ((txtIDCardNo.Text == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Id card should not be empty ";
                    ViewState["Error"] = "Error";
                }

                if ((ddcategory.SelectedIndex == 0))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Employee category";
                    ViewState["Error"] = "Error";
                }
                //if ((rbtnagreement.SelectedValue.ToString() != "1") || (rbtnagreement.SelectedValue.ToString() != "2"))
                if ((rbtnagree == "") || (rbtnempstat == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Agreement Signed & in Live ";
                    ViewState["Error"] = "Error";
                }
                if ((rbtnconstat == "") || (rbtnconstat == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Contractor Status";
                    ViewState["Error"] = "Error";
                }
                if ((rbtnempstat == "") || (rbtnempstat == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Employee Status";
                    ViewState["Error"] = "Error";
                }

                if ((gv.Rows[0].Cells[1].Text.Trim() == "") || (gv.Rows[0].Cells[1].Text.Trim() == string.Empty))
                {
                    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please add atleast one 'Training Details'";
                    ViewState["Error"] = "Error";
                }
                //if ((rbtnagreement.c == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* State should not be empty ";
                //    ViewState["Error"] = "Error";
                //}
                //if ((ddVendorNo.SelectedIndex == 0))
                //{
                //    lblmsg.Text = lblmsg.Text + Environment.NewLine + "* Please select Vendor";
                //    ViewState["Error"] = "Error";
                //}

            }
            catch (Exception ex)
            {

            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtActualDate.Text = "";
                txtESINo.Text = "";
                txtIDCardNo.Text = "";
                txtLocation.Text = "";
                txtPhoneNo.Text = "";
                txtPlannedDate.Text = "";
                txtUNINO.Text = "";
                ddcategory.SelectedIndex = 0;
                ddEmployeeName.SelectedIndex = 0;
                ddMainSubConName.SelectedIndex = 0;
                ddEmployeeName.Enabled = true;
                ddMainSubConName.Enabled = true;
                ddNatureofWork.SelectedIndex = 0;
                ddState.SelectedIndex = 0;
                ddTrainingModule.SelectedIndex = 0;
                ddTrainingName.SelectedIndex = 0;
                ViewState["CurrentTable"] = "";
                ViewState["Update"] = "";
                ViewState["TrainingHDR"] = "";
                ViewState["Emp"] = "";
                ViewState["TNM"] = "";
                lblmsg.Text = "";
                gv.DataSource = null;
                gv.DataBind();
                SetInitialRow();
                rbtnagreement.SelectedValue = null;
                rbtnconStatus.SelectedValue = null;
                rbtnEmpstatus.SelectedValue = null;
                ddDepartment.SelectedIndex = 0;
                //sendmail();
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

        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtPlannedDate.Text = Calendar1.SelectedDate.ToShortDateString();
                // strRoomName =
                Calendar1.Visible = false;
                // FetchInitializeDetails();
                //SetInitialRow();
            }
            catch (Exception ex)
            { }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
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

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Calendar1.Visible)
                    Calendar1.Visible = false;
                else
                    Calendar1.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtActualDate.Text = Calendar2.SelectedDate.ToShortDateString();
                // strRoomName =
                Calendar2.Visible = false;
                // FetchInitializeDetails();
                //SetInitialRow();
            }
            catch (Exception ex)
            { }
        }
        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
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

            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (Calendar2.Visible)
                    Calendar2.Visible = false;
                else
                    Calendar2.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string module = ddTrainingModule.SelectedItem.ToString() + "~" + ddTrainingModule.SelectedValue;
                string training = ddTrainingName.SelectedItem.ToString() + "~" + ddTrainingName.SelectedValue;
                bool val = false;
                if (gv.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gv.Rows)
                    {
                        if ((module == row.Cells[1].Text) && (training == row.Cells[2].Text))
                        {
                            //AddNewRowToGrid();
                            val = true;
                        }
                        //else
                        //{
                        //    lblmsg.ForeColor = System.Drawing.Color.Red;
                        //    lblmsg.Text = "This is already added in the list";
                        //}

                    }
                    if (val == false)
                    {
                        AddNewRowToGrid();
                    }
                    else if (val == true)
                    {
                        val = false;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "It is already added in the list";
                    }
                }
                else
                    AddNewRowToGrid();

                txtActualDate.Text = "";

                txtPlannedDate.Text = "";

                ddTrainingModule.SelectedIndex = 0;
                ddTrainingName.SelectedIndex = 0;

                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void sendmail()
        {
            try
            {
                ////----------------------------

                string Body = "Dear Sir/Madam,<br /><br />";
                Body = Body + "<b>Branch Wise Summary - Pending units for GAD Request[Won to GAD Request]:</b><br />";
                //Body = Body + "<b>Note:</b><b style=\"font-family:Calibri, sans-serif;font-weight:bold;color:red;\"> The below summary of cost exclude labour and DLP reversal in both forecast and actual.</b> <br /><br />";
                //Body = Body + "<div>";
                Body = Body + "<table border=\"0\" style=\"width:500px;height: 200px; margin:2px;padding: 20px; border:1px solid #6cb5d9; border-collapse:collapse;box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.1);\">";
                Body = Body + "<thead style=\"margin:0;padding:0;background-color:#d9edf7;\">";
                Body = Body + "<tr><td colspan=6 align=center style=\"border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td colspan=3  style=\"border: 1px solid black; padding: 10px;\">Name:</td>";
                Body = Body + "<td colspan=3 style=\"border: 1px solid black; padding: 10px;\">ID:</td></tr>";
                Body = Body + "<tr><td colspan=3  style=\" border: 1px solid black; padding: 10px;\">Location:</td>";
                Body = Body + "<td colspan=3 style=\"border: 1px solid black; padding: 10px;\">Dept:</td></tr>";
                Body = Body + "<tr><td colspan=1 style=\"width:5%;border: 1px solid black; padding: 10px;\">S.No</td>";
                Body = Body + "<td colspan=1 style=\"width: 45 %; border: 1px solid black; padding: 10px; \">Process Details</td>";
                Body = Body + "<td colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Training Details</td>";
                Body = Body + " <td  colspan=2  style=\"width:25%; border: 1px solid black; padding: 10px;\">Certification Details</td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Date</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px; \">Trained By</td><td style=\"border: 1px solid black; padding: 10px;\">Date</td><td style=\"border: 1px solid black; padding: 10px;\">Certified by</td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\"></td><td style=\"border: 1px solid black; padding: 10px;\">Under Observation</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">1</td><td style=\"border: 1px solid black; padding: 10px;\">Scaffolding entry / exit certification </td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">2</td><td style=\"border: 1px solid black; padding: 10px;\">Hoisting</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">3</td><td style=\"border: 1px solid black; padding: 10px;\">Car Top Entry & Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">4</td><td style=\"border: 1px solid black; padding: 10px;\">PitEntry Exit</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                Body = Body + "<tr><td style=\"border: 1px solid black; padding: 10px;\">5</td><td style=\"border: 1px solid black; padding: 10px;\">LOTO</td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td>";
                Body = Body + "<td style=\"border: 1px solid black; padding: 10px;\"></td></tr>";
                // Body = Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + DateTime.Now.ToString() + "</td></tr>";
                // Body = Body + "<tr><td align=Right colspan=2 style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">Signature of Main Contractor</td></tr>";
                Body = //Body + "<td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + txtUNINO.Text.ToString() + "</td></tr>";
                //Body = Body + "<td align=center style=\"width:50%;margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif;font-size:14px;font-weight:bold;\">No.of Units</td>";
                Body = Body + "</tr></thead><tbody style=\"margin:0;padding:0;background-color:#fff;\">";

                //  string contractno = dt.Rows[0]["ContractNo"].ToString();
                //for (int i = 0; i < dt3.Rows.Count; i++)
                //{
                //    Body = Body + "<tr><td style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif; font-size:14px;font-weight:bold;\">" + dt3.Rows[i]["Branch"].ToString() + "</td>";
                //    Body = Body + "<td align=right style=\"margin:0;padding:5px 8px;border-bottom:1px solid #6cb5d9;border-right:1px solid #6cb5d9;font-family:Calibri, sans-serif;font-size:14px;font-weight:bold;\">" + dt3.Rows[i]["ProjCount"].ToString() + "</td></tr>";
                //}
                Body = Body + "</tbody></table><br />";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                    | SecurityProtocolType.Tls11
                                    | SecurityProtocolType.Tls12;
                using (MailMessage mail = new MailMessage())
                {
                    //mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "BookingListDetails.xlsx"));
                    mail.From = new MailAddress("ftecin-notification@fujitec.co.in", "MIS Report");
                    mail.To.Add("vinothkumar.s@in.fujitec.com");
                    mail.CC.Add("vinothkumar.s@in.fujitec.com");
                    mail.Bcc.Add("vinothkumar.s@in.fujitec.com");
                    mail.Subject = "ID Card";// "Delivery Dashboard - Status as on Date : " + MonthYear;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("ftecin-notification@fujitec.co.in", "HelpDesk@20$22");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void clr()
        {
            try
            {
                txtActualDate.Text = "";
                txtESINo.Text = "";
                txtIDCardNo.Text = "";
                txtLocation.Text = "";
                txtPhoneNo.Text = "";
                txtPlannedDate.Text = "";
                txtUNINO.Text = "";
                ddcategory.SelectedIndex = 0;
                ddNatureofWork.SelectedIndex = 0;
                ddState.SelectedIndex = 0;
                ddTrainingModule.SelectedIndex = 0;
                ddTrainingName.SelectedIndex = 0;
                ViewState["CurrentTable"] = "";
                ViewState["Update"] = "";
                ViewState["TrainingHDR"] = "";
                ViewState["Emp"] = "";
                ViewState["TNM"] = "";
                lblmsg.Text = "";
                gv.DataSource = null;
                gv.DataBind();
                SetInitialRow();
                rbtnagreement.SelectedValue = null;
                rbtnconStatus.SelectedValue = null;
                rbtnEmpstatus.SelectedValue = null;


            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void btnTrainingClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtActualDate.Text = "";

                txtPlannedDate.Text = "";

                ddTrainingModule.SelectedIndex = 0;
                ddTrainingName.SelectedIndex = 0;

                lblmsg.Text = "";
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["TrainingModule"] = ddTrainingModule.SelectedItem + "~" + ddTrainingModule.SelectedValue;
                drCurrentRow["TrainingName"] = ddTrainingName.SelectedItem + "~" + ddTrainingName.SelectedValue;
                drCurrentRow["PlannedDate"] = txtPlannedDate.Text;
                //string actualdatetext = "";
                if ((txtActualDate.Text == string.Empty) || (txtActualDate.Text == ""))
                {
                    txtActualDate.Text = "01/01/1900";
                }
                drCurrentRow["ActualDate"] = txtActualDate.Text;
                dtCurrentTable.Rows.Add(drCurrentRow);

                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtCurrentTable.Rows[i][1].ToString() == string.Empty)
                        dtCurrentTable.Rows[i].Delete();
                }
                dtCurrentTable.AcceptChanges();

                ViewState["CurrentTable"] = dtCurrentTable;


                gv.DataSource = dtCurrentTable;
                gv.DataBind();
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void AddNewRowToGridtest()

        {

            int rowIndex = 0;



            if (ViewState["CurrentTable"] != null)

            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)

                {

                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)

                    {

                        //extract the TextBox values

                        //TextBox box1 = (TextBox)gv.Rows[rowIndex].Cells[1].FindControl("TextBox1");

                        //TextBox box2 = (TextBox)gv.Rows[rowIndex].Cells[2].FindControl("TextBox2");

                        //TextBox box3 = (TextBox)gv.Rows[rowIndex].Cells[3].FindControl("TextBox3");



                        drCurrentRow = dtCurrentTable.NewRow();

                        // drCurrentRow["RowNumber"] = i + 1;



                        // dtCurrentTable.Rows[i - 1]["TrainingName"] = txtTraininigName.Text;

                        dtCurrentTable.Rows[i - 1]["PlannedDate"] = txtPlannedDate.Text;

                        dtCurrentTable.Rows[i - 1]["ActualDate"] = txtActualDate.Text;



                        rowIndex++;

                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;



                    gv.DataSource = dtCurrentTable;

                    gv.DataBind();

                }

            }

            else

            {

                Response.Write("ViewState is null");

            }



            //Set Previous Data on Postbacks

            SetPreviousData();

        }
        private void SetPreviousData()
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)

            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)

                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        //TextBox box1 = (TextBox)gv.Rows[rowIndex].Cells[1].FindControl("TextBox1");

                        //TextBox box2 = (TextBox)gv.Rows[rowIndex].Cells[2].FindControl("TextBox2");

                        //TextBox box3 = (TextBox)gv.Rows[rowIndex].Cells[3].FindControl("TextBox3");



                        //box1.Text = dt.Rows[i]["Column1"].ToString();

                        //box2.Text = dt.Rows[i]["Column2"].ToString();

                        //box3.Text = dt.Rows[i]["Column3"].ToString();



                        rowIndex++;

                    }

                }

            }

        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("TrainingModule", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingName", typeof(string)));
            dt.Columns.Add(new DataColumn("PlannedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ActualDate", typeof(string)));
            dr = dt.NewRow();
            dr["TrainingModule"] = "";
            dr["TrainingName"] = "";
            dr["PlannedDate"] = string.Empty;
            dr["ActualDate"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            gv.DataSource = dt;
            gv.DataBind();
        }
        public void FetchEmployeedetails(string contractorname)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FetchEmployeeDetails", sqlConnection);
                    cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = contractorname.Trim();
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select SubCon-Employee--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddEmployeeName.DataTextField = "Emp_Name";
                        ddEmployeeName.DataValueField = "Emp_Id";
                        ddEmployeeName.DataSource = ds.Tables[0];
                        ddEmployeeName.DataBind();

                        ViewState["Emp"] = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void ddMainSubConName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchEmployeedetails(ddMainSubConName.SelectedValue);
        }

        protected void ddEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                rbtnagreement.SelectedValue = null;
                rbtnconStatus.SelectedValue = null;
                rbtnEmpstatus.SelectedValue = null;
                txtIDCardNo.Text = "";
                gv.DataSource = null;
                gv.DataBind();
                SetInitialRow();

                FetchEmployeeTracking();
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Emp"];
                foreach (DataRow row in dt.Rows)
                {
                    if (row["EMP_ID"].ToString() == ddEmployeeName.SelectedValue.ToString())
                    {
                        ddcategory.SelectedValue = row["CATEGORY"].ToString();
                        txtESINo.Text = row["ESINO"].ToString();
                        txtUNINO.Text = row["UNINO"].ToString();
                        txtPhoneNo.Text = row["MOBILENO"].ToString();
                        txtLocation.Text = row["locationname"].ToString();
                        ddState.SelectedValue = row["Statecode"].ToString();
                        break;
                    }
                }
                txtIDCardNo.Text = ddEmployeeName.SelectedValue;
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        public void FetchEmployeeTracking()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FetchEmployeeTrackingDetails", sqlConnection);
                    cmd.Parameters.Add("@VENDORID", SqlDbType.VarChar).Value = ddMainSubConName.SelectedValue;
                    cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = ddEmployeeName.SelectedValue;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            ViewState["TrainingHDR"] = ds.Tables[1];
                            ViewState["CurrentTable"] = ds.Tables[2];
                            gv.DataSource = ds.Tables[2];
                            gv.DataBind();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow row in ds.Tables[1].Rows)
                                {
                                    txtIDCardNo.Text = row["IDcardNo"].ToString();
                                    rbtnagreement.SelectedValue = row["AgreementSigneInLive"].ToString();
                                    rbtnconStatus.SelectedValue = row["contractorstatus"].ToString();
                                    rbtnEmpstatus.SelectedValue = row["employeestatus"].ToString();
                                    ddNatureofWork.SelectedValue = row["NatureofWork"].ToString();
                                    ddDepartment.SelectedValue = row["Department"].ToString();
                                }
                                ViewState["CurrentTable"] = ds.Tables[2];
                                gv.DataSource = ds.Tables[2];
                                gv.DataBind();
                                ViewState["Update"] = "Update";
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

        protected void ddTrainingName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void FetchTrainingName(string TRAININGCATEGORY)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FetchTrainingDetails", sqlConnection);
                    cmd.Parameters.Add("@TRAININGCATEGORY", SqlDbType.VarChar).Value = TRAININGCATEGORY.Trim();
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = hdUserId.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.ItemArray = new object[] { 0, "--Select Training Module--" };
                        ds.Tables[0].Rows.InsertAt(dr, 0);
                        ddTrainingModule.DataTextField = "TRAININGNAME";
                        ddTrainingModule.DataValueField = "TRAININGID";
                        ddTrainingModule.DataSource = ds.Tables[0];
                        ddTrainingModule.DataBind();

                        ViewState["TNM"] = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
        protected void ddTrainingModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchTrainingName(ddTrainingName.SelectedValue);
        }

        private void Save()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string sp = "";
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        string Update = ViewState["Update"] as string;

                        if (Update == "Update")
                        {
                            sp = "SP_TRAININGANDTRACKINGHDR_UPDATE";
                        }
                        else
                        {
                            sp = "SP_TRAININGANDTRACKINGHDR_INSERT";
                        }
                        SqlCommand cmd = new SqlCommand(sp, sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ContractorName", SqlDbType.VarChar).Value = ddMainSubConName.SelectedValue;
                        cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar).Value = ddEmployeeName.SelectedValue;
                        cmd.Parameters.Add("@IDcardNo", SqlDbType.VarChar).Value = txtIDCardNo.Text;
                        cmd.Parameters.Add("@ESINo", SqlDbType.VarChar).Value = txtESINo.Text;
                        cmd.Parameters.Add("@UANNo", SqlDbType.VarChar).Value = txtUNINO.Text;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = ddcategory.SelectedValue;
                        cmd.Parameters.Add("@locationname", SqlDbType.VarChar).Value = txtLocation.Text;
                        cmd.Parameters.Add("@statecode", SqlDbType.VarChar).Value = ddState.SelectedValue;

                        cmd.Parameters.Add("@Phoneno", SqlDbType.VarChar).Value = txtPhoneNo.Text;
                        cmd.Parameters.Add("@AgreementSigneInLive", SqlDbType.Int).Value = int.Parse(rbtnagreement.SelectedValue);
                        cmd.Parameters.Add("@contractorstatus", SqlDbType.Int).Value = int.Parse(rbtnconStatus.SelectedValue);
                        cmd.Parameters.Add("@employeestatus", SqlDbType.Int).Value = int.Parse(rbtnEmpstatus.SelectedValue);
                        cmd.Parameters.Add("@NatureofWork", SqlDbType.VarChar).Value = ddNatureofWork.SelectedValue;
                        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddDepartment.SelectedValue;

                        cmd.Parameters.Add("@Createdon", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@Modifiedon", SqlDbType.DateTime).Value = DateTime.Now;

                        cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                        cmd.Parameters.Add("@MODIFIEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  @TRAININGNO
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                            {
                                ViewState["TrainingID"] = ds.Tables[0].Rows[0]["TM"].ToString();
                            }
                        }
                    }


                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {

                            SqlCommand cmd = new SqlCommand("SP_TRAININGANDTRACKINGDETAIL_INSERT", sqlConnection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@TrainingTrackingID", SqlDbType.VarChar).Value = ViewState["TrainingID"].ToString();
                            string[] splitmodule = gv.Rows[i].Cells[1].Text.Split('~');
                            string[] splittraining = gv.Rows[i].Cells[2].Text.Split('~');
                            string splitModuleId, splitModuleName, splitTrainingId, splitTrainingName = string.Empty;
                            splitModuleId = splitmodule[1].ToString();
                            splitModuleName = splitmodule[0].ToString();
                            splitTrainingId = splittraining[1].ToString();
                            splitTrainingName = splittraining[0].ToString();
                            cmd.Parameters.Add("@TrainingModule", SqlDbType.VarChar).Value = splitModuleName;
                            cmd.Parameters.Add("@TrainingName", SqlDbType.VarChar).Value = splitTrainingName;
                            cmd.Parameters.Add("@planneddate", SqlDbType.DateTime).Value = Convert.ToDateTime(gv.Rows[i].Cells[3].Text);
                            string actualdate = string.Empty;
                            if ((gv.Rows[i].Cells[4].Text.Trim() != "") || (gv.Rows[i].Cells[4].Text.Trim() != string.Empty))
                            {
                                actualdate = gv.Rows[i].Cells[4].Text;
                            }
                            else
                            {
                                actualdate = "01/01/1900";
                            }
                            cmd.Parameters.Add("@Actual", SqlDbType.DateTime).Value = Convert.ToDateTime(actualdate);
                            cmd.Parameters.Add("@TrainingModuleId", SqlDbType.VarChar).Value = splitModuleId;
                            cmd.Parameters.Add("@TrainingNameID", SqlDbType.VarChar).Value = splitTrainingId;

                            cmd.Parameters.Add("@Createdon", SqlDbType.DateTime).Value = DateTime.Now;
                            cmd.Parameters.Add("@Modifiedon", SqlDbType.DateTime).Value = DateTime.Now;

                            cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                            cmd.Parameters.Add("@MODIFIEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString(); //@ToDate date = null  @TRAININGNO
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                                {
                                    ViewState["TrainingID"] = ds.Tables[0].Rows[0]["TM"].ToString();
                                    if (sp == "SP_TRAININGANDTRACKINGHDR_UPDATE")
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Green;
                                        lblmsg.Text = "Training For " + ddEmployeeName.SelectedItem + " -- record Updated Successfully";
                                        ddEmployeeName.Enabled = false;
                                        ddMainSubConName.Enabled = false;
                                    }
                                    else
                                    {
                                        lblmsg.ForeColor = System.Drawing.Color.Green;
                                        lblmsg.Text = "Training For " + ddEmployeeName.SelectedItem + " -- record Created Successfully";
                                        ddEmployeeName.Enabled = false;
                                        ddMainSubConName.Enabled = false;
                                    }
                                }
                            }
                            // scope.Complete();
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

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)gv.Rows[e.RowIndex];
                string TrainingModule = (row.Cells[1].Controls[0] as TextBox).Text;
                string Trainingname = (row.Cells[2].Controls[0] as TextBox).Text;
                string PlannedDate = (row.Cells[3].Controls[0] as TextBox).Text;
                string Actual = (row.Cells[4].Controls[0] as TextBox).Text;
                DataTable dt = ViewState["CurrentTable"] as DataTable;
                dt.Rows[row.RowIndex]["TrainingModule"] = TrainingModule;
                dt.Rows[row.RowIndex]["TrainingName"] = Trainingname;
                dt.Rows[row.RowIndex]["PlannedDate"] = PlannedDate;
                dt.Rows[row.RowIndex]["ActualDate"] = Actual;
                ViewState["CurrentTable"] = dt;
                gv.EditIndex = -1;
                gv.DataSource = dt;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if ((ddMainSubConName.SelectedIndex != 0) & (ddEmployeeName.SelectedIndex != 0))
                {
                    // FetchEmployeeTracking();

                    DataTable dt = new DataTable();
                    dt = (DataTable)(ViewState["CurrentTable"]);
                    int count = dt.Rows.Count;
                    dt.Rows.RemoveAt(e.RowIndex);
                    gv.DataSource = dt;
                    gv.DataBind();
                    if (count <= 1)
                        SetInitialRow();
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }

        protected void OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gv.EditIndex = -1;
                if ((ddMainSubConName.SelectedIndex != 0) & (ddEmployeeName.SelectedIndex != 0))
                    FetchEmployeeTracking();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gv.EditIndex)
                {
                    (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gv.EditIndex = e.NewEditIndex;
                if ((ddMainSubConName.SelectedIndex != 0) & (ddEmployeeName.SelectedIndex != 0))
                    FetchEmployeeTracking();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }
    }
}