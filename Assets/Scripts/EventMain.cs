using UnityEngine;

public class EventMain : MonoBehaviour
{
    [SerializeField] private TextLog textLog = null;

    private void Start()
    {
        textLog = textLog != null ? textLog : GetComponent<TextLog>();
    }
}

public class EventScene : ScriptableObject
{
  
}