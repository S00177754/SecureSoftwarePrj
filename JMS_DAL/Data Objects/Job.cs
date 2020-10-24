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
    }

    public class Job
    {
        public Guid ID;
        public string Name;
        public Guid ClientID;
        public List<Guid> EquipmentList;

        public void Transfer(JobDTO data)
        {
            ID = Guid.Parse(data.ID);
            Name = data.Name;
            ClientID = Guid.Parse(data.ClientID);
            data.EquipmentList.ForEach(d => EquipmentList.Add(Guid.Parse(d)));
        }
    }
}
