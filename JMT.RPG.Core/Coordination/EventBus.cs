using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace JMT.Roguelike.Core.Coordination
{
    public class EventBus : IEventBus
    {
        private readonly Subject<IGameEvent> subject = new Subject<IGameEvent>();

        public IObservable<T> GetEventStream<T>() where T : IGameEvent
        {
            return subject.OfType<T>();
        }

        public void Publish<T>(T @event) where T : IGameEvent
        {
            subject.OnNext(@event);
        }
    }
}
