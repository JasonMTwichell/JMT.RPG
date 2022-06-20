using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Game.Domain
{
    public class Item
    {
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemFlavorText { get; set; }
        public IEnumerable<Effect> Effects { get; set; }
    }
}
