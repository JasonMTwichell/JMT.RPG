using JMT.RPG.Core.Game;
using JMT.RPG.Core.Game.Domain;

namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignPartyItem
    {
        public string ItemID { get; init; }
        public string ItemName { get; init; }
        public string ItemDescription { get; init; }
        public IEnumerable<ItemEffect> Effects { get; init; }
    }
}