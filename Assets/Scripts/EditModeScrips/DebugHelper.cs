using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugHelper
{
    /// <summary>
    /// Call this method to check if the component of a script is null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="variableToCheck">Components you want to check.</param>
    /// <param name="objectCallingThis">Script calling this method. Just pass in *this* as a parameter.</param>
    public static void CheckupComponent<T>(T variableToCheck, Object objectCallingThis)
    {
        string missingComponentText = string.Empty;
        string objectString = string.Empty;
        string typeName = typeof(T).Name;

        if (variableToCheck == null)
        {
            missingComponentText += $"<b><color=red> {typeName} </color></b> ";
            objectString += $"<b><color=yellow> {objectCallingThis} </color></b> ";
            Debug.LogWarning($"<color=orange> <size=12> <b> Checkup {objectString} Missing Component: </b></size></color> {missingComponentText} ", objectCallingThis);
        }
    }
}
