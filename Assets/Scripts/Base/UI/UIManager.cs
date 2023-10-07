using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    private Assembly Assembly;

    private Dictionary<string, Type> allUITypes = new Dictionary<string, Type>();
    private Dictionary<string, UICpt> allShowUICpts = new Dictionary<string, UICpt>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(allShowUICpts.Count == 0) return;
        
        // 调用所有显示的UI的Update
        foreach (var uiCpt in allShowUICpts.Values)
        {
            uiCpt.Update();
        }
    }

    private void FixedUpdate()
    {
        if(allShowUICpts.Count == 0) return;
        
        // 调用所有显示的UI的FixedUpdate
        foreach (var uiCpt in allShowUICpts.Values)
        {
            uiCpt.FixedUpdate();
        }
    }

    private void LateUpdate()
    {
        if(allShowUICpts.Count == 0) return;
        
        // 调用所有显示的UI的LateUpdate
        foreach (var uiCpt in allShowUICpts.Values)
        {
            uiCpt.LateUpdate();
        }
    }

    public void Init(Assembly assembly)
    {
        Assembly = assembly;
        allUITypes.Clear();
        foreach (Type type in Assembly.GetTypes())
        {
            object[] attrs = type.GetCustomAttributes(typeof(UIAttribute), false);
            if (attrs.Length == 0)
            {
                continue;
            }

            if (attrs[0] != null)
            {
                if(!(attrs[0] is UIAttribute)) continue;
            }
            
            UIAttribute uiAttribute = attrs[0] as UIAttribute;
            if (uiAttribute != null && allUITypes.ContainsKey(uiAttribute.Name))
            {
                throw new Exception($"{uiAttribute.Name} 有重复的UI Type");
            }
            allUITypes.Add(uiAttribute.Name, type);
        }
    }

    public void ShowPanel(string name, object data = null)
    {
        InputLockSystem.Instance.Lock(name);
        UICpt uiCpt = GetUIBaseCpt(name);
        if (uiCpt == null)
        {
            Debug.LogError("UICpt is null, name = " + name);
            return;
        }

        GuiSystem.Instance.ShowLayer(uiCpt.layerDesc);
        StartCoroutine(uiCpt.ShowPanel(data));
    }
    
    public void ClosePanel(string name)
    {
        if (!allShowUICpts.TryGetValue(name, out var uiCpt)) return;

        InputLockSystem.Instance.Lock(name);
        allShowUICpts.Remove(name);
        StartCoroutine(uiCpt.ClosePanel());
    }
    
    public void HidePanel(string name)
    {
        if (!allShowUICpts.TryGetValue(name, out var uiCpt)) return;
        
        InputLockSystem.Instance.Lock(name);
        uiCpt.Hide();
        GuiSystem.Instance.HideLayer(uiCpt.layerDesc);
        InputLockSystem.Instance.Unlock(name);
    }
    
    public UICpt GetUICpt(string name)
    {
        if (!allShowUICpts.TryGetValue(name, out var uiCpt)) return null;
        return uiCpt;
    }
    
    private UICpt GetUIBaseCpt(string name)
    {
        if (allShowUICpts.TryGetValue(name, out var uiCpt))
        {
            return uiCpt;
        }

        GameObject go = UIFactory.Create(name);
        Type type = allUITypes[name];
        uiCpt = Activator.CreateInstance(type) as UICpt;
        uiCpt?.MAwake(name, go);
        
        allShowUICpts.Add(name, uiCpt);
        return uiCpt;
    }
}