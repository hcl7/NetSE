using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public class pafap_sql
{
    public SqlConnection Sql_Conn;
    public bool isConnected = false;
    private int sqlQueryTimeout = 120;
    public string errors = "";
    public int rowCounting = 0;

    //sql connection class constructor;
    public pafap_sql(string cstr)
    {
        try
        {
            Sql_Conn = new SqlConnection(cstr);
            Sql_Conn.Open();
            isConnected = true;
        }
        catch (Exception e)
        {
            errors = "sqlConn Error: " + e.Message;
            isConnected = false;
        }
    }

    public void closeSqlConn()
    {
        Sql_Conn.Close();
        isConnected = false;
    }

    // run querys;
    public bool runSQLQuery(string SQL)
    {
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            return false;

        SqlCommand dbcmd = Sql_Conn.CreateCommand();
        dbcmd.CommandType = CommandType.Text;
        dbcmd.CommandText = SQL;
        dbcmd.CommandTimeout = sqlQueryTimeout;
        bool ret = true;
        try
        {
            dbcmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            ret = false;
            errors = "runSQLQuery Error: " + e.Message;
        }
        finally
        {
            dbcmd.Dispose();
            dbcmd = null;
        }
        return ret;
    }

    // run querys;
    public SqlDataReader getSQLResult(string sql)
    {
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            return null;
        SqlCommand dbcmd = Sql_Conn.CreateCommand();
        dbcmd.CommandText = sql;
        dbcmd.CommandTimeout = sqlQueryTimeout;
        SqlDataReader reader = null;
        try
        {
            reader = dbcmd.ExecuteReader();
        }
        catch (Exception e)
        {
            reader = null;
            errors = "getSQLResult Error: " + e.Message;
        }
        finally
        {
            dbcmd.Dispose();
            dbcmd = null;
        }
        return reader;
    }

    // run querys;
    public string getValueFromTable(string sql, int fieldNum)
    {
        string strReturn = string.Empty;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            return strReturn;
        SqlCommand dbcmd = Sql_Conn.CreateCommand();
        dbcmd.CommandText = sql;
        dbcmd.CommandTimeout = sqlQueryTimeout;
        try
        {
            SqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                strReturn = (string)reader[fieldNum].ToString();
            }
            reader.Close();
            reader = null;
        }
        catch (Exception e)
        {
            errors = "getValueFromTable Error: " + e.Message;
        }
        finally
        {
            dbcmd.Dispose();
            dbcmd = null;
        }

        return strReturn;
    }

    // ping database;
    public bool ping()
    {
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            SqlCommand dbcmd = Sql_Conn.CreateCommand();
            dbcmd.CommandText = "SELECT 1+1";
            dbcmd.CommandTimeout = sqlQueryTimeout;
            try
            {
                dbcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                connState = false;
                errors = "Ping Error: " + e.Message;
            }
            finally
            {
                dbcmd.Dispose();
                dbcmd = null;
            }
        }
        return connState;
    }

    // bind repeater controls;
    public void BindRepeater(string query, Repeater rpt, int page, int ps, LinkButton lnkprev, LinkButton lnknext)
    {
        int rowcount, vrowcount;
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            PagedDataSource pds = new PagedDataSource();
            SqlDataAdapter da = new SqlDataAdapter(query, Sql_Conn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                rowcount = ds.Tables[0].Rows.Count;
                getRowCount(rowcount);
                pds.DataSource = ds.Tables[0].DefaultView;
                pds.CurrentPageIndex = page;
                pds.AllowPaging = true;
                pds.PageSize = ps;
                vrowcount = rowcount / pds.PageSize;
                if (page < 1) lnkprev.Visible = false;
                else if (page > 0) lnkprev.Visible = true;
                if (page == vrowcount) lnknext.Visible = false;
                else if (page < vrowcount) lnknext.Visible = true;
                rpt.DataSource = pds;
                rpt.DataBind();
            }
            catch (Exception e)
            {
                errors = "BindRepeater Error: " + e.Message;
            }
            finally
            {
                da.Dispose();
                da = null;
            }
        }
    }

    //get number of row;
    public void getRowCount(int rc)
    {
        rowCounting = rc;
    }

    // bind gridview controls;
    public void BindGridView(string query, GridView gv)
    {
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, Sql_Conn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                gv.DataSource = ds;
                gv.DataBind();
            }
            catch (Exception e)
            {
                errors = "BindGridView Error: " + e.Message;
            }
            finally
            {
                da.Dispose();
                da = null;
            }
        }
    }

    // bind datalist controls;
    public void BindDataList(string query, DataList dl)
    {
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, Sql_Conn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dl.DataSource = ds;
                dl.DataBind();
            }
            catch (Exception e)
            {
                errors = "BindDataList Error: " + e.Message;
            }
            finally
            {
                da.Dispose();
                da = null;
            }
        }
    }

    //bind datalist controls with page number
    public void BindDataListWithPageNr(string query, DataList dl, int page, int ps, LinkButton lnkprev, LinkButton lnknext)
    {
        int rowcount, vrowcount;
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            PagedDataSource pds = new PagedDataSource();
            SqlDataAdapter da = new SqlDataAdapter(query, Sql_Conn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                rowcount = ds.Tables[0].Rows.Count;
                getRowCount(rowcount);
                pds.DataSource = ds.Tables[0].DefaultView;
                pds.CurrentPageIndex = page;
                pds.AllowPaging = true;
                pds.PageSize = ps;
                vrowcount = rowcount / pds.PageSize;
                if (page < 1) lnkprev.Visible = false;
                else if (page > 0) lnkprev.Visible = true;
                if (page == vrowcount) lnknext.Visible = false;
                else if (page < vrowcount) lnknext.Visible = true;
                dl.DataSource = pds;
                dl.DataBind();
            }
            catch (Exception e)
            {
                errors = "BindDataListWithPageNr Error: " + e.Message;
            }
            finally
            {
                da.Dispose();
                da = null;
            }
        }
    }

    //bind dropdownlist controls;
    public void BindDropDownList(string query, DropDownList ddl, string field)
    {
        bool connState = true;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (connState == true)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, Sql_Conn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                ddl.DataSource = ds;
                ddl.DataTextField = field;
                ddl.DataBind();
            }
            catch (SqlException e)
            {
                errors = "BindDropDownList Error: " + e.Message;
            }
            finally
            {
                da.Dispose();
                da = null;
                Sql_Conn.Close();
            }
        }
    }

    //bind arraylist;
    public void BindArrayList(ArrayList al, string sqlquery, string tablefield)
    {
        bool connState = isConnected;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        if (isConnected == true)
        {
            SqlDataReader reader = new SqlCommand(sqlquery, Sql_Conn).ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        al.Add(reader.GetString(reader.GetOrdinal(tablefield)));
                    }
                }
            }
            catch (Exception ex)
            {
                errors = "bindArrayList error: " + ex.Message;
            }
            finally
            {
                reader.Dispose();
                Sql_Conn.Close();
            }
        }
    }

    //get integer from table;
    public int getIntFromTable(string sql, int fieldNum)
    {
        int intReturn = 0;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            return intReturn;
        SqlCommand dbcmd = Sql_Conn.CreateCommand();
        dbcmd.CommandText = sql;
        dbcmd.CommandTimeout = 120;
        try
        {
            SqlDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                intReturn = (int)reader[fieldNum];
            }
            reader.Close();
            reader = null;
        }
        catch (Exception e)
        {
            errors = "getIntFromTable Error: " + e.Message;
        }
        finally
        {
            dbcmd.Dispose();
            dbcmd = null;
        }

        return intReturn;
    }

    public DataTable dbGetDataTable(string sql, string strcon)
    {
        SqlDataAdapter da = new SqlDataAdapter(sql, strcon);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public bool checkValue(string sqlquery)
    {
        bool connState = isConnected;
        if (isConnected == false || !(Sql_Conn.State == System.Data.ConnectionState.Open))
            connState = false;
        string icheck = "";
        try
        {
            SqlCommand com = new SqlCommand(sqlquery, Sql_Conn);
            icheck = (string)com.ExecuteScalar();
        }
        catch (Exception ex)
        {
            errors = "checkValue error: " + ex.Message;
        }
        if (icheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}