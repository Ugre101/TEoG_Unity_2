using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpStatusUpdater : MonoBehaviour
{
    // Public
    public playerMain player;
    public TextMeshProUGUI _statusExp, _statusLevel;
    // Private
    private Slider slider;
    // Start is called before the first frame update

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        ExpSystem.expChange += expSsagdsaa;
        player.expSystem.manualExpUpdate();
    }
    private void OnDisable()
    {
        ExpSystem.expChange -= expSsagdsaa;
    }


    // Update is called once per frame

    public void expSsagdsaa()
    {
        if (slider != null)
        {
            slider.value = player.expSystem.ExpSlider();
        }
        if (_statusExp != null)
        {
            _statusExp.text = player.expSystem.ExpStatus();
        }
        if (_statusLevel != null)
        {
            _statusLevel.text = player.expSystem.LevelStatus();
        }
    }
}
