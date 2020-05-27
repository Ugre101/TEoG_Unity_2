using TMPro;
using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceMenu : MonoBehaviour
    {
        [SerializeField] private PlayerMain _player = null;
        [SerializeField] private TextMeshProUGUI stableAmount = null;

        // Start is called before the first frame update
        private void Start()
        {
            _player = _player != null ? _player : PlayerMain.GetPlayer;
        }

        private void OnEnable()
        {
            stableAmount.text = $"Stable essence: {_player.TotalStableEssence()}";
        }
    }
}