using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class EquipmentDTO
    {
        public string ID;
        public string Name;
        public string Manufacturer;
        public string Model;
        public int Amount;
    }

    public class Equipment
    {
        public Guid ID;
        public string Name;
        public string Manufacturer;
        public string Model;
        public int Amount;

        public void Transfer(EquipmentDTO data)
        {
            ID = Guid.Parse(data.ID);
            Name = data.Name;
            Manufacturer = data.Manufacturer;
            Model = data.Model;
            Amount = data.Amount;
        }
    }
}
