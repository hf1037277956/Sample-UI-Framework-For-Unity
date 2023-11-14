using UnityEngine;
using UnityEngine.UI;

public class PanelThreeItemCptCpt : UIBaseCpt
{
    #region BindingFields
            
    private Text _InfoText;
    public Text InfoText
    {
        get
        {
            if (_InfoText == null)
            {
                _InfoText = this.Get<Text>("InfoText");
            }
            return _InfoText;
        }
    }
            
                
    private Image _Bg;
    public Image Bg
    {
        get
        {
            if (_Bg == null)
            {
                _Bg = this.Get<Image>("Bg");
            }
            return _Bg;
        }
    }
            
    #endregion
    
    
    
    public override void Awake()
    {
        base.Awake();
    }

}