using System.Collections;
using UnityEngine;

public partial class PopOneCpt
{
    public override IEnumerator ShowPanel(object data)
    {
        DoPopAnimation(1, 0.5f);
        
        yield return new WaitForSeconds(0.5f);

        yield return base.ShowPanel(data);
    }

    public override IEnumerator ClosePanel()
    {
        DoPopAnimation(0, 0.5f);
        
        yield return new WaitForSeconds(0.5f);

        yield return base.ClosePanel();
    }
}