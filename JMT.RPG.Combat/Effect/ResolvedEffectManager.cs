using JMT.RPG.Combat.Ability;
using JMT.RPG.Combat.Combatants;

namespace JMT.RPG.Combat.Effect
{
    public class ResolvedEffectManager : IResolvedEffectManager
    {
        public CombatantBattleContext ApplyEffects(CombatantBattleContext ctx, IEnumerable<ResolvedEffect> resolvedEffects)
        {
            List<ResolvedEffect> appliedEffects = new List<ResolvedEffect>(ctx.AppliedEffects);
            appliedEffects.AddRange(resolvedEffects);

            return ctx with { AppliedEffects = appliedEffects };
        }

        public CombatantBattleContext ResolveAppliedEffects(CombatantBattleContext ctx)
        {
            List<ResolvedEffect> carryFwdEff = new List<ResolvedEffect>(ctx.CarryForwardEffects);
            CombatantState state = ctx.CombatantState;

            foreach (ResolvedEffect effect in ctx.AppliedEffects)
            {
                state = ResolveEffect(state, effect);
                if (effect.ForwardEffect != null)
                {
                    carryFwdEff.Add(effect.ForwardEffect);
                }
            }

            return ctx with { CombatantState = state, CarryForwardEffects = carryFwdEff, AppliedEffects = Array.Empty<ResolvedEffect>() };
        }

        public CombatantBattleContext CarryForwardEffects(CombatantBattleContext ctx)
        {
            return ctx with { AppliedEffects = ctx.CarryForwardEffects, CarryForwardEffects = Array.Empty<ResolvedEffect>() };
        }

        private CombatantState ResolveEffect(CombatantState state, ResolvedEffect effect)
        {
            CombatantState resolvedState = state with
            {
                RemainingHealth = effect.EffectedAttribute == EffectedAttribute.HEALTH ? state.RemainingHealth + effect.ResolvedMagnitude : state.RemainingHealth,
                Strength = effect.EffectedAttribute == EffectedAttribute.STRENGTH ? state.Strength + effect.ResolvedMagnitude : state.Strength,
                Intellect = effect.EffectedAttribute == EffectedAttribute.INTELLECT ? state.Intellect + effect.ResolvedMagnitude : state.Intellect,
                Speed = effect.EffectedAttribute == EffectedAttribute.SPEED ? state.Speed + effect.ResolvedMagnitude : state.Speed,
            };

            return resolvedState;
        }
    }
}
