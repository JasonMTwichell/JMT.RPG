using System;
using System.Linq;
using System.Threading.Tasks;
using JMT.RPG.Campaign;
using JMT.RPG.Core.Campaign;
using JMT.RPG.Core.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CampaignEvent = JMT.RPG.Core.Campaign.CampaignEvent;


namespace JMT.RPG.Test.Campaign
{
    [TestClass]
    public class CampaignTests
    {
        [TestMethod]
        public async Task CampaignTest()
        {
            CampaignContext ctx = new CampaignContext()
            {
                PlayerPartyCurrency = 0,
                PlayerParty = new CampaignCharacter[]
                {
                    new CampaignCharacter()
                    {
                        CampaignCharacterID = "PLAYER",
                        Name = "PLAYER",
                        Level = 1,
                        TotalHealth = 100,
                        RemainingHealth = 100,
                        Strength = 10,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = Array.Empty<CombatAbility>(),
                    }
                },
                PlayerPartyItems = new CampaignPartyItem[]
                {
                    new CampaignPartyItem()
                    {
                        ItemID = "HEALTH_POTION",
                        ItemName = "HEALTH POTION",
                        ItemDescription = "HEALS 10 HP",
                        Effects = new ItemEffect[]
                        {
                            new ItemEffect()
                            {
                                EffectedAttribute = "HEALTH",
                                EffectType = "ITEM",
                                Magnitude = 10,
                                MagnitudeFactor = 1,
                            }
                        }
                    }
                },
                CampaignEvents = new CampaignEvent[]
                {
                    new CampaignEvent()
                    {
                        EventSequence = 1,
                        IsCombatEvent = true,
                        LootTable = null,
                        NumberOfLootRolls = 1,
                        AwardedCurrency = 100,
                        CampaignDialog = new CampaignEventDialog[]
                        {
                            new CampaignEventDialog()
                            {
                                Dialog = "TEST",
                                DialogSequence = 1,
                            },
                            new CampaignEventDialog()
                            {
                                Dialog = "TEST 2",
                                DialogSequence = 2,                             
                            }
                        },
                        EnemyParty = new CampaignCharacter[]
                        {
                            new CampaignCharacter()
                            {
                                CampaignCharacterID = "ENEMY",
                                Name = "ENEMY",
                                Level = 1,
                                TotalHealth = 50,
                                RemainingHealth = 50,
                                Strength = 10,
                                Intellect = 10,
                                Speed = 10,
                                CombatAbilities = Array.Empty<CombatAbility>(),
                            }
                        },
                    }
                }
            };

            ICombatManager combatMgr = new DummyCombatManager(new CombatResult()
            {
                FinalTurnNum = 10,
                PlayerPartyWon = true,
                RemainingPlayerPartyCombatItems = new CombatItem[]
                {
                    new CombatItem()
                    {
                        CombatItemID = "HEALTH_POTION",
                        CombatItemName = "HEALTH POTION",
                        CombatItemDescription = "HEALS 10 HP",
                        Effects = new CombatEffect[]
                        {
                            new CombatEffect()
                            {
                                EffectedAttribute = "HEALTH",
                                EffectType = "ITEM",
                                Magnitude = 10,
                                MagnitudeFactor = 1,
                            }
                        }
                    }
                },
                PlayerPartyCombatants = new CombatantContext[]
                {
                    new CombatantContext()
                    {
                        CombatantID = "PLAYER",
                        Name = "PLAYER",
                        Level = 1,
                        TotalHealth = 100,
                        RemainingHealth = 50,
                        Strength = 10,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = Array.Empty<CombatAbility>(),
                    }
                }
            });

            ICampaignInputHandler inputHandler = new DummyCampaignInputHandler();
            ICampaignLootManager lootMgr = new DummyLootManager(new CampaignPartyItem[]
            {
                new CampaignPartyItem()
                {
                    ItemID = "ITEM_FIREBOMB",
                    ItemName = "FIRE BOMB",
                    ItemDescription = "BOOM GOES THE DYNAMITE",
                    Effects = Array.Empty<ItemEffect>(),
                }
            });

            ICampaignManager campMgr = new CampaignManager(combatMgr, inputHandler, lootMgr);
            CampaignResult result = await campMgr.PerformCampaign(ctx);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CampaignCompleted);
            Assert.AreEqual(100, result.PlayerPartyCurrency);
            Assert.AreEqual(2, result.PlayerPartyItems.Count());
        }
    }
}
