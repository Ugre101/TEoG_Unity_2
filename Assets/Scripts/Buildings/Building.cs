using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] protected PlayerMain player;

    public virtual void OnEnable()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
    }
}

public class Shop : Building
{
    [SerializeField] protected Transform container = null;
    [SerializeField] protected BuyItem buyItem = null;
    [SerializeField] protected Items ItemsRef = null;

    public new virtual void OnEnable()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
        if (ItemsRef == null)
        {
            Debug.Log("You forgot to assing itemsHolder at " + new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType);
        }
    }

    public virtual void ShowWares(List<Ware> wares, BasicChar buyer)
    {
        container.KillChildren();
        wares.ForEach(w =>
        {
            Instantiate(buyItem, container).Setup(w, buyer);
        });
    }
}