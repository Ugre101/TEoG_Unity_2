using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SexButtonBase : MonoBehaviour
{
    [SerializeField]
    protected SexScenes scene = null;

    [SerializeField]
    protected Button btn = null;

    [SerializeField]
    protected TextMeshProUGUI title = null;

    protected PlayerMain player;
    protected EnemyPrefab other;

    public virtual void Start()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
    }
}