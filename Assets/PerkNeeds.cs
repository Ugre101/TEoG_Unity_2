using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkNeeds : MonoBehaviour
{
    private Image image;
    private Color current;
    private TextMeshProUGUI Number;
    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        current = image.color;
        Number = GetComponentInChildren<TextMeshProUGUI>();
        Number.text = null;
        SetState(PerkState.CantBeTaken);
    }
    private void SetState(PerkState state)
    {
        switch (state)
        {
            case PerkState.Taken:
                current.a = 1f;
                break;
            case PerkState.CanBeTaken:
                current.a = 0.6f;
                break;
            case PerkState.CantBeTaken:
                current.a = 0.2f;
                break;
        }
        image.color = current;
    }
}
public enum PerkState
{
    Taken,
    CanBeTaken,
    CantBeTaken
}