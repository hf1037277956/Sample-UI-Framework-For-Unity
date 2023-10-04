using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[UI(UIName.PopOne)]
public partial class PopOneCpt : UICpt
{
    private Button backBtn;
    private Button popTwoBtn;
    private Button panelOneBtn; 
    private Button panelTwoBtn;

    private GameObject root;
    
    public override void Awake()
    {
        backBtn = Get<Button>("BackBtn");
        popTwoBtn = Get<Button>("PopTwoBtn");
        panelOneBtn = Get<Button>("PanelOneBtn");
        panelTwoBtn = Get<Button>("PanelTwoBtn");
        
        backBtn.onClick.AddListener(OnBackBtnClick);
        popTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        panelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
        panelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);

        root = Get<GameObject>("Root");
        
        root.transform.localScale = Vector3.zero;
    }

    private void DoPopAnimation(float endValue, float duration = 0.5f)
    {
        root.transform.DOScale(endValue, duration);
        canvasGroup.DOFade(endValue, duration);
    }

    private void OnBackBtnClick()
    {
        UIManager.Instance.ClosePanel(UIName.PopOne);
    }
    
    private void OnPanelOneBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelOne);
    }
     
    private void OnPanelTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PanelTwo);
    }
    
    private void OnPopTwoBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopTwo);
    }
}