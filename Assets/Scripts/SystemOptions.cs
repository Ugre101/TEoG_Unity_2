using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemOptions : MonoBehaviour
{
    public TextMeshProUGUI screenBtn;
    private FullScreenMode curMode;
    // Start is called before the first frame update
    void Start()
    {
        screenBtn.text = Screen.fullScreenMode.ToString();
        curMode = Screen.fullScreenMode;
    }

    public void ChangeScreenMode()
    {
        FullScreenMode mode()
        {
            switch (curMode)
            {
                case FullScreenMode.ExclusiveFullScreen:
                    return FullScreenMode.FullScreenWindow;
                case FullScreenMode.FullScreenWindow:
                    return FullScreenMode.MaximizedWindow;
                case FullScreenMode.MaximizedWindow:
                    return FullScreenMode.Windowed;
                case FullScreenMode.Windowed:
                default:
                    return FullScreenMode.ExclusiveFullScreen;
            }
        }
        Debug.Log(mode());
        curMode = mode();
        Screen.fullScreenMode = curMode;
        Screen.SetResolution(Screen.width, Screen.height, curMode);
         screenBtn.text = Screen.fullScreenMode.ToString();
        Debug.Log(Screen.fullScreenMode);
    }
    public void SetResolution()
    {
        int i = 0;
        switch (i)
        {
            case 0:
                Screen.SetResolution(800, 600, curMode);
                break;
            case 1:
                Screen.SetResolution(1024, 768, curMode);
                break;
            case 2:
                Screen.SetResolution(1280, 720, curMode);
                break;
            case 3:
                Screen.SetResolution(1280, 960, curMode);
                break;
            case 4:
                Screen.SetResolution(1280, 1024, curMode);
                break;
            case 5:
                Screen.SetResolution(1440, 900, curMode);
                break;
            case 6:
                Screen.SetResolution(1600, 900, curMode);
                break;
            case 7:
                Screen.SetResolution(1600, 1200, curMode);
                break;
            case 8:
                Screen.SetResolution(1680, 1050, curMode);
                break;
            case 9:
                Screen.SetResolution(1920, 1080, curMode);
                break;
            case 10:
                Screen.SetResolution(1920, 1200, curMode);
                break;
            case 11:
                Screen.SetResolution(2560, 1440, curMode);
                break;
        }
    }
}
