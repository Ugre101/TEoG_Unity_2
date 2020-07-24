using System;
using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private BigPanel Gameui = null;

    // Start is called before the first frame update
    private void Start() => GameManager.GameStateChangeEvent += ShowGameUI;

    // Update is called once per frame
    private void Update()
    {
        if (KeyBindings.HideAllKey.KeyDown)
        {
            if (GameManager.CurState.Equals(GameState.Free))
            {
                Gameui.gameObject.SetActive(!Gameui.gameObject.activeSelf);
                gameUI_Active = Gameui.gameObject.activeSelf;
            }
        }
    }

    private bool gameUI_Active = true;

    public void ShowGameUI(GameState state)
    {
        if (state == GameState.Free)
            Gameui.gameObject.SetActive(gameUI_Active);
        else
            Gameui.gameObject.SetActive(false);
    }
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

    public static SliderType ToggleSliderType => FluidSliderType = UgreTools.CycleThoughEnum(FluidSliderType);
}