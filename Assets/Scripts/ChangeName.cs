using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public BasicChar player;
    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public Button Accept;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Accept.onClick.AddListener(NameChange);
    }

    private void OnDisable()
    {
        if (firstName != null)
        {
            firstName.text = null;
        }
        if (lastName != null)
        {
            lastName.text = null;
        }
    }
    public void NameChange()
    {
        if (firstName != null)
        {
            if (firstName.text.Length > 0)
            {
                player.firstName = firstName.text;
            }
        }
        if (lastName != null)
        {
            if (lastName.text.Length > 0)
            {
                player.lastName = lastName.text;
            }
        }
    }
}