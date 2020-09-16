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
        quickText.text = keyCode != KeyCode.None ? keyCode.ToString() : string.Empty;
    }

    private static void SurrenderBattle()
    {
        if (CombatHandler.Target != null)
            CombatMain.GetCombatMain.LoseBattle();
        else
            GameManager.ReturnToLastState();
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