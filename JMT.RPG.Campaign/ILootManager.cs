namespace JMT.RPG.Campaign
{
    public interface ILootManager
    {
        IEnumerable<CampaignLoot> RollForLoot(CampaignEventLootTable lootTable, int numRolls);
    }

    public class CampaignLootManager : ILootManager
    {
        public IEnumerable<CampaignLoot> RollForLoot(CampaignEventLootTable lootTable, int numRolls)
        {
            int minRoll = lootTable.Loot.Select(r => r.Key).Min();
            int maxRoll = lootTable.Loot.Select(r => r.Key).Max();
            Random random = new Random();            

            for(int i = 0; i < numRolls; i++)
            {
                int roll = random.Next(minRoll, maxRoll);
                CampaignLoot loot = lootTable.Loot.FirstOrDefault(l => l.Key == roll);
                if (loot != null) yield return loot;
            }
        }
    }
}