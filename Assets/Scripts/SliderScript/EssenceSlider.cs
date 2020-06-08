﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class EssenceSlider : MonoBehaviour
{
    [SerializeField] private CharHolder charHolder = null;
    protected BasicChar basicChar = null;

    [SerializeField] protected TextMeshProUGUI essValue = null;

    [SerializeField] protected Image _image = null;
    protected abstract Essence Ess { get; }

    protected virtual void Start()
    {
        if (charHolder != null)
        {
            Init(charHolder.BasicChar);
        }
    }

    public virtual void Init(BasicChar who)
    {
        basicChar = who;
        enabled = true;
        Ess.EssenceSliderEvent += ChangeEss;
        ChangeEss();
        started = true;
    }

    protected bool started = false;

    private void OnEnable()
    {
        if (started)
        {
            Ess.EssenceSliderEvent += ChangeEss;
            ChangeEss();
        }
    }

    private void OnDisable() => Ess.EssenceSliderEvent -= ChangeEss;

    protected abstract void ChangeEss();
}