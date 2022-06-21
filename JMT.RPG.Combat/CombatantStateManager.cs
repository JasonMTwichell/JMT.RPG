using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public class CombatantStateManager : ICombatantStateManager
    {
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        private List<ResolvedEffect> _queuedEffects { get; set; }
        private List<ResolvedEffect> _forwardEffects { get; set; }
        public CombatantStateManager()
        {
            _queuedEffects = new List<ResolvedEffect>();
            _forwardEffects = new List<ResolvedEffect>();
        }

        public void ApplyEffects(IEnumerable<ResolvedEffect> resolvedEffects)
        {
            _queuedEffects.AddRange(resolvedEffects);
        }

        public CombatantState ResolveAppliedEffects()
        {
            foreach (ResolvedEffect effect in _queuedEffects)
            {
                ResolveEffect(effect);
                if (effect.ForwardEffect != null)
                {
                    _forwardEffects.Add(effect.ForwardEffect);
                }
            }

            _queuedEffects.Clear();

            return GetCombatantState();
        }

        private void ResolveEffect(ResolvedEffect effect)
        {
            switch (effect.EffectedAttribute)
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
                TotalHealth = TotalHealth,
                RemainingHealth = RemainingHealth,
                Intellect = Intellect,
                Speed = Speed,
                Strength = Strength,               
            };

            return combatantState;
        }

        public void CarryForwardEffects()
        {
            _queuedEffects.AddRange(_forwardEffects);
            _forwardEffects.Clear();
        }
    }
}
