using UnityEngine;

public class SmartTree : MonoBehaviour
{
    [SerializeField] private PlayerHolder player = null;
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
        player = player != null ? player : PlayerHolder.GetPlayerHolder;

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
            Action(player);
        }
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = chopped ? stump : tree;
        capsule.size = chopped ? smallerSize : normalSize;
    }

    private void OnMouseDown() => Action(player);

    public void Action(CharHolder npc)
    {
        if (Vector2.Distance(npc.transform.position, transform.position) < 5f)
        {
            hp -= 25;
            if (hp <= 0)
            {
                chopped = true;
                timeChoppedDown = DateSystem.Save;
                SetSprite();
                npc.BasicChar.Inventory.AddItem(ItemIds.Wood);
            }
        }
    }
}