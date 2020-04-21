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
                    string output = input.Replace(@"\x0\", "");
                    output = output.Replace(@"\X0\", "");
                    output = output.Replace(@"x2\", @"\u");
                    output = output.Replace(@"X2\", @"\u");
                    //output = "L0160 Rondelle X\u1fc8x6 mit \u25A0 (INOX)"; //for testing
                    output = System.Text.RegularExpressions.Regex.Unescape(output);
                    return output;
                }
                // From string to byte array
                return input;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex.Message + " from original input: " + input + "\n" + ex.StackTrace);
                return input;
            }
        }
    }
}
