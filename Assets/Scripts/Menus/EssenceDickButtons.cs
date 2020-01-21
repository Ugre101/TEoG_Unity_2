using UnityEngine;

namespace EssenceMenu
{
    public class EssenceDickButtons : EssenceOrganButtons
    {
        [SerializeField] private AddDick addDickPrefab = null;
        [SerializeField] private GrowDick growDickPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addDickPrefab, transform).Setup(player);
            foreach (Dick d in player.SexualOrgans.Dicks)
            {
                Instantiate(growDickPrefab, transform).Setup(player, d);
            }
            lastAmount = player.SexualOrgans.Dicks.Count;
        }
    }
}