using UnityEngine;
using UnityEngine.UI;

public class ChangeNamePlayer : ChangeName
{
    [SerializeField] private Button closeBtn = null;

    protected override void Start()
    {
        base.Start();
        closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable() => Setup(PlayerMain.GetPlayer.Identity);
}