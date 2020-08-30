using TMPro;
using UnityEngine;

public class ChangeNamesPerson : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI person = null;
    [SerializeField] private TMP_InputField firstName = null, lastName = null;
    private Identity identity;

    public void Setup(int num, Identity identity)
    {
        this.identity = identity;
        person.text = $"Person {num}";
        firstName.onValueChanged.AddListener(ChangeFirstName);
        lastName.onValueChanged.AddListener(ChangeLastName);
    }

    public void Setup(int num, Identity identity, string lastNamePlaceholder)
    {
        this.identity = identity;
        person.text = $"Person {num}";
        firstName.onValueChanged.AddListener(ChangeFirstName);
        lastName.text = lastNamePlaceholder;
        lastName.onValueChanged.AddListener(ChangeLastName);
    }

    private void ChangeFirstName(string input) => identity.SetFirstName(input);

    private void ChangeLastName(string input) => identity.SetLastName(input);
}