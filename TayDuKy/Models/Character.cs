using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TayDuKy.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public String CharacterName { get; set; }
        public String DocumentUrl { get; set; }
        public String UserId { get; set; }

        public bool IsDelete { get; set; }
        public User User { get; set; }
        public ICollection<CalamityCharacter> CalamityCharacters { get; set; }
    }
}
