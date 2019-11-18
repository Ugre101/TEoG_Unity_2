using UnityEngine;

public class BasicCharGame : MonoBehaviour
{
    [SerializeField]
    private CharSprites sprites = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [Header("Scriptable objects")]
    [SerializeField]
    private Settings settings = null;

    [SerializeField]
    private EventLog eventLog = null;

    private BasicChar whom;

    public Settings Settings => settings;

    public EventLog EventLog => eventLog;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (whom == null)
        {
            whom = GetComponent<BasicChar>();
        }
        spriteRenderer.sprite = sprites.GetSprite(whom);
    }
}