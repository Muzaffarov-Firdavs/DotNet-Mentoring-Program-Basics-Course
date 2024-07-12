using Serialization.Library;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Department department = new Department
        {
            DepartmentName = "Engineering with XML",
            Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "John Doe" },
                    new Employee { EmployeeName = "Jane Smith" }
                }
        };

        // Serialize
        XmlSerializer serializer = new XmlSerializer(typeof(Department));
        using (FileStream stream = new FileStream("department.xml", FileMode.Create))
        {
            serializer.Serialize(stream, department);
        }

        // Deserialize
        using (FileStream stream = new FileStream("department.xml", FileMode.Open))
        {
            Department deserializedDepartment = (Department)serializer.Deserialize(stream);
            Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}");
            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee: {employee.EmployeeName}");
            }
        }
    }
}