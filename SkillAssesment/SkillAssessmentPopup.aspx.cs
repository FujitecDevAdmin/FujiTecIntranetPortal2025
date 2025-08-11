using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal.SkillAssesment
{
    public partial class SkillAssessmentPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //  FetchInitializeDetails();
                    //txtEmployeeID.Focus();
                    DataTable dt = (DataTable)Session["SGV"];
                    gv.DataSource = dt;
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Skilltype = e.Row.Cells[1].Text;

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (Skilltype == "Knowledge")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00ff19");
                    }
                    if (Skilltype == "Skill")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00ff99");//#ff9900
                    }
                    if (Skilltype == "Attitude")
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#00e6ff");//#00ff19
                    }
                }
            }
        }
    }
}