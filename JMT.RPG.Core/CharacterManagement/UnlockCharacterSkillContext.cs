namespace JMT.RPG.Core.CharacterManagement
{
    public record UnlockCharacterSkillContext
    {
        public int CharacterID { get; init; }
        public int Currency { get; init; }
        public IEnumerable<CharacterSkill> KnownSkills { get; init; }
        public CharacterSkillCopse CharacterSkillCopse { get; init; }        
    }
}
