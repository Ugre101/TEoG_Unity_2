using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HomeStuff
{
    public class HomeUIDormBtn : MonoBehaviour
    {
        [SerializeField] private HomeMain home = null;
        [SerializeField] private Button btn = null;
        [SerializeField] private TextMeshProUGUI btnText = null;

        // Start is called before the first frame update
        private void Start()
        {
            home = home != null ? home : GetComponentInParent<HomeMain>();
            btn = btn != null ? btn : GetComponent<Button>();
            btnText = btnText != null ? btnText : GetComponentInChildren<TextMeshProUGUI>();
            btn.onClick.AddListener(home.EnterDorm);
        }

        private void OnEnable() => btnText.text = Settings.FollowersSettings.TheyLiveIn.Title;
    }
}