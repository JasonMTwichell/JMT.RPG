namespace JMT.RPG.Core.CharacterManagement
{
    public record CharacterSkillCopse
    {
        public IEnumerable<CharacterSkillTree> SkillTrees { get; set; }
    }
}
