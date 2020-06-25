using TMPro;
using UnityEngine;

namespace SexCharStuff
{
    public abstract class OrganInfo : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI text = null;
        [SerializeField] protected BasicChar whom = null;
        protected bool detailed = false;
        protected SexualOrgans Organs => whom.SexualOrgans;

        protected void SetText(bool hasOrgan, string orgLook) => text.text = hasOrgan ? orgLook : string.Empty;

        public void Setup(BasicChar basicChar, bool isDetailed = false)
        {
            whom = basicChar;
            detailed = isDetailed;
            PrintOrganInfo();
        }

        public abstract void PrintOrganInfo();
    }
}