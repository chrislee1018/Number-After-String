using System;
using System.Collections;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public class CLRFunction
{

    [Microsoft.SqlServer.Server.SqlFunction]

    public static SqlString NumberAfterString(SqlString str, SqlString patt)
    {
        if (str.IsNull || patt.IsNull)
        {
            return null;
        }

        string theString = str.ToString();
        string pattern = patt.ToString();

        if (!theString.Contains(pattern))
        {
            return null;
        }

        int index = theString.IndexOf(pattern, 0) + pattern.Length;

        Regex r = new Regex(@"\G\s*[-+]?\d+(\.\d+)?");
        Match m = r.Match(theString, index);

        if (!m.Success)
        {
            return null;
        }

        return new SqlString(m.Value);
    }
};