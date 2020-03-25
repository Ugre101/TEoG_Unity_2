using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TabBetweenFields : MonoBehaviour
{
    private List<TMP_InputField> fields;

    [SerializeField]    private KeyCode tabKey = KeyCode.Tab;

    private void Start() => fields = GetComponentsInChildren<TMP_InputField>().ToList();

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(tabKey))
        {
            if (fields.Find(f => f.isFocused) != null)
            {
                int indexOf = fields.IndexOf(fields.Find(f => f.isFocused));
                int index = indexOf > fields.Count - 2 ? 0 : indexOf + 1;
                fields[index].Select();
            }
            else
            {
                fields[0].Select();
            }
        }
    }
}