﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class PerkTreeBasicBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected PlayerMain player = null;

    [SerializeField] protected TextMeshProUGUI amount = null;

    [SerializeField] protected Button btn = null;

    [SerializeField] protected Image rune = null;

    public void SetRune(Sprite newRune) => rune.sprite = newRune;

    protected Color color;

    private bool taken = false;

    public bool Taken
    {
        get => taken;
        protected set
        {
            taken = value;
            RuneOpacity();
        }
    }

    protected void SetRuneColor(float value)
    {
        if (rune != null)
        {
            color = rune.color;
            color.a = value;
            rune.color = color;
        }
    }

    protected virtual void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        player = player != null ? player : PlayerMain.GetPlayer;
        btn.onClick.AddListener(Use);
    }

    protected virtual void OnEnable() => RuneOpacity();

    protected void RuneOpacity() => SetRuneColor(Taken ? 1f : 0.5f);

    protected abstract void Use();

    public abstract void OnPointerEnter(PointerEventData eventData);

    public void OnPointerExit(PointerEventData eventData) => PerkTreeHoverText.StopHovering();
}