using JMT.RPG.Combat.Ability;
using JMT.RPG.Combat.Combatants;
using JMT.RPG.Combat.Effect;
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
    public class ResolvedEffectManagerTests
    {
        [TestMethod]
        public void TestEffectsAppliedToState()
        {
            IResolvedEffectManager mgr = new ResolvedEffectManager();

            CombatantBattleContext btlCtx = new CombatantBattleContext()
            {
                CombatantID = "1",
                IsEnemyCombatant = false,
                CombatantState = new CombatantState()
                {
                    TotalHealth = 100,
                    RemainingHealth = 100,
                    Strength = 10,
                    Intellect = 10,
                    Speed = 10,
                },
                AppliedEffects = new List<ResolvedEffect>(),
                CarryForwardEffects = new List<ResolvedEffect>(),
            };
           
            btlCtx = mgr.ApplyEffects(btlCtx, new ResolvedEffect[]
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

            btlCtx = mgr.ResolveAppliedEffects(btlCtx);

            Assert.AreEqual(90, btlCtx.CombatantState.RemainingHealth);
            Assert.AreEqual(11, btlCtx.CombatantState.Strength);
            Assert.AreEqual(11, btlCtx.CombatantState.Intellect);
            Assert.AreEqual(11, btlCtx.CombatantState.Speed);
        }

        [TestMethod]
        public void TestEffectsCarriedForward()
        {
            IResolvedEffectManager mgr = new ResolvedEffectManager();

            CombatantBattleContext btlCtx = new CombatantBattleContext()
            {
                CombatantID = "1",
                IsEnemyCombatant = false,
                CombatantState = new CombatantState()
                {
                    TotalHealth = 100,
                    RemainingHealth = 100,
                    Strength = 10,
                    Intellect = 10,
                    Speed = 10,
                },
                AppliedEffects = new List<ResolvedEffect>(),
                CarryForwardEffects = new List<ResolvedEffect>(),
            };

            btlCtx = mgr.ApplyEffects(btlCtx, new ResolvedEffect[]
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

            btlCtx = mgr.ResolveAppliedEffects(btlCtx);
            Assert.AreEqual(99, btlCtx.CombatantState.RemainingHealth);

            btlCtx = mgr.CarryForwardEffects(btlCtx);
            btlCtx = mgr.ResolveAppliedEffects(btlCtx);
            Assert.AreEqual(96, btlCtx.CombatantState.RemainingHealth);


            btlCtx = mgr.CarryForwardEffects(btlCtx);
            btlCtx = mgr.ResolveAppliedEffects(btlCtx);
            Assert.AreEqual(91, btlCtx.CombatantState.RemainingHealth);
        }
    }
}
