﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace Framework2.Infra.MQ.Core
{
    /// <summary>
    /// The base event queue, derived services should have a singleton scope
    /// </summary>
    public abstract class EventQueue : IEventQueue
    {
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Event names -> List of handlers for the event
        /// </summary>
        protected readonly Dictionary<string, List<FxEventHandler>> _eventHandlers;

        public abstract Task<uint> Count<TEvent>()
            where TEvent : FxEvent;

        protected abstract Task StartConsumingEvents<TEvent>(string eventName)
            where TEvent : FxEvent;

        public EventQueue(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _eventHandlers = new();
        }

        public abstract Task Publish<TEvent>(TEvent @event)
            where TEvent : FxEvent;

        public virtual async Task Subscribe<TEvent, TEventHandler>()
            where TEvent : FxEvent
            where TEventHandler : FxEventHandler<TEvent>
        {
            var eventName = typeof(TEvent).Name;
            var handler = _serviceProvider.GetRequiredService<TEventHandler>();

            if (!_eventHandlers.ContainsKey(eventName))
                _eventHandlers[eventName] = new();

            if (!_eventHandlers[eventName].Any(x => x.GetType() == handler.GetType()))
                _eventHandlers[eventName].Add(handler);

            await StartConsumingEvents<TEvent>(eventName);
        }

        protected virtual async Task ConsumeEvent(string eventName, string payload)
        {
            if (!_eventHandlers.ContainsKey(eventName))
                return;

            var handlers = _eventHandlers[eventName];
            foreach (var handler in handlers)
            {
                var @event = JsonConvert.DeserializeObject(payload, handler.EventHandled);
                var genericType = typeof(FxEventHandler<>).MakeGenericType(handler.EventHandled);
                var handlerName = nameof(FxEventHandler<FxEvent>.Handle);

                try
                {
                    await (Task)genericType.GetMethod(handlerName)!.Invoke(handler, new[] { @event, default })!;
                }
                catch (TargetInvocationException ex)
                {
                    /// throw the actual exception from the handler, since reflection by default hides it under <see cref="TargetInvocationException"/>
                    throw ex.InnerException!;
                }
            }
        }
    }
}
