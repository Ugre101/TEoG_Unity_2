using UnityEngine;

public class SmartTree : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite stump = null, tree = null;
    private bool chopped = false;

    private bool started = false;
    private int hp = 100;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = spriteRenderer != null ? spriteRenderer : GetComponent<SpriteRenderer>();
        player = player != null ? player : PlayerMain.GetPlayer;
        if (stump == null || tree == null)
        {
            Debug.LogError("A tree is missing sprites and was DESTROYED");
            Destroy(gameObject);
        }
        started = true;
    }

    private void OnEnable()
    {
        if (started)
        {
            SetSprite();
        }
        if (chopped && timeChoppedDown.HasValue)
        {
            if (timeChoppedDown.Value.CompareDateDays() > 2)
            {
                Destroy(gameObject);
            }
        }
    }

    private void SetSprite() => spriteRenderer.sprite = chopped ? stump : tree;

    private DateSave? timeChoppedDown;

    private void OnMouseDown()
    {
        Debug.Log("Click tree?");
        if (Vector2.Distance(player.transform.position, transform.position) < 5f)
        {
            hp -= 25;
            if (hp <= 0)
            {
                chopped = true;
                timeChoppedDown = DateSystem.Save;
                SetSprite();
                player.Inventory.AddItem(ItemIds.Wood);
            }
        }
    }
}