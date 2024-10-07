using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Extensions
{
    public static void ClearTransform(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static void SetListener(this Button button, UnityAction listener)
    {
        button.onClick.SetListener(listener);
    }

    public static void SetListener(this UnityEvent unityEvent, UnityAction listener)
    {
        unityEvent.RemoveAllListeners();
        unityEvent.AddListener(listener);
    }
}