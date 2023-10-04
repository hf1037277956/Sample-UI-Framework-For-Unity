using System;
using System.Collections.Generic;
using System.Reflection;

public sealed class EventSystem
{
    private static EventSystem _instance;

    public static EventSystem Instance
    {
        get => _instance ??= new EventSystem();
        set => _instance = value;
    }

    private readonly Dictionary<Type, List<object>> _allEvents = new();
    
    // 利用反射获取所有的事件
    public void Init(Assembly assembly)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if(type.IsAbstract) continue;
            
            object[] attrs = type.GetCustomAttributes(typeof(EventAttribute), true);
            if (attrs.Length == 0)
            {
                continue;
            }

            if (attrs[0] is not EventAttribute eventAttribute)
            {
                continue;
            }

            if (_allEvents.ContainsKey(type))
            {
                throw new Exception($"{eventAttribute} 有重复的Event");
            }

            IEvent iEvent = Activator.CreateInstance(type) as IEvent;
            if (iEvent != null)
            {
                Type eventType = iEvent.GetEventType();
                if (!this._allEvents.ContainsKey(eventType))
                {
                    this._allEvents.Add(eventType, new List<object>());
                }

                this._allEvents[eventType].Add(iEvent);
            }
        }
    }
    
    public void Publish<T>(T a) where T : struct
    {
        List<object> iEvents;
        if (!this._allEvents.TryGetValue(a.GetType(), out iEvents))
        {
            return;
        }

        for (int i = 0; i < iEvents.Count; ++i)
        {
            object obj = iEvents[i];
            if (!(obj is AEvent<T> aEvent))
            {
                throw new Exception($"event error: {obj.GetType().Name}");
                //continue;
            }
            aEvent.Handle(a);
        }
    }
}