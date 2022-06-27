using JMT.RPG.Campaign;
using JMT.RPG.Core.Contracts.Campaign;
using JMT.RPG.Core.Game.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
