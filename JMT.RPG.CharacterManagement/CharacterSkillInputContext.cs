using JMT.RPG.Core.CharacterManagement;

namespace JMT.RPG.CharacterManagement
{
    public record CharacterSkillInputContext
    {
        public int CharacterID { get; init; }
        public int AvailableCurrency { get; init; }
        public CharacterSkillCopse CharacterSkillCopse { get; init; }
    }
}
