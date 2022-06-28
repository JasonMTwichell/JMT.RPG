using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Campaign
{
    public interface ICampaignManager
    {
        Task<CampaignResult> PerformCampaign(CampaignContext ctx);
    }
}
