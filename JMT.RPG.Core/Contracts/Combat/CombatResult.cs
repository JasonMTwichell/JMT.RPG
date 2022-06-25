namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatResult
    {
        public int FinalTurnNum { get; init; }
        public bool PlayerPartyWon { get; init; }
        public IEnumerable<CombatantContext>? PlayerPartyCombatants { get; init; }
    }
}
