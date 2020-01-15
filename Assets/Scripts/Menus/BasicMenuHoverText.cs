using TMPro;
using UnityEngine;

public class BasicMenuHoverText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI titleText = null, descText = null;

    // Start is called before the first frame update
    protected virtual void Start() => StopHovering();

    protected virtual void OnDisable() => StopHovering();

    public virtual void Hovering(string title, string desc)
    {
        gameObject.SetActive(true);
        titleText.text = title;
        descText.text = desc;
    }

    public virtual void StopHovering() => gameObject.SetActive(false);
}