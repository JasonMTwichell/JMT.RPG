using JMT.RPG.Campaign;
using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;
using JMT.RPG.Core.Game;
using JMT.RPG.Core.Game.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampaignEvent = JMT.RPG.Campaign.CampaignEvent;

namespace JMT.RPG.Test.Campaign
{
    [TestClass]
    public class CampaignTests
    {
        [TestMethod]
        public async Task CampaignTest()
        {
            CampaignContext campaignCtx = new CampaignContext()
            {
                CampaignEvents = new CampaignEvent[]
                {
                    new CampaignEvent()
                    {
                        NumberOfLootRolls = 1,
                        EventSequence = 1,
                        IsCombatEvent = true,
                        AwardedCurrency = 100,
                        LootTable = new CampaignEventLootTable()
                        {
                            Loot = new CampaignLoot[]
                            {
                                new CampaignLoot()
                                {
                                    Key = 1,
                                    CampaignPartyItems = new CampaignPartyItem[]
                                    {
                                        new CampaignPartyItem()
                                        {
                                            ItemID = "HEALTH_POTION",
                                            ItemDescription = "RESTORES HEALTH",
                                            ItemName = "HEALTH POTION",
                                            Effects = new Effect[]
                                            {
                                                new Effect()
                                                {
                                                    EffectType = "NONE",
                                                    Magnitude = 10,
                                                    MagnitudeFactor = -1,
                                                    EffectedAttribute = EffectedAttribute.HEALTH,
                                                },
                                            }
                                        }
                                    },

                                },
                                new CampaignLoot()
                                {
                                    Key = 2,
                                    CampaignPartyItems = new CampaignPartyItem[]
                                    {
                                        new CampaignPartyItem()
                                        {
                                            ItemID = "HEALTH_POTION",
                                            ItemDescription = "RESTORES HEALTH",
                                            ItemName = "HEALTH POTION",
                                            Effects = new Effect[]
                                            {
                                                new Effect()
                                                {
                                                    EffectType = "NONE",
                                                    Magnitude = 10,
                                                    MagnitudeFactor = -1,
                                                    EffectedAttribute = EffectedAttribute.HEALTH,
                                                },
                                            }
                                        }
                                    },

                                },
                                new CampaignLoot()
                                {
                                    Key = 3,
                                    CampaignPartyItems = new CampaignPartyItem[]
                                    {
                                        new CampaignPartyItem()
                                        {
                                            ItemID = "HEALTH_POTION",
                                            ItemDescription = "RESTORES HEALTH",
                                            ItemName = "HEALTH POTION",
                                            Effects = new Effect[]
                                            {
                                                new Effect()
                                                {
                                                    EffectType = "NONE",
                                                    Magnitude = 10,
                                                    MagnitudeFactor = -1,
                                                    EffectedAttribute = EffectedAttribute.HEALTH,
                                                },
                                            }
                                        }
                                    },

                                }
                            }
                        }
                    }
                },
                PlayerPartyCurrency = 0,
                PlayerPartyItems = new List<CampaignPartyItem>(),
            };


            Combatant[] playerParty = new Combatant[]
            {
                new Combatant(
                    new DummyCombatInputHandler("1", "2"),
                    new CombatAbilityManager(new CombatAbility[]
                    {
                        new CombatAbility()
                        {
                            CombatAbilityID = "1",
                            Name = "TEST ABILITY",
                            Description = "TEST ABILITY",
                            Cooldown = 0,
                            RemainingCooldown = 0,
                            Effects = new CombatEffect[]
                            {
                                new CombatEffect()
                                {
                                    EffectedAttribute = "HEALTH",
                                    EffectType = EffectType.PHYSICAL,
                                    Magnitude = 0,
                                    MagnitudeFactor = -1,
                                }
                            }
                        }
                    }),
                    new CombatantStateManager()
                    {
                        TotalHealth = 100,
                        RemainingHealth = 100,
                        Strength = 10,
                        Intellect = 10,
                        Speed = 10,
                    })
                {
                    CombatantID = "1",
                    Name = "TEST PLAYER",
                }
            };

            Combatant[] enemyParty = new Combatant[]
            {
                new Combatant(
                    new DummyCombatInputHandler("2", "2"),
                    new CombatAbilityManager(new CombatAbility[]
                        {
                            new CombatAbility()
                            {
                                CombatAbilityID = "2",
                                Name = "TEST ABILITY",
                                Description = "TEST ABILITY",
                                Cooldown = 0,
                                RemainingCooldown = 0,
                                Effects = new CombatEffect[]
                                {
                                    new CombatEffect()
                                    {
                                        EffectedAttribute = EffectedAttribute.HEALTH,
                                        EffectType = EffectType.PHYSICAL,
                                        MagnitudeFactor = -1,
                                        Magnitude = 1,
                                    }
                                }
                            }
                        }),
                    new CombatantStateManager()
                    {
                        Speed = 1,
                        Strength = 1,
                        Intellect = 1,
                        TotalHealth = 50,
                        RemainingHealth= 50,
                    }
                )
                {
                        CombatantID = "2",
                        Name = "TEST ENEMY",

                }
            };

            CombatManager combatMgr = new CombatManager(playerParty, enemyParty);

            CampaignManager campaignMgr = new CampaignManager(combatMgr, new DummyCampaignInputHandler(), new CampaignLootManager());
            CampaignResult result = await campaignMgr.PerformCampaign(campaignCtx);
            Assert.IsNotNull(result);
        }
    }
}
