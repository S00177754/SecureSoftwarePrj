using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class JobDTO
    {
        public string ID;
        public string Name;
        public string ClientID;
        public List<string> EquipmentList;

        public JobDTO()
        {
        }

        public JobDTO(Job job)
        {
            ID = job.ID.ToString();
            Name = job.Name;
            ClientID = job.ClientID.ToString();
            job.EquipmentList.ForEach(e => EquipmentList.Add(e.ToString()));
        }
    }

    public class Job
    {
        public Guid ID;
        public string Name;
        public Guid ClientID;
        public List<Guid> EquipmentList;

        public Job()
        {
        }

        public Job(JobDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            Name = dto.Name;
            ClientID = Guid.Parse(dto.ClientID);
            dto.EquipmentList.ForEach(d => EquipmentList.Add(Guid.Parse(d)));
        }

    }
}
