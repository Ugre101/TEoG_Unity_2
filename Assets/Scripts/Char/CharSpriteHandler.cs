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

        spriteRenderer.sprite = sprites.GetSprite(whom);
        whom.RaceSystem.RaceChangeEvent += RaceChange;
        whom.GenderChangeEvent += GenderChange;
       // Height();
    }

    private void RaceChange() => ChangeSprite();

    private void GenderChange() => ChangeSprite();

    private void ChangeSprite() => spriteRenderer.sprite = sprites.GetSprite(whom);

    private void OnDestroy()
    {
        whom.RaceSystem.RaceChangeEvent -= RaceChange;
        whom.GenderChangeEvent -= GenderChange;
    }

    private void Height()
    {
        float heightRatio = whom.Body.Height.Value / 160f;
        float newSize = 0.1f * heightRatio;
        transform.localScale = new Vector3(newSize, newSize);
    }
}