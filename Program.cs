using secreatsanta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

class Program
{
    [STAThread] 
    static void Main(string[] args)
    {
        try
        {
            string currentYearFile = GetFilePath("Select the Current Year Employees CSV file");
            string previousYearFile = GetFilePath("Select the Previous Year Assignments CSV file");
            if (string.IsNullOrEmpty(currentYearFile) || string.IsNullOrEmpty(previousYearFile))
            {
                Console.WriteLine("❌ File selection cancelled. Exiting program.");
                return;
            }
            string outputFile = Path.Combine(GetDownloadsFolder(), "SecretSantaAssignments.csv");
            List<Employee> employees = ReadEmployees(currentYearFile);
            List<SecretSantaAssignment> previousAssignments = ReadPreviousAssignments(previousYearFile);
            SecretSantaAssigner assigner = new SecretSantaAssigner(employees, previousAssignments);
            List<SecretSantaAssignment> assignments = assigner.AssignSecretSanta();

            WriteAssignments(outputFile, assignments);
            Console.WriteLine($"✅ Secret Santa assignments have been successfully created! Output saved to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ An error occurred: " + ex.Message);
        }
    }

  
    static string GetFilePath(string title)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Title = title;
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Multiselect = false; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
        }

        return null;
    }

    static List<Employee> ReadEmployees(string filePath)
    {
        List<Employee> employees = new List<Employee>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string headerLine = reader.ReadLine(); 
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                if (values.Length >= 2)
                {
                    employees.Add(new Employee
                    {
                        Employee_Name = values[0].Trim(),
                        Employee_EmailID = values[1].Trim()
                    });
                }
            }
        }

        return employees;
    }

    static List<SecretSantaAssignment> ReadPreviousAssignments(string filePath)
    {
        List<SecretSantaAssignment> assignments = new List<SecretSantaAssignment>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string headerLine = reader.ReadLine(); 
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                if (values.Length >= 4)
                {
                    assignments.Add(new SecretSantaAssignment
                    {
                        Employee_Name = values[0].Trim(),
                        Employee_EmailID = values[1].Trim(),
                        Secret_Child_Name = values[2].Trim(),
                        Secret_Child_EmailID = values[3].Trim()
                    });
                }
            }
        }

        return assignments;
    }

    static void WriteAssignments(string filePath, List<SecretSantaAssignment> assignments)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("GiverName,GiverEmail,ReceiverName,ReceiverEmail");

            foreach (var assignment in assignments)
            {
                writer.WriteLine($"{assignment.Employee_Name},{assignment.Employee_EmailID},{assignment.Secret_Child_Name},{assignment.Secret_Child_EmailID}");
            }
        }
    }

    static string GetDownloadsFolder()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
    }
}
