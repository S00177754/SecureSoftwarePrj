using System;
using System.Collections.Generic;
using System.Text;


namespace JMS_DAL.Data_Objects
{
    public enum EmployeeRole { Staff, Manager, CEO, Admin }

    [Serializable]
    public class EmployeeDTO: DTO
    {

        public string FirstName { get { return firstName; } set { firstName = value; } }
        private string firstName;

        public string LastName { get { return lastName; } set { lastName = value; } }
        private string lastName;

        public int Role { get { return role; } set { role = value; } }
        private int role;

        public string UserUID { get { return userUID; } set { userUID = value; } }
        private string userUID;

        public EmployeeDTO()
        {
        }

        public EmployeeDTO(Employee employee)
        {
            ID = employee.ID.ToString();
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Role = (int)employee.Role;
            UserUID = employee.UserUID;
            LastModifiedBy = employee.LastModifiedBy;
        }

        public override string ToString()
        {
            return $"ID:{ID} - Name:{FirstName} {LastName} - Role:{Enum.GetName(typeof(EmployeeRole),(EmployeeRole)Role)} - UserUID:{UserUID} - Last Modified By:{LastModifiedBy}";
        }
    }

    public class Employee : DataObject
    {
        public string FirstName { get { return firstName; } set { firstName = value; } }
        private string firstName;

        public string LastName { get { return lastName; } set { lastName = value; } }
        private string lastName;

        public EmployeeRole Role { get { return role; } set { role = value; } }
        private EmployeeRole role;

        public string UserUID { get { return userUID; } set { userUID = value; } }
        private string userUID;

        public Employee()
        {
        }

        public Employee(EmployeeDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Role = (EmployeeRole)dto.Role;
            UserUID = dto.UserUID;
            LastModifiedBy = dto.LastModifiedBy;
        }

        public override string ToString()
        {
            return $"ID:{ID} - Name:{FirstName} {LastName} - Role:{Enum.GetName(typeof(EmployeeRole), (EmployeeRole)Role)} - UserUID:{UserUID} - Last Modified By:{LastModifiedBy}";
        }
    }
}
