namespace JMT.RPG.Campaign
{
    public class CampaignLoot
    {
        public int Key { get; set; }        
        public IEnumerable<CampaignPartyItem> CampaignPartyItems { get; set; }
    }
}