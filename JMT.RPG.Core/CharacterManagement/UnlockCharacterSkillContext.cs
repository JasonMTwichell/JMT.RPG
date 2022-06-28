namespace JMT.RPG.Core.CharacterManagement
{
    public record UnlockCharacterSkillContext
    {
        public int Currency { get; init; }
        public CharacterSkillCopse CharacterSkillCopse { get; init; }        
    }
}
