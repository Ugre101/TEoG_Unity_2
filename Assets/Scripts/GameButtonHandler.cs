using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameButtonHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject Vore = null, LevelUp = null;

        [SerializeField]
        private playerMain player = null;

        [SerializeField]
        private Image levelBtnImg = null;

        [SerializeField]
        private Sprite levelImg = null, noLevelImg = null;

        private bool hasLeveld = false;

        private void OnEnable()
        {
            hasLeveld = player.ExpSystem.PerkPoints > 0;
            levelBtnImg.sprite = hasLeveld ? levelImg : noLevelImg;
            Vore.SetActive(player.Vore.Active);
        }
    }
}