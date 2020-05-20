using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmBarn : Shop
{
    [SerializeField] private GameObject shopPart = null, normalPart = null;
    [SerializeField] private Button toggleBtn = null;
    [SerializeField] private TextMeshProUGUI toggleBtnText = null;

    private void ToggleBetween()
    {
        bool normal = normalPart.activeSelf;
        SetActiveExclusive(normal);
    }

    private void SetActiveExclusive(bool shop)
    {
        normalPart.SetActive(!shop);
        shopPart.SetActive(shop);
        toggleBtnText.text = shop ? "Back" : "Shop";
    }

    public override void Start()
    {
        base.Start();
        toggleBtn.onClick.AddListener(ToggleBetween);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetActiveExclusive(false);
    }

    protected override void SellWares()
    {
        base.SellWares();
    }

    public override void ShowWares()
    {
        base.ShowWares();
    }

    protected override void ToggleSelling()
    {
        base.ToggleSelling();
    }
}