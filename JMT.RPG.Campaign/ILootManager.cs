namespace JMT.RPG.Campaign
{
    public interface ILootManager
    {
        IEnumerable<CampaignLoot> RollForLoot(CampaignEventLootTable lootTable, int numRolls);
    }
}