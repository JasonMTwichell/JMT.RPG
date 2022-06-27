using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat.Combatants
{
    public record CombatantState
    {
        public int TotalHealth { get; init; }
        public int RemainingHealth { get; init; }
        public int Intellect { get; init; }
        public int Strength { get; init; }
        public int Speed { get; init; }
    }
}