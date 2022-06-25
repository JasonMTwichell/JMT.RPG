using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.Game.Domain;

namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatItem
    {
        public string CombatItemID { get; init; }
        public string CombatItemName { get; init; }
        public string CombatItemDescription { get; init; }
        public string CombatItemFlavorText { get; init; }
        public IEnumerable<CombatEffect> Effects { get; init; }
    }
}
