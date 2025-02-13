using secreatsanta;
using System.Collections.Generic;
using System;
using System.Linq;

public class SecretSantaAssigner
{
    private List<Employee> _employees;
    private List<SecretSantaAssignment> _previousAssignments;

    public SecretSantaAssigner(List<Employee> employees, List<SecretSantaAssignment> previousAssignments)
    {
        _employees = new List<Employee>(employees);
        _previousAssignments = previousAssignments;
    }

    public List<SecretSantaAssignment> AssignSecretSanta()
    {
        var shuffledEmployees = _employees.OrderBy(x => Guid.NewGuid()).ToList();
        var assignments = new List<SecretSantaAssignment>();
        Dictionary<string, string> lastYearAssignments = _previousAssignments.ToDictionary(a => a.Employee_EmailID, a => a.Secret_Child_EmailID);

        for (int i = 0; i < shuffledEmployees.Count; i++)
        {
            var giver = shuffledEmployees[i];
            var potentialReceivers = shuffledEmployees.Where(e => e.Employee_EmailID != giver.Employee_EmailID && (!lastYearAssignments.ContainsKey(giver.Employee_EmailID) || lastYearAssignments[giver.Employee_EmailID] != e.Employee_EmailID)).ToList();

            if (!potentialReceivers.Any())
            {
                throw new Exception("Failed to assign Secret Santa due to constraints.");
            }

            var receiver = potentialReceivers.First();
            assignments.Add(new SecretSantaAssignment
            {
                Employee_Name = giver.Employee_Name,
                Employee_EmailID = giver.Employee_EmailID,
                Secret_Child_Name = receiver.Employee_Name,
                Secret_Child_EmailID = receiver.Employee_EmailID
            });
            shuffledEmployees.Remove(receiver);
        }
        return assignments;
    }
}
