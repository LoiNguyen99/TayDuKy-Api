﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class Calamity
    {
        public int CalamityId { get; set; }
        public String CalamityName { get; set; }
        public String Description { get; set; }
        public String FilmingLocation { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ShotQuantity { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CalamityEquipment> CalamityEquipment { get; set; }
        public ICollection<CalamityCharacter> CalamityCharacters { get; set; }
    }
}
