using UnityEngine;

public class BasicCharGame : MonoBehaviour
{
    [SerializeField]
    private CharSprites sprites = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private BasicChar whom;

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