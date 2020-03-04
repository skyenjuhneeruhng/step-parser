using System;
using System.IO;
using System.Text;
using System.Xml;

namespace StepParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    string fullPath = Path.GetFullPath(args[0]);
                    string onlyPath = Directory.GetParent(fullPath).FullName;
                    string fileName = Path.GetFileNameWithoutExtension(args[0]);
                    Console.WriteLine("Loading STP file...");
                    StepFile stepFile;
                    using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                    {
                        stepFile = StepFile.Load(fs);
                    }

                    XmlDocument doc = new XmlDocument();
                    XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                    Console.WriteLine("Writing XML file...");
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        ConformanceLevel = ConformanceLevel.Fragment,
                        Indent = true,
                        IndentChars = "    ",
                        OmitXmlDeclaration = false,
                        CloseOutput = false,
                        Encoding = Encoding.Unicode
                    };
                    XmlWriter xmlWriter = XmlWriter.Create(onlyPath + "/" + fileName + ".xml", settings);
                    StepWriter stepWriter = new StepWriter(stepFile, false, xmlWriter);
                    stepWriter.Save();

                    xmlWriter.Flush();
                    xmlWriter.Close();

                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine("File Not Exists");
                }
            }
            else
            {
                Console.WriteLine("No STP File found");
            }
        }
    }
}
