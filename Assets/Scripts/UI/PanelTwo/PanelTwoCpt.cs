using UnityEngine.UI;

[UI(UIName.PanelTwo)]
public class PanelTwoCpt : UICpt
{
    private Button backBtn;
    private Button popOneBtn;
    private Button popTwoBtn;
    private Button panelOneBtn;
    
    public override void Awake()
    {
        backBtn = Get<Button>("BackBtn");
        popOneBtn = Get<Button>("PopOneBtn");
        popTwoBtn = Get<Button>("PopTwoBtn");
        panelOneBtn = Get<Button>("PanelOneBtn");
        
        backBtn.onClick.AddListener(OnBackBtnClick);
        popOneBtn.onClick.AddListener(OnPopOneBtnClick);
        popTwoBtn.onClick.AddListener(OnPopTwoBtnClick);
        panelOneBtn.onClick.AddListener(OnPanelOneBtnClick);
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