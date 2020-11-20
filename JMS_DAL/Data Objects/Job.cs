using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class JobDTO:DTO
    {
        public string Name { get { return name; } set { name = value; } }
        private string name;

        public string ClientID { get { return clientID; } set { clientID = value; } }
        private string clientID;

        public List<string> EquipmentList { get { return equipmentList; } set { equipmentList = value; } }
        private List<string> equipmentList;

        public JobDTO()
        {
        }

        public JobDTO(Job job)
        {
            ID = job.ID.ToString();
            Name = job.Name;
            ClientID = job.ClientID.ToString();
            job.EquipmentList.ForEach(e => EquipmentList.Add(e.ToString()));
            LastModifiedBy = job.LastModifiedBy;
        }

        public override string ToString()
        {
            return $"ID:{ID} - Name:{Name} - Client ID:{ClientID} - Last Modified By:{LastModifiedBy}";
        }
    }

    public class Job : DataObject
    {
        public string Name { get { return name; } set { name = value; } }
        private string name;

        public Guid ClientID { get { return clientID; } set { clientID = value; } }
        private Guid clientID;

        public List<Guid> EquipmentList { get { return equipmentList; } set { equipmentList = value; } }
        private List<Guid> equipmentList;

        public Job()
        {
        }

        public Job(JobDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            Name = dto.Name;
            ClientID = Guid.Parse(dto.ClientID);
            dto.EquipmentList.ForEach(d => EquipmentList.Add(Guid.Parse(d)));
            LastModifiedBy = dto.LastModifiedBy;
        }

        public override string ToString()
        {
            return $"ID:{ID} - Name:{Name} - Client ID:{ClientID} - Last Modified By:{LastModifiedBy}";
        }
    }
}
