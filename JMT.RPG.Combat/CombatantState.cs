namespace JMT.Roguelike.Combat
{
    public record CombatantState
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public CombatAbility[] CombatAbilities { get; set; }
    }
}