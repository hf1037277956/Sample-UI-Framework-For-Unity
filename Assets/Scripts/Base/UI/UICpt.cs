using System.Collections;
using UnityEngine;

public class UICpt : UIBaseCpt
{
    public CanvasGroup canvasGroup;

    public LayerDesc layerDesc;
    
    public string Name { get; set; }
    public object Data { get; set; }

    public virtual void ApplyInfo() {}
    
    public virtual void PostApplyInfo(){}

    public virtual IEnumerator ShowPanel(object data)
    {
        Data = data;
        this.ApplyInfo();
        canvasGroup.alpha = 1;
        SetActive(true, PostApplyInfo);
        InputLockSystem.Instance.Unlock(Name);
        yield break;
    }

    public virtual IEnumerator ClosePanel()
    {
        SetActive(false);
        GuiSystem.Instance.RemoveLayer(this.layerDesc);
        this.Destroy();
        InputLockSystem.Instance.Unlock(Name);
        yield break;
    }

    private void Destroy(bool force = false)
    {
        UnityEngine.Object.Destroy(gameObject);
        
        Data = null;
    }
    
    public void MAwake(string name, GameObject go)
    {
        if (go == null) return;
        
        Name = name;
        canvasGroup = go.transform.GetComponent<CanvasGroup>();
        layerDesc = go.transform.GetComponent<LayerDesc>();
            
        base.MAwake(go);
    }
}