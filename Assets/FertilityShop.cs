using System.Collections.Generic;
using UnityEngine;

public class FertilityShop : Shop
{
    [SerializeField] private List<ItemWare> itemWares = new List<ItemWare>();

    private void OnEnable()
    {
        ShowWares();
    }

    public void ShowWares()
    {
        container.KillChildren();
        itemWares.ForEach(w =>
        {
            Instantiate(buyItem, container).Setup(w, player);
        });
    }
}