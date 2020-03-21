using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetGenderNames : MonoBehaviour
{
    [SerializeField] private TMP_InputField male = null, female = null, dickGirl = null, cuntBoy = null, herm = null, doll = null;
    [SerializeField] private Button closeBtn = null;

    private void SetField(TMP_InputField field, string val) => field.placeholder.GetComponent<TextMeshProUGUI>().text = val;

    private void MaleBind(string call) => Settings.Male = call;

    private void FemaleBind(string call) => Settings.Female = call;

    private void DickgirlBind(string call) => Settings.Dickgirl = call;

    private void CuntBoyBind(string call) => Settings.Cuntboy = call;

    private void HermBind(string call) => Settings.Herm = call;

    private void DollBind(string call) => Settings.Doll = call;

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
        SetField(male, Settings.Male);
        SetField(female, Settings.Female);
        SetField(dickGirl, Settings.Dickgirl);
        SetField(cuntBoy, Settings.Cuntboy);
        SetField(herm, Settings.Herm);
        SetField(doll, Settings.Doll);
    }

    private void OnDisable()
    {
        GameManager.KeyBindsActive = true;
        gameObject.SetActive(false);
    }
}