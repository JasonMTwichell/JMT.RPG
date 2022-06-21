using JMT.RPG.Core.Coordination;
using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class Combatant: ICombatant
    {
        #region Stats
        public string Id { get; init; }
        public string Name { get; init; }        
        public int Speed
        {
            get
            {
                return _stateMgr.GetCombatantState().Speed;
            }
        }
        public int RemainingHealth
        {
            get
            {
                return _stateMgr.GetCombatantState().RemainingHealth;
            }
        }
        #endregion

        private ICombatInputHandler _inputHandler;
        private ICombatAbilityManager _abilityMgr;
        private ICombatantStateManager _stateMgr;        

        public Combatant(ICombatInputHandler inputHandler, ICombatAbilityManager abilityMgr, ICombatantStateManager stateMgr)
        {
            _inputHandler = inputHandler;
            _abilityMgr = abilityMgr;
            _stateMgr = stateMgr;
        }

        public async Task<IEnumerable<ResolvedEffect>> ChooseCombatAbility(CombatContext combatContext)
        {
            CombatInputResult input = await _inputHandler.GetInput(combatContext);

            CombatAbilityResolutionContext resCtx = new CombatAbilityResolutionContext()
            {
                CombatAbilityID = input.ChosenAbilityId,
                TargetId = input.TargetId,
                Strength = _stateMgr.GetCombatantState().Strength,
                Intellect = _stateMgr.GetCombatantState().Intellect,
                Speed = _stateMgr.GetCombatantState().Speed,
            };

            ResolvedEffect[] resolvedEffects = _abilityMgr.ResolveCombatAbility(resCtx).ToArray();
            return resolvedEffects;
        }

        public void StartOfTurnPhase()
        {
            // resolve forward effects
            _stateMgr.ResolveAppliedEffects();
            _abilityMgr.ApplyCooldownAllAbilities(-1);
        }

        public void ApplyEffects(ResolvedEffect[] targetingEffects)
        {
            _stateMgr.ApplyEffects(targetingEffects);
        }

        public void ActionResolutionPhase()
        {
            _stateMgr.ResolveAppliedEffects();
        }

        public void EndOfTurnPhase()
        {
            _stateMgr.CarryForwardEffects();
        }

        public CombatantContext GetCombatantContext()
        {
            CombatantContext context = new CombatantContext()
            {
                Id = Id,
                Name = Name,                
                TotalHealth = _stateMgr.GetCombatantState().TotalHealth,
                RemainingHealth = _stateMgr.GetCombatantState().RemainingHealth,
                Strength = _stateMgr.GetCombatantState().Strength,
                Intellect = _stateMgr.GetCombatantState().Intellect,
                Speed = _stateMgr.GetCombatantState().Speed,
                CombatAbilities = _abilityMgr.GetCombatAbilities().ToArray(),
            };

            return context;
        }
    }
}
