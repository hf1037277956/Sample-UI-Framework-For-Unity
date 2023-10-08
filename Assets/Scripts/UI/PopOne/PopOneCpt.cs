using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopOne)]
public partial class PopOneCpt : UICpt
{
    #region BindingFields
    
    public Button BackBtn;
    
        
    public Button PopTwoBtn;
    
        
    public Button PanelOneBtn;
    
        
    public Button PanelTwoBtn;
    
        
    public GameObject Root;
    
    #endregion
    
    
    
    public override void Awake()
    {
        #region BindingInit
        
        BackBtn = this.Get<Button>("BackBtn");
        
                
        PopTwoBtn = this.Get<Button>("PopTwoBtn");
        
                
        PanelOneBtn = this.Get<Button>("PanelOneBtn");
        
                
        PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
        
                
        Root = this.Get<GameObject>("Root");
        
        #endregion
        
        BackBtn.onClick.AddListener(OnBackBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
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