using UnityEngine;

public class CharSpriteHandler : MonoBehaviour
{
    [SerializeField] protected CharSprites sprites = null;
    [SerializeField] protected SpriteRenderer spriteRenderer = null;
    protected CharSprite currentSprite = null;
    protected BasicChar whom;

    public void Setup(BasicChar whom)
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        this.whom = whom;

        whom.RaceSystem.RaceChange += ChangeSprite;
        ChangeSprite();
    }

    public void ChangeSprite(Races newRacem, Races oldRace) => ChangeSprite();

    public void ChangeSprite()
    {
        spriteRenderer.sprite = sprites.GetSprite(whom);
        currentSprite = sprites.BestMatch(whom);
        Height();
    }

    protected void OnDestroy()
    {
        if (whom != null)
        {
            whom.RaceSystem.RaceChange -= ChangeSprite;
        }
    }

    protected void Height()
    {
        float heightRatio = 0.5f + ((whom.Body.Height.Value / 160f) / 2);
        float newSize = Mathf.Clamp(0.1f * heightRatio * currentSprite.HeightOfSprite, 0.05f, 0.5f);
        transform.localScale = new Vector3(newSize, newSize);
    }
}