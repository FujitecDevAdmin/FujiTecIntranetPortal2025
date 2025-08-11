using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class Site : System.Web.UI.MasterPage
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        string struserid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(Session["USERID"] as string))
                {
                    lblusername.Text = Session["USERNAME"].ToString().ToUpper();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //DataRow ddl = ds.Tables[0].NewRow();
                    //ddl.ItemArray = new object[] { 0, " " };
                    //dt.Rows.InsertAt(ddl, 0);

                    //DDOption.DataTextField = "CountryName";

                    //DDOption.DataValueField = "CountryImage";

                    //DDOption.DataSource = dt;

                    //DDOption.DataBind();

                    if (!IsPostBack)
                    {
                        string sessionId = this.Session.SessionID;

                        PgLoad();
                        /////////////////////////////////////////////////////////
                        //MenuItem[] itemsToRemove = new MenuItem[Menu1.Items.Count];
                        //int i = 0;

                        //foreach (MenuItem item in Menu1.Items)
                        //{
                        //    itemsToRemove[i] = item;
                        //    i++;
                        //}
                        ////string v = "";
                        ////for (int j = 0; j < i; j++)
                        ////{
                        //string Roles = Session["ROLES"] as string;
                        //if (Roles != "Admin")
                        //    Menu1.Items.Remove(itemsToRemove[7]);
                        //}


                        //Menu1.FindItem("Application").ChildItems.Clear();
                        /////////////Test Purpose///////////////////////////////
                        //string vmuserid = Session["VMSUSERID"] as string;
                        //string mrbuserid = Session["MRBUSERID"] as string;
                        //string eqpbrkupuserid = Session["EQPBRKUPUSERID"] as string;
                        //struserid = Session["USERID"] as string;
                        //if (string.IsNullOrEmpty(vmuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/VMS"));
                        //if (string.IsNullOrEmpty(mrbuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/MRB"));
                        //if (string.IsNullOrEmpty(eqpbrkupuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/EBOM"));
                        //if (!string.IsNullOrEmpty(struserid))
                        //{
                        //    if ((struserid.Trim() == "00444") || (struserid.Trim() == "01321") || (struserid.Trim() == "02021") || (struserid.Trim() == "01259") || (struserid.Trim() == "00450") || (struserid.Trim() == "00243") || (struserid.Trim() == "00717") || (struserid.Trim() == "01339") || (struserid.Trim() == "01454") || (struserid.Trim() == "01867") || (struserid.Trim() == "01646") || (struserid.Trim() == "01649") || (struserid.Trim() == "02037") || (struserid.Trim() == "02026") || (struserid.Trim() == "01972"))
                        //    {
                        //    }
                        //    else
                        //        Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpEmpReg"));
                        //}
                        //else if (string.IsNullOrEmpty(mrbuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpEmpReg"));

                        //if (!string.IsNullOrEmpty(struserid))
                        //{
                        //    //if ((struserid.Trim() == "00444") || (struserid.Trim() == "01321") || (struserid.Trim() == "02021") || (struserid.Trim() == "01259") || (struserid.Trim() == "00450"))
                        //    if ((struserid.Trim() == "00444") || (struserid.Trim() == "01321") || (struserid.Trim() == "02021") || (struserid.Trim() == "01259") || (struserid.Trim() == "00450") || (struserid.Trim() == "00243") || (struserid.Trim() == "00717") || (struserid.Trim() == "01339") || (struserid.Trim() == "01454") || (struserid.Trim() == "01867") || (struserid.Trim() == "01646") || (struserid.Trim() == "01649") || (struserid.Trim() == "02037") || (struserid.Trim() == "02026") || (struserid.Trim() == "01972"))
                        //    {
                        //    }
                        //    else
                        //        Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpNE"));
                        //}
                        //else if (string.IsNullOrEmpty(mrbuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpNE"));

                        //if (!string.IsNullOrEmpty(struserid))
                        //{
                        //    if ((struserid.Trim() == "01283") || (struserid.Trim() == "02021") || (struserid.Trim() == "01259") || (struserid.Trim() == "00233") || (struserid.Trim() == "00004"))
                        //    {
                        //        // Menu1.FindItem("ErpMIS").ChildItems.Remove(Menu1.FindItem("ErpMIS/ERPInvtRpt"));
                        //    }
                        //    else //00240
                        //        Menu1.FindItem("ErpReports").ChildItems.Remove(Menu1.FindItem("ErpReports/ErpMIS"));
                        //}
                        //if (!string.IsNullOrEmpty(struserid))
                        //{
                        //    if ((struserid.Trim() == "00240") || (struserid.Trim() == "02021") || (struserid.Trim() == "01259") || (struserid.Trim() == "00450"))
                        //    {
                        //        // Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpAPP"));
                        //    }
                        //    else // 00240
                        //        Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpAPP"));
                        //}
                        /////////////Test Purpose///////////////////////////////
                        //else if(mrbuserid== '00444'))
                        //        Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpEmpReg"));
                        //if (string.IsNullOrEmpty(mrbuserid))
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/ErpEmpReg"));

                        //if (Session["VMSUSERID"].ToString() == null)
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/MSTT"));
                        //if (Session["VMSUSERID"].ToString() == null)
                        //    Menu1.FindItem("Application").ChildItems.Remove(Menu1.FindItem("Application/EBOM"));
                    }
                }
                else
                    Response.Redirect("~/LandingPage/LandingPage.aspx");


            }
            catch (Exception ex)
            { }
        }

        //private void PgLoad()
        //{
        //    try
        //    {
        //        struserid = Session["USERID"].ToString();
        //        using (SqlConnection con = new SqlConnection(strcon))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_SITEMASTERPAGELOAD", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = Session["USERID"].ToString();
        //            SqlDataAdapter DA = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            con.Open();
        //            DA.Fill(ds);
        //            //response.result = false;
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                ViewState["Users"] = ds.Tables[0];

        //                if (!string.IsNullOrEmpty(struserid))
        //                {
        //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                    {
        //                        string screenval = ds.Tables[0].Rows[i]["CVALUE"].ToString();
        //                        string Parentval = ds.Tables[0].Rows[i]["PValue"].ToString();
        //                        string SCREENTYPE = ds.Tables[0].Rows[i]["SCREENTYPE"].ToString();
        //                        if (SCREENTYPE != "0")
        //                        {
        //                            // if(Parentval == "ErpReports")
        //                            //   Menu1.FindItem(Parentval).ChildItems.Remove(Menu1.FindItem(Parentval + "/" + screenval));
        //                            if ((SCREENTYPE != "P") && (screenval != "MSTT") && (screenval != "ErpMIS"))
        //                                Menu1.FindItem(Parentval).ChildItems.Remove(Menu1.FindItem(Parentval + "/" + screenval));
        //                        }
        //                    }
        //                }

        //                if ((ds.Tables[0].Rows[0]["PValue"].ToString() == "0"))
        //                {
        //                    MenuItem[] itemsToRemove = new MenuItem[Menu1.Items.Count];
        //                    int i = 0;

        //                    foreach (MenuItem item in Menu1.Items)
        //                    {
        //                        itemsToRemove[i] = item;
        //                        i++;
        //                    }
        //                    for (int j = 1; j <= 7; j++)
        //                        Menu1.Items.Remove(itemsToRemove[j]);
        //                }

        //                //ViewState["DGV"] = ds.Tables[1];
        //                //DGV.DataSource = ds.Tables[1];
        //                //DGV.DataBind();

        //                ////////////////////////////////////////////////////
        //            }
        //            //else
        //            //{
        //            //    if (!string.IsNullOrEmpty(struserid))
        //            //    {
        //            //        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //            //        {
        //            //            string screenval = ds.Tables[1].Rows[i]["CVALUE"].ToString();
        //            //            string Parentval = ds.Tables[1].Rows[i]["PValue"].ToString();
        //            //            string SCREENTYPE = ds.Tables[1].Rows[i]["SCREENTYPE"].ToString();
        //            //            if ((SCREENTYPE != "P") && (screenval!= "MSTT") && (screenval!= "ErpMIS"))
        //            //                Menu1.FindItem(Parentval).ChildItems.Remove(Menu1.FindItem(Parentval + "/" + screenval));



        //            //        }
        //            //    }

        //            //}

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void PgLoad()
        {
            try
            {
                struserid = Session["USERID"].ToString();
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_SITEMASTERPAGELOAD1", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = struserid;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    con.Open();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["Users"] = ds.Tables[0];

                        Menu1.Items.Clear(); // Clear existing items

                        // Group items by parent
                        var parentGroups = ds.Tables[0].AsEnumerable()
                            .GroupBy(row => row["PValue"].ToString());

                        foreach (var parentGroup in parentGroups)
                        {
                            string parentVal = parentGroup.Key;
                            MenuItem parentItem = new MenuItem(parentVal);

                            foreach (DataRow row in parentGroup)
                            {
                                string screenVal = row["CVALUE"].ToString();
                                string screenName = row["DISPLAYNAME"].ToString();
                                string screenType = row["SCREENTYPE"].ToString();

                                if (screenType == "P")
                                {
                                    parentItem = new MenuItem(screenName, "", "", screenVal);
                                }
                                else
                                {
                                    MenuItem childItem = new MenuItem(screenName, "", "", screenVal);
                                    parentItem.ChildItems.Add(childItem);
                                }
                            }

                            Menu1.Items.Add(parentItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally log ex
            }
        }


        private void ClearSession()
        {
            Session["USERNAME"] = null;
            Session["ROLES"] = null;
            Session["USERID"] = null;
            Session["DEPT"] = null;
            /////////
            Session["VMSUSERNAME"] = null;
            Session["VMSROLES"] = null;
            Session["VMSUSERID"] = null;
            Session["VMS"] = null;
            ////////////////
            Session["MRBUSERNAME"] = null;
            Session["MRBROLES"] = null;
            Session["MRBUSERID"] = null;
            Session["MRB"] = null;
            ////////////////
            Session["EQPBRKUPUSERNAME"] = null;
            Session["EQPBRKUPROLES"] = null;
            Session["EQPBRKUPUSERID"] = null;
            Session["EQPBRKUP"] = null;
            Session["Loc"] = null;
            Session["TTAPPID"] = null;
            ////////////////
            // Menu1.Items.Remove(Menu1.FindItem(@"APPLICATION\EBOM").Enabled = false);
            // Menu1.FindItem(@"APPLICATION\EBOM").Enabled = false;
            // Menu1.FindItem(@"APPLICATION\EBOM").Enabled = false;
            // Menu1.Items.Remove(Menu1.FindItem(@"APPLICATION\EBOM"));
            // Menu1.FindItem(@"APPLICATION\EBOM").Enabled = false;
            // Menu1.FindItem("APPLICATION").ChildItems.Remove(Menu1.FindItem("APPLICATION/EBOM"));
            ////////////////
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string selectedItem = (sender as Menu).SelectedItem.Value;
            if (selectedItem == "VMS")
            {
                string url;
                string vms = ConfigurationManager.AppSettings["VMSApp"].ToString();
                url = vms + "?Userid=" +
                    Server.UrlEncode(Session["VMSUSERID"] as string);
                Response.Redirect(url);
            }
            else if (selectedItem == "MRB")
            {
                string url;
                string vms = ConfigurationManager.AppSettings["MRBApp"].ToString();
                url = vms + "?Userid=" +
                    Server.UrlEncode(Session["MRBUSERID"] as string);
                Response.Redirect(url);
            }
            else if (selectedItem == "EBOM")
            {
                string url;
                string vms = ConfigurationManager.AppSettings["EBOMApp"].ToString();
                url = vms + "?Userid=" +
                    Server.UrlEncode(Session["EQPBRKUPUSERID"] as string);
                Response.Redirect(url);
            }
            if (!string.IsNullOrEmpty(e.Item.NavigateUrl))
            {
                Response.Redirect(e.Item.NavigateUrl);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            ClearSession();
            Response.Redirect("~/LandingPage/LandingPage.aspx");
        }

        protected void btndefault_Click(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = btndefault.UniqueID;
        }

        protected void DDOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDOption.SelectedValue == "LG")
            {
                ClearSession();
                Response.Redirect("~/LandingPage/LandingPage.aspx");
            }
            if (DDOption.SelectedValue == "CP")
            {

                Response.Redirect("~/Admin/UserCreation.aspx?");
            }
        }
    }
}