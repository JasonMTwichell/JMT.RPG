using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Campaign
{
    public class CampaignContext
    {
        public IEnumerable<CampaignEvent> CampaignEvents { get; set; }
        public int PlayerPartyCurrency { get; set; }
        public List<CampaignPartyItem> PlayerPartyItems { get; set; }
    }
}
