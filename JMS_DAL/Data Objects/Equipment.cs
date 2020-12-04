// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS_DAL.Data_Objects
{
    [Serializable]
    public class EquipmentDTO:DTO
    {
        
        public string Name { get { return name; } set { name = value; } }
        private string name;

        public string Manufacturer { get { return manufacturer; } set { manufacturer = value; } }
        private string manufacturer;

        public string Model { get { return model; } set { model = value; } }
        private string model;

        public int Amount { get { return amount; } set { amount = value; } }
        private int amount;

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
            LastModifiedBy = equipment.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"ID:{ID} - Name:{Name} - Manufacturer:{Manufacturer} - Model:{Model} - Amount:{Amount} - Last Modified By:{LastModifiedBy}");
        }
    }

    public class Equipment:DataObject
    {
        public string Name { get { return name; } set { name = value; } }
        private string name;

        public string Manufacturer { get { return manufacturer; } set { manufacturer = value; } }
        private string manufacturer;

        public string Model { get { return model; } set { model = value; } }
        private string model;

        public int Amount { get { return amount; } set { amount = value; } }
        private int amount;


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
            LastModifiedBy = dto.LastModifiedBy;
        }

        public override string ToString()
        {
            return string.Concat($"ID:{ID} - Name:{Name} - Manufacturer:{Manufacturer} - Model:{Model} - Amount:{Amount} - Last Modified By:{LastModifiedBy}");
        }

    }
}
