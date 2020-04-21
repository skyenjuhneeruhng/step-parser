using StepParser.Base;
using StepParser.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace StepParser
{
    class Program
    {
        private static List<string> _failedFiles = new List<string>();
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    if (args.Length >= 2)
                        LogWriter.LogLevel = int.Parse(args[1]);
                    LogWriter.Instance.WriteInfoLog("Begin parser " + Properties.Resource.BuildDate);
                    Console.WriteLine("Begin parser " + Properties.Resource.BuildDate);
                    int countSuccess = 0;
                    if (Directory.Exists(args[0]))
                    {
                        Console.WriteLine("Parse STP File in directories");
                        string[] filePaths = Directory.GetFiles(args[0], "*.stp",
                                         SearchOption.TopDirectoryOnly);
                        if(filePaths != null && filePaths.Length > 0)
                        {
                            foreach(string filePath in filePaths)
                            {
                                ParseStepFile(filePath, ref countSuccess);
                            }
                            Console.WriteLine(string.Format("\nParse result: {0}/{1} successes", countSuccess, filePaths.Length));
                            if(_failedFiles.Count > 0)
                            {
                                Console.WriteLine(string.Format("Failed files:"));
                                foreach (string failedFile in _failedFiles)
                                {
                                    Console.WriteLine(failedFile);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No STP Files found in directories");
                        }
                    }
                    else if (File.Exists(args[0]))
                    {
                        ParseStepFile(args[0], ref countSuccess);
                    }
                    else
                    {
                        Console.WriteLine("File Not Exists");
                        LogWriter.Instance.WriteInfoLog("File Not Exists");
                    }
                }
                else
                {
                    Console.WriteLine("No STP File found");
                    LogWriter.Instance.WriteInfoLog("No STP File found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogWriter.Instance.WriteErrorLog(ex);
            }
        }

        private static void ParseStepFile(string fileInput, ref int countSuccess)
        {
            string fileName = string.Empty;
            try
            {
                string fullPath = Path.GetFullPath(fileInput);
                string onlyPath = Directory.GetParent(fullPath).FullName;
                fileName = Path.GetFileNameWithoutExtension(fileInput);
                Console.WriteLine("\nLoading STP file... " + fileName);
                LogWriter.Instance.ParsingFileName = fileInput;
                LogWriter.Instance.WriteInfoLog("Loading STP file... " + fileName);
                StepFile stepFile;
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    stepFile = StepFile.Load(fs);
                }

                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                Console.WriteLine("Writing XML file...");
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    ConformanceLevel = ConformanceLevel.Auto,
                    Indent = true,
                    IndentChars = "    ",
                    OmitXmlDeclaration = false,
                    CloseOutput = false,
                    Encoding = Encoding.UTF8
                };
                XmlWriter xmlWriter = XmlWriter.Create(onlyPath + "/" + fileName + ".xml", settings);
                StepWriter stepWriter = new StepWriter(stepFile, false, xmlWriter);

                xmlWriter.WriteStartDocument(true);
                xmlWriter.WriteStartElement("STP");
                stepWriter.Save();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();

                Console.WriteLine("Success!");
                LogWriter.Instance.WriteInfoLog("Success!");
                countSuccess++;
            }
            catch (Exception ex)
            {
                _failedFiles.Add(fileName);
                LogWriter.Instance.WriteErrorLog(ex);
                Console.WriteLine("Failed to parse file " + fileName + " " + ex.Message);
            }
        }
    }
}
