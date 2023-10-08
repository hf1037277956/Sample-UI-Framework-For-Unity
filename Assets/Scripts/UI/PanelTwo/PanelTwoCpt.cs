using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PanelTwo)]
public partial class PanelTwoCpt : UICpt
{
    #region BindingFields
    
    public Button BackBtn;
    
        
    public Button PopOneBtn;
    
        
    public Button PopTwoBtn;
    
        
    public Button PanelOneBtn;
    
    #endregion
    
    
    
    public override void Awake()
    {
        #region BindingInit
        
        BackBtn = this.Get<Button>("BackBtn");
        
                
        PopOneBtn = this.Get<Button>("PopOneBtn");
        
                
        PopTwoBtn = this.Get<Button>("PopTwoBtn");
        
                
        PanelOneBtn = this.Get<Button>("PanelOneBtn");
        
        #endregion
        
        BackBtn.onClick.AddListener(OnBackBtnClick);
        PopOneBtn.onClick.AddListener(OnPopOneBtnClick);
        PopTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        PanelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
    }

    private void OnBackBtnClick()
    {
        UIManager.Instance.ClosePanel(UIName.PanelTwo);
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
        UIManager.Instance.ShowPanel(UIName.PanelOne);
    }
}