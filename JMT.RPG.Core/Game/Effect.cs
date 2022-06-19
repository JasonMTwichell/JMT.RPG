namespace JMT.Roguelike.Core.Game
{
    public class Effect
    {
        public string EffectedAttribute { get; set; }
        public int Magnitude { get; set; }
        public Effect ForwardEffect { get; set; }
    }
}
