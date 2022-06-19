using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Core.Coordination
{
    public interface IEventBus
    {
        IObservable<T> GetEventStream<T>() where T : IGameEvent;
        void Publish<T>(T @event) where T : IGameEvent;
    }
}
