namespace JMT.Roguelike.Combat
{
    public record InputResult
    {
        public string TargetId { get; set; }
        public string ChosenAbilityId { get; set; }
    }
}