using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Campaign
{
    public class CampaignDialog
    {
        public bool IsPreCombat { get; set; }
        public string CampaignId { get; set; }
        public string Dialog { get; set; }
        public int DialogSequence { get; set; }
    }
}
