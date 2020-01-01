using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    private BasicChar player => PlayerMain.GetPlayer;

    [SerializeField]
    private TMP_InputField firstName = null, lastName = null;

    [SerializeField]
    private Button Accept = null;

    private void Start()
    {
        Accept.onClick.AddListener(NameChange);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (player == null)
        {
            enabled = false;
        }
        if (firstName != null)
        {
            firstName.text = player.Identity.FirstName;
        }
        if (lastName != null)
        {
            lastName.text = player.Identity.LastName;
        }
    }

    public void NameChange()
    {
        if (firstName != null)
        {
            if (firstName.text.Length > 0)
            {
                player.Identity.FirstName = firstName.text;
            }
        }
        if (lastName != null)
        {
            if (lastName.text.Length > 0)
            {
                player.Identity.LastName = lastName.text;
            }
        }
    }
}