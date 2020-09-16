using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabBetweenSelectables : MonoBehaviour
{
    private List<Selectable> selectables = new List<Selectable>();
    [SerializeField] private KeyCode tabKey = KeyCode.Tab;
    [SerializeField] private bool startSelected = false;
    private Selectable selected = null;

    private void Start()
    {
        selectables = GetComponentsInChildren<Selectable>().ToList();
        if (startSelected) SelectSelectable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(tabKey)) SelectSelectable();
    }

    private int i = 0;

    private int I
    {
        get
        {
            if (i == selectables.Count) i = 0;
            return i;
        }
        set => i = value;
    }

    private void SelectSelectable()
    {
        // I don't know how to get which is currenly focused
        selectables[I].Select();
        I++;
    }
}