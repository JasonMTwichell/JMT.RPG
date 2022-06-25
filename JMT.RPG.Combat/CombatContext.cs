using JMT.RPG.Combat.Combatants;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public record CombatContext
    {
        public int TurnNumber { get; init; }
        public CombatantState[] CombatantContexts { get; init; }        
    }
}