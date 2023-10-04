using System;
using System.Collections.Generic;
using UnityEngine;

public class InputLockSystem : MonoBehaviour
{
    public static InputLockSystem Instance { get; set; }

    private GameObject InvisibleMask{ get; set; }

    private readonly Dictionary<string, int> InvisibleLocks = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
        
        Init();
    }

    private void Init()
    {
        InvisibleMask = this.transform.Find("InvisibleMask").gameObject;
    }
    
    public void Lock(string key)
    {
        InvisibleLocks.TryGetValue(key, out int num);
        num++;
        InvisibleLocks[key] = num;

        InvisibleMask.SetActiveIfNot(InvisibleLocks.Count > 0);
    }
        
    public void Unlock(string key)
    {
        if (InvisibleLocks.Count == 0) return;
            
        if (InvisibleLocks.TryGetValue(key, out int num))
        {
            num--;
                
            if (num > 0) 
            {
                InvisibleLocks[key] = num;
            }
            else
            {
                InvisibleLocks.Remove(key);
            }
        }

        InvisibleMask.SetActiveIfNot(InvisibleLocks.Count > 0);
    }
}