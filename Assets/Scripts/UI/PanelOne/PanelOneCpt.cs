using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[UI(UIName.PanelOne)]
public partial class PanelOneCpt : UICpt
{
    #region BindingFields
            
    private Text _CurNumText;
    public Text CurNumText
    {
        get
        {
            if (_CurNumText == null)
            {
                _CurNumText = this.Get<Text>("CurNumText");
            }
            return _CurNumText;
        }
    }
            
                
    private Button _BackBtn;
    public Button BackBtn
    {
        get
        {
            if (_BackBtn == null)
            {
                _BackBtn = this.Get<Button>("BackBtn");
            }
            return _BackBtn;
        }
    }
            
                
    private Button _PopOneBtn;
    public Button PopOneBtn
    {
        get
        {
            if (_PopOneBtn == null)
            {
                _PopOneBtn = this.Get<Button>("PopOneBtn");
            }
            return _PopOneBtn;
        }
    }
            
                
    private Button _PopTwoBtn;
    public Button PopTwoBtn
    {
        get
        {
            if (_PopTwoBtn == null)
            {
                _PopTwoBtn = this.Get<Button>("PopTwoBtn");
            }
            return _PopTwoBtn;
        }
    }
            
                
    private Button _PanelTwoBtn;
    public Button PanelTwoBtn
    {
        get
        {
            if (_PanelTwoBtn == null)
            {
                _PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
            }
            return _PanelTwoBtn;
        }
    }
            
                
    private ScrollRect _ScrollView;
    public ScrollRect ScrollView
    {
        get
        {
            if (_ScrollView == null)
            {
                _ScrollView = this.Get<ScrollRect>("Scroll View");
            }
            return _ScrollView;
        }
    }
            
                
    private GameObject _PanelOneItem;
    public GameObject PanelOneItem
    {
        get
        {
            if (_PanelOneItem == null)
            {
                _PanelOneItem = this.Get<GameObject>("PanelOneItem");
            }
            return _PanelOneItem;
        }
    }
            
    #endregion

    public class PanelOneData
    {
        public Action Action;
        public List<int> NumList;
    }

    private Action _action;
    
    private readonly List<PanelOneItemCpt> _itemCptList = new List<PanelOneItemCpt>();

    public override void Awake()
    {
        BackBtn.onClick.AddListener(OnBackBtnClick);
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        PanelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
    }

    public override void ApplyInfo()
    {
        PanelOneData data = (PanelOneData)Data;
        if (data == null) return;
        
        _action = data.Action;
        
        if(data.NumList == null) return;
        for (int i = 0; i < data.NumList.Count; i++)
        {
            _itemCptList.Add(CreateItemCpt(ScrollView.content));
            _itemCptList[i].ApplyInfo(data.NumList[i]);
        }
    }
    
    public void OnItemBtnClick(int num)
    {
        CurNumText.text = num.ToString();
    }

    private void OnBackBtnClick()
    {
        UIManager.Instance.ClosePanel(UIName.PanelOne);
        _action?.Invoke();
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