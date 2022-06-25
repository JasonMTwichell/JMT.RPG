namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatInputResult
    {
        public string CombatantID { get; init; }
        public string TargetID { get; init; }
        public string ChosenAbilityID { get; init; }
    }
}