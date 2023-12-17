namespace JMT.RPG.Core.Combat
{
    public record CombatantContext
    {
        public string CombatantID { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public int TotalHealth { get; init; }
        public int RemainingHealth { get; init; }
        public int Intellect { get; init; }
        public int Strength { get; init; }
        public int Speed { get; init; }
        public bool IsEnemyCombatant { get; init; }
        public IEnumerable<CombatAbility> CombatAbilities { get; init; }
    }
}