using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.GameManagement
{
    public class GameState
    {
        public int Currency { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
