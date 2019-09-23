using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PerkButton : MonoBehaviour
{
    public bool taken = false;
    public playerMain player;
    public TextMeshProUGUI amount;
    public PerksTypes perk;
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

    }
}
