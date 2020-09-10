using System;
using UnityEngine;

public class 
    
    
    GameUICanvas : MonoBehaviour
{
    [SerializeField] private BigPanel Gameui = null;

    // Start is called before the first frame update
    private void Start() => GameManager.GameStateChangeEvent += ShowGameUi;

    // Update is called once per frame
    private void Update()
    {
        if (KeyBindings.HideAllKey.KeyDown)
        {
            if (GameManager.CurState.Equals(GameState.Free))
            {
                GameObject gameUiObject = Gameui.gameObject;
                gameUiObject.SetActive(!gameUiObject.activeSelf);
                gameUI_Active = gameUiObject.activeSelf;
            }
        }
    }

    private bool gameUI_Active = true;

    private void ShowGameUi(GameState state) => Gameui.gameObject.SetActive(state == GameState.Free && gameUI_Active);
}

public static class GameUISettings
{
    public enum SliderType
    {
        Slider,
        Sphere,
    }

    private const string FluidSliderTypeSaveName = "FluidSliderType";
    private static SliderType fluidSliderType = SliderType.Sphere;
    private static bool FluidFirstTimeGet = true;

    public static SliderType FluidSliderType
    {
        get
        {
            if (FluidFirstTimeGet && PlayerPrefs.HasKey(FluidSliderTypeSaveName))
            {
                FluidSliderType = (SliderType)PlayerPrefs.GetInt(FluidSliderTypeSaveName);
                FluidFirstTimeGet = false;
            }
            return fluidSliderType;
        }

        private set
        {
            if (Enum.IsDefined(typeof(SliderType), value))
            {
                PlayerPrefs.SetInt(FluidSliderTypeSaveName, (int)value);
                fluidSliderType = value;
            }
        }
    }

    public static SliderType ToggleSliderType => FluidSliderType = FluidSliderType.CycleThoughEnum();
}