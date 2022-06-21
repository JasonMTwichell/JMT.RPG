namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatInputResult
    {
        public string TargetId { get; set; }
        public string ChosenAbilityId { get; set; }
    }
}