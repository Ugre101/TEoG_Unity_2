using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpStatusUpdater : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;

    [SerializeField] private TextMeshProUGUI _statusExp = null, _statusLevel = null;

    [SerializeField] private Slider slider = null;

    private bool started = false;

    private void Start()
    {
        player = player != null ? player : PlayerHolder.Player;
        slider = slider != null ? slider : GetComponent<Slider>();
        started = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if (started)
        {
            ExpSystem.ExpChangeEvent += ExpStatus;
            ExpStatus();
        }
    }

    private void OnDisable() => ExpSystem.ExpChangeEvent -= ExpStatus;

    private void ExpStatus()
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