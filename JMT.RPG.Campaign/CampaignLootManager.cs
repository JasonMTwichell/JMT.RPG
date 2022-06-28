using JMT.RPG.Core.Campaign;

namespace JMT.RPG.Campaign
{
    public class CampaignLootManager : ICampaignLootManager
    {
        public IEnumerable<CampaignPartyItem> RollForLoot(CampaignEventLootTable lootTable, int numRolls)
        {
            int minRoll = 0;
            int maxRoll = lootTable.Loot.Count();
            Random random = new Random();

            for(int i = 0; i < numRolls; i++)
            {
                int lootIndex = random.Next(minRoll, maxRoll);
                CampaignLoot loot = lootTable.Loot.ElementAt(lootIndex);
                if(loot.CampaignPartyItem != null && loot.NumItemAwarded > 0)
                {
                    for(int l = 0; l < loot.NumItemAwarded; l++)
                    {
                        yield return loot.CampaignPartyItem;
                    }
                }
            }
        }
    }
}