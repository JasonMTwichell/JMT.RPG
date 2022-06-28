namespace JMT.RPG.Core.Combat
{
    public record CombatEncounterContext
    {
        public IEnumerable<CombatantContext> Combatants { get; init; }
        public IEnumerable<CombatItem> PlayerPartyCombatItems { get; init; }
    }
}
