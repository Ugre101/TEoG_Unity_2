using System.Collections;
using UnityEngine;

public class CharSpriteHandler : MonoBehaviour
{
    [SerializeField] private CharSprites sprites = null;
    private CharSprite currentSprite = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BasicChar whom;

    public void Setup(BasicChar whom)
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        this.whom = whom;

        whom.RaceSystem.RaceChangeEvent += ChangeSprite;
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        spriteRenderer.sprite = sprites.GetSprite(whom);
        currentSprite = sprites.BestMatch(whom);
        Height();
    }

    private void OnDestroy() => whom.RaceSystem.RaceChangeEvent -= ChangeSprite;

    private void Height()
    {
        float heightRatio = 0.5f + ((whom.Body.Height.Value / 160f) / 2);
        float newSize = Mathf.Clamp(0.1f * heightRatio * currentSprite.HeightOfSprite, 0.05f, 0.5f);
        transform.localScale = new Vector3(newSize, newSize);
    }
}