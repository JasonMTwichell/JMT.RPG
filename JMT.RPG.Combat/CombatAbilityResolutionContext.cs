using JMT.RPG.Combat;
using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Combat
{
    public record CombatAbilityResolutionContext
    {
        public string TargetID { get; set; }
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }
        public CombatAbility CombatAbility { get; set; }
        
    }
}
