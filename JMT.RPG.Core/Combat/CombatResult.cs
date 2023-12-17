namespace JMT.RPG.Core.Combat
{
    public record CombatResult
    {
        public int FinalTurnNum { get; init; }
        public bool PlayerPartyWon { get; init; }
        public IEnumerable<CombatantContext>? PlayerPartyCombatants { get; init; }
        public IEnumerable<CombatItem> RemainingPlayerPartyCombatItems { get; init; }
    }
}
