using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopTwo)]
public partial class PopTwoCpt : UICpt
{
    private Button backBtn;
    private Button popOneBtn;
    private Button panelOneBtn;
    private Button panelTwoBtn;

    private GameObject root;
    
    public override void Awake()
    {
        backBtn = Get<Button>("BackBtn");
        popOneBtn = Get<Button>("PopOneBtn");
        panelOneBtn = Get<Button>("PanelOneBtn");
        panelTwoBtn = Get<Button>("PanelTwoBtn");
        
        backBtn.onClick.AddListener(OnBackBtnClick);
        popOneBtn.onClick.AddListener(OnPopOneBtnClick);
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
        UIManager.Instance.ClosePanel(UIName.PopTwo);
    }
    
    private void OnPanelOneBtnClick()
    {
        UIManager.Instance.HidePanel(UIName.PopTwo);
        UIManager.Instance.ShowPanel(UIName.PanelOne, new PanelOneCpt.PanelOneData()
        {
            action = OnPanelOneBackPopTwo
        });
    }
    
    private void OnPanelTwoBtnClick()
    {
        UIManager.Instance.ClosePanel(UIName.PopTwo);
        UIManager.Instance.ShowPanel(UIName.PanelTwo);
    }
    
    private void OnPopOneBtnClick()
    {
        UIManager.Instance.ShowPanel(UIName.PopOne);
    }

    private void OnPanelOneBackPopTwo()
    {
        UIManager.Instance.ShowPanel(UIName.PopTwo);
    }
}