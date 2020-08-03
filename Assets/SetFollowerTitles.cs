using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetFollowerTitles : MonoBehaviour
{
    [SerializeField] private TMP_InputField theyCallYou = null, youCallThem = null, takeHome = null, dorm = null;
    [SerializeField] private Button closeBtn = null;

    private void SetField(TMP_InputField field, string val) => field.placeholder.GetComponent<TextMeshProUGUI>().text = val;

    private void Close() => gameObject.SetActive(false);

    // Start is called before the first frame update
    private void Start()
    {
        closeBtn.onClick.AddListener(Close);
        theyCallYou.onValueChanged.AddListener(Settings.FollowersSettings.TheyCallYou.SetTitle);
        youCallThem.onValueChanged.AddListener(Settings.FollowersSettings.YouCallThem.SetTitle);
        takeHome.onValueChanged.AddListener(Settings.FollowersSettings.WhenTakingThemYou.SetTitle);
        dorm.onValueChanged.AddListener(Settings.FollowersSettings.TheyLiveIn.SetTitle);
    }

    private void OnEnable()
    {
        SetField(theyCallYou, Settings.FollowersSettings.TheyCallYou.Title);
        SetField(youCallThem, Settings.FollowersSettings.YouCallThem.Title);
        SetField(takeHome, Settings.FollowersSettings.WhenTakingThemYou.Title);
        SetField(dorm, Settings.FollowersSettings.TheyLiveIn.Title);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false); // Ensure that this menu is closed when user exits options with "Esc"
        GameManager.KeyBindsActive = true;
    }
}