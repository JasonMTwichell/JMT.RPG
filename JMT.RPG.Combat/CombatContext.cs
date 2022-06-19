namespace JMT.RPG.Combat
{
    public record CombatContext
    {
        public int TurnNumber { get; set; }
        public CombatantContext[] CombatantContexts { get; set; }        
    }
}