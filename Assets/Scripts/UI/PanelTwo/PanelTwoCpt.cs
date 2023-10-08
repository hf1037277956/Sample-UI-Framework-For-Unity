using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PanelTwo)]
public partial class PanelTwoCpt : UICpt
{
    #region BindingFields
            
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
            
    #endregion

    public override void Awake()
    {
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