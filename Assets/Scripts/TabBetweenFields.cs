using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TabBetweenFields : MonoBehaviour
{
    private List<TMP_InputField> inputFields;

    [SerializeField] private KeyCode tabKey = KeyCode.Tab;
    [SerializeField] private bool startSelected = false;

    private void Start()
    {
        inputFields = GetComponentsInChildren<TMP_InputField>().ToList();
        if (startSelected)
        {
            SelectField();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(tabKey))
        {
            SelectField();
        }
    }

    private void SelectField()
    {
        if (inputFields.Exists(f => f.isFocused))
        {
            int indexOf = inputFields.IndexOf(inputFields.Find(f => f.isFocused));
            int index = indexOf > inputFields.Count - 2 ? 0 : indexOf + 1;
            inputFields[index].Select();
        }
        else
        {
            inputFields[0].Select();
        }
    }
}
