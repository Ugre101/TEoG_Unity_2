using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetGenderNames : MonoBehaviour
{
    [SerializeField] private TMP_InputField male = null, female = null, dickGirl = null, cuntBoy = null, herm = null, doll = null;
    [SerializeField] private Button closeBtn = null;

    private void SetField(TMP_InputField field, string val) => field.placeholder.GetComponent<TextMeshProUGUI>().text = val;

    private void MaleBind(string call) => Settings.GenderNames.Male = call;

    private void FemaleBind(string call) => Settings.GenderNames.Female = call;

    private void DickgirlBind(string call) => Settings.GenderNames.Dickgirl = call;

    private void CuntBoyBind(string call) => Settings.GenderNames.Cuntboy = call;

    private void HermBind(string call) => Settings.GenderNames.Herm = call;

    private void DollBind(string call) => Settings.GenderNames.Doll = call;

    private void Close() => gameObject.SetActive(false);

    // Start is called before the first frame update
    private void Start()
    {
        closeBtn.onClick.AddListener(Close);
        male.onValueChanged.AddListener(MaleBind);
        female.onValueChanged.AddListener(FemaleBind);
        dickGirl.onValueChanged.AddListener(DickgirlBind);
        cuntBoy.onValueChanged.AddListener(CuntBoyBind);
        herm.onValueChanged.AddListener(HermBind);
        doll.onValueChanged.AddListener(DollBind);
    }

    private void OnEnable()
    {
        SetField(male, Settings.GenderNames.Male);
        SetField(female, Settings.GenderNames.Female);
        SetField(dickGirl, Settings.GenderNames.Dickgirl);
        SetField(cuntBoy, Settings.GenderNames.Cuntboy);
        SetField(herm, Settings.GenderNames.Herm);
        SetField(doll, Settings.GenderNames.Doll);
    }

    private void OnDisable()
    {
        GameManager.KeyBindsActive = true;
        gameObject.SetActive(false);
    }
}