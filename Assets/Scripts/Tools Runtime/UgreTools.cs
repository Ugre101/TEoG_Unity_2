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
    public static bool GetPlayerPrefBool(string name) => PlayerPrefs.HasKey(name) ? PlayerPrefs.GetInt(name) == 1 : false;

    public static void SetPlayerPrefBool(string name, bool boolVal) => PlayerPrefs.SetInt(name, boolVal ? 1 : 0);

    public static float GetFloatPref(float val, string saveName) => PlayerPrefs.HasKey(saveName) ? PlayerPrefs.GetFloat(saveName) : val;

    /// <summary> Destroy the children of gameobject calling it </summary>
    public static void KillChildren(this Transform parTransform)
    {
        foreach (Transform child in parTransform)
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

    public static void SleepChildren(this Transform parTransfrom, List<Transform> exceptions)
    {
        parTransfrom.SleepChildren();
        foreach (Transform transform in exceptions)
        {
            transform.gameObject.SetActive(true);
        }
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

    public static List<T> EnumToList<T>()
    {
        if (typeof(T).IsEnum)
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = array.ToList();
            return list;
        }
        throw new ArgumentException("<T> must be enum");
    }

    public static List<TMP_Dropdown.OptionData> EnumToOptionDataList<T>()
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

    public static T JsonClone<T>(T source) => JsonUtility.FromJson<T>(JsonUtility.ToJson(source));

    public static T CycleThoughEnum<T>(T curValue)
    {
        if (typeof(T).IsEnum)
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            int index = Array.FindIndex(array, s => s.Equals(curValue));
            return array[index == array.Length - 1 ? 0 : index + 1];
        }
        throw new ArgumentException("Type must be a enum");
    }
    public static IEnumerator waitAFrame(UnityAction action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
}