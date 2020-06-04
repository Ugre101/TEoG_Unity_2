using UnityEngine;

namespace EssenceMenuStuff
{
    public class EssenceDickButtons : EssenceOrganButtons
    {
        [SerializeField] private AddDick addDickPrefab = null;
        [SerializeField] private GrowDick growDickPrefab = null;

        protected override void UpdateButtons()
        {
            transform.KillChildren();
            Instantiate(addDickPrefab, transform).Setup().onClick.AddListener(UpdateButtons);
            foreach (Dick d in player.SexualOrgans.Dicks)
            {
                Instantiate(growDickPrefab, transform).Setup(player, d);
            }
        }
    }
}