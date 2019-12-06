using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SystemOptions : MonoBehaviour
{
    private FullScreenMode curMode => Screen.fullScreenMode;
    public TMP_Dropdown dropDown, screenMode;
    private Resolution[] resolutions;
    private Array screenModes;

    // Start is called before the first frame update
    private void Start()
    {
        GetResolutions();
        GetScreenModes();
        dropDown.onValueChanged.AddListener(delegate { SetResolution(dropDown); }); ;
        screenMode.onValueChanged.AddListener(delegate { ChangeScreenMode(screenMode); });
    }

    public void ChangeScreenMode(TMP_Dropdown parDrop)
    {
        if (Enum.IsDefined(typeof(FullScreenMode), (FullScreenMode)parDrop.value))
        {
            FullScreenMode mode = (FullScreenMode)parDrop.value;
            Debug.Log(mode);
            Screen.SetResolution(Screen.width, Screen.height, mode);
        }
    }

    public void GetScreenModes()
    {
        screenMode.ClearOptions();
        screenModes = Enum.GetValues(typeof(FullScreenMode));
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var e in screenModes)
        {
            options.Add(new TMP_Dropdown.OptionData(e.ToString()));
        }
        screenMode.AddOptions(options);
        screenMode.value = Array.IndexOf(screenModes, curMode);
    }

    public void SetResolution(TMP_Dropdown parDrop)
    {
        int i = Mathf.Clamp(parDrop.value, 0, resolutions.Length - 1);
        Resolution rs = resolutions[i];
        Screen.SetResolution(rs.width, rs.height, curMode);
    }

    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
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