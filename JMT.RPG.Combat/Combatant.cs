using JMT.RPG.Core.Coordination;
using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public class Combatant
    {
        #region Stats
        public string Id { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }        
        #endregion

        private IInputHandler _inputHandler;
        private ICombatAbilityManager _combatAbilityManager;
        private List<ResolvedEffect> _queuedEffects { get; set; }
        private List<ResolvedEffect> _forwardEffects { get; set; }

        public Combatant(IInputHandler inputHandler, ICombatAbilityManager combatAbilityManager)
        {
            _inputHandler = inputHandler;
            _combatAbilityManager = combatAbilityManager;

            _queuedEffects = new List<ResolvedEffect>();
            _forwardEffects = new List<ResolvedEffect>();
        }

        public async Task<IEnumerable<ResolvedEffect>> ChooseCombatAbility(CombatState combatState)
        {
            CombatantContext context = new CombatantContext()
            {
                TurnNumber = combatState.TurnNumber,
                CombatantStates = combatState.CombatantStates,
                CombatantAbilities = _combatAbilityManager.GetCombatAbilities().ToArray(),
            };

            InputResult input = await _inputHandler.GetInput(context);

            CombatAbilityResolutionContext resCtx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = input.ChosenAbilityId,
                TargetId = input.TargetId,
                Strength = Strength,
                Intellect = Intellect,
                Speed = Speed,
            };

            ResolvedEffect[] resolvedEffects = _combatAbilityManager.ResolveCombatAbility(resCtx).ToArray();
            return resolvedEffects;
        }

        internal void StartOfTurn()
        {
            // resolve forward effects
            ResolveEffects();
            _combatAbilityManager.ApplyCooldownAllAbilities(-1);
        }

        internal void EndOfTurn()
        {
            // move forward effects to effect queue
            _queuedEffects.AddRange(_forwardEffects);
            _forwardEffects.Clear();
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
                case EffectedAttribute.HEALTH:
                    RemainingHealth += effect.ResolvedMagnitude;
                    break;
                case EffectedAttribute.INTELLECT:
                    Intellect += effect.ResolvedMagnitude;
                    break;
                case EffectedAttribute.STRENGTH:
                    Strength += effect.ResolvedMagnitude;
                    break;
                case EffectedAttribute.SPEED:
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
                CombatAbilities = _combatAbilityManager.GetCombatAbilities().ToArray(),
            };

            return combatantState;
        }
    }
}
