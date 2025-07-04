using LeagueOfLegendsCharachters.Models.Enums;

namespace LeagueOfLegendsCharachters.DTO.CharachterDTO
{
    public class PostCharachterDTO
    {
        public required string Name { get; set; }
        public required RolePositionEnum RolePosition { get; set; }
        public required int BlueEssence { get; set; }
        //Relationship
        public DisplayStatusDTO? Status { get; set; }
    }
}
