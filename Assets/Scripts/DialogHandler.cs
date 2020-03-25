using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] protected string intro = "";
    public string Intro => intro;
    [SerializeField] protected List<DialogOption> dialogOptions = new List<DialogOption>();
    public List<DialogOption> DialogOptions => dialogOptions;
}

[System.Serializable]
public class DialogOption
{
    [SerializeField] protected string btnTitle = "";
    public string ButtonTitle => btnTitle;
    [SerializeField] protected string text = "";
    public string DialogText => text;
    [SerializeField] protected List<DialogOption> dialogOptions = new List<DialogOption>();
    public List<DialogOption> DialogOptions => dialogOptions;
}

public class DialogHandler : MonoBehaviour
{
    [SerializeField] private TextLog textLog = null;
    [SerializeField] private DialogBtn dialogBtn = null;
    [SerializeField] private Transform btnContainer = null;

    public void StartDialog(Dialog dialog)
    {
        textLog.SetText(dialog.Intro);
        dialog.DialogOptions.ForEach(d => Instantiate(dialogBtn, btnContainer).Setup(d.ButtonTitle).onClick.AddListener(() => BtnClick(d)));
    }

    private void BtnClick(DialogOption d)
    {
        btnContainer.KillChildren();
        textLog.SetText(d.DialogText);
        if (d.DialogOptions.Count > 0)
        {
            d.DialogOptions.ForEach(dn => Instantiate(dialogBtn, btnContainer).Setup(dn.ButtonTitle).onClick.AddListener(() => BtnClick(dn)));
        }
        else
        {
            // TODO add leave
        }
    }

    private void Fight()
    {
       
    }

    private void Leave()
    {
    }
}