// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class ClientDTO : DTO
    {
        
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        private string companyName;
        public string Address { get { return address; } set { address = value; } }
        private string address;

        

        public ClientDTO() 
        {
           
        }

        public ClientDTO(Client client)
        {
            ID = client.ID.ToString();
            CompanyName = client.CompanyName;
            Address = client.Address;
            LastModifiedBy = client.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"ID:{ID} - Company:{CompanyName} - Address:{Address} - Last Modified By:{LastModifiedBy}");
        }

    }


    public class Client: DataObject
    {
        public string CompanyName { get { return companyName; } set { companyName = value; } }
        private string companyName;

        public string Address { get { return address; } set { address = value; } }
        private string address;


        public Client()
        {
        }

        public Client(ClientDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            CompanyName = dto.CompanyName;
            Address = dto.Address;
            LastModifiedBy = dto.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"ID:{ID} - Company:{CompanyName} - Address:{Address} - Last Modified By:{LastModifiedBy}");
        }
    }

}
