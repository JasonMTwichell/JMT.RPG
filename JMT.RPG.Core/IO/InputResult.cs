namespace JMT.RPG.Combat
{
    public record InputResult
    {
        public string TargetId { get; set; }
        public string ChosenAbilityId { get; set; }
    }
}