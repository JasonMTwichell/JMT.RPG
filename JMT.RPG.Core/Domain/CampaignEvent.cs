namespace JMT.RPG.Core.Game.Domain
{
    public class CampaignEvent
    {
        public bool IsCombatEvent { get; set; }
        public int EventSequence { get; set; }
        public int NumberOfLootRolls { get; set; }
        public ICollection<Character> EnemyParty { get; set; }
        public LootTable LootTable { get; set; }
        public ICollection<string> PrecombatDialog { get; set; }
        public ICollection<string> PostCombatDialog { get; set; }
    }
}