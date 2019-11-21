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

    // Update is called once per frame
    void Update()
    {
        
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
}
