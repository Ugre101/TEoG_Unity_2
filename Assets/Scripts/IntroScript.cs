using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntroScript : MonoBehaviour
{
    public TMP_InputField firstName, lastName;
    public VoreToggleBtn voreToggle;
    public AutoTFBtn AutoTFToggle;
    public bool Vore => voreToggle.Status;
    public bool AutoTF => AutoTFToggle.Status;
    public string FirstName => firstName.text;
    public string LastName => lastName.text;
}
