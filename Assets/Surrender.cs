using UnityEngine;
using UnityEngine.UI;

public class Surrender : MonoBehaviour
{
    [SerializeField]
    private Button btn = null;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();
        btn.onClick.AddListener(SurrenderBattle);
    }

    private void SurrenderBattle() => CombatMain.GetCombatMain.LoseBattle();

    // Update is called once per frame
    private void Update()
    {
    }
}