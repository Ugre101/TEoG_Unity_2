using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OneButtonHighlighted : MonoBehaviour
{
    private List<Button> btns = new List<Button>();

    [SerializeField]
    [Header("Leave empty for first button")]
    private Button highlighted = null;

    [Header("Color for button")]
    [SerializeField]
    private Color selected = new Color(0.5f, 0.5f, 0.5f, 1f), notSelected = new Color(0, 0, 0, 1);

    private void Start()
    {
        btns = GetComponentsInChildren<Button>().ToList();
        if (btns.Count < 1)
        {
            return;
        }
        if (highlighted == null)
        {
            highlighted = btns.First();
        }
        btns.ForEach(btn => btn.onClick.AddListener(() => SelectBtn(btn)));
        Highlight();
    }

    public void SelectBtn(Button toHightlight)
    {
        highlighted = toHightlight;
        Highlight();
    }

    private void Highlight()
    {
        btns.ForEach(btn =>
        {
            btn.image.color = btn == highlighted ? selected : notSelected;
        });
    }
}