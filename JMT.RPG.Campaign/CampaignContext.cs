using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Campaign
{
    public class CampaignContext
    {
        public IEnumerable<CampaignEvent> CampaignEvents { get; set; }
        public int PlayerPartyCurrency { get; set; }
        public IEnumerable<Combatant> PlayerParty { get; set; }
        public List<CampaignPartyItem> PlayerPartyItems { get; set; }
    }
}
