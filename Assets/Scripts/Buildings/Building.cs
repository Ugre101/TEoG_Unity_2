using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected PlayerMain player;

    public virtual void OnEnable()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
    }

}

public abstract class Shop : Building
{
    [SerializeField] protected Transform container = null;
    [SerializeField] protected BuyItem buyItem = null;
    [SerializeField] protected ItemHolder ItemsRef = null;
    [SerializeField] protected Button sellBtn = null;
    [SerializeField] protected TextMeshProUGUI sellBtnText = null;
    [SerializeField] protected SellItem sellItemPrefab = null;
    [SerializeField] protected List<ItemIds> buyItems = new List<ItemIds>();
    [SerializeField] protected Wares wares = null;

    protected bool selling = false;

    public virtual void Start()
    {
        wares = wares != null ? wares : GetComponentInChildren<Wares>();
        sellBtn.onClick.AddListener(ToggleSelling);
    }

    public new virtual void OnEnable()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
        if (ItemsRef == null)
        {
            Debug.Log("You forgot to assing itemsHolder at " + new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType);
        }
        ShowWares();
        selling = false;
        sellBtnText.text = "Sell";
    }

    protected virtual void ToggleSelling()
    {
        if (selling)
        {
            selling = false;
            sellBtnText.text = "Sell";
            OnEnable();
        }
        else
        {
            SellWares();
            sellBtnText.text = "Buy";
        }
    }

    public virtual void ShowWares()
    {
        container.KillChildren();
        wares.BuyItems(player, buyItems);
    }

    protected virtual void SellWares()
    {
        selling = true;
        container.KillChildren();
        wares.SellItems(player);
    }
}