using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<T>();
            
            if (_instance == null)
            {
                new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
            }
            else
            {
                _instance.Init();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this as T;
        Init();
    }

    protected virtual void Init()
    {
        
    }
}