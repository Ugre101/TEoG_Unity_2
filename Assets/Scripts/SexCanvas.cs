using System.Collections.Generic;
using UnityEngine;

public class SexCanvas : MonoBehaviour
{
    [SerializeField] private DormSex dormSex = null;

    private void Start()
    {
        GameManager.GameStateChangeEvent += LeaveSex;
    }

    private bool inSex = false;

    public void DormSex(BasicChar partner) => DormSex(new List<BasicChar>() { partner });

    public void DormSex(List<BasicChar> partners)
    {
        inSex = true;
        GameManager.SetCurState(GameState.NonCombatSex);

        transform.SleepChildren(dormSex.transform);
        dormSex.Setup(partners);
    }

    private void LeaveSex(GameState newState)
    {
        if (inSex && newState != GameState.NonCombatSex)
        {
            inSex = false;
            transform.SleepChildren();
        }
    }
}