namespace JMT.RPG.Core.Interfaces
{
    public record InputResult
    {
        public string TargetId { get; set; }
        public string ChosenAbilityId { get; set; }
    }
}