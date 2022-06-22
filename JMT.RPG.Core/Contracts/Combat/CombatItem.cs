using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMT.RPG.Core.Game.Domain;

namespace JMT.RPG.Core.Contracts.Combat
{
    public class CombatItem
    {
        public string CombatItemID { get; set; }
        public string CombatItemName { get; set; }
        public string CombatItemDescription { get; set; }
        public string CombatItemFlavorText { get; set; }
        public IEnumerable<CombatEffect> Effects { get; set; }
    }
}
