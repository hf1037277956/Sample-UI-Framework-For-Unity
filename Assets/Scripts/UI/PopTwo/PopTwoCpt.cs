using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopTwo)]
public partial class PopTwoCpt : UICpt
{
    #region BindingFields
    
    public Button BackBtn;
    
        
    public Button PopOneBtn;
    
        
    public Button PanelOneBtn;
    
        
    public Button PanelTwoBtn;
    
        
    public GameObject Root;
    
    #endregion
    
    
    
    public override void Awake()
    {
        #region BindingInit
        
        BackBtn = this.Get<Button>("BackBtn");
        
                
        PopOneBtn = this.Get<Button>("PopOneBtn");
        
                
        PanelOneBtn = this.Get<Button>("PanelOneBtn");
        
                
        PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
        
                
        Root = this.Get<GameObject>("Root");
        
        #endregion
        
        BackBtn.onClick.AddListener(OnBackBtnClick);
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PanelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
        PanelTwoBtn.onClick.AddListener(OnPanelTwoBtnClick);
        
        Root.transform.localScale = Vector3.zero;
    }

    private void DoPopAnimation(float endValue, float duration = 0.5f)
    {
        Root.transform.DOScale(endValue, duration);
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