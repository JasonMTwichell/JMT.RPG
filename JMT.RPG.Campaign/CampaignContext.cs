﻿using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Campaign
{
    public record CampaignContext
    {
        public IEnumerable<CampaignEvent> CampaignEvents { get; set; }
        public CampaignParty PlayerParty { get; set; }
    }
}