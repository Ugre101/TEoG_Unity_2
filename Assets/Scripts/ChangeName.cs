using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    private BasicChar Player => PlayerMain.GetPlayer;

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
        if (Player == null)
        {
            enabled = false;
        }
        if (firstName != null)
        {
            firstName.text = Player.Identity.FirstName;
        }
        if (lastName != null)
        {
            lastName.text = Player.Identity.LastName;
        }
    }

    public void NameChange()
    {
        if (firstName != null)
        {
            if (firstName.text.Length > 0)
            {
                Player.Identity.FirstName = firstName.text;
            }
        }
        if (lastName != null)
        {
            if (lastName.text.Length > 0)
            {
                Player.Identity.LastName = lastName.text;
            }
        }
    }
}