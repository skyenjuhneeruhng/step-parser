using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StepParser.Base
{
    public class BaseLogWriter
    {
        private string _fileNamePath = null;
        private string _fileName = null;
        public string FileName { get { return _fileName; } set { _fileName = value; } }
        private object _lock = new object();
        private bool _isWriteDetail = true;
        private string _logFolder = null;
        private string _logFolderDate = null;
        private DateTime _lastDateTime = DateTime.Now;

        private static int _logLevel = (int)LOG_LEVEL_ENUM.INFO;
        public static int LogLevel { get { return _logLevel; } set { _logLevel = value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logFile"></param>
        public BaseLogWriter(string logFile)
        {
            _fileName = string.IsNullOrEmpty(logFile) ? "PSafe_log.txt" : logFile;
            var directory = AppDomain.CurrentDomain.BaseDirectory; // Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _logFolder = Path.Combine(directory, "Logs");
            MakeLogFile();
        }

        /// <summary>
        /// MakeLogFile
        /// </summary>
        /// <returns></returns>
        public bool MakeLogFile()
        {
            try
            {
                lock (_lock)
                {
                    bool isError = false;
                    if (!Directory.Exists(_logFolder))
                    {
                        try
                        {
                            Directory.CreateDirectory(_logFolder);
                        }
                        catch
                        {
                            isError = true;
                        }
                    }
                    _lastDateTime = DateTime.Now;
                    _logFolderDate = Path.Combine(_logFolder, _lastDateTime.ToString("yyyy_MM_dd"));
                    if (!Directory.Exists(_logFolderDate))
                    {
                        try
                        {
                            Directory.CreateDirectory(_logFolderDate);
                        }
                        catch
                        {
                            isError = true;
                        }
                    }
                    if (!isError)
                    {
                        _fileNamePath = Path.Combine(_logFolderDate, _fileName);
                        if (!File.Exists(_fileNamePath))
                        {
                            try
                            {
                                File.Create(_fileNamePath).Close();
                            }
                            catch { }
                        }
                    }
                    return !isError;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// WriteLog
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logMessage"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        private void WriteLog(int logLevel, string logMessage, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            if (logLevel < _logLevel)
                return;
            lock (_lock)
            {
                if (_lastDateTime.Date != DateTime.Now.Date)
                    MakeLogFile();
                if (!File.Exists(_fileNamePath))
                {
                    try
                    {
                        File.Create(_fileNamePath).Close();
                    }
                    catch { }
                }
                if (File.Exists(_fileNamePath))
                {
                    try
                    {
                        using (StreamWriter w = File.AppendText(_fileNamePath))
                        {
                            if (_isWriteDetail)
                            {
                                Log((LOG_LEVEL_ENUM)logLevel, logMessage,
                                    lineNumber, caller == null ? string.Empty : caller,
                                    callerFileName == null ? string.Empty : Path.GetFileName(callerFileName),
                                    w);
                            }
                            else
                                Log((LOG_LEVEL_ENUM)logLevel, logMessage, w);
                        }
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// ReadLog
        /// </summary>
        /// <param name="dateLog"></param>
        /// <returns></returns>
        public string ReadLog(string dateLog = null) //yyyy_MM_dd
        {
            lock (_lock)
            {
                string fileNamePath = _fileNamePath;
                if (!string.IsNullOrEmpty(dateLog))
                {
                    string logFolder = Path.Combine(_logFolder, dateLog);
                    fileNamePath = Path.Combine(logFolder, _fileName);
                }
                if (File.Exists(fileNamePath))
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader streamReader = new StreamReader(fileStream))
                            {
                                string text = streamReader.ReadToEnd();
                                return text;
                            }
                        }
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        private void Log(LOG_LEVEL_ENUM logLevel, string logMessage, int lineNumber, string caller, string fileName, TextWriter w)
        {
            w.WriteLine("{0} {1} {2} {3} {4} {5}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logLevel.ToString().PadRight(5), fileName.PadRight(30), caller.PadRight(35), lineNumber.ToString().PadRight(5), logMessage);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="w"></param>
        private void Log(LOG_LEVEL_ENUM logLevel, string logMessage, TextWriter w)
        {
            w.WriteLine("{0} {1} {2}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logLevel.ToString(), logMessage);
        }

        /// <summary>
        /// WriteErrorLog
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void WriteErrorLog(string logMessage, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            WriteLog((int)LOG_LEVEL_ENUM.ERROR, logMessage, lineNumber, caller, callerFileName);
        }

        /// <summary>
        /// WriteErrorLog
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void WriteErrorLog(Exception ex, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            if (ex.InnerException != null)
                WriteLog((int)LOG_LEVEL_ENUM.ERROR, ex.InnerException.Message + "\nStackTrace: " + ex.InnerException.StackTrace, lineNumber, caller, callerFileName);
            else
                WriteLog((int)LOG_LEVEL_ENUM.ERROR, ex.Message + "\nStackTrace: " + ex.StackTrace, lineNumber, caller, callerFileName);
        }

        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void WriteDebugLog(string logMessage, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            WriteLog((int)LOG_LEVEL_ENUM.DEBUG, logMessage, lineNumber, caller, callerFileName);
        }

        /// <summary>
        /// WriteInfoLog
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void WriteInfoLog(string logMessage, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            WriteLog((int)LOG_LEVEL_ENUM.INFO, logMessage, lineNumber, caller, callerFileName);
        }

        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="lineNumber"></param>
        /// <param name="caller"></param>
        public void WriteDebugLog(Exception ex, [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null, [CallerFilePath] string callerFileName = null)
        {
            if (ex.InnerException != null)
                WriteLog((int)LOG_LEVEL_ENUM.DEBUG, ex.InnerException.Message + "\nStackTrace: " + ex.InnerException.StackTrace, lineNumber, caller, callerFileName);
            else
                WriteLog((int)LOG_LEVEL_ENUM.DEBUG, ex.Message + "\nStackTrace: " + ex.StackTrace, lineNumber, caller, callerFileName);
        }
    }
}

