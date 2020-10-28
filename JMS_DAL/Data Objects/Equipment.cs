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

        public EquipmentDTO()
        {
        }

        public EquipmentDTO(Equipment equipment)
        {
            ID = equipment.ID.ToString();
            Name = equipment.Name;
            Manufacturer = equipment.Manufacturer;
            Model = equipment.Model;
            Amount = equipment.Amount;
        }
    }

    public class Equipment
    {
        public Guid ID;
        public string Name;
        public string Manufacturer;
        public string Model;
        public int Amount;

        public Equipment()
        {
        }

        public Equipment(EquipmentDTO dto)
        {
            ID = Guid.Parse(dto.ID);
            Name = dto.Name;
            Manufacturer = dto.Manufacturer;
            Model = dto.Model;
            Amount = dto.Amount;
        }

    }
}
