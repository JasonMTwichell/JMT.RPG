using JMT.RPG.Campaign;
using JMT.RPG.Combat;
using JMT.RPG.Core.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Campaign
{
    [TestClass]
    public class CampaignTests
    {
        //[TestMethod]
        //public async Task CampaignTest()
        //{
        //    CampaignContext ctx = new CampaignContext()
        //    {
        //        PlayerParty = new CampaignParty()
        //        {
        //            PartyCurrency = 0,
        //            CampaignPartyItems = new List<CampaignPartyItem>()
        //            {
        //                new CampaignPartyItem()
        //                {
        //                    ItemName = "HEALTH POTION",
        //                    ItemDescription = "HEALS YOU",
        //                    ItemID = "HEALTH_POTION",
        //                    Effects = new Effect[]
        //                    {
        //                        new Effect()
        //                        {
        //                            EffectedAttribute = "HEALTH",
        //                            EffectType = "ITEM",
        //                            Magnitude = 10,
        //                            MagnitudeFactor = -1,                                    
        //                        }
        //                    }
        //                }
        //            },
        //            PlayerParty = new List<CampaignCharacter>()
        //            {
        //                new CampaignCharacter()
        //                {
        //                    Id = "1",
        //                    Name = "TEST",
        //                    TotalHealth = 100,
        //                    RemainingHealth = 100,
        //                    Strength = 10,
        //                    Intellect = 10,
        //                    Speed = 10,
        //                    CombatAbilities = new CombatAbility[]
        //                    {
        //                        new CombatAbility()
        //                        {
        //                            // TODO: OOPS, REALIZED I GOOFED, NEED TO BE ABLE TO INJECT THE CORRECT IMPLEMENTATION IN WHICH MEANS WE NEED A HARD DEPENDENCY ON COMBAT LIBRARY IN CAMPAIGN LIBRARY MUST BE ADDED
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
