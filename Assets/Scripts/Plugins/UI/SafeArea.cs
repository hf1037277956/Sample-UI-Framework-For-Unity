using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Update()
    {
        float minX = Screen.safeArea.xMin / Screen.width;
        float maxX = Screen.safeArea.xMax / Screen.width;
        float minY = Screen.safeArea.yMin / Screen.height;
        float maxY = Screen.safeArea.yMax / Screen.height;

        var rt = this.transform as RectTransform;
        rt.anchorMin = new Vector2(minX, minY);
        rt.anchorMax = new Vector2(maxX, maxY);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
