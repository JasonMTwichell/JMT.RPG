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
        public int Speed
        {
            get
            {
                return _combatantStateMgr.GetCombatantState().Speed;
            }
        }

        public int RemainingHealth
        {
            get
            {
                return _combatantStateMgr.GetCombatantState().RemainingHealth;
            }
        }
        #endregion

        private IInputHandler _inputHandler;
        private ICombatAbilityManager _combatantAbilityMgr;
        private ICombatantStateManager _combatantStateMgr;        

        public Combatant(IInputHandler inputHandler, ICombatAbilityManager combatantAbilityMgr, ICombatantStateManager combatantStateMgr)
        {
            _inputHandler = inputHandler;
            _combatantAbilityMgr = combatantAbilityMgr;
            _combatantStateMgr = combatantStateMgr;
        }

        public async Task<IEnumerable<ResolvedEffect>> ChooseCombatAbility(CombatContext combatContext)
        {
            InputResult input = await _inputHandler.GetInput(combatContext);

            CombatAbilityResolutionContext resCtx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = input.ChosenAbilityId,
                TargetId = input.TargetId,
                Strength = _combatantStateMgr.GetCombatantState().Strength,
                Intellect = _combatantStateMgr.GetCombatantState().Intellect,
                Speed = _combatantStateMgr.GetCombatantState().Speed,
            };

            ResolvedEffect[] resolvedEffects = _combatantAbilityMgr.ResolveCombatAbility(resCtx).ToArray();
            return resolvedEffects;
        }

        internal void StartOfTurnPhase()
        {
            // resolve forward effects
            _combatantStateMgr.ResolveAppliedEffects();
            _combatantAbilityMgr.ApplyCooldownAllAbilities(-1);
        }

        public void ApplyEffects(ResolvedEffect[] targetingEffects)
        {
            _combatantStateMgr.ApplyEffects(targetingEffects);
        }

        internal void ActionResolutionPhase()
        {
            _combatantStateMgr.ResolveAppliedEffects();
        }

        internal void EndOfTurnPhase()
        {
            _combatantStateMgr.CarryForwardEffects();
        }

        public CombatantContext GetCombatantContext()
        {
            CombatantContext context = new CombatantContext()
            {
                Id = Id,
                Name = Name,
                Level = Level,
                TotalHealth = _combatantStateMgr.GetCombatantState().TotalHealth,
                RemainingHealth = _combatantStateMgr.GetCombatantState().RemainingHealth,
                Strength = _combatantStateMgr.GetCombatantState().Strength,
                Intellect = _combatantStateMgr.GetCombatantState().Intellect,
                Speed = _combatantStateMgr.GetCombatantState().Speed,
                CombatAbilities = _combatantAbilityMgr.GetCombatAbilities().ToArray(),
            };

            return context;
        }
    }
}
