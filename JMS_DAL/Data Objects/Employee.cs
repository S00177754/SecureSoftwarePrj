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
    }

    public class Employee
    {
        public Guid ID;
        public string FirstName;
        public string LastName;
        public EmployeeRole Role;
        public string UserUID;

        public void Transfer(EmployeeDTO data)
        {
            ID = Guid.Parse(data.ID);
            FirstName = data.FirstName;
            LastName = data.LastName;
            Role = (EmployeeRole)data.Role;
            UserUID = data.UserUID;
        }
    }
}
