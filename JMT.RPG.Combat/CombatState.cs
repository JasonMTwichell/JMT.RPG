namespace JMT.Roguelike.Combat
{
    public record CombatState
    {
        public int TurnNumber { get; set; }
        public CombatantState[] CombatantStates { get; set; }        
    }
}