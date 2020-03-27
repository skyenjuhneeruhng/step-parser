using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using StepParser.Base;

namespace StepParser.ViewModel
{
    public class LogWriter : BaseLogWriter
    {
        private static LogWriter _instance = null;
        public static LogWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogWriter(Assembly.GetExecutingAssembly().GetName().Name + "_log.txt");
                }
                return _instance;
            }
        }
        public LogWriter(string fileName) : base(fileName)
        {

        }
    }
}
