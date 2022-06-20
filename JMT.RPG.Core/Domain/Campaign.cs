using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Game.Domain
{
    public class Campaign
    {
        public ICollection<CampaignEvent> CombatEvents { get; set; }
    }
}
