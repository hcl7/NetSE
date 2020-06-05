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

public class pafap_lists
{
    public ArrayList l;
    public bool isInitialized = false;
    
    public bool init()
    {
        try
        {
            l = new ArrayList();
            isInitialized = true;
        }
        catch
        {
            isInitialized = false;
        }
        return isInitialized;
    }
    public void clear()
    {
        l.Clear();
    }
    public bool addString(string str)
    {
        if (isInitialized)
        {
            l.Add(str);
        }
        return true;
    }
    public uint count()
    {
        if (isInitialized)
            return (uint)l.Count;

        return 0;
    }
}

