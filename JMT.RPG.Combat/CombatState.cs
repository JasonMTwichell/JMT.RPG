namespace JMT.RPG.Combat
{
    public record CombatState
    {
        public int TurnNumber { get; set; }
        public CombatantState[] CombatantStates { get; set; }        
    }
}