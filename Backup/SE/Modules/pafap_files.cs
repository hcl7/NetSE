using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Diagnostics;
using System.Web.Handlers;
using System.Xml;
using System.Xml.XPath;
using System.Threading;

public class pafap_files
{
    public string errors = "";
    public string ResultPerPage;
    public string FileName;
    public void makePaths(string sourcestr, string deststr, string fn)
    {
        try
        {
            StreamWriter swfile;
            swfile = File.AppendText(fn);
            swfile.Write(sourcestr);
            swfile.Write(" > ");
            swfile.WriteLine(deststr);
            swfile.Close();
        }
        catch (Exception e)
        {
            errors = "putString2File Error: " + e.Message;
        }
    }
    public string GetValueFromXmlFile(string fn, string expr)
    {
        string xmlReturnValue = "";
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fn);
            XPathNavigator xpn = xmldoc.CreateNavigator();
            XPathNodeIterator xpni = xpn.Select(expr);
            while (xpni.MoveNext())
            {
                xmlReturnValue = xpni.Current.Value;
            }
        }
        catch ( Exception e )
        {
            errors = "GetValueFromXmlFile error: " + e.Message;
        }
        return xmlReturnValue;
    }
    //check is the file is open for read/write or don't exists;
    public bool checkIfIsOpen(string file)
    {
        FileStream stream = null;
        FileInfo fileInfo = new FileInfo(file);
        try
        {
            stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }
        return false;
    }
    //check if the file exists;
    public bool checkIfExist(string fn)
    {
        return (File.Exists(fn)) ? true : false;
    }
    //clear the file;
    public void clearFile(string fn)
    {
        try
        {
            FileStream fileStream = File.Open(fn, FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Flush();
            fileStream.Close();
        }
        catch (Exception e)
        {
            errors = "clearFile error: " + e.Message;
        }
    }
    //bind dropdownlist from xml file;
    public void bindDDLFromXMLFile(string path, DropDownList ddlpaths, string dvTable, string sort, string dtfBy, string dvfBy) 
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            DataView dv = ds.Tables[dvTable].DefaultView;
            dv.Sort = sort;
            ddlpaths.DataTextField = dtfBy;
            ddlpaths.DataValueField = dvfBy;
            ddlpaths.DataSource = dv;
            ddlpaths.DataBind();
        }
        catch (Exception e)
        {
            errors = "bindDDLFromXMLFile error: " + e.Message;
        }
    }

    public void readXmlFileToDDL(string xmlfn, DropDownList ddl)
    {
        DataSet rxf = new DataSet();
        rxf.ReadXml(xmlfn);
        ddl.DataSource = rxf;
        ddl.DataBind();
    }
    public void loadXml(string xmlfn)
    {
        XmlTextReader reader = new XmlTextReader(xmlfn);
        reader.WhitespaceHandling = WhitespaceHandling.None;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        reader.Close();
    }

}