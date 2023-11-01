using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseCpt
{
    public Animation animation;
    public GameObject gameObject;
    public RectTransform rectTransform;
    public ReferenceCollector rc;
    
    private readonly Dictionary<long, UIBaseCpt> childUIBaseCpts = new Dictionary<long, UIBaseCpt>();

    public long Id { get; set; }

    public virtual void Awake()
    {
        
    }

    public virtual void Update()
    {
        // 调用所有子UIBaseCpt的Update
        if (childUIBaseCpts.Count <= 0) return;
        foreach (var uiBaseCpt in childUIBaseCpts.Values)
        {
            uiBaseCpt.Update();
        }
    }
    
    public virtual void FixedUpdate()
    {
        // 调用所有子UIBaseCpt的FixedUpdate
        if (childUIBaseCpts.Count <= 0) return;
        foreach (var uiBaseCpt in childUIBaseCpts.Values)
        {
            uiBaseCpt.FixedUpdate();
        }
    }
    
    public virtual void LateUpdate()
    {
        // 调用所有子UIBaseCpt的LateUpdate
        if (childUIBaseCpts.Count <= 0) return;
        foreach (var uiBaseCpt in childUIBaseCpts.Values)
        {
            uiBaseCpt.LateUpdate();
        }
    }

    public virtual void SetActive(bool active , Action action = null)
    {
        gameObject.SetActiveIfNot(active);
        action?.Invoke();
    }

    public virtual void Hide()
    {
        SetActive(false);
    }
    
    public virtual T Get<T>(string key) where T : class
    {
        return rc.Get<T>(key);
    }
    
    public virtual T Find<T>(string path)
    {
        return rectTransform.Find(path).GetComponent<T>();
    }
    
    public virtual GameObject Find(string path)
    {
        return rectTransform.Find(path).gameObject;
    }
    
    public void MAwake(GameObject go)
    {
        gameObject = go;
        rectTransform = go.transform as RectTransform;

        animation = go.GetComponent<Animation>();
        rc = go.GetComponent<ReferenceCollector>();
        
        Id = long.Parse(TimestampID.GetInstance().GetID());

        Awake();
    }
    
    public T Create<T>(GameObject go) where T : UIBaseCpt
    {
        Type type = typeof(T);
        T t = (T)Activator.CreateInstance(type);
        t.MAwake(go);

        childUIBaseCpts.Add(t.Id, t);
        return t;
    }
}
