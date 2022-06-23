using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class Combatant: ICombatant
    {
        #region Stats
        public string CombatantID { get; init; }
        public string Name { get; init; }
        public int Level { get; init; }
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public bool IsEnemyCombatant { get; set; }
        public IEnumerable<CombatAbility> CombatAbilities { get; set; } // TODO: NEED A DTO TO BREAK THE DEPENDENCY ON CORE
        public List<ResolvedEffect> AppliedEffects { get; set; }
        public List<ResolvedEffect> CarryForwardEffects { get; set; }
        #endregion

        public CombatantBattleContext GetCombatantBattleContext()
        {
            return new CombatantBattleContext()
            {
                CombatantID = CombatantID,
                Level = Level,
                Name = Name,
                IsEnemyCombatant = IsEnemyCombatant,
                CombatAbilities = CombatAbilities.ToArray(),
                AppliedEffects = AppliedEffects.ToArray(),
                CarryForwardEffects = CarryForwardEffects.ToArray(),
                CombatantState = new CombatantState()
                {
                    TotalHealth = TotalHealth,
                    RemainingHealth = RemainingHealth,
                    Strength = Strength,
                    Intellect = Intellect,
                    Speed = Speed,
                },
            };
        }

        public void ApplyCombatantBattleContext(CombatantBattleContext combatantContext)
        {
            IsEnemyCombatant = combatantContext.IsEnemyCombatant;
            TotalHealth = combatantContext.CombatantState.TotalHealth;
            RemainingHealth = combatantContext.CombatantState.RemainingHealth;
            Strength = combatantContext.CombatantState.Strength;
            Intellect = combatantContext.CombatantState.Intellect;
            Speed = combatantContext.CombatantState.Speed;
            CombatAbilities = combatantContext.CombatAbilities.ToArray();
            AppliedEffects = combatantContext.AppliedEffects.ToList();
            CarryForwardEffects = combatantContext.CarryForwardEffects.ToList();
        }
    }
}
