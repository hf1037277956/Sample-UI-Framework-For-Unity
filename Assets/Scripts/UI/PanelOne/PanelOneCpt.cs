using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[UI(UIName.PanelOne)]
public class PanelOneCpt : UICpt
{
    public class PanelOneData
    {
        public Action action;
        public List<int> numList;
    }
    
    private Text curNumText;
    
    private Button backBtn;
    private Button popOneBtn;
    private Button popTwoBtn;
    private Button panelTwoBtn;
    
    private ScrollRect scrollRect;
    
    private List<PanelOneItemCpt> itemCptList = new();

    private Action action;
    
    public override void Awake()
    {
        curNumText = Get<Text>("CurNumText");
        
        backBtn = Get<Button>("BackBtn");
        popOneBtn = Get<Button>("PopOneBtn");
        popTwoBtn = Get<Button>("PopTwoBtn");
        panelTwoBtn = Get<Button>("PanelTwoBtn");
        
        backBtn.onClick.AddListener(OnBackBtnClick);
        popOneBtn.onClick.AddListener(OnPopOneBtnClick);
        popTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        panelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
        
        scrollRect = Get<ScrollRect>("Scroll View");
    }

    public override void ApplyInfo()
    {
        PanelOneData data = (PanelOneData)Data;
        if (data == null) return;
        
        action = data.action;
        
        if(data.numList == null) return;
        for (int i = 0; i < data.numList.Count; i++)
        {
            itemCptList.Add(CreateItemCpt(scrollRect.content));
            itemCptList[i].ApplyInfo(data.numList[i]);
        }
    }
    
    public void OnItemBtnClick(int num)
    {
        curNumText.text = num.ToString();
    }

    private void OnBackBtnClick()
    {
        UIManager.Instance.ClosePanel(UIName.PanelOne);
        action?.Invoke();
    }
    
    private void OnPopOneBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopOne);
    }
    
    private void OnPopTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopTwo);
    }
    
    private void OnPanelTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelTwo);
    }

    private PanelOneItemCpt CreateItemCpt(Transform root)
    {
        GameObject prefab = Get<GameObject>("PanelOneItem");
        GameObject go = Object.Instantiate(prefab, root);
        PanelOneItemCpt itemCpt = Create<PanelOneItemCpt>(go);
        return itemCpt;
    }
}