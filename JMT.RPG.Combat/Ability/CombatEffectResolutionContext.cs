using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat.Ability
{
    public record CombatEffectResolutionContext
    {
        public CombatEffect CombatEffect { get; init; }
        public string TargetId { get; init; }
        public int Strength { get; init; }
        public int Intellect { get; init; }
        public int Speed { get; init; }
    }
}
