using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FujiTecIntranetPortal
{
    public partial class IntranetPortalNewLogin2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to another page
            Response.Redirect("/LandingPage/LandingPage.aspx");

            if (!IsPostBack)
            {
                LoadMetaData();
            }
        }
        private void LoadMetaData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_LoginPageload", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    con.Open();
                    sqlDataAdapter.Fill(dataSet);

                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        ViewState["GrdEmplWel"] = dataSet.Tables[0];
                        ShowEmployeeSlideShow(dataSet.Tables[0]);

                        ViewState["NEWSEVENTS"] = dataSet.Tables[1];
                        ShowEventsSlideShow(dataSet.Tables[1]);
                    }

                }

                ShowItAwareness();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void ShowEmployeeSlideShow(DataTable dtEmployee)
        {
            foreach (DataRow row in dtEmployee.Rows)
            {
                row["Emp_Photo"] = Path.Combine(@"EmployeePhoto\", row["Emp_Photo"].ToString());
            }
            rptEmployeeSlideShow.DataSource = dtEmployee;
            rptEmployeeSlideShow.DataBind();
        }
        protected void ShowEventsSlideShow(DataTable dtEvents)
        {
            //dtEvents.Clear();
            //dtEvents.Rows.Add(new object[] { "Disposable of waste paper for recycling purpose ", ".jpg", @"~\NewsEvents\FUJI\20220601152940.jpg" });
            //dtEvents.Rows.Add(new object[] { "Birthday celebration - Pune Branch Manager Mr.Shridhar Bilolikar", ".jpg", @"~\NewsEvents\FUJI\20220608122137.jpg" });
            //dtEvents.Rows.Add(new object[] { "Photo with Mr.Kunal Shah of Kirtee Consultants Navi Mumbai along with our MD,Mr.Thamarai and Mr.Praveen Yabaji, Visited factory on Thursday 9th June 2022.", ".jpg", @"~\NewsEvents\FUJI\20220614075402.jpg" });
            //dtEvents.Rows.Add(new object[] { "Fujitec Installation Process & Orientation Training Program held on 13th & 14th at CHQ ", ".jpg", @"~\NewsEvents\FUJI\20220614154102.jpg" });
            //dtEvents.Rows.Add(new object[] { "Fujitec Installation Process & Orientation Training Program in Board Room", ".jpg", @"~\NewsEvents\FUJI\20220620083120.jpg" });
            //dtEvents.Rows.Add(new object[] { "Fujitec Kolkata Office Inauguration ribbon cutting by Mr. Shakir Ahmed-MD, Mr.Naoki Nakachika-DMD and Mr.Anil Gadia (Joint MD Meridian Developers) on 16-06-2022", ".jpg", @"~\NewsEvents\FUJI\20220628110411.JPG" });
            //dtEvents.Rows.Add(new object[] { "Fujitec Kolkata Office Inauguration Lamp Lighting by Mr.Naoki Nakachika -DMD", ".jpg", @"~\NewsEvents\FUJI\20220628110628.JPG" });
            //dtEvents.Rows.Add(new object[] { "Kolkata Office inauguration our team left to right Mr.Simanta Sinha- Sr.Sales Engg, Mr.Kingshok.de- Sales Manager, Mr.Anil Gadia (Joint MD Meridian Developers), Mr. Abhishek Kaushik- National Sales Head,Mr.Sreekumar Nambiar-Director, Mr.Shakir Ahmed-MD,  Mr.Naoki Nakachika-DMD ", ".jpg", @"~\NewsEvents\FUJI\20220628110949.JPG" });
            //dtEvents.Rows.Add(new object[] { "GSTN takes immense pleasure in conveying to you a certificate of appreciation issued by the Central Board of Indirect taxes & Customs, Ministry of Finance, Govt. of India for your timely return filing and sizeable payment of GST in cash.", ".jpg", @"~\NewsEvents\FUJI\20220810165255.jpg" });
            //dtEvents.Rows.Add(new object[] { "Safety Awarness programme conducted for school students, Safety handbook and chocolates distributed by our Service Head Mr.K.SathyaNarayanan", ".jpg", @"~\NewsEvents\FUJI\20220728082559.jpg" });
            //dtEvents.Rows.Add(new object[] { "Safety Awarness programme conducted for school students, Safety handbook and chocolates distributed by our Service Head Mr.K.SathyaNarayanan", ".jpg", @"~\NewsEvents\FUJI\20220728082611.jpg" });
            //dtEvents.Rows.Add(new object[] { "Safety Awarness programme conducted for school students, Safety handbook and chocolates distributed by our Service Head Mr.K.SathyaNarayanan", ".jpg", @"~\NewsEvents\FUJI\20220728082616.jpg" });

            foreach (DataRow row in dtEvents.Rows)
            {
                row["FileNamepath"] = ResolveUrl(row["FileNamepath"].ToString());
            }
            rptEventsSlideShow.DataSource = dtEvents;
            rptEventsSlideShow.DataBind();
        }

        protected void ShowItAwareness()
        {
            DataTable ItAwarenessDt = new DataTable();
            ItAwarenessDt.Columns.Add("ItAwarenessImageTitle", typeof(string));
            ItAwarenessDt.Columns.Add("ItAwarenessImagePath", typeof(string));

            //ItAwarenessDt.Rows.Add(new object[] { "Malware", @"~\IT_Awareness\31.03.2023\Malware.png" });
            //ItAwarenessDt.Rows.Add(new object[] { "Phishing", @"~\IT_Awareness\31.03.2023\Phishing.png" });
            //ItAwarenessDt.Rows.Add(new object[] { "SocialEngineering", @"~\IT_Awareness\31.03.2023\SocialEngineering.png" });
            //ItAwarenessDt.Rows.Add(new object[] { "SQL_Injection", @"~\IT_Awareness\31.03.2023\SQL_Injection.png" });

            //   ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (01/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_01.jpg" });
            //  ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (02/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_02.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (03/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_03.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (04/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_04.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (05/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_05.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (06/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_06.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (07/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_07.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (08/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_08.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (09/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_09.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (10/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_10.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (11/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_11.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (12/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_12.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (13/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_13.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (14/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_14.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (15/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_15.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (16/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_16.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (17/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_17.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (18/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_18.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (19/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_19.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (20/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_20.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (21/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_21.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (22/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_22.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (23/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_23.jpg" });
            ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (24/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_24.jpg" });
            // ItAwarenessDt.Rows.Add(new object[] { "Security-Awareness-Training (25/25)", @"~\IT_Awareness\31.03.2023\Security-Awareness-Training_25.jpg" });


            foreach (DataRow row in ItAwarenessDt.Rows)
            {
                row["ItAwarenessImagePath"] = ResolveUrl(row["ItAwarenessImagePath"].ToString());
            }

            rptItAwarenessSlideShow.DataSource = ItAwarenessDt;
            rptItAwarenessSlideShow.DataBind();
        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session["USERID"] = txtUsername.Text;
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand("SP_APPLICATION_BSDON_USERLOGIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = txtUsername.Text;
                    cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = txtPassword.Text;
                    cmd.Parameters.Add("@SESSIONID", SqlDbType.VarChar).Value = this.Session.SessionID;
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DataSet Ds = new DataSet();
                    con.Open();
                    DA.Fill(Ds);
                    //response.result = false;
                    if (Ds.Tables[0].Rows[0]["RESULT"].ToString() == "SUCCESS")
                    {
                        if (Ds.Tables[1].Rows.Count > 0)
                        {
                            Session["VMSUSERNAME"] = Ds.Tables[1].Rows[0]["USERNAME"].ToString();
                            Session["VMSROLES"] = Ds.Tables[1].Rows[0]["ROLES"].ToString();
                            Session["VMSUSERID"] = txtUsername.Text;
                        }
                        if (Ds.Tables[2].Rows.Count > 0)
                        {
                            Session["MRBUSERNAME"] = Ds.Tables[2].Rows[0]["USERNAME"].ToString();
                            Session["MRBROLES"] = Ds.Tables[2].Rows[0]["ROLES"].ToString();
                            Session["MRBUSERID"] = txtUsername.Text;
                        }
                        if (Ds.Tables[3].Rows.Count > 0)
                        {
                            Session["EQPBRKUPUSERNAME"] = Ds.Tables[3].Rows[0]["USERNAME"].ToString();
                            Session["EQPBRKUPROLES"] = Ds.Tables[3].Rows[0]["ROLES"].ToString();
                            Session["EQPBRKUPUSERID"] = txtUsername.Text;
                            // Session["EQPBRKUP"] = true;
                        }
                        if (Ds.Tables[4].Rows.Count > 0)
                        {
                            Session["USERNAME"] = Ds.Tables[4].Rows[0]["USERNAME"].ToString();
                            Session["ROLES"] = Ds.Tables[4].Rows[0]["ROLES"].ToString();
                            Session["USERID"] = txtUsername.Text;
                            Session["Loc"] = Ds.Tables[4].Rows[0]["BranchID"].ToString();
                            // Session["EQPBRKUP"] = true;
                        }
                        Response.Redirect("~/Home.aspx");
                    }
                    else
                    {
                        // Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Please enter valid username and password')</script>");
                        lblmsg.ForeColor = System.Drawing.Color.Red;

                        //  if ((txtUsername.Text != String.Empty) || (txtPassword.Text != String.Empty))
                        lblmsg.Text = "Please enter correct login details";
                    }
                }

            }
            catch (Exception ex)
            {
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = ex.Message;
            }
        }

    }
}