using JMT.RPG.Core.Combat;

namespace JMT.RPG.Combat.Ability
{
    public record CombatAbilityResolutionContext
    {
        public string TargetID { get; init; }
        public int Strength { get; init; }
        public int Intellect { get; init; }
        public int Speed { get; init; }
        public CombatAbility CombatAbility { get; init; }

    }
}
