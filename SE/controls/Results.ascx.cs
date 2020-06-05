using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Handlers;

namespace pafap.controls
{
    public partial class Results : System.Web.UI.UserControl
    {
        string strConn = WebConfigurationManager.ConnectionStrings["ConnectionNetSE"].ConnectionString;
        public string QueryString;
        public int ResultPerPage = 10;
        public int NrPage;
        ArrayList al = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            //QueryString = (string)Session["querystring"];
        }
        public int page
        {
            get
            {
                if (ViewState["page"] != null)
                    return Convert.ToInt32(ViewState["page"]);
                else
                    return 0;
            }
            set
            {
                ViewState["page"] = value;
            }
        }
        //draw pages;
        public void drawPages(int results, int resultperpage)
        {
            int left = 0;
            int right = 0;
            if (page < 0)
                page = 0;
            if (page - resultperpage < 0)
                left = 0;
            else
                left = page - resultperpage;
            if (page + resultperpage > results / resultperpage)
                right = results / resultperpage;
            else
                right = page + resultperpage;
            for (int i = left; i < right; i++)
            {
                if (i != page)
                {
                    LinkButton lnk = new LinkButton();
                    lnk.Click += new EventHandler(lbl_Click);
                    lnk.ID = "#" + (i + 1).ToString();
                    lnk.Text = (i + 1).ToString();
                    plcPaging.Controls.Add(lnk);
                }
                else
                {
                    LinkButton lnk = new LinkButton();
                    lnk.Click += new EventHandler(lbl_Click);
                    lnk.ID = "#" + (i + 1).ToString();
                    lnk.Text = (i + 1).ToString();
                    plcPaging.Controls.Add(lnk);
                }
            }
        }

        public void lbl_Click(object sender, EventArgs e)
        {
            page += ResultPerPage;
            LoadDataRepeater();
        }

        public void LoadDataRepeater()
        {
            pafap_search ps = new pafap_search();
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            pafap_global.Init.InitSql = new pafap_sql(strConn);
            string CountResQuery = "SELECT COUNT(fn) FROM files WHERE fn LIKE '%" + QueryString + "%'";
            al = ps.stringSpliter(QueryString);
            foreach (string s in al)
            {
                string ResQuery = "SELECT fn, pth, ext, date_modified, aspect_ratio FROM files WHERE fn LIKE '%" + s + "%'";
                pafap_global.Init.InitSql.BindRepeater(ResQuery, rptResults, page, 3, lnkPrev, lnkNext);
            }
            lblQueryTime.Text = ((string)ps.swMicrotime(stopw));
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            page += 1;
            LoadDataRepeater();
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            page -= 1;
            LoadDataRepeater();
        }

    }
}