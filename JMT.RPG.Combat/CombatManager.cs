using JMT.RPG.Combat.Ability;
using JMT.RPG.Combat.Combatants;
using JMT.RPG.Combat.Effect;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class CombatManager: ICombatManager
    {
        ICombatInputHandler _inputHandler;
        ICombatAbilityManager _abilityMgr;
        IResolvedEffectManager _effMgr;
        public CombatManager(ICombatInputHandler inputHandler, ICombatAbilityManager abilityMgr, IResolvedEffectManager effMgr)
        {
            _inputHandler = inputHandler;
            _abilityMgr = abilityMgr;
            _effMgr = effMgr;
        }

        public async Task<CombatResult> PerformCombat(CombatEncounterContext combatEncounterCtx)
        {
            // map to combat classes
            Combatant[] combatants = combatEncounterCtx.Combatants.Select(ctx => MapCombatantContext(ctx))
                                                                  .OrderByDescending(c => c.Speed)
                                                                  .ToArray();

            int turnNumber = 0;
            bool combatConcluded = false;

            while (!combatConcluded)
            {
                // start of turn phase
                StartOfTurnPhase(combatants);

                // each combatant takes their turn selecting and applying their action
                foreach (Combatant combatant in combatants)
                {
                    CombatInputResult combatantInput = await _inputHandler.GetInput(new CombatInputContext()
                    {
                        CombatantID = combatant.CombatantID,
                        TurnNumber = turnNumber,
                        CombatantContexts = combatants.Select(c => c.GetCombatantBattleContext()).Select(c => new CombatantContext()
                        {
                            CombatantID = c.CombatantID,
                            Name = c.Name,
                            IsEnemyCombatant = c.IsEnemyCombatant,
                            Level = c.Level,
                            TotalHealth = c.CombatantState.TotalHealth,
                            RemainingHealth = c.CombatantState.RemainingHealth,
                            Strength = c.CombatantState.Strength,
                            Intellect = c.CombatantState.Intellect,
                            Speed = c.CombatantState.Speed,
                            CombatAbilities = c.CombatAbilities.ToArray(),
                        }).ToArray(),
                    });

                    // resolve the ability and apply cooldown
                    CombatAbility chosenAbility = combatant.CombatAbilities.First(c => c.CombatAbilityID == combatantInput.ChosenAbilityID);
                    CombatAbilityResolutionContext abilityResCtx = new CombatAbilityResolutionContext()
                    {
                        TargetID = combatantInput.TargetID,
                        Strength = combatant.Strength,
                        Intellect = combatant.Intellect,
                        Speed = combatant.Speed,
                        CombatAbility = chosenAbility,
                    };
                    
                    IEnumerable<ResolvedEffect> resolvedEffects = _abilityMgr.ResolveCombatAbility(abilityResCtx).ToArray();
                    chosenAbility = _abilityMgr.ApplyCooldown(chosenAbility, chosenAbility.Cooldown);
                    combatant.CombatAbilities.RemoveAll(ca => ca.CombatAbilityID == chosenAbility.CombatAbilityID);
                    combatant.CombatAbilities.Add(chosenAbility);

                    // distribute them to their targets
                    foreach (Combatant targetedCombatant in combatants)
                    {
                        ResolvedEffect[] targetingEffects = resolvedEffects.Where(e => e.TargetID == targetedCombatant.CombatantID)?.ToArray();
                        CombatantBattleContext ctx = _effMgr.ApplyEffects(targetedCombatant.GetCombatantBattleContext(), targetingEffects);
                        targetedCombatant.ApplyCombatantBattleContext(ctx);
                    }

                    ActionResolutionPhase(combatants);

                    // check if combat is concluded
                    combatConcluded = CheckCombatConcluded(combatants);
                    if (combatConcluded) break;
                }

                EndOfTurnPhase(combatants);
                turnNumber += 1;
            }

            CombatResult combatResult = new CombatResult()
            {
                FinalTurnNum = turnNumber,
                PlayerPartyWon = combatants.Where(c => c.IsEnemyCombatant).All(ec => ec.RemainingHealth <= 0)
                                    &&
                                 combatants.Where(c => !c.IsEnemyCombatant).Any(ec => ec.RemainingHealth >= 0),
                PlayerPartyCombatants = combatants.Select(c => MapToCombatContext(c)).ToArray(),
            };

            return combatResult;
        }

        private CombatantContext MapToCombatContext(Combatant ctx)
        {
            return new CombatantContext()
            {
                CombatantID = ctx.CombatantID,
                Name = ctx.Name,
                Level = ctx.Level,
                IsEnemyCombatant = ctx.IsEnemyCombatant,
                TotalHealth = ctx.TotalHealth,
                RemainingHealth = ctx.RemainingHealth,
                Strength = ctx.Strength,
                Intellect = ctx.Intellect,
                Speed = ctx.Speed,
                CombatAbilities = ctx.CombatAbilities,                
            };
        }

        private Combatant MapCombatantContext(CombatantContext ctx)
        {
            return new Combatant()
            {
                CombatantID = ctx.CombatantID,
                Name = ctx.Name,
                Level = ctx.Level,
                IsEnemyCombatant = ctx.IsEnemyCombatant,
                TotalHealth = ctx.TotalHealth,
                RemainingHealth = ctx.RemainingHealth,
                Strength = ctx.Strength,
                Intellect = ctx.Intellect,
                Speed = ctx.Speed,
                CombatAbilities = ctx.CombatAbilities.ToList(),
                AppliedEffects = new List<ResolvedEffect>(),
                CarryForwardEffects = new List<ResolvedEffect>(),
            };
        }

        private void StartOfTurnPhase(IEnumerable<Combatant> combatants)
        {
            foreach(Combatant combatant in combatants)
            {
                CombatantBattleContext ctx = _effMgr.ResolveAppliedEffects(combatant.GetCombatantBattleContext());
                combatant.ApplyCombatantBattleContext(ctx);
            }
        }

        private void ActionResolutionPhase(IEnumerable<Combatant> combatants)
        {
            foreach (Combatant combatant in combatants)
            {
                CombatantBattleContext ctx = _effMgr.ResolveAppliedEffects(combatant.GetCombatantBattleContext());
                combatant.ApplyCombatantBattleContext(ctx);
            }
        }

        private void EndOfTurnPhase(IEnumerable<Combatant> combatants)
        {
            foreach (Combatant combatant in combatants)
            {
                CombatantBattleContext ctx = _effMgr.CarryForwardEffects(combatant.GetCombatantBattleContext());
                combatant.ApplyCombatantBattleContext(ctx);
            }
        }
        
        private bool CheckCombatConcluded(IEnumerable<Combatant> combatants)
        {
            return (combatants.Where(c => c.IsEnemyCombatant).All(ec => ec.RemainingHealth <= 0) || combatants.Where(c => !c.IsEnemyCombatant).All(ec => ec.RemainingHealth <= 0));
        }
    }
}
