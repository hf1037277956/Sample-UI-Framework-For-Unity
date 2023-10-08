using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[UI(UIName.PanelOne)]
public partial class PanelOneCpt : UICpt
{
    public class PanelOneData
    {
        public List<int> numList;
        public Action action;
    }
    
    #region BindingFields
    
    public Text CurNumText;
    
        
    public Button BackBtn;
    
        
    public Button PopOneBtn;
    
        
    public Button PopTwoBtn;
    
        
    public Button PanelTwoBtn;
    
        
    public ScrollRect ScrollView;
    
        
    public GameObject PanelOneItem;
    
    #endregion
    
    private Action action;
    
    private List<PanelOneItemCpt> itemCptList = new List<PanelOneItemCpt>();
    
    public override void Awake()
    {
        #region BindingInit
        
        CurNumText = this.Get<Text>("CurNumText");
        
                
        BackBtn = this.Get<Button>("BackBtn");
        
                
        PopOneBtn = this.Get<Button>("PopOneBtn");
        
                
        PopTwoBtn = this.Get<Button>("PopTwoBtn");
        
                
        PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
        
                
        ScrollView = this.Get<ScrollRect>("Scroll View");
        
                
        PanelOneItem = this.Get<GameObject>("PanelOneItem");
        
        #endregion
        
        BackBtn.onClick.AddListener(OnBackBtnClick);
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        PanelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
    }

    public override void ApplyInfo()
    {
        PanelOneData data = (PanelOneData)Data;
        if (data == null) return;
        
        action = data.action;
        
        if(data.numList == null) return;
        for (int i = 0; i < data.numList.Count; i++)
        {
            itemCptList.Add(CreateItemCpt(ScrollView.content));
            itemCptList[i].ApplyInfo(data.numList[i]);
        }
    }
    
    public void OnItemBtnClick(int num)
    {
        CurNumText.text = num.ToString();
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
        GameObject prefab = PanelOneItem;
        GameObject go = Object.Instantiate(prefab, root);
        PanelOneItemCpt itemCpt = Create<PanelOneItemCpt>(go);
        return itemCpt;
    }
}