using JMT.RPG.Core.Contracts.Campaign;

namespace JMT.RPG.Campaign
{
    public interface ILootManager
    {
        IEnumerable<CampaignPartyItem> RollForLoot(CampaignEventLootTable lootTable, int numRolls);
    }
}