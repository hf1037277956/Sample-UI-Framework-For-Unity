using UnityEngine.UI;

public class PanelOneItemCpt : UIBaseCpt
{
    private Text numText;

    private Button Btn;
    
    private int num;
    
    public override void Awake()
    {
        numText = Get<Text>("NumText");
        
        Btn = Get<Button>("Btn");
        
        Btn.onClick.AddListener(OnSelfClick);
    }

    public void ApplyInfo(int _num)
    {
        num = _num;
        this.numText.text = num.ToString();

        SetActive(true);
    }
    
    private void OnSelfClick()
    {
        (UIManager.Instance.GetUICpt(UIName.PanelOne) as PanelOneCpt)?.OnItemBtnClick(num);
    }
}