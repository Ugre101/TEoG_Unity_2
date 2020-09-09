using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackToHomeStart : MonoBehaviour
{
    private HomeMain homeMain;
    private Button btn;
    // Start is called before the first frame update
    private void Start()
    {
        homeMain = GetComponentInParent<HomeMain>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(homeMain.ToStart);
    }
}
