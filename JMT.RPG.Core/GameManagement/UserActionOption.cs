namespace JMT.RPG.Core.GameManagement
{
    public record UserActionOption
    {
        public string OptionID { get; init; }
        public string OptionTitle { get; init; }
        public string OptionDescription { get; init; }
    }
}
