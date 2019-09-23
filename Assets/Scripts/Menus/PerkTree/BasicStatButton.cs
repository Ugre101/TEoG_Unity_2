using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicStatButton : MonoBehaviour
{
    public bool taken = false;
    public playerMain player;
    public TextMeshProUGUI amount;
    public StatType stat;
    public int statAmount = 1;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Use);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void Use()
    {
        if (player.PerkBool)
        {
            taken = true;
            switch (stat)
            {
                case StatType.Charm:
                    player.charm._baseValue += statAmount;
                    break;
                case StatType.Dex:
                    player.dexterity._baseValue += statAmount;
                    break;
                case StatType.End:
                    player.dexterity._baseValue += statAmount;
                    break;
                case StatType.Str:
                    player.strength._baseValue += statAmount;
                    break;
            }
        }
    }
}
