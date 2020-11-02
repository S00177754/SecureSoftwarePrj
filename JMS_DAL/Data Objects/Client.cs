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

        public ClientDTO() 
        { 
        }

        public ClientDTO(Client client)
        {
            ID = client.ID.ToString();
            CompanyName = client.CompanyName;
            Address = client.Address;
        }

        
    }

    public class Client
    {
        public Guid ID;
        public string CompanyName;
        public string Address;

        public Client()
        {
        }

        public Client(ClientDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            CompanyName = dto.CompanyName;
            Address = dto.Address;
        }

        

    }
}
