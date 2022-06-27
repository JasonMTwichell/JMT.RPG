using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat.Ability
{
    public class AbilityCooldownException : Exception
    {
        public AbilityCooldownException(string message) : base(message)
        {
        }
    }
}
