using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowServant : MonoBehaviour
{
    [SerializeField]
    private BasicChar who;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;
    public void Init(BasicChar whom)
    {
        who = whom;
        Title.text = who.FullName;
        Desc.text = CharDesc();
    }

    private string CharDesc()
    {
        string desc = $"{who.Gender} {who.Race}";
        return desc;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
