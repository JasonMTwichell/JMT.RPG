﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Campaign
{
    public record CampaignEventDialog
    {
        public string Dialog { get; init; }
        public int DialogSequence { get; init; }
    }
}
