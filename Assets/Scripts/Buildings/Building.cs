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
    protected bool selling = false;

    public virtual void Start() => sellBtn.onClick.AddListener(ToggleSelling);

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
        buyItems.ForEach(i => Instantiate(buyItem, container).Setup(player, ItemsRef.GetById(i)));
    }

    protected virtual void SellWares()
    {
        selling = true;
        container.KillChildren();
        player.Inventory.Items.ForEach(i => Instantiate(sellItemPrefab, container).Setup(ItemsRef.GetById(i.Id), i, player));
    }
}