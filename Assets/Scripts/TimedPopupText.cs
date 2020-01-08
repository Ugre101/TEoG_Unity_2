using TMPro;
using UnityEngine;

public class TimedPopupText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textbox = null;

    private float timeBeforeDeSpawn = 3;
    private float spawnTime;

    // Start is called before the first frame update
    private void Start()
    {
        textbox = textbox != null ? textbox : GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(string text)
    {
        textbox.text = text;
        spawnTime = Time.unscaledTime;
    }

    public void Setup(string text, float time)
    {
        textbox.text = text;
        timeBeforeDeSpawn = time;
        spawnTime = Time.unscaledTime;
    }

    private void Update()
    {
        if (spawnTime + timeBeforeDeSpawn < Time.unscaledTime)
        {
            Destroy(gameObject);
        }
    }
}