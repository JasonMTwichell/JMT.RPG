namespace JMT.RPG.Combat
{
    public record CombatAbilityResolutionContext
    {
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }
        public string CombatAbilityID { get; set; }
        public string TargetId { get; set; }
    }
}
