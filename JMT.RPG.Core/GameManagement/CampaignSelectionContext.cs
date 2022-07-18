namespace JMT.RPG.Core.GameManagement
{
    public record CampaignSelectionContext
    {
        public IEnumerable<CampaignOption> AvailableCampaigns { get; init; } 
    }
}
