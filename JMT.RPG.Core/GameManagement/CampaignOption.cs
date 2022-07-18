namespace JMT.RPG.Core.GameManagement
{
    public record CampaignOption
    {
        public string CampaignID { get; init; }
        public string CampaignTitle { get; init; }
        public string CampaignDescription { get; init; }        
    }
}
