using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    public enum EmployeeRole { Staff, Manager, CEO, Admin }

    [Serializable]
    public class EmployeeDTO
    {
        public string ID;
        public string FirstName;
        public string LastName;
        public int Role;
        public string UserUID;

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
        }
    }

    public class Employee
    {
        public Guid ID;
        public string FirstName;
        public string LastName;
        public EmployeeRole Role;
        public string UserUID;

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
        }

    }
}
