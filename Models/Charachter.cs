using LeagueOfLegendsCharachters.DTO.CharachterDTO;
using LeagueOfLegendsCharachters.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueOfLegendsCharachters.Models
{
    public class Charachter
    {
        [Key]
        public string Name { get; set; } = null!;
        [MaxLength(10)]
        [Required]
        public required RolePositionEnum RolePosition { get; set; }
        [Required]
        public required int BlueEssence { get; set; }
        //Relationship
        public Status Status { get; set; } = null!;

    }
}
