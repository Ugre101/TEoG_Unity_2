using UnityEngine;

public class CharSpriteHandler : MonoBehaviour
{
    [SerializeField] private CharSprites sprites = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BasicChar whom;

    private void Start()
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        whom = whom != null ? whom : GetComponent<BasicChar>();

        whom.RaceSystem.RaceChangeEvent += ChangeSprite;
        whom.GenderChangeEvent += ChangeSprite;
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        Height();
        spriteRenderer.sprite = sprites.GetSprite(whom);
    }

    private void OnDestroy()
    {
        whom.RaceSystem.RaceChangeEvent -= ChangeSprite;
        whom.GenderChangeEvent -= ChangeSprite;
    }

    private void Height()
    {
        float heightRatio = whom.Body.Height.Value / 160f;
        float newSize = Mathf.Clamp(0.1f * heightRatio, 0.05f, 0.5f);
        transform.localScale = new Vector3(newSize, newSize);
    }
}