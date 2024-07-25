using Serialization.CustomBinary.Models;

class Program
{
    static void Main()
    {
        var department = new Department
        {
            DepartmentName = "HR",
            Employees = new List<Employee>
            {
                new Employee { EmployeeName = "John Doe" },
                new Employee { EmployeeName = "Jane Doe" }
            }
        };

        // Serialize
        SerializeToFile("department.bin", department);

        // Deserialize
        var deserializedDepartment = DeserializeFromFile("department.bin");

        Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}");
        foreach (var employee in deserializedDepartment.Employees)
        {
            Console.WriteLine($"Employee: {employee.EmployeeName}");
        }
    }

    static void SerializeToFile(string fileName, Department department)
    {
        using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new BinaryWriter(stream))
        {
            department.Serialize(writer);
        }
    }

    static Department DeserializeFromFile(string fileName)
    {
        using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (var reader = new BinaryReader(stream))
        {
            return Department.Deserialize(reader);
        }
    }
}
