using LeagueOfLegendsCharachters.Models;

namespace LeagueOfLegendsCharachters.DTO.CharachterDTO
{
    public class DisplayStatusDTO
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int MagicResist { get; set; }
        public int MovementSpeed { get; set; }
        public int AttackRange { get; set; }
    }
}
