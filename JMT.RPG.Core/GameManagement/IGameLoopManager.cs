using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.GameManagement
{
    public interface IGameLoopManager
    {
        Task<GameLoopResult> PerformGameLoop(GameLoopContext context);
    }
}
