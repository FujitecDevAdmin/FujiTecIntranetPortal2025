using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FujiTecIntranetPortal.TIMESHEET
{
    public partial class HybridModel : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    btnSubmit.Enabled = true;
                    btnUpdate.Enabled = false;
                    FetchInitializeDetails();
                    //txtBookingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                    {
                        txtBookingDate.Text = parsedDate.ToString("yyyy-MM-dd");
                        Fetchdata(txtBookingDate.Text);
                    }
                    //string message = "Operation completed successfully!";
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "showMessage('" + message + "');", true);

                }
            }
            catch (Exception ex)
            {
                // lblmsg.Text = ex.Message;
            }
        }

        public void FetchInitializeDetails()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_hyb_pageload", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = (Session["USERID"] as string);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        txtEmployee.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                        txtDepartment.Text = ds.Tables[0].Rows[0]["Departmentname"].ToString();
                        txtTotalseats.Text = ds.Tables[0].Rows[0]["TotalSeats"].ToString();
                        //txtAvailableSeats.Text = ds.Tables[0].Rows[0]["AvailableSeats"].ToString();
                        //txtBookingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        ViewState["GV"] = ds.Tables[1];
                        gvBookings.DataSource = ds.Tables[1];
                        gvBookings.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Fetchdata(string data)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_hyb_Bookingdate", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Empid", SqlDbType.VarChar).Value = (Session["USERID"] as string);
                    cmd.Parameters.Add("@Bookingdate", SqlDbType.Date).Value = data;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null)
                    {
                        txtEmployee.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                        txtDepartment.Text = ds.Tables[0].Rows[0]["Departmentname"].ToString();
                        txtTotalseats.Text = ds.Tables[0].Rows[0]["TotalSeats"].ToString();
                        txtAvailableSeats.Text = ds.Tables[0].Rows[0]["AvailableSeats"].ToString();
                        //txtBookingDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                        lblmsg.Text = ds.Tables[0].Rows[0]["DepartmentId"].ToString();
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
                //  txtEmployee.Text = "";
                //  txtDepartment.Text = "";
                txtBookingDate.Text = DateTime.Now.ToString("dd-MM-yyyy"); // Set to today's date
                txtTotalseats.Text = "";
                txtAvailableSeats.Text = "";
                chkFood.Checked = false;
                chkTransport.Checked = false;
                lblmsg.Text = "";
                lblMessage.Text = "";
                btnSubmit.Enabled = true;
                btnUpdate.Enabled = false;
            }
            catch (Exception ex) { }
        }

        protected void txtBookingDate_TextChanged(object sender, EventArgs e)
        {
            DateTime selectedDate;
            if (DateTime.TryParse(txtBookingDate.Text, out selectedDate))
            {
                // Handle your logic here based on the new date
                Fetchdata(txtBookingDate.Text);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int employeeId = Convert.ToInt32((Session["USERID"] as string)); // Or get from dropdown/textbox
                int departmentId = Convert.ToInt32(lblmsg.Text);
                bool foodRequired = chkFood.Checked;
                bool transportRequired = chkTransport.Checked;
                DateTime bookingDate = DateTime.ParseExact(txtBookingDate.Text, "yyyy-MM-dd", null);
                //TimeSpan bookingTime = TimeSpan.Parse(txtBookingDate.Text); // Expecting HH:mm format

                //  string connStr = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_hyb_InsertBooking", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                        cmd.Parameters.AddWithValue("@FoodRequired", foodRequired);
                        cmd.Parameters.AddWithValue("@TransportRequire", transportRequired);
                        cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
                        // cmd.Parameters.AddWithValue("@BookingTime", bookingTime);
                        con.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "1")
                            {
                                FetchInitializeDetails();
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                lblMessage.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "-1")
                            {
                                FetchInitializeDetails();
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                            }
                            else if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "-2")
                            {
                                FetchInitializeDetails();
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["SelectedBookingID"] == null)
                {
                    lblMessage.Text = "Please select a booking to update.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                int bookingId = Convert.ToInt32(ViewState["SelectedBookingID"]);
                int employeeId = Convert.ToInt32((Session["USERID"] as string));
                int departmentId = Convert.ToInt32(lblmsg.Text);
                bool food = chkFood.Checked;
                bool transport = chkTransport.Checked;
                DateTime bookingDate = Convert.ToDateTime(txtBookingDate.Text);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_hyb_UpdateBooking", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookingID", bookingId);
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
                        cmd.Parameters.AddWithValue("@FoodRequired", food);
                        cmd.Parameters.AddWithValue("@TransportRequire", transport);
                        cmd.Parameters.AddWithValue("@BookingDate", bookingDate);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        try
                        {
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0]["StatusCode"].ToString() == "1")
                                {

                                    FetchInitializeDetails();
                                    lblMessage.ForeColor = System.Drawing.Color.Green;
                                    lblMessage.Text = ds.Tables[0].Rows[0]["Message"].ToString();
                                }

                                //ClearFields();
                                ViewState["SelectedBookingID"] = null;
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Message.Contains("No available seats"))
                            {
                                lblMessage.Text = ex.Message;
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                throw; // Unknown DB error
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = true;
            GridViewRow row = gvBookings.SelectedRow;

            // Assuming 1st column is Select button, then:
            string bookingId = row.Cells[1].Text;
            string employeeId = row.Cells[2].Text;
            string departmentId = row.Cells[3].Text;
            string food = row.Cells[4].Text;
            string transport = row.Cells[5].Text;
            string bookingDate = row.Cells[6].Text;
            
            ViewState["SelectedBookingID"] = bookingId;
            // Fill into fields
            //txtEmployee.Text = employeeId;
            // txtDepartment.Text = departmentId;
            //chkFood.Checked = food.ToLower() == "true";
            //chkTransport.Checked = transport.ToLower() == "true";
            chkFood.Checked = row.Cells[4].Text.Trim().ToLower() == "true" || row.Cells[4].Text.Trim() == "1";
            chkTransport.Checked = row.Cells[5].Text.Trim().ToLower() == "true" || row.Cells[5].Text.Trim() == "1";



            // Convert dd-MM-yyyy to yyyy-MM-dd for textbox
            DateTime parsedDate;
            if (DateTime.TryParseExact(bookingDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                txtBookingDate.Text = parsedDate.ToString("yyyy-MM-dd");
                Fetchdata(txtBookingDate.Text);
            }

            // Optional: store BookingID in ViewState for later updates
            ViewState["SelectedBookingID"] = bookingId;
        }
    }
}