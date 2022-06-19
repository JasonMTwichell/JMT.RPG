using JMT.Roguelike.Core.Coordination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Combat
{
    public class Combatant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public IEnumerable<CombatAbility> CombatAbilities { get; set; }
        private IInputHandler _inputHandler;
        private List<ResolvedEffect> _queuedEffects { get; set; }
        private List<ResolvedEffect> _forwardEffects { get; set; }

        public Combatant(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _queuedEffects = new List<ResolvedEffect>();
            _forwardEffects = new List<ResolvedEffect>();
        }

        public async Task<IEnumerable<ResolvedEffect>> ChooseCombatAbility(CombatState combatState)
        {
            CombatantContext context = new CombatantContext()
            {
                TurnNumber = combatState.TurnNumber,
                CombatantStates = combatState.CombatantStates,
                CombatantAbilities = CombatAbilities.ToArray(),
            };

            InputResult input = await _inputHandler.GetInput(context);
            CombatAbility chosenAbility = CombatAbilities.First(ca => ca.CombatAbilityId == input.ChosenAbilityId);
            ResolvedEffect[] resolvedEffects = ResolveCombatAbility(chosenAbility, input.TargetId).ToArray();
            return resolvedEffects;
        }
        
        private IEnumerable<ResolvedEffect> ResolveCombatAbility(CombatAbility ability, string targetId)
        {
            // set cooldown
            ability.RemainingCooldown = ability.Cooldown;

            // apply stats and armor influence to effects
            foreach(CombatEffect combatEffect in ability.Effects)
            {
                yield return BuildResolvedEffect(combatEffect, targetId);
            }
        }

        private ResolvedEffect BuildResolvedEffect(CombatEffect combatEffect, string targetId)
        {
            int resolvedMagnitude = ResolveCombatEffectMagnitude(combatEffect.Magnitude, combatEffect.EffectType) * combatEffect.MagnitudeFactor;
            ResolvedEffect resolvedEffect = new ResolvedEffect()
            {
                TargetID = targetId,
                EffectedAttribute = combatEffect.EffectedAttribute,
                ResolvedMagnitude = resolvedMagnitude,
                ForwardEffect = combatEffect.ForwardEffect != null ? BuildResolvedEffect(combatEffect.ForwardEffect, targetId) : null,
            };

            return resolvedEffect;
        }

        internal void StartOfTurn()
        {
            // resolve forward effects
            ResolveEffects();
        }

        internal void EndOfTurn()
        {
            // move forward effects to effect queue
            _queuedEffects.AddRange(_forwardEffects);
            _forwardEffects.Clear();
        }

        private int ResolveCombatEffectMagnitude(int defaultMagnitude, string abilityType)
        {
            switch(abilityType)
            {
                case EffectType.PHYSICAL:
                    defaultMagnitude += Strength;
                    break;
                case EffectType.MAGICAL:
                    defaultMagnitude += Intellect;
                    break;
                case EffectType.AGILITY:
                    defaultMagnitude += Speed;
                    break;
            }

            return defaultMagnitude;
        }

        public void ApplyEffects(ResolvedEffect[] targetingEffects)
        {
            _queuedEffects.AddRange(targetingEffects);
        }

        public void ResolveEffects()
        {
            foreach(ResolvedEffect effect in _queuedEffects)
            {
                ResolveEffect(effect);
                if(effect.ForwardEffect != null)
                {
                    _forwardEffects.Add(effect.ForwardEffect);
                }                
            }

            _queuedEffects.Clear();
        }

        private void ResolveEffect(ResolvedEffect effect)
        {
            switch(effect.EffectedAttribute)
            {
                case Attribute.HEALTH:
                    RemainingHealth += effect.ResolvedMagnitude;
                    break;
                case Attribute.INTELLECT:
                    Intellect += effect.ResolvedMagnitude;
                    break;
                case Attribute.STRENGTH:
                    Strength += effect.ResolvedMagnitude;
                    break;
                case Attribute.SPEED:
                    Speed += effect.ResolvedMagnitude;
                    break;                
            }
        }

        public CombatantState GetCombatantState()
        {
            CombatantState combatantState = new CombatantState()
            {
                Id = Id,
                Name = Name,
                Level = Level,
                TotalHealth = TotalHealth,
                RemainingHealth = RemainingHealth,
                Intellect = Intellect,
                Speed = Speed,
                Strength = Strength,
                CombatAbilities = CombatAbilities.ToArray(),
            };

            return combatantState;
        }
    }
}
