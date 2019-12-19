using UnityEngine;

public class PerkTreeHoverText : BasicMenuHoverText
{
    public override void Start()
    {
        base.Start();
    }

    public override void Hovering(GameObject hoverOver, Vector2 mosPos)
    {
        PerkInfo info = hoverOver.GetComponent<PerkTreeBasicBtn>().perkInfo;
        if (info != null)
        {
            hovertext.text = info.Info;
        }else
        {
            hovertext.text = string.Empty;
        }
        base.Hovering(hoverOver,mosPos);
        /*
            hoverblock.SetActive(true);
            RectTransform rt = (RectTransform)hoverOver.transform;
            RectTransform hrt = (RectTransform)hoverblock.transform;
            Vector3 finalPos = rt.localPosition;
            Debug.Log("rt pos: " + rt.position);
            Debug.Log("rt localpos: " + rt.localPosition);
            Debug.Log("rt width: " + rt.rect.width);
            Debug.Log("hrt width: " + hrt.rect.width);
            finalPos.x += rt.rect.width / 2;
            finalPos.x += hrt.rect.width / 2;
            hrt.localPosition = finalPos;
        */
    }
}