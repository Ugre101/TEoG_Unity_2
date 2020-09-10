using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    private Identity whoms;

    [SerializeField] private TMP_InputField firstName = null, lastName = null;

    [SerializeField] private Button Accept = null;

    protected virtual void Start() => Accept.onClick.AddListener(AcceptNameChange);

    public void Setup(Identity identity)
    {
        whoms = identity;
        if (firstName != null)
        {
            firstName.text = whoms.FirstName;
        }
        if (lastName != null)
        {
            lastName.text = whoms.LastName;
        }
    }

    private void AcceptNameChange()
    {
        if (firstName != null && firstName.text.Length > 0)
        {
            whoms.SetFirstName(firstName.text);
        }
        if (lastName != null && lastName.text.Length > 0)
        {
            whoms.SetLastName(lastName.text);
        }
    }
}