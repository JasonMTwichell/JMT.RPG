using JMT.RPG.Combat.Effect;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat.Ability
{
    public class CombatAbilityManager : ICombatAbilityManager
    {
        public CombatAbility ApplyCooldown(CombatAbility combatAbility, int coolDown)
        {
            CombatAbility cdAbility = combatAbility with
            {
                RemainingCooldown = Math.Max(0, (combatAbility.RemainingCooldown + coolDown)),
            };

            return cdAbility;
        }

        public IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbilityResolutionContext resCtx)
        {
            if (resCtx.CombatAbility.RemainingCooldown > 0) throw new AbilityCooldownException(string.Format("Ability is still on cooldown, it will be ready in {0} turns.", resCtx.CombatAbility.RemainingCooldown));

            //ApplyCooldown(resCtx.CombatAbility, resCtx.CombatAbility.Cooldown);

            // apply stats and armor influence to effects
            foreach (CombatEffect combatEffect in resCtx.CombatAbility.Effects)
            {
                CombatEffectResolutionContext effResCtx = new CombatEffectResolutionContext()
                {
                    CombatEffect = combatEffect,
                    TargetId = resCtx.TargetID,
                    Strength = resCtx.Strength,
                    Intellect = resCtx.Intellect,
                    Speed = resCtx.Speed,
                };

                yield return BuildResolvedEffect(effResCtx);
            }
        }

        private ResolvedEffect BuildResolvedEffect(CombatEffectResolutionContext resCtx)
        {
            MagnitudeResolutionContext magCtx = new MagnitudeResolutionContext()
            {
                EffectType = resCtx.CombatEffect.EffectType,
                DefaultMagnitude = resCtx.CombatEffect.Magnitude,
                MagnitudeFactor = resCtx.CombatEffect.MagnitudeFactor,
                Strength = resCtx.Strength,
                Intellect = resCtx.Intellect,
                Speed = resCtx.Speed,
            };

            int resolvedMagnitude = ResolveCombatEffectMagnitude(magCtx);

            ResolvedEffect resolvedEffect = new ResolvedEffect()
            {
                TargetID = resCtx.TargetId,
                EffectedAttribute = resCtx.CombatEffect.EffectedAttribute,
                ResolvedMagnitude = resolvedMagnitude,
                ForwardEffect = resCtx.CombatEffect.ForwardEffect != null ? BuildResolvedEffect(new CombatEffectResolutionContext()
                {
                    CombatEffect = resCtx.CombatEffect.ForwardEffect,
                    TargetId = resCtx.TargetId,
                    Strength = resCtx.Strength,
                    Intellect = resCtx.Intellect,
                    Speed = resCtx.Speed,
                }) : null,
            };

            return resolvedEffect;
        }

        private int ResolveCombatEffectMagnitude(MagnitudeResolutionContext resCtx)
        {
            int resolvedMagnitude = resCtx.DefaultMagnitude;
            switch (resCtx.EffectType)
            {
                case EffectType.PHYSICAL:
                    resolvedMagnitude += resCtx.Strength;
                    break;
                case EffectType.MAGICAL:
                    resolvedMagnitude += resCtx.Intellect;
                    break;
                case EffectType.AGILITY:
                    resolvedMagnitude += resCtx.Speed;
                    break;
            }

            return resolvedMagnitude * resCtx.MagnitudeFactor;
        }
    }
}
