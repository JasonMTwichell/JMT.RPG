using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.CharacterManagement
{
    public record CharacterSkillTree
    {
        public IEnumerable<CharacterSkill> Skills { get; init; }
    }
}
