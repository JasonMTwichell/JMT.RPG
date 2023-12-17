using System.Linq;
using System.Threading.Tasks;
using JMT.RPG.Campaign;
using JMT.RPG.Core.Campaign;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMT.RPG.Test.Campaign
{
    [TestClass]
    public class LootManagerTests
    {
        [TestMethod]
        public async Task TestLootAwarded()
        {
            CampaignEventLootTable lootTable = new CampaignEventLootTable()
            {
                Loot = new CampaignLoot[]
                {
                    new CampaignLoot()
                    {
                        NumItemAwarded = 1,
                        CampaignPartyItem = new CampaignPartyItem()
                        {
                            ItemID = "A",
                            ItemName = "A",
                        }
                    },
                    new CampaignLoot()
                    {
                        NumItemAwarded = 1,
                        CampaignPartyItem = new CampaignPartyItem()
                        {
                            ItemID = "B",
                            ItemName = "B",
                        }
                    }
                }
            };

            ICampaignLootManager lootMgr = new CampaignLootManager();
            CampaignPartyItem[] lootedItems = lootMgr.RollForLoot(lootTable, 2).ToArray();

            Assert.AreEqual(2, lootedItems.Length);
        }
    }
}
