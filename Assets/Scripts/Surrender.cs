using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Surrender : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI quickText = null;

    [SerializeField]
    private KeyCode keyCode = KeyCode.None;

    [SerializeField]
    private Button btn = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(SurrenderBattle);
        if (keyCode != KeyCode.None)
        {
            quickText.text = keyCode.ToString();
        }
        else
        {
            quickText.text = string.Empty;
        }
    }

    private void SurrenderBattle()
    {
        if (CombatHandler.Target != null)
        {
            CombatMain.GetCombatMain.LoseBattle();
        }
        else
        {
            CanvasMain.GetCanvasMain.Resume();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            btn.onClick?.Invoke();
        }
    }
}