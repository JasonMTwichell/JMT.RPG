namespace JMT.RPG.Core.Combat
{
    public record CombatItem
    {
        public string CombatItemID { get; init; }
        public string CombatItemName { get; init; }
        public string CombatItemDescription { get; init; }
        public string CombatItemFlavorText { get; init; }
        public IEnumerable<CombatEffect> Effects { get; init; }
    }
}
