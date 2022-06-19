using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Combat
{
    public class CombatManager
    {
        private IEnumerable<Combatant> _playerParty;
        private IEnumerable<Combatant> _enemyParty;
        private IEnumerable<Combatant> _combatants { 
            get
            {
                return _playerParty.Concat(_enemyParty);
            }
        }

        public CombatManager(IEnumerable<Combatant> playerParty, IEnumerable<Combatant> enemyParty)
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

                Combatant[] speedSortedCombatants = _combatants.OrderByDescending(c => c.Speed).ToArray();
                foreach (Combatant combatant in speedSortedCombatants)
                {
                    // effects after being run through bonuses and triggers
                    CombatState combatState = GetCombatState(turnNumber);
                    IEnumerable<ResolvedEffect> resolvedEffects = await combatant.ChooseCombatAbility(combatState);

                    // distribute them to their targets
                    foreach(Combatant targetedCombatant in _combatants)
                    {
                        ResolvedEffect[] targetingEffects = resolvedEffects.ToArray().Where(e => e.TargetID == targetedCombatant.Id).ToArray();
                        targetedCombatant.ApplyEffects(targetingEffects);
                        targetedCombatant.ResolveEffects();
                    }

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

        private void StartOfTurnPhase()
        {
            foreach(Combatant combatant in _combatants)
            {
                combatant.StartOfTurn();
            }
        }

        private void EndOfTurnPhase()
        {
            foreach (Combatant combatant in _combatants)
            {
                combatant.EndOfTurn();
            }
        }

        private CombatState GetCombatState(int turnNumber)
        {
            CombatState combatState = new CombatState()
            {
                CombatantStates = _combatants.Select(c => c.GetCombatantState()).ToArray(),
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
