using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UI(UIName.PopTwo)]
public partial class PopTwoCpt : UICpt
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
            Action = OnPanelOneBackPopTwo
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