using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.Contracts.Campaign;

namespace JMT.RPG.Test.Campaign
{
    internal class DummyCampaignInputHandler : ICampaignInputHandler
    {
        public async Task<CampaignInputResult> GetInput(CampaignInputContext ctx)
        {
            return new CampaignInputResult();
        }
    }
}
