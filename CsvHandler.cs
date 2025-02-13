using CsvHelper;
using CsvHelper.Configuration;
using secreatsanta;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class CsvHandler
{
    public static List<Employee> ReadEmployees(string filePath)
    {
        var reader = new StreamReader(filePath);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<Employee>().ToList();
    }

    public static List<SecretSantaAssignment> ReadPreviousAssignments(string filePath)
    {
        if (!File.Exists(filePath)) return new List<SecretSantaAssignment>();
        var reader = new StreamReader(filePath);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<SecretSantaAssignment>().ToList();
    }

    public static void WriteAssignments(string filePath, List<SecretSantaAssignment> assignments)
    {
        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(assignments);
        }
    }

}

