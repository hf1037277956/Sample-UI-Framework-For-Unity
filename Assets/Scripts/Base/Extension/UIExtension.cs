using UnityEngine;

public static class UIExtension
{
    public static void SetActiveIfNot(this GameObject gameObject, bool active)
    {
        if (!gameObject || gameObject.activeSelf == active) return;

        gameObject.SetActive(active);
    }
}