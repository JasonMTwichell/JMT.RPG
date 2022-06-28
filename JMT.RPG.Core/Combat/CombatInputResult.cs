namespace JMT.RPG.Core.Combat
{
    public record CombatInputResult
    {
        public string CombatantID { get; init; }
        public string TargetID { get; init; }
        public string ChosenAbilityID { get; init; }
        public string ChoseItemID { get; set; }
    }
}