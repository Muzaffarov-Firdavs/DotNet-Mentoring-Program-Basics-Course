using Serialization.Library;
using System.Text.Json;

class Program
{
    static void Main()
    {
        var department = new Department
        {
            DepartmentName = "Engineering with Binary",
            Employees = new List<Employee>
            {
                new Employee { EmployeeName = "John Doe" },
                new Employee { EmployeeName = "Jane Doe" }
            }
        };

        var clonedDepartment = DeepClone(department);

        clonedDepartment.DepartmentName = "Finance";
        clonedDepartment.Employees[0].EmployeeName = "Alice";

        Console.WriteLine($"Original Department: {department.DepartmentName}");
        foreach (var employee in department.Employees)
        {
            Console.WriteLine($"Employee: {employee.EmployeeName}");
        }

        Console.WriteLine($"Cloned Department: {clonedDepartment.DepartmentName}");
        foreach (var employee in clonedDepartment.Employees)
        {
            Console.WriteLine($"Employee: {employee.EmployeeName}");
        }
    }

    static T DeepClone<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return JsonSerializer.Deserialize<T>(json);
    }
}
