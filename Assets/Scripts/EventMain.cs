using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMain : MonoBehaviour
{
    [SerializeField] private TextLog textLog = null;
    [SerializeField] private Button optionBtn = null;
    [SerializeField] private Transform optionContainer = null;
    [SerializeField] private CanvasMain canvasMain = null;

    // Percipants
    [SerializeField] private PlayerMain player = null;

    private void Start()
    {
        textLog = textLog != null ? textLog : GetComponent<TextLog>();
        canvasMain = canvasMain != null ? canvasMain : CanvasMain.GetCanvasMain;
    }

    public void EventSolo(bool CanLeaveDiretly = true)
    {
        Setup(CanLeaveDiretly);
    }

    public void EventWith(BasicChar whom, bool CanLeaveDiretly = true)
    {
        Setup(CanLeaveDiretly);
    }

    public void EventWithSeveral(List<BasicChar> basicChars, bool CanLeaveDiretly = true)
    {
        Setup(CanLeaveDiretly);
    }

    private void Setup(bool canLeave)
    {
        optionContainer.KillChildren();
        if (canLeave)
        {
            LeaveBtn();
        }
    }

    private void LeaveBtn() => Instantiate(optionBtn, optionContainer).onClick.AddListener(Leave);

    private void Leave()
    {
        gameObject.SetActive(false);
        canvasMain.Resume();
    }
}