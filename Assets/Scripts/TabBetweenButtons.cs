using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// The script does work but feels unpolished.
public class TabBetweenButtons : MonoBehaviour
{
    private List<Button> buttons;

    [SerializeField] private KeyCode key = KeyCode.Tab;

    private int nextBtn = 0;
    private int btnCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
        btnCount = buttons.Count;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SelectBtn();
        }
    }

    private void SelectBtn()
    {
        if (btnCount > 0)
        {
            buttons[nextBtn].Select();
            nextBtn++;
            if (nextBtn > btnCount - 1)
            {
                nextBtn = 0;
            }
        }
    }
}