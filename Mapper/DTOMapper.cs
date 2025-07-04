using LeagueOfLegendsCharachters.DTO.CharachterDTO;
using LeagueOfLegendsCharachters.Models;

namespace LeagueOfLegendsCharachters.Mapper
{
    public static class DTOMapper
    {
        public static DisplayCharachterDTO DisplayCharachterDTO(this Charachter model)
        {
            return new DisplayCharachterDTO
            {
                Name = model.Name,
                BlueEssence = model.BlueEssence,
                RolePosition = model.RolePosition,
                Status = new DisplayStatusDTO
                {
                    Health = model.Status.Health,
                    Armor = model.Status.Armor,
                    AttackRange = model.Status.AttackRange,
                    Damage = model.Status.Damage,
                    MagicResist = model.Status.MagicResist,
                    MovementSpeed = model.Status.MovementSpeed
                }
            };
        }
        public static DisplayStatusDTO ToDisplayStatusDTO(this Status model)
        {
            return new DisplayStatusDTO
            {
                Health = model.Health,
                Armor = model.Armor,
                AttackRange = model.AttackRange,
                Damage = model.Damage,
                MagicResist = model.MagicResist,
                MovementSpeed = model.MovementSpeed,
                CharachterName = model.CharachterName
            };
        }
        public static Charachter PostCharachterDTO(this PostCharachterDTO model)
        {
            return new Charachter
            {
                BlueEssence = model.BlueEssence,
                Name = model.Name,
                RolePosition = model.RolePosition,
                Status = new Status
                {
                    Armor = model.Status!.Armor,
                    AttackRange = model.Status.AttackRange,
                    Damage = model.Status.Damage,
                    Health = model.Status.Health,
                    MagicResist = model.Status.MagicResist,
                    MovementSpeed = model.Status.MovementSpeed
                }
            };
        }
    }
}
