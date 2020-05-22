using UnityEngine;

public class Npc : BasicChar
{
    [SerializeField] private bool triggerColider = true, hasNpcMenuPage = false;
    [SerializeField] private GameObject npcMenuPage = null;

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
        if (hasNpcMenuPage)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerColider && collision.gameObject.CompareTag(PlayerMain.GetTag))
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