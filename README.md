# Secret Santa Console Application

## Overview
This is a console-based application that facilitates a Secret Santa gift exchange. It reads employee details from a CSV file, considers past assignments to avoid repeated pairings, and generates new assignments for the current year.

## Features
- Reads current year employee data from a CSV file.
- Reads previous year Secret Santa assignments to prevent duplicate pairings.
- Randomly assigns Secret Santa pairings.
- Saves the generated assignments to a CSV file in the Downloads folder.

## Prerequisites
- .NET Framework or .NET Core installed.
- CSV files containing employee details and past assignments.
- Windows OS (for OpenFileDialog support).

## Installation
1. Open the solution in Visual Studio or use a terminal to build and run the project.
2. Ensure required CSV files are available before running the program.

## Usage
1. Run the application.
2. Select the current year employees CSV file.
3. Select the previous year assignments CSV file.
4. The program will generate Secret Santa assignments and save them to `SecretSantaAssignments.csv` in the Downloads folder.
5. If any errors occur, they will be displayed in the console.

## File Format
### Employee CSV Format:
```
Employee_Name, Employee_EmailID
John Doe, john.doe@example.com
Jane Smith, jane.smith@example.com
```

### Previous Assignments CSV Format:
```
Employee_Name, Employee_EmailID, Secret_Child_Name, Secret_Child_EmailID
John Doe, john.doe@example.com, Jane Smith, jane.smith@example.com
Jane Smith, jane.smith@example.com, John Doe, john.doe@example.com
```

## Code Structure
- `Program.cs`: Entry point of the application.
- `SecretSantaAssigner.cs`: Logic for assigning Secret Santa pairings.
- `Employee.cs`: Employee model.
- `SecretSantaAssignment.cs`: Secret Santa assignment model.
- `CsvHandler.cs`: Handles reading and writing CSV files.

## Dependencies
- `CsvHelper` (for CSV file handling)

## Future Enhancements
- Add a graphical user interface (GUI) for better user experience.
- Support for different file formats (e.g., JSON, Excel).
- Option to send assignment emails automatically.

## License
This project is open-source and available under the MIT License.

