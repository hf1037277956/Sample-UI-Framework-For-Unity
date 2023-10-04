using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseCpt
{
    public RectTransform rectTransform;
    public GameObject gameObject;
    public Animation animation;
    public ReferenceCollector rc;
    
    private Dictionary<long, UIBaseCpt> childUIBaseCpts = new();

    public long Id { get; set; }

    public virtual void Awake()
    {
        
    }

    public virtual void Update()
    {
        
    }
    
    public virtual void FixedUpdate()
    {
        
    }
    
    public virtual void LateUpdate()
    {
        
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
