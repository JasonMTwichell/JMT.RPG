using JMT.RPG.Core.Contracts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat
{
    public class CombatManager: ICombatManager
    {
        private IEnumerable<ICombatant> _playerParty;
        private IEnumerable<ICombatant> _enemyParty;
        private IEnumerable<ICombatant> _combatants { 
            get
            {
                return _playerParty.Concat(_enemyParty);
            }
        }

        public CombatManager(IEnumerable<ICombatant> playerParty, IEnumerable<ICombatant> enemyParty)
        {
            _playerParty = playerParty;
            _enemyParty = enemyParty;
        }

        public async Task<CombatResult> PerformCombat()
        {
            bool combatConcluded = false;
            int turnNumber = 0;

            while(!combatConcluded)
            {
                // start of turn phase
                StartOfTurnPhase();

                ICombatant[] speedSortedCombatants = _combatants.OrderByDescending(c => c.Speed).ToArray();

                // each combatant takes their turn selecting and applying their action
                foreach (Combatant combatant in speedSortedCombatants)
                {
                    // effects after being run through bonuses and triggers
                    CombatContext combatState = GetCombatContext(turnNumber);
                    IEnumerable<ResolvedEffect> resolvedEffects = await combatant.ChooseCombatAbility(combatState);

                    // distribute them to their targets
                    foreach(Combatant targetedCombatant in _combatants)
                    {
                        ResolvedEffect[] targetingEffects = resolvedEffects.ToArray().Where(e => e.TargetID == targetedCombatant.Id).ToArray();
                        targetedCombatant.ApplyEffects(targetingEffects);
                    }

                    ActionResolutionPhase();

                    // check if combat is concluded
                    combatConcluded = CheckCombatConcluded();
                    if(combatConcluded) break; 
                }

                EndOfTurnPhase();
                turnNumber += 1;
            }

            CombatResult combatResult = new CombatResult()
            {
                FinalTurn = turnNumber,
                PlayerPartyWon = _enemyParty.All(ec => ec.RemainingHealth <= 0) && _playerParty.Any(ec => ec.RemainingHealth >= 0)
            };

            return combatResult;
        }

        private void ActionResolutionPhase()
        {
            foreach (Combatant combatant in _combatants)
            {
                combatant.ActionResolutionPhase();
            }
        }

        private void StartOfTurnPhase()
        {
            foreach(Combatant combatant in _combatants)
            {
                combatant.StartOfTurnPhase();
            }
        }

        private void EndOfTurnPhase()
        {
            foreach (Combatant combatant in _combatants)
            {
                combatant.EndOfTurnPhase();
            }
        }

        private CombatContext GetCombatContext(int turnNumber)
        {
            CombatContext combatState = new CombatContext()
            {
                CombatantContexts = _combatants.Select(c => c.GetCombatantContext()).ToArray(),
                TurnNumber = turnNumber,
            };

            return combatState;
        }

        
        private bool CheckCombatConcluded()
        {
            return _enemyParty.All(ec => ec.RemainingHealth <= 0) || _playerParty.All(ec => ec.RemainingHealth <= 0);
        }
    }
}
