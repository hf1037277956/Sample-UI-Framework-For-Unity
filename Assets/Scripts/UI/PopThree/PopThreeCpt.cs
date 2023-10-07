using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopThree)]
public partial class PopThreeCpt : UICpt
{
    #region Binding
            
    private Button _BackBtn;
    public Button BackBtn
    {
        get
        {
            if (_BackBtn == null)
            {
                _BackBtn = this.Get<Button>("BackBtn");
            }
            return _BackBtn;
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
            
                
    private GameObject _Root;
    public GameObject Root
    {
        get
        {
            if (_Root == null)
            {
                _Root = this.Get<GameObject>("Root");
            }
            return _Root;
        }
    }
            
    #endregion
    
    public override void Awake()
    {
        base.Awake();
    }

    public override void ApplyInfo()
    {
        base.ApplyInfo();
    }
}