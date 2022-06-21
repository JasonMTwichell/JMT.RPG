using System.Collections.Generic;
using System.Linq;
using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JMT.RPG.Test.Combat
{
    [TestClass]
    public class CombatAbilityManagerTests
    {
        [TestMethod]
        public void TestSingleResolvedEffect()
        {
            CombatAbility[] abilities = new CombatAbility[]
            {
                new CombatAbility()
                {
                    CombatAbilityId = "1",
                    Cooldown = 3,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,
                        }
                    }
                }
            };

            ICombatAbilityManager abilityManager = new CombatAbilityManager(abilities);

            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = "1",
                TargetId = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            ResolvedEffect[] resolvedEffects = abilityManager.ResolveCombatAbility(ctx).ToArray();
            Assert.AreEqual(1, resolvedEffects.Count());

            ResolvedEffect resolvedEffect = resolvedEffects.First();
            Assert.AreEqual(-10, resolvedEffect.ResolvedMagnitude);
            Assert.AreEqual("1", resolvedEffect.TargetID);
            Assert.AreEqual(EffectedAttribute.HEALTH, resolvedEffect.EffectedAttribute);
            Assert.IsNull(resolvedEffect.ForwardEffect);
        }

        [TestMethod]
        public void TestForwardResolvedEffect()
        {
            CombatAbility[] abilities = new CombatAbility[]
            {
                new CombatAbility()
                {
                    CombatAbilityId = "1",
                    Cooldown = 3,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,
                            ForwardEffect = new CombatEffect()
                            {
                                 EffectType = EffectType.PHYSICAL,
                                EffectedAttribute = EffectedAttribute.HEALTH,
                                Magnitude = 0,
                                MagnitudeFactor = -1,
                            }
                        }
                    }
                }
            };

            ICombatAbilityManager abilityManager = new CombatAbilityManager(abilities);

            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = "1",
                TargetId = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            ResolvedEffect[] resolvedEffects = abilityManager.ResolveCombatAbility(ctx).ToArray();
            Assert.AreEqual(1, resolvedEffects.Count());

            ResolvedEffect resolvedEffect = resolvedEffects.First();
            Assert.AreEqual(-10, resolvedEffect.ResolvedMagnitude);
            Assert.AreEqual("1", resolvedEffect.TargetID);
            Assert.AreEqual(EffectedAttribute.HEALTH, resolvedEffect.EffectedAttribute);
            Assert.IsNotNull(resolvedEffect.ForwardEffect);

            ResolvedEffect forwardEffect = resolvedEffect.ForwardEffect;
            Assert.AreEqual(-10, forwardEffect.ResolvedMagnitude);
            Assert.AreEqual("1", forwardEffect.TargetID);
            Assert.AreEqual(EffectedAttribute.HEALTH, forwardEffect.EffectedAttribute);
            Assert.IsNull(forwardEffect.ForwardEffect);
        }

        [TestMethod]
        public void TestRemainingCooldownApplied()
        {
            CombatAbility[] abilities = new CombatAbility[]
            {
                new CombatAbility()
                {
                    CombatAbilityId = "1",
                    Cooldown = 3,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,                            
                        }
                    }
                }
            };

            ICombatAbilityManager abilityManager = new CombatAbilityManager(abilities);

            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = "1",
                TargetId = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            abilityManager.ResolveCombatAbility(ctx);
            IEnumerable<CombatAbility> combatAbilities = abilityManager.GetCombatAbilities();
            Assert.AreEqual(1, combatAbilities.Count());

            CombatAbility combatAbility = combatAbilities.First();
            Assert.AreEqual(3, combatAbility.Cooldown);            
        }

        [TestMethod]
        public void TestCooldownErrorThrown()
        {
            CombatAbility[] abilities = new CombatAbility[]
            {
                new CombatAbility()
                {
                    CombatAbilityId = "1",
                    RemainingCooldown = 3,
                    Cooldown = 3,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,
                        }
                    }
                }
            };

            ICombatAbilityManager abilityManager = new CombatAbilityManager(abilities);

            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = "1",
                TargetId = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            Assert.ThrowsException<AbilityCooldownException>(() => abilityManager.ResolveCombatAbility(ctx).ToArray());
        }

        [TestMethod]
        public void TestCooldownApplied()
        {
            CombatAbility[] abilities = new CombatAbility[]
            {
                new CombatAbility()
                {
                    CombatAbilityId = "1",
                    RemainingCooldown = 0,
                    Cooldown = 3,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,
                        }
                    }
                },
                new CombatAbility()
                {
                    CombatAbilityId = "2",
                    RemainingCooldown = 0,
                    Cooldown = 0,
                    Effects = new CombatEffect[]
                    {
                        new CombatEffect()
                        {
                            EffectType = EffectType.PHYSICAL,
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            Magnitude = 0,
                            MagnitudeFactor = -1,
                        }
                    }
                }
            };

            ICombatAbilityManager abilityManager = new CombatAbilityManager(abilities);

            abilityManager.ApplyCooldownAllAbilities(1);
            Assert.IsTrue(abilityManager.GetCombatAbilities().All(ca => ca.RemainingCooldown == 1));

            abilityManager.ApplyCooldownAllAbilities(-1);
            Assert.IsTrue(abilityManager.GetCombatAbilities().All(ca => ca.RemainingCooldown == 0));

            abilityManager.ApplyCooldown("1", 2);
            Assert.IsTrue(abilityManager.GetCombatAbilities().First(ca => ca.CombatAbilityId == "1").RemainingCooldown == 2);
            Assert.IsTrue(abilityManager.GetCombatAbilities().First(ca => ca.CombatAbilityId == "2").RemainingCooldown == 0);
        }
    }
}
