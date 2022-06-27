using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignContext
    {
        public int PlayerPartyCurrency { get; init; }
        public IEnumerable<CampaignCharacter> PlayerParty { get; set; }
        public IEnumerable<CampaignEvent> CampaignEvents { get; init; }
        public IEnumerable<CampaignPartyItem> PlayerPartyItems { get; init; }
    }
}
