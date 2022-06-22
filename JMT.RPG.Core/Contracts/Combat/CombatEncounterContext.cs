using JMT.RPG.Core.Domain;
using JMT.RPG.Core.Game.Domain;

namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatEncounterContext
    {
        public IEnumerable<CombatantContext> EnemyPartyCombatants { get; set; }
        public IEnumerable<CombatantContext> PlayerPartyCombatants { get; set; }
        public IEnumerable<CombatItem> PlayerPartyCombatItems { get; set; }

    }
}
