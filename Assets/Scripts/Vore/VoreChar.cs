using UnityEngine;

namespace Vore
{
    public class VoreChar : MonoBehaviour
    {
        [SerializeField]
        private BasicChar pred;
        public BasicChar Pred => pred;

        private void Start()
        {
            pred = GetComponentInParent<BasicChar>();
        }
        public void AddPrey(GameObject parPrey)
        {
            
            parPrey.transform.SetParent(transform);
        }
    }
}