using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueOfLegendsCharachters.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int Damage { get; set; }
        [Required]
        public int Armor { get; set; }
        [Required]
        public int MagicResist { get; set; }
        [Required]
        public int MovementSpeed { get; set; }
        [Required]
        public int AttackRange { get; set; }
        // Relationship
        public string CharachterName { get; set; } = null!;
        public Charachter Charachter { get; set; } = null!;
        
    }
}
