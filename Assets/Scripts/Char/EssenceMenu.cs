using TMPro;
using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceMenu : MonoBehaviour
    {
        private PlayerMain _player => PlayerHolder.Player;
        [SerializeField] private TextMeshProUGUI stableAmount = null;

        private void OnEnable()
        {
            stableAmount.text = $"Stable essence: {_player.TotalStableEssence()}";
        }
    }
}