using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkTreeHoverText : BasicMenuHoverText
{
    public override void Start()
    {
        base.Start();
    }
    public override void Hovering(GameObject hoverOver)
    {
        hoverblock.SetActive(true);
        RectTransform rt = (RectTransform)hoverOver.transform;
        RectTransform hrt = (RectTransform)hoverblock.transform;
        Vector3 finalPos = rt.position;
        Debug.Log(rt.position);
        Debug.Log(rt.rect.position);
        Debug.Log(rt.rect.width);
        finalPos.x += hrt.rect.width;
        hoverblock.transform.position = finalPos;
        //  base.Hovering(hoverOver);
    }

}
