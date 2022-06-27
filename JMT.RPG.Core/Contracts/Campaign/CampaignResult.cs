namespace JMT.RPG.Core.Contracts.Campaign
{
    public class CampaignResult
    {
        public bool CampaignCompleted { get; set; }
        public int PlayerPartyCurrency { get; set; }
        public List<CampaignPartyItem> PlayerPartyItems { get; set; }
    }
}