using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopThree)]
public partial class PopThreeCpt : UICpt
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
        
        
    }

    public override void ApplyInfo()
    {
        base.ApplyInfo();
    }
}