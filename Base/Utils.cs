using StepParser.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StepParser.Base
{
    public class Utils
    {
        public static string FromStringToReadableString(string input)
        {
            try
            {
                if (input.ToLower().Contains(@"x2\") || input.ToLower().Contains(@"\x0\"))
                {
                    string ouput = input.Replace(@"\x0\", "");
                    ouput = ouput.Replace(@"\X0\", "");
                    ouput = ouput.Replace(@"x2\", @"\u");
                    ouput = ouput.Replace(@"X2\", @"\u");
                    return System.Text.RegularExpressions.Regex.Unescape(ouput);
                }
                // From string to byte array
                return input;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
                return input;
            }
        }
    }
}
