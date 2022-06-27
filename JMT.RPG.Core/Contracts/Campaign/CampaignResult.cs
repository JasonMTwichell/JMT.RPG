namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignResult
    {
        public bool CampaignCompleted { get; init; }
        public int PlayerPartyCurrency { get; init; }
        public List<CampaignPartyItem> PlayerPartyItems { get; init; }
    }
}