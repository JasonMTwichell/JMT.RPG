using JMT.RPG.Core.Domain;
using JMT.RPG.Core.Game.Domain;

namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatEncounterContext
    {
        public IEnumerable<CombatantContext> Combatants { get; set; }
        public IEnumerable<CombatItem> PlayerPartyCombatItems { get; set; }

    }
}
