using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A space for me to build tools for methods I often use.
public static class UgreTools 
{
    /// <summary>
    /// Destroy the children of gameobject calling it, if feed a transform as parameter it will kill the 
    /// children of that transform instead.
    /// </summary>
    /// <param name="parTransform"></param>
    /// <param name="parAlternative"></param>
    public static void KillChildren(this Transform parTransform, Transform parAlternative = null)
    {
        Transform parent = parAlternative != null ? parAlternative : parTransform;
        foreach(Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
