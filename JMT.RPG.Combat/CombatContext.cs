namespace JMT.RPG.Combat
{
    internal record CombatantContext
    {
        public int TurnNumber { get; set; }
        public CombatantState[] CombatantStates { get; set; }
        public CombatAbility[] CombatantAbilities { get; set; }
    }
}