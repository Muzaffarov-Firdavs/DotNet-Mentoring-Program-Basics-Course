using Newtonsoft.Json;
using Serialization.Library;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department
        {
            DepartmentName = "Engineering iwth Json",
            Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John Doe" },
                    new Employee { EmployeeName = "Jane Smith" }
                }
        };

        // Serialize
        string json = JsonConvert.SerializeObject(department);
        File.WriteAllText("department.json", json);

        // Deserialize
        string jsonFromFile = File.ReadAllText("department.json");
        Department deserializedDepartment = JsonConvert.DeserializeObject<Department>(jsonFromFile);
        Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}");
        foreach (var employee in deserializedDepartment.Employees)
        {
            Console.WriteLine($"Employee: {employee.EmployeeName}");
        }
    }
}
