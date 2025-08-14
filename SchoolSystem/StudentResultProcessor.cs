using System;
using System.Collections.Generic;
using System.IO;

public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        List<Student> students = new List<Student>();

        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                // Check if all fields are present
                if (parts.Length < 3)
                {
                    throw new MissingFieldException($"Missing fields in line: {line}");
                }

                // Parse ID
                if (!int.TryParse(parts[0].Trim(), out int id))
                {
                    throw new FormatException($"Invalid ID format: {parts[0]}");
                }

                string fullName = parts[1].Trim();

                // Parse Score
                if (!int.TryParse(parts[2].Trim(), out int score))
                {
                    throw new InvalidScoreFormatException($"Invalid score format for {fullName}: {parts[2]}");
                }

                students.Add(new Student { Id = id, FullName = fullName, Score = score });
            }
        }

        return students;
    }

    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
    }
}
