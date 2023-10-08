using System.Collections;
using UnityEngine;

public partial class PopOneCpt
{
    public override IEnumerator ShowPanel(object data)
    {
        DoPopAnimation(1, 0.2f);
        
        yield return new WaitForSeconds(0.2f);
        
        yield return base.ShowPanel(data);
    }
    
    public override IEnumerator ClosePanel()
    {
        DoPopAnimation(0, 0.2f);
        
        yield return new WaitForSeconds(0.2f);
        
        yield return base.ClosePanel();
    }
}