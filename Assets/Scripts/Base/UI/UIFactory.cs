using UnityEngine;

public static class UIFactory
{
    public static GameObject Create(string name)
    {
        GameObject obj = Resources.Load<GameObject>("UI/" + name);
        
        if (obj == null)
        {
            Debug.LogError("UIPrefab is null, name = " + name);
            return null;
        }

        GameObject go = Object.Instantiate(obj, GuiSystem.Instance.Layer_Create);
        go.name = name;
        go.GetComponent<CanvasGroup>().alpha = 0;
        
        return go;
    }
}