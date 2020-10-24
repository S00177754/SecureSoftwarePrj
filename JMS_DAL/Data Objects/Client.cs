using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class ClientDTO
    {
        public string ID;
        public string CompanyName;
        public string Address;
    }

    public class Client
    {
        public Guid ID;
        public string CompanyName;
        public string Address;

        public void Transfer(ClientDTO data)
        {
            ID = Guid.Parse(data.ID);
            CompanyName = data.CompanyName;
            Address = data.Address;
            
        }
    }
}
