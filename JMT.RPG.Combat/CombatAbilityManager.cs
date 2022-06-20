using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class CombatAbilityManager: ICombatAbilityManager
    {
        private IEnumerable<CombatAbility> _combatAbilities;
        public CombatAbilityManager(IEnumerable<CombatAbility> combatAbilities)
        {
            _combatAbilities = combatAbilities;
        }

        public IEnumerable<CombatAbility> GetCombatAbilities()
        {
            return _combatAbilities;
        }

        public void ApplyCooldownAllAbilities(int coolDown)
        {
            foreach(CombatAbility ability in _combatAbilities)
            {
                ability.RemainingCooldown = Math.Max(0, ability.RemainingCooldown += coolDown);
            }
        }
        public void ApplyCooldown(string combatAbilityID, int coolDown)
        {
            CombatAbility ability = _combatAbilities.First(ca => ca.CombatAbilityId == combatAbilityID);
            ability.RemainingCooldown = Math.Max(0, ability.RemainingCooldown += coolDown); 
        }

        public IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbilityResolutionContext resCtx)
        {
            CombatAbility ability = _combatAbilities.First(ca => ca.CombatAbilityId == resCtx.CombatAbilityID);

            if (ability.RemainingCooldown > 0) throw new AbilityCooldownException(string.Format("Ability is still on cooldown, it will be ready in {0} turns.", ability.RemainingCooldown));
            
            ability.RemainingCooldown = ability.Cooldown;

            // apply stats and armor influence to effects
            foreach (CombatEffect combatEffect in ability.Effects)
            {
                CombatEffectResolutionContext effResCtx = new CombatEffectResolutionContext()
                {
                    CombatEffect = combatEffect,
                    TargetId = resCtx.TargetId,
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
