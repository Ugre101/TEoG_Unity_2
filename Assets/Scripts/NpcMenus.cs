using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMenus : MonoBehaviour
{
   public void EnterNpc(NpcMenuPage page)
    {
        transform.SleepChildren(page.transform);
    }
}
