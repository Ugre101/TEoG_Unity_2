using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpStatusUpdater : MonoBehaviour
{
    // Public
    public PlayerMain player;
    public TextMeshProUGUI _statusExp, _statusLevel;
    // Private
    private Slider slider;
    // Start is called before the first frame update

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        ExpSystem.ExpChangeEvent += ExpStatus;
        player.ExpSystem.ManualExpUpdate();
    }
    private void OnDisable()
    {
        ExpSystem.ExpChangeEvent -= ExpStatus;
    }


    // Update is called once per frame

    public void ExpStatus()
    {
        if (slider != null)
        {
            slider.value = player.ExpSystem.ExpSlider;
        }
        if (_statusExp != null)
        {
            _statusExp.text = player.ExpSystem.ExpStatus;
        }
        if (_statusLevel != null)
        {
            _statusLevel.text = player.ExpSystem.LevelStatus;
        }
    }
}
