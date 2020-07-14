using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class CalamityCharacter
    {
        public int CalamityId { get; set; }
        public int CharacterId { get; set; }

        public Character Character { get; set; }
        public Calamity Calamity { get; set; }
    }
}
