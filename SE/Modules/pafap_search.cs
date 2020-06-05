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

public class pafap_search
{
    Label lblerrors = new Label();
    public SortedList wordFrequency(string fn)
    {
        SortedList wordlist = new SortedList();
        StreamReader sr = File.OpenText(fn);
        int ch = sr.Read();
        while (ch >= 0) 
        {
            while (ch >= 0 && !Char.IsLetter((char)ch)) ch = sr.Read();
            StringBuilder sb = new StringBuilder();
            while (ch >= 0 && Char.IsLetterOrDigit((char)ch)) 
            {
                sb.Append((char)ch);
                ch = sr.Read();
            }
            if (sb.Length > 0) 
            {
                string word = sb.ToString();
                if (wordlist.Contains(word))
                    wordlist[word] = (int)wordlist[word] + 1;
                else
                wordlist[word] = 1;
            }
        }
        sr.Close();
        return wordlist;
    }

    public SortedList letterFrequency(string fn)
    {
        SortedList letterlist = new SortedList();
        int[] c = new int[(int)char.MaxValue];
        string str = File.ReadAllText(fn);
        foreach (char t in str)
        {
            c[(int)t]++;
        }
        for (int i = 0; i < (int)char.MaxValue; i++)
        {
            if (c[i] > 0 && char.IsLetterOrDigit((char)i))
            {
                letterlist.Add((char)i, c[i]);
            }
        }
        return letterlist;
    }
    //time calculator in Milliseconds;
    public string swMicrotime(Stopwatch sw)
    {
        sw.Stop();
        TimeSpan ts = sw.Elapsed;
        string et = String.Format("{0:00}.{1:000}", ts.Seconds, ts.Milliseconds / 10);
        return et;
    }
    //string spliter;
    public ArrayList stringSpliter(string str)
    {
        ArrayList words = new ArrayList(str.Trim().Split(' '));
        return words;
    }
    //string combinator;
    public ArrayList stringCombinator(string str)
    {
        ArrayList words = new ArrayList();
        words = stringSpliter(str);
        return words;
    }
}
