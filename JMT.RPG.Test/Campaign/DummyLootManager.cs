using JMT.RPG.Campaign;
using JMT.RPG.Core.Contracts.Campaign;
using System.Collections.Generic;

namespace JMT.RPG.Test.Campaign
{
    internal class DummyLootManager : ICampaignLootManager
    {
        private IEnumerable<CampaignPartyItem> _loot;
        public DummyLootManager(IEnumerable<CampaignPartyItem> loot)
        {
            _loot = loot;
        }

        public IEnumerable<CampaignPartyItem> RollForLoot(CampaignEventLootTable lootTable, int numRolls)
        {
            return _loot;
        }
    }
}