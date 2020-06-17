using UnityEngine;

namespace EssenceMenuStuff
{
    public abstract class EssenceOrganButtons : MonoBehaviour
    {
        [SerializeField] protected PlayerMain player => PlayerHolder.Player;

        // Start is called before the first frame update
        protected virtual void OnEnable()
        {
            UpdateButtons();
            player.SexualOrgans.AllOrgans.ForEach(o => o.Change += UpdateButtons);
        }

        protected virtual void OnDisable() => player.SexualOrgans.AllOrgans.ForEach(o => o.Change -= UpdateButtons);


        protected abstract void UpdateButtons();
    }
}