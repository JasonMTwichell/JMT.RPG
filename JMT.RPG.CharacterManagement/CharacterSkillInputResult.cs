using JMT.RPG.Core.CharacterManagement;

namespace JMT.RPG.CharacterManagement
{
    public record CharacterSkillInputResult
    {
        public int CurrencySpent { get; init; }
        public CharacterSkill? PurchasedSkill { get; init; }
    }
}
