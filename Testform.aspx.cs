using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujiTecIntranetPortal
{
    public partial class Testform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridviewData();
            }
        }

        protected void gvconverted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              //  e.Row.Cells[0].CssClass = "gridcss";
            }
        }

        public DataTable ConvertColumnsAsRows(DataTable dt)
        {
            DataTable dtnew = new DataTable();
            //Convert all the rows to columns
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtnew.Columns.Add(Convert.ToString(i));
            }
            DataRow dr;
            // Convert All the Columns to Rows
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr = dtnew.NewRow();
                dr[0] = dt.Columns[j].ToString();
                for (int k = 1; k <= dt.Rows.Count; k++)
                    dr[k] = dt.Rows[k - 1][j];
                dtnew.Rows.Add(dr);
            }
            return dtnew;
        }
        protected void BindGridviewData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("Education", typeof(string));
            dt.Columns.Add("Location", typeof(string));
            DataRow dtrow = dt.NewRow();    // Create New Row
            dtrow["UserId"] = 1;            //Bind Data to Columns
            dtrow["UserName"] = "SureshDasari";
            dtrow["Education"] = "B.Tech";
            dtrow["Location"] = "Chennai";
            dt.Rows.Add(dtrow);
            dtrow = dt.NewRow();               // Create New Row
            dtrow["UserId"] = 2;               //Bind Data to Columns
            dtrow["UserName"] = "MadhavSai";
            dtrow["Education"] = "MBA";
            dtrow["Location"] = "Nagpur";
            dt.Rows.Add(dtrow);
            dtrow = dt.NewRow();              // Create New Row
            dtrow["UserId"] = 3;              //Bind Data to Columns
            dtrow["UserName"] = "MaheshDasari";
            dtrow["Education"] = "B.Tech";
            dtrow["Location"] = "Nuzividu";
            dt.Rows.Add(dtrow);
          //  gvnormal.DataSource = dt;
           // gvnormal.DataBind();
            //gvconverted.DataSource = ConvertColumnsAsRows(dt);
            //gvconverted.DataBind();
            //gvconverted.HeaderRow.Visible = false;
        }
    }
}