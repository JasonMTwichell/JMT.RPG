using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignContext
    {
        public int PlayerPartyCurrency { get; init; }
        public IEnumerable<CampaignEvent> CampaignEvents { get; init; }
        public List<CampaignPartyItem> PlayerPartyItems { get; init; }
    }
}
