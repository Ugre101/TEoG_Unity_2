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
        player = player ?? PlayerHolder.Player;
        slider = slider != null ? slider : GetComponent<Slider>();
        started = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if (started)
        {
            player.ExpSystem.ExpChangeEvent += ExpStatus;
            ExpStatus();
        }
    }

    private void OnDisable() => player.ExpSystem.ExpChangeEvent -= ExpStatus;

    private void ExpStatus()
    {
        slider.value = player.ExpSystem.ExpSlider;
        _statusExp.text = player.ExpSystem.ExpStatus;
        _statusLevel.text = player.ExpSystem.LevelStatus;
    }
}