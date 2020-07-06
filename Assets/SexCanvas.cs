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

    public void DormSex(List<BasicChar> partners)
    {
        inSex = true;
        GameManager.SetCurState(GameState.Battle);

        transform.SleepChildren(dormSex.transform);
        dormSex.Setup(partners);
    }

    private void LeaveSex(GameState newState)
    {
        if (inSex && newState != GameState.Battle)
        {
            inSex = false;
            transform.SleepChildren();
        }
    }
}