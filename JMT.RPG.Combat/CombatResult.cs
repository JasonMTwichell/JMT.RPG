namespace JMT.RPG.Combat
{
    public record CombatResult
    {
        public int FinalTurn { get; set; }
        public bool PlayerPartyWon { get; set; }
    }
}
