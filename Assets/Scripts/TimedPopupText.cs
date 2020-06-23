using System.Collections;
using TMPro;
using UnityEngine;

public class TimedPopupText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox = null;

    private float timeBeforeDeSpawn = 3;

    public void Setup(string text)
    {
        textbox = textbox != null ? textbox : GetComponentInChildren<TextMeshProUGUI>();
        textbox.text = text;
        StartCoroutine(DestroySelf());
    }

    public void Setup(string text, float time)
    {
        timeBeforeDeSpawn = time;
        Setup(text);
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSecondsRealtime(timeBeforeDeSpawn);
        Destroy(gameObject);
    }
}