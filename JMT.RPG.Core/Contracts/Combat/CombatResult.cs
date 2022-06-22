namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatResult
    {
        public int FinalTurnNum { get; set; }
        public bool PlayerPartyWon { get; set; }
        public IEnumerable<CombatantContext>? PlayerPartyCombatants { get; set; }
    }
}
