using JMT.RPG.Core.Combat;

namespace JMT.RPG.Core.Campaign
{
    public record CampaignContext
    {
        public int PlayerPartyCurrency { get; init; }
        public IEnumerable<CampaignCharacter> PlayerParty { get; init; }
        public IEnumerable<CampaignEvent> CampaignEvents { get; init; }
        public IEnumerable<CampaignPartyItem> PlayerPartyItems { get; init; }
    }
}
