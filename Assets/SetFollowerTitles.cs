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
        theyCallYou.onValueChanged.AddListener(Settings.FollowersSettings.TheyCallYou.SetTitle);
        youCallThem.onValueChanged.AddListener(Settings.FollowersSettings.YouCallThem.SetTitle);
        takeHome.onValueChanged.AddListener(Settings.FollowersSettings.WhenTakingThemYou.SetTitle);
        dorm.onValueChanged.AddListener(Settings.FollowersSettings.TheyLiveIn.SetTitle);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}