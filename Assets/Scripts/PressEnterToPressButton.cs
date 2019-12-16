using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressEnterToPressButton : MonoBehaviour
{
    [SerializeField]
    private List<Button> btns = new List<Button>();

    [SerializeField]
    private KeyCode key = KeyCode.Return;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            btns.ForEach(b => b.onClick.Invoke());
        }
    }
}