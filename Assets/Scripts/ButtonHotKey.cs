using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHotKey : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private KeyCode hotKey = KeyCode.KeypadEnter;
    [SerializeField] private TextMeshProUGUI altCodeText = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        if (altCodeText != null)
        {
            altCodeText.text = hotKey.ToString();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(hotKey))
        {
            btn.onClick.Invoke();
        }
    }
}