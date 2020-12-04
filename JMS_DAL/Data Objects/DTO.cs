// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    public class DTO
    {
        public string ID { get { return id; } set { id = value; } }
        private string id;

        public string LastModifiedBy { get { return lastModifiedBy; } set { lastModifiedBy = value; } }
        private string lastModifiedBy;

        public override string ToString()
        {
            return $"ID:{ID} - Last Modified By:{LastModifiedBy}";
        }
    }

    public class DataObject
    {
        public Guid ID { get { return id; } set { id = value; } }
        private Guid id;

        public string LastModifiedBy { get { return lastModifiedBy; } set { lastModifiedBy = value; } }
        private string lastModifiedBy;

        public override string ToString()
        {
            return $"ID:{ID} - Last Modified By:{LastModifiedBy}";
        }
    }
}
