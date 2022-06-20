using JMT.RPG.Core.Game;

namespace JMT.RPG.Campaign
{
    public record CampaignPartyItem
    {
        internal string ItemName;
        internal string ItemDescription;
        internal string ItemID;
        internal IEnumerable<Effect> Effects;
    }
}