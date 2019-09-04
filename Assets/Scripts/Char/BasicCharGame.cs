using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharGame : MonoBehaviour
{
    public CharSprites sprites;
    private SpriteRenderer spriteRenderer;
    [Header("Scriptable objects")]
    public Settings settings;
    public EventLog eventLog;
    private BasicChar whom;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        whom = GetComponent<BasicChar>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites.GetSprite(whom);
    }
}
