namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatInputResult
    {
        public string CombatantID { get; set; }
        public string TargetID { get; set; }
        public string ChosenAbilityID { get; set; }
    }
}