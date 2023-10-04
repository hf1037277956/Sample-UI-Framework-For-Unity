using System;
using System.Collections.Generic;
using UnityEngine;

public class GuiSystem : MonoBehaviour
{
    public static GuiSystem Instance { get; set; }
    
    [NonSerialized]
    public GameObject GUI;

    public Transform Layer_Create { get; set; }
    [NonSerialized] 
    public Transform Layer_Hide;
    [NonSerialized ]
    public Transform Layer_ShowButHide;
    [NonSerialized ]
    public Transform Layer_Show;
    [NonSerialized ]
    public bool IsLayerDirty;
    
    
    private void Awake()
    {
        Instance = this;
        
        Init();
    }
    
    private void OnDestroy()
    {
        Instance = null;
        
        UnityEngine.Object.Destroy(GUI);
        GUI = null;
        Layer_Hide = null;
        Layer_Show = null;
    }
    
    private void Update()
    {
        if (!IsLayerDirty)
        {
            return;
        }
        IsLayerDirty = false;
        
        var layerDiscs = new List<LayerDesc>();
        layerDiscs.AddRange(Layer_ShowButHide.GetComponentsInChildren<LayerDesc>());
        layerDiscs.AddRange(Layer_Show.GetComponentsInChildren<LayerDesc>());
        layerDiscs.Sort((a, b) => a.Priority.CompareTo(b.Priority));

        bool isHidden = false;
        for (int i = layerDiscs.Count - 1; i > -1; i--)
        {
            var layerDesc = layerDiscs[i];
            
            var parent = isHidden? Layer_ShowButHide : Layer_Show;
            if (layerDesc.transform.parent != parent)
            {
                layerDesc.transform.SetParent(parent, false);
            }
            layerDesc.transform.SetAsFirstSibling();
            
            if(!isHidden && layerDesc.HideLowerLayer)
            {
                isHidden = true;
            }
        }
    }

    private void Init()
    {
        GUI = this.gameObject;

        Layer_Create = GUI.transform.Find("LayerRoot/CreateLayer");
        Layer_Hide = GUI.transform.Find("LayerRoot/HideLayer");
        Layer_ShowButHide = GUI.transform.Find("LayerRoot/ShowButHideLayer");
        Layer_Show = GUI.transform.Find("LayerRoot/ShowLayer");
    }
    
    public void ShowLayer(LayerDesc layerDesc)
    {
        layerDesc.transform.SetParent(Instance.Layer_Show, false);
        layerDesc.gameObject.SetActiveIfNot(true);
        
        Instance.IsLayerDirty = true;
    }
    
    public void HideLayer(LayerDesc layerDesc)
    {
        layerDesc.transform.SetParent(Instance.Layer_Hide, false);

        Instance.IsLayerDirty = true;
    }
    
    public void RemoveLayer(LayerDesc layerDesc)
    {
        Instance.IsLayerDirty = true;
    }
}