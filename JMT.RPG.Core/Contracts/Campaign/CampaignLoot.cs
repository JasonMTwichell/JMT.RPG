namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignLoot
    {
        public int NumItemAwarded { get; init; }
        public CampaignPartyItem CampaignPartyItem { get; init; }
    }
}