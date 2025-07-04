using LeagueOfLegendsCharachters.Models;
using LeagueOfLegendsCharachters.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LeagueOfLegendsCharachters.DTO.CharachterDTO
{
    public class DisplayCharachterDTO
    {
        public string Name { get; set; } = null!;
        public required RolePositionEnum RolePosition { get; set; }
        public required int BlueEssence { get; set; }
        //Relationship
        public DisplayStatusDTO? Status { get; set; }
    }
}
