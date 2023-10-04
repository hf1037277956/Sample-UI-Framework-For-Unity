using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDesc : MonoBehaviour
{
    [Header("Layer优先度, 值越大越上层")]
    public int Priority;

    [Header("是否隐藏下层")]
    public bool HideLowerLayer;
}