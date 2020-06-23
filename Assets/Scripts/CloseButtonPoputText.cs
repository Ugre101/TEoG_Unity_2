using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonPoputText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox = null;
    [SerializeField] private Button closeBtn = null;

    public void Setup(string text)
    {
        textBox = textBox != null ? textBox : GetComponent<TextMeshProUGUI>();
        textBox.text = text;

        closeBtn = closeBtn != null ? closeBtn : GetComponent<Button>();
        closeBtn.onClick.AddListener(DestroySelf);
    }

    private void DestroySelf() => Destroy(gameObject);
}