using JMT.RPG.Combat;

namespace JMT.RPG.Combat
{
    public record CombatEffectResolutionContext
    {
        public CombatEffect CombatEffect { get; set; }
        public string TargetId { get; set; }        
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }
    }
}
