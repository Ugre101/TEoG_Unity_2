using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// A space for me to build tools for methods I often use.
public static class UgreTools
{
    private static readonly System.Random Rnd = new System.Random();

    public static T CycleThoughEnum<T>(this T curValue) where T : Enum
    {
        if (typeof(T).IsEnum)
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            int index = Array.FindIndex(array, s => s.Equals(curValue));
            return array[index == array.Length - 1 ? 0 : index + 1];
        }
        throw new ArgumentException("Type must be of type enum");
    }

    public static List<T> EnumToList<T>() where T : Enum
    {
        if (typeof(T).IsEnum)
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = array.ToList();
            return list;
        }
        throw new ArgumentException("<T> must be enum");
    }

    public static List<TMP_Dropdown.OptionData> EnumToOptionDataList<T>() where T : Enum
    {
        if (typeof(T).IsEnum)
        {
            TMP_Dropdown.OptionDataList optionList = new TMP_Dropdown.OptionDataList();
            List<T> list = EnumToList<T>();
            list.ForEach(l => optionList.options.Add(new TMP_Dropdown.OptionData(l.ToString())));
            return optionList.options;
        }
        throw new ArgumentException("<T> must be enum");
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

    public static float GetFloatPref(float val, string saveName) => PlayerPrefs.HasKey(saveName) ? PlayerPrefs.GetFloat(saveName) : val;

    public static bool GetPlayerPrefBool(string name) => PlayerPrefs.HasKey(name) && PlayerPrefs.GetInt(name) == 1;

    public static T JsonClone<T>(T source) => JsonUtility.FromJson<T>(JsonUtility.ToJson(source));

    /// <summary> Destroy the children of gameobject calling it </summary>
    public static void KillChildren(this Transform parTransform)
    {
        foreach (Transform child in parTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static bool OnHotKeyDownIfKeybindsActive(this KeyCode key) => GameManager.KeyBindsActive && Input.GetKeyDown(key);

    public static void SetActiveChildren(this Transform transform, bool setTo = true)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(setTo);
        }
    }

    public static void SetPlayerPrefBool(string name, bool boolVal) => PlayerPrefs.SetInt(name, boolVal ? 1 : 0);

    /// <summary> Sets children of transform to inactive; (setActive(false)) </summary>
    public static void SleepChildren(this Transform parTransform) => SetActiveChildren(parTransform, false);

    /// <summary> Sets all except except to inactive. </summary>
    public static void SleepChildren(this Transform parTransfrom, Transform except)
    {
        parTransfrom.SleepChildren();
        except.gameObject.SetActive(true);
    }

    public static void SleepChildren(this Transform parTransfrom, IEnumerable<Transform> exceptions)
    {
        parTransfrom.SleepChildren();
        foreach (Transform transform in exceptions)
        {
            transform.gameObject.SetActive(true);
        }
    }

    public static void ToggleGameObject(this GameObject go) => go.SetActive(!go.activeSelf);

    public static IEnumerator WaitAFrameAndExecute(UnityAction action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    public static IEnumerator WaitAFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    public static T RandomListValue<T>(this List<T> list) => list[Rnd.Next(list.Count)];

    public static bool HitPlayer(this Collider2D collider) => collider.CompareTag(PlayerSprite.Tag);

}