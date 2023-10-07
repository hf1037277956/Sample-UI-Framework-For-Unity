using UnityEngine;
using UnityEngine.UI;

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
    
    
    
    public override void Awake()
    {
        #region BindingInit
        
        TimeText = this.Get<Text>("TimeText");
        
                
        PopOneBtn = this.Get<Button>("PopOneBtn");
        
                
        PopTwoBtn = this.Get<Button>("PopTwoBtn");
        
                
        PanelOneBtn = this.Get<Button>("PanelOneBtn");
        
                
        PanelTwoBtn = this.Get<Button>("PanelTwoBtn");
        
        #endregion
        
        
    }

    public override void ApplyInfo()
    {
        base.ApplyInfo();
    }
}