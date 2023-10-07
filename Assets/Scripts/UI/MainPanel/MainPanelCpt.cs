using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[UI(UIName.MainPanel)]
public partial class MainPanelCpt : UICpt
{
    #region BindingFields
    
    public Text TimeText;
    
        
    public Button PopOneBtn;
    
        
    public Button PopTwoBtn;
    
        
    public Button PanelOneBtn;
    
        
    public Button PanelTwoBtn;
    
    #endregion
    
    private float lastUpdateTime;
    
    private List<int> numList = new List<int>();
    
    public override void Awake()
    {
        #region BindingInit
        
        TimeText = this.Get<Text>("TimeText");
        
                
        PopOneBtn = this.Get<Button>("PopOneBtn");
        
                
        PopTwoBtn = this.Get<Button>("PopTwoBtn");
        
                
        PanelOneBtn = this.Get<Button>("PanelOneBtn");
        
                
        PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
        
        #endregion
        
        TimeText.text = DateTime.Now.ToString("HH:mm:ss");
        
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        PanelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
        PanelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
    }

    public override void Update()
    {
        //限制刷新速度
        if(Time.time - lastUpdateTime < 1f) return;
        lastUpdateTime = Time.time;
            
        TimeText.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public override void ApplyInfo()
    {
        // 随机生成20个数字
        Random random = new Random();
        numList.Clear();
        for (int i = 0; i < 20; i++)
        {
            var num = random.Next(0, 100);
            numList.Add(num);
        }
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
            numList = numList
        });
    }
    
    private void OnPanelTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelTwo);
    }
}