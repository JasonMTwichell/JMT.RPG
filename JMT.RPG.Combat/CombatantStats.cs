namespace JMT.RPG.Combat
{
    public record CombatantStats
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public IEnumerable<CombatAbility> CombatAbilities { get; init; }
    }
}
