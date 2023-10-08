using UnityEngine.UI;

public class PanelOneItemCpt : UIBaseCpt
{
    private Text _numText;

    private Button _btn;
    
    private int _num;
    
    public override void Awake()
    {
        _numText = Get<Text>("NumText");
        
        _btn = Get<Button>("Btn");
        
        _btn.onClick.AddListener(OnSelfClick);
    }

    public void ApplyInfo(int num)
    {
        this._num = num;
        this._numText.text = this._num.ToString();

        SetActive(true);
    }
    
    private void OnSelfClick()
    {
        (UIManager.Instance.GetUICpt(UIName.PanelOne) as PanelOneCpt)?.OnItemBtnClick(_num);
    }
}