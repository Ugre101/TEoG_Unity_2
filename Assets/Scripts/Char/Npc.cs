using UnityEngine;

public class Npc : BasicChar
{
    [SerializeField] private NpcMenuPage npcMenuPage = null;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    protected virtual void OnCollision()
    {
        if (npcMenuPage != null)
        {
            CanvasMain.GetCanvasMain.EnterNpc(npcMenuPage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerMain.GetTag))
        {
            OnCollision();
        }
    }

    public void Save()
    {
        // Relationship and stuff
    }

    public void Load()
    {
    }
}