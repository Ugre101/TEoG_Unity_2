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
        whom.RaceSystem.RaceChangeEvent += RaceChange;
        whom.GenderChangeEvent += RaceChange;
    }


    private void RaceChange()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = sprites.GetSprite(whom);
    }
}