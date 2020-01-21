using UnityEngine;

namespace EssenceMenu
{
    public abstract class EssenceOrganButtons : MonoBehaviour
    {
        [SerializeField] protected PlayerMain player = null;
        protected int lastAmount;

        // Start is called before the first frame update
        protected virtual void OnEnable() => UpdateButtons();

        protected virtual void Update()
        {
            if (lastAmount != player.SexualOrgans.Vaginas.Count)
            {
                UpdateButtons();
            }
        }

        protected abstract void UpdateButtons();
    }
}