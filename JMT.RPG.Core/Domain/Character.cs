namespace JMT.RPG.Core.Game.Domain
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int SpellSlots { get; set; }
        public int WeaponAbilitySlots { get; set; }
        public int Speed { get; set; }
        public int Intellect { get; set; }
        public int Strength { get; set; }
        public int Constitution { get; set; }
        public ICollection<Ability> CombatAbilities { get; set; }        
    }
}
