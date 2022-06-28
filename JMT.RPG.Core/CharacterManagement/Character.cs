namespace JMT.RPG.Core.CharacterManagement
{
    public class Character
    {
        public string CharacterID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Speed { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Constitution { get; set; }
        public ICollection<Ability> Abilities { get; set; }
    }
}
