using JMT.Roguelike.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace JMT.Roguelike.Test
{
    [TestClass]
    public class CombatTests
    {
        [TestMethod]
        public async Task TestCombat()
        {
            Combatant[] playerParty = new Combatant[]
            {
                new Combatant(new DummyInputHandler("1", "2"))
                {
                    Id = "1",
                    Name = "TEST PLAYER",
                    Level= 1,
                    Speed = 10,
                    Strength = 10,
                    Intellect = 10,
                    TotalHealth = 100,
                    RemainingHealth= 100,
                    CombatAbilities = new CombatAbility[]
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
                    }
                }
            };

            Combatant[] enemyParty = new Combatant[]
            {
                new Combatant(new DummyInputHandler("2", "2"))
                {
                    Id = "2",
                    Name = "TEST ENEMY",
                    Level= 1,
                    Speed = 1,
                    Strength = 1,
                    Intellect = 1,
                    TotalHealth = 50,
                    RemainingHealth= 50,
                    CombatAbilities = new CombatAbility[]
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
                                    EffectedAttribute = Attribute.HEALTH,
                                    EffectType = EffectType.PHYSICAL,
                                    MagnitudeFactor = -1,
                                    Magnitude = 1,
                                }
                            }
                        }
                    }
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
                new Combatant(new DummyInputHandler("1", "2"))
                {
                    Id = "1",
                    Name = "TEST PLAYER",
                    Level= 1,
                    Speed = 10,
                    Strength = 10,
                    Intellect = 10,
                    TotalHealth = 100,
                    RemainingHealth= 100,
                    CombatAbilities = new CombatAbility[]
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
                    }
                }
            };

            Combatant[] enemyParty = new Combatant[]
            {
                new Combatant(new DummyInputHandler("2", "2"))
                {
                    Id = "2",
                    Name = "TEST ENEMY",
                    Level= 1,
                    Speed = 1,
                    Strength = 1,
                    Intellect = 1,
                    TotalHealth = 50,
                    RemainingHealth= 50,
                    CombatAbilities = new CombatAbility[]
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
                                    EffectedAttribute = Attribute.HEALTH,
                                    EffectType = EffectType.PHYSICAL,
                                    MagnitudeFactor = -1,
                                    Magnitude = 1,
                                }
                            }
                        }
                    }
                }
            };

            CombatManager combatManager = new CombatManager(playerParty, enemyParty);

            CombatResult result = await combatManager.PerformCombat();

            // player is doing 10 damage first turn, then 20 the next two
            Assert.AreEqual(3, result.FinalTurn);
        }
    }
}