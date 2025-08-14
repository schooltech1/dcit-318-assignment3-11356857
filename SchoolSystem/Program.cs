using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "Student.txt" ; // Input file path
        string outputFile = "report.txt";    // Output file path

        StudentResultProcessor processor = new StudentResultProcessor();

        try
        {
            var students = processor.ReadStudentsFromFile(inputFile);
            processor.WriteReportToFile(students, outputFile);
            Console.WriteLine("Report generated successfully!");

            foreach (var student in students)
            {
                Console.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: Input file not found. {ex.Message}");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
           
        }
        Console.ReadLine();
    }
   
}
