using System.Collections;
using TMPro;
using UnityEngine;

public class TimedPopupText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox = null;

    private float timeBeforeDeSpawn = 3;
    private float spawnTime;

    public void Setup(string text)
    {
        textbox = textbox != null ? textbox : GetComponentInChildren<TextMeshProUGUI>();
        textbox.text = text;
        spawnTime = Time.unscaledTime;
    }

    public void Setup(string text, float time)
    {
        Setup(text);
        timeBeforeDeSpawn = time;
    }

    private void Update()
    {
        if (spawnTime + timeBeforeDeSpawn < Time.unscaledTime)
        {
            Destroy(gameObject);
        }
    }

}