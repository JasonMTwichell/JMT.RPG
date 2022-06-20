namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatResult
    {
        public int FinalTurn { get; set; }
        public bool PlayerPartyWon { get; set; }
    }
}
