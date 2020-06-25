using UnityEngine;
using UnityEngine.UI;

public class ToggleGameobjectButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private GameObject toToggle = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn ?? GetComponent<Button>();
        if (toToggle != null)
        {
            btn.onClick.AddListener(ToggleGameobject);
        }
    }

    private void ToggleGameobject() => toToggle.SetActive(!toToggle.activeSelf);

}