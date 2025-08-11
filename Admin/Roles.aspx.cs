using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FujiTecIntranetPortal.Admin
{
    public partial class Roles : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PgLoad();
                    // string vi = DateTime.Now.ToString("yyyyMMddHHmmss");
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void ApproveReject(string userid)
        {
            // Iterate through the Products.Rows property
            try
            {
                int i = 0; bool selflag = false;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DGV"];
                foreach (GridViewRow row in DGV.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkRow = (row.Cells[3].FindControl("Chckselect") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string ID = dt.Rows[i]["SCREENID"].ToString();
                            SaveRec(ID, true, userid);
                            //if (ID.Length < 5)
                            //    ID = "0" + ID;
                            //string ApprovalFor = dt.Rows[i]["NEWSANDEVENTS"].ToString();
                            //string country = (row.Cells[2].FindControl("lblCountry") as Label).Text;
                            // ApproveNewsEvents(ApprovalFor, ID, ApprovalStatus, dt.Rows[i]["EVENTNAME"].ToString());
                            //initialize();
                            selflag = true;
                        }
                        else
                        {
                            string ID = dt.Rows[i]["SCREENID"].ToString();
                            SaveRec(ID, false, userid);
                        }
                        i++;
                    }
                }
                if (selflag == false)
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please select any record to Approve or Reject";
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
                //SaveRec("","");
                //DataTable dt = new DataTable();
                //dt = (DataTable)ViewState["Users"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                ApproveReject(ddUser.SelectedValue);
                //}
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
                    SqlCommand cmd = new SqlCommand("SP_RolePageLoad", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Users"] = ds.Tables[0];
                        ////////////////////////////////////////////////////

                        DataRow ddl = ds.Tables[0].NewRow();
                        ddl.ItemArray = new object[] { 0, "--Select--" };
                        ds.Tables[0].Rows.InsertAt(ddl, 0);

                        ddUser.DataTextField = "UserName";
                        ddUser.DataValueField = "UserID";
                        ddUser.DataSource = ds.Tables[0];
                        ddUser.DataBind();

                        ViewState["DGVFilter"] = ds.Tables[1];
                        //DGV.DataSource = ds.Tables[1];
                        //DGV.DataBind();

                        DataRow ddl1 = ds.Tables[1].NewRow();
                        ddl1.ItemArray = new object[] { 0, "--Select--" };
                        ds.Tables[1].Rows.InsertAt(ddl1, 0);

                        DropDownList1.DataTextField = "DISPLAYNAME";
                        DropDownList1.DataValueField = "PARENTID";
                        DropDownList1.DataSource = ds.Tables[1];
                        DropDownList1.DataBind();

                        ////////////////////////////////////////////////////
                    }

                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        private void SaveRec(string Screenid, bool Accessflag, string userid)
        {
            try
            {
                lblmsg.Text = "";
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_ACTIVITYROLEMAP_INSERT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = ddUser.SelectedValue;
                    cmd.Parameters.Add("@SCREENID", SqlDbType.VarChar).Value = Screenid;
                    cmd.Parameters.Add("@ACCESSRIGHTS", SqlDbType.Bit).Value = Accessflag;
                    cmd.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["USERID"].ToString();
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Access Granted";

                        }
                        else if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "UPDATE")
                        {
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Access Granted";

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
        private void LoadGrid(string Userid)
        {
            try
            {
                lblmsg.Text = "";
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_ROLEUSERLOAD", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Userid;
                    if (DropDownList1.SelectedIndex != 0)
                        cmd.Parameters.Add("@PARENTID", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
                    else
                        cmd.Parameters.Add("@PARENTID", SqlDbType.VarChar).Value = null;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    DA.Fill(ds);
                    //response.result = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //  ViewState["RoleUL"] = ds.Tables[0];
                        ////////////////////////////////////////////////////
                        ViewState["DGV"] = ds.Tables[0];
                        DGV.DataSource = ds.Tables[0];
                        DGV.DataBind();
                        ////////////////////////////////////////////////////
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string v = ds.Tables[0].Rows[i]["ACCESS"].ToString();
                            if (ds.Tables[0].Rows[i]["ACCESS"].ToString().ToUpper() == "true".ToUpper())
                            {
                                CheckBox cb = (CheckBox)DGV.Rows[i].FindControl("Chckselect");
                                if (cb != null)
                                    cb.Checked = true;
                            }
                            if (DropDownList1.SelectedIndex == 0)
                            {
                                if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Home".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.LightBlue;
                                }
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Application".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.Cyan;
                                }
                                //else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "ERP Reports".ToUpper())
                                //{
                                //    DGV.Rows[i].BackColor = System.Drawing.Color.Lavender;
                                //}
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Induction".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.LightBlue;
                                }
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Management".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.Cyan;
                                }
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Contacts".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.Lavender;
                                }
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "IT Support".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.LightBlue;
                                }
                                else if (ds.Tables[0].Rows[i]["MODULE"].ToString().ToUpper() == "Security".ToUpper())
                                {
                                    DGV.Rows[i].BackColor = System.Drawing.Color.LightBlue;
                                }
                            }
                        }
                        ////////////////////////////////////////////////////
                    }

                }
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
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Right;
                //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[1].VerticalAlign = VerticalAlign.Middle;


                //if (DGV.Rows[5].Cells[1].Text.ToString() == "Application")
                //{
                //    DGV.Rows[5].BackColor = System.Drawing.Color.Cyan;
                //}

                //}
                // }

                //if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Application")) == 50)
                //{
                //    e.Row.BackColor = System.Drawing.Color.Cyan;
                //}
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    CheckBox cb = new CheckBox();
                //    // cb.id = ... and other control setup
                //    // add your control here:
                //    e.Row.Cells[3].Controls.Add(cb);
                //}
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }


        }

        private void ToggleCheckState(bool checkState)
        {
            // Iterate through the Products.Rows property
            try
            {
                foreach (GridViewRow row in DGV.Rows)
                {
                    // Access the CheckBox
                    CheckBox cb = (CheckBox)row.FindControl("Chckselect");
                    if (cb != null)
                        cb.Checked = checkState;
                }
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleCheckState(true);
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }

        }

        protected void btnUnCheckAll_Click(object sender, EventArgs e)
        {
            ToggleCheckState(false);
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownList1.SelectedIndex = 0;
                ddUser.SelectedIndex = 0;
                lblmsg.Text = "";
                DGV.DataSource = null;
                DGV.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(ddUser.SelectedValue.ToString());
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGrid(ddUser.SelectedValue.ToString());
                //DataTable dt = new DataTable();
                //dt = (DataTable)ViewState["DGV"];
                ////DataRow[] rslt = dt.Select("PARENTID =" + DropDownList1.SelectedValue);
                ////DataView dataView = dt.DefaultView;
                ////if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
                ////{
                ////    dataView.RowFilter = "PARENTID = '" + DropDownList1.SelectedValue + "'";
                ////}
                //DGV.DataSource = dt;
                //DGV.DataBind();
            }
            catch (Exception ex)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

        protected void ddUser_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(strcon))
            //    {
            //        SqlCommand cmd = new SqlCommand("SP_RoleUserFilter", con);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = ddUser.Text;
            //        SqlDataAdapter DA = new SqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        con.Open();
            //        DA.Fill(ds);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            //  ViewState["RoleUL"] = ds.Tables[0];
            //            ////////////////////////////////////////////////////
            //            DataRow ddl = ds.Tables[0].NewRow();
            //            ddl.ItemArray = new object[] { 0, "--Select--" };
            //            ds.Tables[0].Rows.InsertAt(ddl, 0);

            //            ddUser.DataTextField = "UserName";
            //            ddUser.DataValueField = "UserID";
            //            ddUser.DataSource = ds.Tables[0];
            //            ddUser.DataBind();
            //            ////////////////////////////////////////////////////
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}