﻿using JMT.RPG.Core.Game;

namespace JMT.RPG.Campaign
{
    public record CampaignPartyItem
    {
        public string ItemName;
        public string ItemDescription;
        public string ItemID;
        public IEnumerable<Effect> Effects;
    }
}