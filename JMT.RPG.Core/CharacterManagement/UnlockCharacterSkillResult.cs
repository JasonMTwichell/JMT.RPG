namespace JMT.RPG.Core.CharacterManagement
{
    public record UnlockCharacterSkillResult
    {
        public bool SkillWasPurchased { get; init; }
        public int CurrencySpent { get; init; }
        public CharacterSkill? LearnedSkill { get; init; }
    }
}
