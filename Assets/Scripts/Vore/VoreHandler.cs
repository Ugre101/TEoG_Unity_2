using System.Collections.Generic;
using UnityEngine;

namespace Vore
{
    public class VoreHandler : MonoBehaviour
    {
        public BasicChar player;
        public List<BasicChar> otherPreds;

        public VoreChar prefab;
        private VoreChar playerPred;
        public VoreChar PlayerPred => playerPred;

        private void Start()
        {
            transform.KillChildren();
            playerPred = AddPred(player);
        }

        private VoreChar AddPred(BasicChar pred)
        {
            VoreChar otherPred = Instantiate(prefab, transform);
            //  otherPred.name = pred.name;
            //  otherPred.Setup(pred);
            return otherPred;
        }

        public void Save()
        {
        }

        public void Load()
        {
        }
    }
}