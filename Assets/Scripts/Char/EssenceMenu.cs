using TMPro;
using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceMenu : MonoBehaviour
    {
        private static BasicChar Player => PlayerMain.Player;
        [SerializeField] private TextMeshProUGUI stableAmount = null;

        private void OnEnable()
        {
            stableAmount.text = $"Stable essence: {Player.TotalStableEssence()}";
        }
    }
}