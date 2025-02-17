// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileOrganizerApp
{
    class OfficeFileOrganizer
    {
        static string directoryPath = "FileCollection";
        static string[] fileExtensions = { ".docx", ".xlsx", ".pptx" };

        static void Main()
        {
            Console.WriteLine("Office File Organizer");
            Console.Write("Enter your name: ");
            string studentName = Console.ReadLine();
            Console.Write("Enter your Student ID: ");
            string studentId = Console.ReadLine();
            Console.Write("Enter your Course Name: ");
            string courseName = Console.ReadLine();

            Directory.CreateDirectory(directoryPath);
            GenerateRandomFiles(6);
            OrganizeFiles();
            GenerateSummary(studentName, studentId, courseName);
            Console.WriteLine("File organization complete. Summary generated.");
        }

        static void GenerateRandomFiles(int count)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                string extension = fileExtensions[rand.Next(fileExtensions.Length)];
                string fileName = Path.Combine(directoryPath, $"File_{i + 1}{extension}");
                File.WriteAllText(fileName, "Sample content");
            }
        }

        static void OrganizeFiles()
        {
            foreach (string ext in fileExtensions)
            {
                string subDir = Path.Combine(directoryPath, ext.TrimStart('.').ToUpper());
                Directory.CreateDirectory(subDir);

                foreach (string file in Directory.EnumerateFiles(directoryPath, "*" + ext))
                {
                    string destination = Path.Combine(subDir, Path.GetFileName(file));
                    File.Move(file, destination);
                }
            }
        }

        static void GenerateSummary(string studentName, string studentId, string courseName)
        {
            StringBuilder summary = new StringBuilder();
            summary.AppendLine("Student Information:");
            summary.AppendLine($"Name: {studentName}");
            summary.AppendLine($"ID: {studentId}");
            summary.AppendLine($"Course: {courseName}");
            summary.AppendLine(new string('-', 30));
            summary.AppendLine("Office File Summary:");
            summary.AppendLine(new string('-', 30));

            int totalCount = 0;
            long totalSize = 0;
            
            foreach (string ext in fileExtensions)
            {
                string subDir = Path.Combine(directoryPath, ext.TrimStart('.').ToUpper());
                var files = Directory.EnumerateFiles(subDir).Select(f => new FileInfo(f)).ToList();
                
                summary.AppendLine($"File Type: {ext.TrimStart('.')}");
                summary.AppendLine($"Count: {files.Count}");
                summary.AppendLine($"Total Size: {files.Sum(f => f.Length)} bytes");
                summary.AppendLine();
                
                totalCount += files.Count;
                totalSize += files.Sum(f => f.Length);
            }
            
            summary.AppendLine(new string('-', 30));
            summary.AppendLine($"Total Files: {totalCount}");
            summary.AppendLine($"Total Size: {totalSize} bytes");
            
            File.WriteAllText(Path.Combine(directoryPath, "SummaryReport.txt"), summary.ToString());
        }
    }
}
