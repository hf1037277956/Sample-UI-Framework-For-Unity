using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[UI(UIName.MainPanel)]
public class MainPanel : UICpt
{
    private Text timeText;
    
    private Button popOneBtn;
    private Button popTwoBtn;
    private Button panelOneBtn;
    private Button panelTwoBtn;
    
    private List<int> numList = new();
    
    private float lastUpdateTime;

    public override void Awake()
    {
        timeText = Get<Text>("TimeText");
        timeText.text = DateTime.Now.ToString("HH:mm:ss");

        popOneBtn = Get<Button>("PopOneBtn");
        popTwoBtn = Get<Button>("PopTwoBtn");
        panelOneBtn = Get<Button>("PanelOneBtn");
        panelTwoBtn = Get<Button>("PanelTwoBtn");
        
        popOneBtn.onClick.AddListener(OnPopOneBtnClick);
        popTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        panelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
        panelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
    }

    public override void Update()
    {
        //限制刷新速度
        if(Time.time - lastUpdateTime < 1f) return;
        lastUpdateTime = Time.time;
            
        timeText.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public override void ApplyInfo()
    {
        // 随机生成20个数字
        Random random = new();
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