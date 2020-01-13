using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    protected PlayerMain player;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
    }
}

public class Shop : Building
{
    [SerializeField] protected Transform container = null;
    [SerializeField] protected BuyItem buyItem = null;

    public virtual void ShowWares(List<Ware> wares)
    {
        container.KillChildren();
        wares.ForEach(w =>
        {
            Instantiate(buyItem, container).Setup(w);
        });
    }
}