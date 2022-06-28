namespace JMT.RPG.Core.CharacterManagement
{
    public class CharacterSkill
    {
        public bool IsPurchased { get; init; }
        public int Tier { get; set; }
        public Ability Ability { get; set; } // TODO need to expand the concept of an to include passives that apply their effects perm in combat
    }
}
