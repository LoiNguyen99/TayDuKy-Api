﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public String EquipmentName { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        public String Status { get; set; }
        public ICollection<CalamityEquipment> CalamityEquipment { get; set; }
    }
}
