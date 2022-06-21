using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Combat
{
    [TestClass]
    public class CombatManagerTests
    {
        [TestMethod]
        public async Task TestCombat()
        {
            Combatant[] playerParty = new Combatant[]
            {
                new Combatant(
                    new DummyCombatInputHandler("1", "2"),
                    new CombatAbilityManager(new CombatAbility[]
                    {
                        new CombatAbility()
                        {
                            CombatAbilityId = "1",
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
                    Id = "1",                    
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
                                CombatAbilityId = "2",
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
                        Id = "2",
                        Name = "TEST ENEMY",                        
                       
                }
            };

            CombatManager combatManager = new CombatManager(playerParty, enemyParty);

            CombatResult result = await combatManager.PerformCombat();

            // player is doing 10 damage per turn to 50 hp enemy 
            Assert.AreEqual(5, result.FinalTurn);
        }

        [TestMethod]
        public async Task TestCombat_ForwardEffect()
        {
            Combatant[] playerParty = new Combatant[]
        {
                new Combatant(
                    new DummyCombatInputHandler("1", "2"),
                    new CombatAbilityManager(new CombatAbility[]
                    {
                        new CombatAbility()
                        {
                            CombatAbilityId = "1",
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
                                    ForwardEffect = new CombatEffect()
                                    {
                                        EffectedAttribute = "HEALTH",
                                        EffectType = EffectType.PHYSICAL,
                                        Magnitude = 0,
                                        MagnitudeFactor = -1,
                                    },
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
                    }
                )
                {
                    Id = "1",                    
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
                            CombatAbilityId = "2",
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
                        Id = "2",
                        Name = "TEST ENEMY",                                               
                }
             };

            CombatManager combatManager = new CombatManager(playerParty, enemyParty);

            CombatResult result = await combatManager.PerformCombat();

            // player is doing 10 damage first turn, then 20 the next two
            Assert.AreEqual(3, result.FinalTurn);
        }
    }
}