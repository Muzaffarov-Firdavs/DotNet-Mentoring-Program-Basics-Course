namespace Serialization.CustomBinary.Models
{
    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(DepartmentName);
            writer.Write(Employees.Count);
            foreach (var employee in Employees)
            {
                employee.Serialize(writer);
            }
        }

        public static Department Deserialize(BinaryReader reader)
        {
            var department = new Department
            {
                DepartmentName = reader.ReadString()
            };

            int employeeCount = reader.ReadInt32();
            for (int i = 0; i < employeeCount; i++)
            {
                department.Employees.Add(Employee.Deserialize(reader));
            }

            return department;
        }
    }
}