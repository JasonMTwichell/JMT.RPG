using System;
using System.Threading.Tasks;
using JMT.RPG.Combat;
using JMT.RPG.Combat.Ability;
using JMT.RPG.Combat.Effect;
using JMT.RPG.Core.Contracts.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMT.RPG.Test.Combat
{
    [TestClass]
    public class CombatManagerTests
    {
        [TestMethod]
        public async Task TestCombat()
        {
            ICombatInputHandler io = new DummyCombatInputHandler("1");
            ICombatAbilityManager abilityMgr = new CombatAbilityManager();
            IResolvedEffectManager effMgr = new ResolvedEffectManager();
            CombatManager combatManager = new CombatManager(io, abilityMgr, effMgr);
            CombatEncounterContext ctx = new CombatEncounterContext()
            {
                Combatants = new CombatantContext[]
                {
                    new CombatantContext()
                    {
                        CombatantID = "1",
                        IsEnemyCombatant = false,
                        TotalHealth = 100,
                        RemainingHealth = 100,
                        Strength = 10,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = new CombatAbility[]
                        {
                            new CombatAbility()
                            {
                                CombatAbilityID = "1",
                                Cooldown = 0,
                                RemainingCooldown = 0,
                                Effects = new CombatEffect[]
                                {
                                    new CombatEffect()
                                    {
                                        EffectedAttribute = EffectedAttribute.HEALTH,
                                        EffectType = EffectType.PHYSICAL,
                                        Magnitude = 10,
                                        MagnitudeFactor = -1,
                                    }
                                }
                            }
                        }
                    },
                    new CombatantContext()
                    {
                        CombatantID = "2",
                        IsEnemyCombatant = true,
                        TotalHealth = 100,
                        RemainingHealth = 100,
                        Strength = 0,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = new CombatAbility[]
                        {
                            new CombatAbility()
                            {
                                CombatAbilityID = "1",
                                Cooldown = 0,
                                RemainingCooldown = 0,
                                Effects = new CombatEffect[]
                                {
                                    new CombatEffect()
                                    {
                                        EffectedAttribute = EffectedAttribute.HEALTH,
                                        EffectType = EffectType.PHYSICAL,
                                        Magnitude = 0,
                                        MagnitudeFactor = 1,
                                    }
                                }
                            }
                        }
                    }
                },
                PlayerPartyCombatItems = Array.Empty<CombatItem>(),
            };

            CombatResult result = await combatManager.PerformCombat(ctx);

            // player is doing 10 damage per turn to 50 hp enemy 
            Assert.AreEqual(5, result.FinalTurnNum);
        }

        [TestMethod]
        public async Task TestCombat_Carryforward()
        {
            ICombatInputHandler io = new DummyCombatInputHandler("1");
            ICombatAbilityManager abilityMgr = new CombatAbilityManager();
            IResolvedEffectManager effMgr = new ResolvedEffectManager();
            CombatManager combatManager = new CombatManager(io, abilityMgr, effMgr);
            CombatEncounterContext ctx = new CombatEncounterContext()
            {
                Combatants = new CombatantContext[]
                {
                    new CombatantContext()
                    {
                        CombatantID = "1",
                        IsEnemyCombatant = false,
                        TotalHealth = 100,
                        RemainingHealth = 100,
                        Strength = 10,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = new CombatAbility[]
                        {
                            new CombatAbility()
                            {
                                CombatAbilityID = "1",
                                Cooldown = 0,
                                RemainingCooldown = 0,
                                Effects = new CombatEffect[]
                                {
                                    new CombatEffect()
                                    {
                                        EffectedAttribute = EffectedAttribute.HEALTH,
                                        EffectType = EffectType.PHYSICAL,
                                        Magnitude = 0,
                                        MagnitudeFactor = -1,
                                        ForwardEffect = new CombatEffect()
                                        {
                                            EffectedAttribute = EffectedAttribute.HEALTH,
                                            EffectType = EffectType.PHYSICAL,
                                            Magnitude = 0,
                                            MagnitudeFactor = -1,
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new CombatantContext()
                    {
                        CombatantID = "2",
                        IsEnemyCombatant = true,
                        TotalHealth = 100,
                        RemainingHealth = 50,
                        Strength = 0,
                        Intellect = 10,
                        Speed = 10,
                        CombatAbilities = new CombatAbility[]
                        {
                            new CombatAbility()
                            {
                                CombatAbilityID = "1",
                                Cooldown = 0,
                                RemainingCooldown = 0,
                                Effects = new CombatEffect[]
                                {
                                    new CombatEffect()
                                    {
                                        EffectedAttribute = EffectedAttribute.HEALTH,
                                        EffectType = EffectType.PHYSICAL,
                                        Magnitude = 0,
                                        MagnitudeFactor = 1,
                                    }
                                }
                            }
                        }
                    }
                },
                PlayerPartyCombatItems = Array.Empty<CombatItem>(),
            };

            CombatResult result = await combatManager.PerformCombat(ctx);

            // player is doing 10 damage per turn to 50 hp enemy 
            Assert.AreEqual(3, result.FinalTurnNum);
        }

    }
}