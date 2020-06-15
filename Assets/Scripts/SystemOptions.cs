using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemOptions : MonoBehaviour
{
    private FullScreenMode CurMode => Screen.fullScreenMode;

    [SerializeField] private TMP_Dropdown dropDown = null, screenMode = null;

    // Start is called before the first frame update
    private void Start()
    {
        GetResolutions();
        GetScreenModes();
        dropDown.onValueChanged.AddListener(delegate { ScreenSetting.SetResolution(dropDown); }); ;
        screenMode.onValueChanged.AddListener(delegate { ScreenSetting.ChangeScreenMode(screenMode); });
    }

    public void GetScreenModes()
    {
        screenMode.ClearOptions();
        Array screenModes = Enum.GetValues(typeof(FullScreenMode));
        List<TMP_Dropdown.OptionData> options = UgreTools.EnumToOptionDataList<FullScreenMode>();
        screenMode.AddOptions(options);
        screenMode.value = Array.IndexOf(screenModes, CurMode);
    }

    private void GetResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        dropDown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution rs in resolutions)
        {
            options.Add(new TMP_Dropdown.OptionData($"{rs.height}x{rs.width}"));
        }
        dropDown.AddOptions(options);
        dropDown.value = options.Count - 1;
    }
}

public static class ScreenSetting
{
    private static FullScreenMode CurMode => Screen.fullScreenMode;

    public static Resolution[] Resolutions { get; } = Screen.resolutions;

    public static Array ScreenModes { get; } = Enum.GetValues(typeof(FullScreenMode));

    private const string ResKey = "Resolution";
    private const string ScreenKey = "ScreenMode";

    public static void Load()
    {
        if (PlayerPrefs.HasKey(ResKey))
        {
            SetResolution(PlayerPrefs.GetInt(ResKey));
        }
        if (PlayerPrefs.HasKey(ScreenKey))
        {
            ChangeScreenMode(PlayerPrefs.GetInt(ScreenKey));
        }
    }

    public static void SetResolution(TMP_Dropdown parDrop) => SetResolution(parDrop.value);

    public static void SetResolution(int parDrop)
    {
        int i = Mathf.Clamp(parDrop, 0, Resolutions.Length - 1);
        Resolution rs = Resolutions[i];
        Screen.SetResolution(rs.width, rs.height, CurMode);
        PlayerPrefs.SetInt(ResKey, i);
    }

    public static void ChangeScreenMode(TMP_Dropdown parDrop) => ChangeScreenMode(parDrop.value);

    public static void ChangeScreenMode(int parDrop)
    {
        if (Enum.IsDefined(typeof(FullScreenMode), (FullScreenMode)parDrop))
        {
            FullScreenMode mode = (FullScreenMode)parDrop;
            Screen.SetResolution(Screen.width, Screen.height, mode);
            PlayerPrefs.SetInt(ScreenKey, parDrop);
        }
    }
}