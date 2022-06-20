namespace JMT.RPG.Core.Game.Domain
{
    public class Effect
    {
        public string EffectType { get; set; }
        public string EffectedAttribute { get; set; }
        public int Magnitude { get; set; }
        public int MagnitudeFactor { get; set; }
        public Effect ForwardEffect { get; set; }
    }
}
