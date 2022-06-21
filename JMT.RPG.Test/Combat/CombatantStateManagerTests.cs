using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Test.Combat
{
    [TestClass]
    public class CombatantStateManagerTests
    {
        [TestMethod]
        public void TestEffectsAppliedToState()
        {
            ICombatantStateManager mgr = new CombatantStateManager()
            {
                TotalHealth = 100,
                RemainingHealth = 100,
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            mgr.ApplyEffects(new ResolvedEffect[]
            {
                new ResolvedEffect()
                {
                    EffectedAttribute = EffectedAttribute.HEALTH,
                    ResolvedMagnitude = -10,
                },
                new ResolvedEffect()
                {
                    EffectedAttribute = EffectedAttribute.STRENGTH,
                    ResolvedMagnitude = 1,
                },
                new ResolvedEffect()
                {
                    EffectedAttribute = EffectedAttribute.INTELLECT,
                    ResolvedMagnitude = 1,
                },
                new ResolvedEffect()
                {
                    EffectedAttribute = EffectedAttribute.SPEED,
                    ResolvedMagnitude = 1,
                }
            });

            CombatantState cs = mgr.ResolveAppliedEffects();

            Assert.AreEqual(90, cs.RemainingHealth);
            Assert.AreEqual(11, cs.Strength);
            Assert.AreEqual(11, cs.Intellect);
            Assert.AreEqual(11, cs.Speed);
        }

        [TestMethod]
        public void TestEffectsCarriedForward()
        {
            ICombatantStateManager mgr = new CombatantStateManager()
            {
                TotalHealth = 100,
                RemainingHealth = 100,
                Strength = 10,
                Intellect = 10,
                Speed = 10,
            };

            mgr.ApplyEffects(new ResolvedEffect[]
            {
                new ResolvedEffect()
                {
                    EffectedAttribute = EffectedAttribute.HEALTH,
                    ResolvedMagnitude = -1,
                    ForwardEffect = new ResolvedEffect()
                    {
                        EffectedAttribute = EffectedAttribute.HEALTH,
                        ResolvedMagnitude = -3,
                        ForwardEffect = new ResolvedEffect()
                        {
                            EffectedAttribute = EffectedAttribute.HEALTH,
                            ResolvedMagnitude = -5,
                        }
                    }
                },               
            });

            CombatantState cs = mgr.ResolveAppliedEffects();
            Assert.AreEqual(99, cs.RemainingHealth);

            mgr.CarryForwardEffects();
            cs = mgr.ResolveAppliedEffects();
            Assert.AreEqual(96, cs.RemainingHealth);


            mgr.CarryForwardEffects();
            cs = mgr.ResolveAppliedEffects();
            Assert.AreEqual(91, cs.RemainingHealth);
        }
    }
}
