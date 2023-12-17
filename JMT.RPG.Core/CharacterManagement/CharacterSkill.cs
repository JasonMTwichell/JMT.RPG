namespace JMT.RPG.Core.CharacterManagement
{
    public class CharacterSkill
    {
        public bool IsLearned { get; init; }
        public int Tier { get; init; }
        public Ability Ability { get; init; } // TODO need to expand the concept of an to include passives that apply their effects perm in combat
    }
}
