using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CombineFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the path to the directory where files are to be combined: ");
            var dirPath = Console.ReadLine();

            List<FileInfo> files = new List<FileInfo>();

            try
            {

                if (Directory.Exists(dirPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(dirPath);

                    foreach (var f in dir.GetFiles("*.txt"))
                    {
                        files.Add(f);
                    }

                    Console.WriteLine("\n");
                    
                    Console.WriteLine("\nInitiating file merge... ");

                    ConcatenateFiles(files);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void ConcatenateFiles(List<FileInfo> inputFiles)
        {
            using (StreamWriter sw = File.CreateText(@"C:\temp\test.txt"))
            {
                string firstline = File.ReadLines(inputFiles.First().FullName).First();
                bool hasWrittenFirstLine = false;

                foreach (var file in inputFiles)
                {
                    foreach (var line in File.ReadLines(file.FullName))
                    {
                        if (line != firstline)
                            sw.WriteLine(line);
                        else if (line == firstline && hasWrittenFirstLine == false)
                        {
                            sw.WriteLine(line);
                            hasWrittenFirstLine = true;
                        }
                    }
                }
            }

            //using (Stream output = new FileStream(primaryFile.FullName, FileMode.Append))
            //{
            //    foreach (var inputFile in inputFiles)
            //    {
            //        using (Stream input = File.OpenRead(inputFile.FullName))
            //        {
            //            input.CopyTo(output);
            //        }
            //    }
            //}
        }
    }
}
