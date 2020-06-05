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
using System.Threading;

namespace pafap_global
{
    public class Init
    {
        public static pafap_sql InitSql;
        public static pafap_search InitSearch;
        public static pafap_files InitFiles;
    }
}

namespace pafap
{
    public partial class pafapSE : System.Web.UI.Page
    {
        public int ResultPerPage = 3;
        string strConn = WebConfigurationManager.ConnectionStrings["ConnectionNetSE"].ConnectionString;
        ArrayList al = new ArrayList();

        public void btnSearch_Click(object sender, EventArgs e)
        {
            page = 0;
            ddlpaths.Visible = true;
            lnkList.Visible = true;
            phview.Visible = true;
            LoadDataRepeater();
        }

        public string getQueryString
        {
            get
            {
                return QuerySearch.Text;
            }
            set
            {
                QuerySearch.Text = value;
            }
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

        public void LoadDataRepeater()
        {
            pafap_global.Init.InitSearch = new pafap_search();
            pafap_global.Init.InitFiles = new pafap_files();
            ResultPerPage = int.Parse(pafap_global.Init.InitFiles.GetValueFromXmlFile(Server.MapPath("controls/configure.xml"), "conf/ResultPerPage"));
            Stopwatch stopw = new Stopwatch();
            stopw.Start();
            pafap_global.Init.InitSql = new pafap_sql(strConn);
            if (pafap_global.Init.InitSql.isConnected)
            {
                string allQuery = "SELECT fn, pth, date_modified, aspect_ratio FROM files WHERE fn LIKE '%" + getQueryString + "%'";
                pafap_global.Init.InitSql.BindRepeater(allQuery, rptResults, page, ResultPerPage, lnkPrev, lnkNext);
                if (pafap_global.Init.InitSql.rowCounting == 0)
                {
                    al = pafap_global.Init.InitSearch.stringSpliter(getQueryString);
                    foreach (string s in al)
                    {
                        string ResQuery = "SELECT fn, pth, date_modified, aspect_ratio FROM files WHERE fn LIKE '%" + s + "%'";
                        pafap_global.Init.InitSql.BindRepeater(ResQuery, rptResults, page, ResultPerPage, lnkPrev, lnkNext);
                    }
                }
                lblQueryTime.Text = "Query: " + getQueryString + ".. " + pafap_global.Init.InitSql.rowCounting + " Results... " + ((string)pafap_global.Init.InitSearch.swMicrotime(stopw)) + " Seconds... ";
            }
            else
            {
                lblstatus.Text = pafap_global.Init.InitSql.errors;
            }
        }
        //Next linkbutton;
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            page += 1;
            LoadDataRepeater();
        }
        //Preview linkbutton;
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            page -= 1;
            LoadDataRepeater();
        }

        public void putStringInFileFromRepeater()
        {
            pafap_global.Init.InitFiles = new pafap_files();
            string str = string.Empty;
            for (int itemCount = 0; itemCount < rptResults.Items.Count; itemCount++)
            {
                CheckBox chkBox = ((CheckBox)rptResults.Items[itemCount].FindControl("chkcp"));
                if (chkBox != null && chkBox.Checked == true)
                {
                    Label lblp = ((Label)rptResults.Items[itemCount].FindControl("lblPath"));
                    Label lblf = ((Label)rptResults.Items[itemCount].FindControl("lblFileName"));
                    str = lblp.Text;
                    str += lblf.Text;
                    pafap_global.Init.InitFiles.makePaths(str, ddlpaths.SelectedValue, pafap_global.Init.InitFiles.GetValueFromXmlFile(Server.MapPath("controls/configure.xml"), "conf/FileName"));
                }
            }
        }
        //put source and desctination in text file;
        protected void lnkList_Click(object sender, EventArgs e)
        {
            //chech if file is open for read/write;
            pafap_global.Init.InitFiles = new pafap_files();
            while (pafap_global.Init.InitFiles.checkIfIsOpen(pafap_global.Init.InitFiles.GetValueFromXmlFile(Server.MapPath("controls/configure.xml"), "conf/FileName")))
                Thread.Sleep(1000);
            putStringInFileFromRepeater();
            lblstatus.Text = pafap_global.Init.InitSql.errors;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            QuerySearch.Focus();
            if (!IsPostBack)
            {
                pafap_global.Init.InitFiles = new pafap_files();
                pafap_global.Init.InitFiles.bindDDLFromXMLFile(Server.MapPath("controls/paths.xml"), ddlpaths, "channel", "Name", "Name", "path");
            }
        }
    }
}
