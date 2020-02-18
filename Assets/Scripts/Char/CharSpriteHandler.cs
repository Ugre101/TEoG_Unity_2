using System.Collections;
using UnityEngine;

public class CharSpriteHandler : MonoBehaviour
{
    [SerializeField] private CharSprites sprites = null;
    private CharSprite currentSprite = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BasicChar whom;

    private void Start()
    {
        // Let char be fully built first
        StartCoroutine("WaitAFrame");
    }

    private IEnumerator WaitAFrame()
    {
        yield return new WaitForEndOfFrame();
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        whom = whom != null ? whom : GetComponent<BasicChar>();

        whom.RaceSystem.RaceChangeEvent += ChangeSprite;
        whom.GenderChangeEvent += ChangeSprite;
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = sprites.GetSprite(whom);
        currentSprite = sprites.BestMatch(whom);
        Height();
    }

    private void OnDestroy()
    {
        whom.RaceSystem.RaceChangeEvent -= ChangeSprite;
        whom.GenderChangeEvent -= ChangeSprite;
    }

    private void Height()
    {
        float heightRatio = 0.5f + ((whom.Body.Height.Value / 160f) / 2);
        float newSize = Mathf.Clamp(0.1f * heightRatio * currentSprite.HeightOfSprite, 0.05f, 0.5f);
        transform.localScale = new Vector3(newSize, newSize);
    }
}