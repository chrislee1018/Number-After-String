// Number After String CLR Function
// Christopher Lee - 2022
//
// This program is free software : you can redistribute itand /or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.If not, see < https://www.gnu.org/licenses/>.

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
