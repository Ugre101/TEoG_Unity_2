﻿using UnityEngine;

public class SmartTree : MonoBehaviour
{
    private PlayerSprite Player => PlayerSprite.Instance;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite stump = null, tree = null;
    [SerializeField] private CapsuleCollider2D capsule = null;
    private Vector2 normalSize = new Vector2(), smallerSize = new Vector2(0.1f, 0.1f);
    private bool chopped = false;

    private int hp = 100;
    private DateSave timeChoppedDown;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        capsule = capsule != null ? capsule : GetComponent<CapsuleCollider2D>();

        normalSize = capsule.size;
        SetSprite();
    }

    private void OnEnable()
    {
        DateSystem.NewDayEvent += RemoveTreeStumpAfterTwoDays;
        RemoveTreeStumpAfterTwoDays();
    }

    private void OnDisable() => DateSystem.NewDayEvent -= RemoveTreeStumpAfterTwoDays;

    private void RemoveTreeStumpAfterTwoDays()
    {
        if (chopped && timeChoppedDown.CompareDateDays() > 2)
        {
            SmartTreeObjectPool.Instance.ReturnTree(this);
        }
    }

    private void Update()
    {
        if (KeyBindings.ActionKey.KeyDown)
        {
            Action(Player);
        }
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = chopped ? stump : tree;
        capsule.size = chopped ? smallerSize : normalSize;
    }

    private void OnMouseDown() => Action(Player);

    public void Action(CharHolder npc, int dmg = 25)
    {
        if (Vector2.Distance(npc.transform.position, transform.position) < 5f)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                chopped = true;
                timeChoppedDown = DateSystem.Save;
                SetSprite();
                npc.BasicChar.Inventory.AddItem(ItemIds.Wood);
            }
        }
    }

    public void Action(PlayerSprite player, int dmg = 25)
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 5f)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                chopped = true;
                timeChoppedDown = DateSystem.Save;
                SetSprite();
                PlayerMain.Player.Inventory.AddItem(ItemIds.Wood);
            }
        }
    }
}