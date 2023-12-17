using JMT.RPG.Core.Campaign;

namespace JMT.RPG.Campaign
{
    public interface ICampaignLootManager
    {
        IEnumerable<CampaignPartyItem> RollForLoot(CampaignEventLootTable lootTable, int numRolls);
    }
}