namespace Serialization.CustomBinary.Models
{
    public class Employee
    {
        public string EmployeeName { get; set; }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EmployeeName);
        }

        public static Employee Deserialize(BinaryReader reader)
        {
            return new Employee
            {
                EmployeeName = reader.ReadString()
            };
        }
    }
}
