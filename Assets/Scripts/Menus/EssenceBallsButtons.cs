using UnityEngine;

namespace EssenceMenu
{
    public class EssenceBallsButtons : EssenceOrganButtons
    {
        [SerializeField] private AddBalls addBallsPrefab = null;
        [SerializeField] private GrowBalls growBallsPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addBallsPrefab, transform).Setup(player).onClick.AddListener(UpdateButtons);
            foreach (Balls b in player.SexualOrgans.Balls)
            {
                Instantiate(growBallsPrefab, transform).Setup(player, b);
            }
        }
    }
}