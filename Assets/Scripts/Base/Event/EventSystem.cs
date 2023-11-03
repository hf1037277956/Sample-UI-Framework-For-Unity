using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public sealed class EventSystem
{
    private static EventSystem _instance;

    public static EventSystem Instance
    {
        get => _instance ??= new EventSystem();
        set => _instance = value;
    }

    private readonly UnOrderMultiMapSet<Type, Type> _types = new UnOrderMultiMapSet<Type, Type>();

    private readonly Dictionary<Type, List<object>> _allEvents = new Dictionary<Type, List<object>>();

    private readonly Dictionary<Type, Dictionary<int, object>> _allInvokes = new Dictionary<Type, Dictionary<int, object>>();
    
    public void Init(Assembly assembly)
    {
        //var types = assembly.GetTypes();

        Dictionary<string, Type> allTypes = AssemblyHelper.GetAssemblyTypes(assembly);

        foreach (var type in allTypes)
        {
            if(type.Value.IsAbstract) continue;
            
            // 记录所有的BaseAttribute标记的类型
            object[] objects = type.Value.GetCustomAttributes(typeof(BaseAttribute), true);

            foreach (object o in objects)
            {
                _types.Add(o.GetType(), type.Value);
            }
        }
        
        foreach (Type type in _types[typeof(EventAttribute)])
        {
            IEvent iEvent = Activator.CreateInstance(type) as IEvent;
            if (iEvent == null)
            {
                throw new Exception($"type not is AEvent: {type.Name}");
            }
            
            object[] attrs = type.GetCustomAttributes(typeof(EventAttribute), true);
            foreach (object attr in attrs)
            {
                Type eventType = iEvent.GetEventType();
                if (!this._allEvents.ContainsKey(eventType))
                {
                    this._allEvents.Add(eventType, new List<object>());
                }
                
                this._allEvents[eventType].Add(iEvent);
            }
        }

        foreach (Type type in _types[typeof(InvokeAttribute)])
        {
            object obj = Activator.CreateInstance(type);
            IInvoke iInvoke = obj as IInvoke;
            if (iInvoke == null)
            {
                throw new Exception($"type not is IInvoke: {type.Name}");
            }
            
            object[] attrs = type.GetCustomAttributes(typeof(InvokeAttribute), true);
            foreach (var attr in attrs)
            {
                if (!_allInvokes.TryGetValue(iInvoke.GetInvokeType(), out var dict))
                {
                    dict = new Dictionary<int, object>();
                    _allInvokes.Add(iInvoke.GetInvokeType(), dict);
                }
                
                InvokeAttribute invokeAttribute = attr as InvokeAttribute;
                
                try
                {
                    if (invokeAttribute != null) dict.Add(invokeAttribute.Type, iInvoke);
                }
                catch (Exception e)
                {
                    if (invokeAttribute != null)
                        throw new Exception($"action type duplicate: {iInvoke.GetInvokeType().Name} {invokeAttribute.Type}", e);
                }
            }
        }
    }
    
    public void Publish<T>(T a) where T : struct
    {
        if (!this._allEvents.TryGetValue(a.GetType(), out var iEvents))
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

    public void Invoke<A>(int type, A args) where A : struct
    {
        if (!_allInvokes.TryGetValue(typeof(A), out var invokeHandlers))
        {
            throw new Exception($"Invoke error: {typeof(A).Name}");
        }

        if (!invokeHandlers.TryGetValue(type,out var invokeHandler))
        {
            throw new Exception($"Invoke error: {typeof(A).Name} {type}");
        }

        var aInvokeHandler = invokeHandler as AInvokeHandler<A>;
        if (aInvokeHandler == null)
        {
            throw new Exception($"Invoke error: {typeof(A).Name} {type}");
        }
        
        aInvokeHandler.Handle(args);
    }
    
    public T Invoke<A, T>(int type, A args) where A: struct
    {
        if (!this._allInvokes.TryGetValue(typeof(A), out var invokeHandlers))
        {
            throw new Exception($"Invoke error: {typeof(A).Name}");
        }
        if (!invokeHandlers.TryGetValue(type, out var invokeHandler))
        {
            throw new Exception($"Invoke error: {typeof(A).Name} {type}");
        }

        var aInvokeHandler = invokeHandler as AInvokeHandler<A, T>;
        if (aInvokeHandler == null)
        {
            throw new Exception($"Invoke error, not AInvokeHandler: {typeof(T).Name} {type}");
        }
            
        return aInvokeHandler.Handle(args);
    }
        
    public void Invoke<A>(A args) where A: struct
    {
        Invoke(0, args);
    }
        
    public T Invoke<A, T>(A args) where A: struct
    {
        return Invoke<A, T>(0, args);
    }
}