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
            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                TargetID = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
                CombatAbility = new CombatAbility()
                {
                    CombatAbilityID = "1",
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

            ICombatAbilityManager abilityManager = new CombatAbilityManager();

            ResolvedEffect[] resolvedEffects = abilityManager.ResolveCombatAbility(ctx).ToArray();
            Assert.AreEqual(1, resolvedEffects.Count());

            ResolvedEffect resolvedEffect = resolvedEffects.First();
            Assert.AreEqual(-10, resolvedEffect.ResolvedMagnitude);
            Assert.AreEqual("1", resolvedEffect.TargetID);
            Assert.AreEqual(EffectedAttribute.HEALTH, resolvedEffect.EffectedAttribute);
            Assert.IsNull(resolvedEffect.ForwardEffect);
        }

        [TestMethod]
        public void TestRemainingCooldownApplied()
        {
            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                TargetID = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
                CombatAbility = new CombatAbility()
                {
                    CombatAbilityID = "1",
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

            ICombatAbilityManager abilityManager = new CombatAbilityManager();

            ResolvedEffect[] resolvedEffect = abilityManager.ResolveCombatAbility(ctx).ToArray();
            
            Assert.AreEqual(3, ctx.CombatAbility.RemainingCooldown);            
        }

        [TestMethod]
        public void TestCooldownErrorThrown()
        {
            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                TargetID = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
                CombatAbility = new CombatAbility()
                {
                    CombatAbilityID = "1",
                    Cooldown = 3,
                    RemainingCooldown = 3,
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

            ICombatAbilityManager abilityManager = new CombatAbilityManager();
            
            Assert.ThrowsException<AbilityCooldownException>(() => abilityManager.ResolveCombatAbility(ctx).ToArray());
        }

        [TestMethod]
        public void TestCooldownApplied()
        {
            CombatAbilityResolutionContext ctx = new CombatAbilityResolutionContext()
            {
                TargetID = "1",
                Strength = 10,
                Intellect = 10,
                Speed = 10,
                CombatAbility = new CombatAbility()
                {
                    CombatAbilityID = "1",
                    Cooldown = 3,
                    RemainingCooldown = 3,
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

            ICombatAbilityManager abilityManager = new CombatAbilityManager();
            
            abilityManager.ApplyCooldown(ctx.CombatAbility, -2);
            Assert.IsTrue(ctx.CombatAbility.RemainingCooldown == 1);
        }
    }
}
