using System;
using System.Collections.Generic;
using Config.charcter;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[UI(UIName.MainPanel)]
public partial class MainPanelCpt : UICpt
{
    #region BindingFields
            
    private Text _TimeText;
    public Text TimeText
    {
        get
        {
            if (_TimeText == null)
            {
                _TimeText = this.Get<Text>("TimeText");
            }
            return _TimeText;
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
            
                
    private Button _PanelOneBtn;
    public Button PanelOneBtn
    {
        get
        {
            if (_PanelOneBtn == null)
            {
                _PanelOneBtn = this.Get<Button>("PanelOneBtn");
            }
            return _PanelOneBtn;
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
            
    #endregion

    private float _lastUpdateTime;
    
    private readonly List<int> _numList = new List<int>();
    
    public override void Awake()
    {
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        PanelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
        PanelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
        
        TimeText.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public override void Update()
    {
        //限制刷新速度
        if(Time.time - _lastUpdateTime < 1f) return;
        _lastUpdateTime = Time.time;
            
        TimeText.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public override void ApplyInfo()
    {
        // 随机生成20个数字
        Random random = new Random();
        _numList.Clear();
        for (int i = 0; i < 20; i++)
        {
            var num = random.Next(0, 100);
            _numList.Add(num);
        }

        Debug.Log(TbCharacterConfig.Instance.Get(101).Name);
    }
    
    public void OnGameStart()
    {
        Debug.Log("MainPanel successfully received GameStart event!");
    }

    private void OnPopOneBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopOne);
    }
    
    private void OnPopTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopTwo);
    }
    
    private void OnPanelOneBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelOne, new PanelOneCpt.PanelOneData()
        {
            NumList = _numList
        });
    }
    
    private void OnPanelTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelTwo);
    }
}