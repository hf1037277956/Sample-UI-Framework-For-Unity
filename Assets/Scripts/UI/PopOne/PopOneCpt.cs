using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopOne)]
public partial class PopOneCpt : UICpt
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