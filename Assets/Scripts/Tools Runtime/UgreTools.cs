using UnityEngine;

// A space for me to build tools for methods I often use.
public static class UgreTools
{
    /// <summary> Destroy the children of gameobject calling it </summary>
    public static void KillChildren(this Transform parTransform)
    {
        foreach (Transform child in parTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary> Destroy the children of given gameobject </summary>
    public static void KillChildren(this Transform parTransform, Transform parAlternative)
    {
        foreach (Transform child in parAlternative)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    /// <summary> Sets children of transform to inactive; (setActive(false)) </summary>
    public static void SleepChildren(this Transform parTransform)
    {
        foreach (Transform child in parTransform)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary> Sets all except except to inactive. </summary>
    public static void SleepChildren(this Transform parTransfrom, Transform except)
    {
        parTransfrom.SleepChildren();
        except.gameObject.SetActive(true);
    }

    public static string FirstSecondEtc(this int parInt, bool capitalLetter = false)
    {
        switch (parInt)
        {
            case 1:
                return capitalLetter ? "First" : "first";

            case 2:
                return capitalLetter ? "Second" : "second";

            case 3:
                return capitalLetter ? "Third" : "third";

            default:
                return parInt + "th";
        }
    }
    public static PlayerMain GetPlayer(this GameObject gameObject) => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
}