using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObjectButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;
    [SerializeField] private GameObject toToggle = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        if (toToggle != null)
        {
            btn.onClick.AddListener(ToggleGameObject);
        }
    }

    private void ToggleGameObject() => toToggle.SetActive(!toToggle.activeSelf);

}