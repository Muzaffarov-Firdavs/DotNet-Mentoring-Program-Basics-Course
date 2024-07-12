using Serialization.Library;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department
        {
            DepartmentName = "Engineering with Binary",
            Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John Doe" },
                    new Employee { EmployeeName = "Jane Smith" }
                }
        };

        // Serialize
        using (FileStream stream = new FileStream("department.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            writer.Write(department.DepartmentName);
            writer.Write(department.Employees.Count);
            foreach (var employee in department.Employees)
            {
                writer.Write(employee.EmployeeName);
            }
        }

        // Deserialize
        using (FileStream stream = new FileStream("department.bin", FileMode.Open))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            string departmentName = reader.ReadString();
            int employeeCount = reader.ReadInt32();
            List<Employee> employees = new List<Employee>();
            for (int i = 0; i < employeeCount; i++)
            {
                string employeeName = reader.ReadString();
                employees.Add(new Employee { EmployeeName = employeeName });
            }

            Department deserializedDepartment = new Department
            {
                DepartmentName = departmentName,
                Employees = employees
            };

            Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}");
            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee: {employee.EmployeeName}");
            }
        }
    }
}