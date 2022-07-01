namespace JMT.RPG.Core.CharacterManagement
{
    public record CharacterSkillCopse
    {
        public string CharacterID { get; init; }
        public IEnumerable<CharacterSkillTree> SkillTrees { get; set; }
    }
}
