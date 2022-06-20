using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Campaign
{
    public class CampaignParty
    {
        public int PartyCurrency { get; set; }
        public IEnumerable<CampaignCharacter> CampaignCharacters { get; set; }
        public List<CampaignPartyItem> CampaignPartyItems { get; set; }
    }
}
