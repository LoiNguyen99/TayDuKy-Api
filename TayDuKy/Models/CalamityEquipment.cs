using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class CalamityEquipment
    {
        public int CalamityId { get; set; }
        public int EquipmentId { get; set; }

        public int quantity { get; set; }

        public Calamity Calamity { get; set; }

        public  Equipment Equipment { get; set; }
    }
}
