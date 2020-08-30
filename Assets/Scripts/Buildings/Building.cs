using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
     protected BasicChar player => PlayerMain.Player;

    public virtual void OnEnable()
    {
    }
}

public abstract class Shop : Building
{
    [SerializeField] protected List<ItemIds> buyItems = new List<ItemIds>();
    [SerializeField] protected Wares wares = null;

    protected Wares Wares
    {
        get
        {
            if (wares == null)
            {
                wares = wares != null ? wares : GetComponentInChildren<Wares>();
            }
            return wares;
        }
    }

    protected bool selling = false;

    public virtual void Start() => Wares.SellBtn.onClick.AddListener(ToggleSelling);

    public new virtual void OnEnable()
    {
        Wares.ClearContainer();
        if (buyItems.Count > 0)
        {
            ShowWares();
        }
        selling = false;
    }

    protected virtual void ToggleSelling()
    {
        if (selling)
        {
            selling = false;
            OnEnable();
            Wares.SellBtnText.text = "Sell";
        }
        else
        {
            SellWares();
            Wares.SellBtnText.text = "Buy";
        }
    }

    public virtual void ShowWares() => Wares.BuyItems(player, buyItems);

    protected virtual void SellWares()
    {
        selling = true;
        Wares.ClearContainer();
        Wares.SellItems(player);
    }
}