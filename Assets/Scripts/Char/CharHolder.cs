using UnityEngine;

public abstract class CharHolder : MonoBehaviour
{
    private void Start()
    {
        spriteHandler = GetComponent<CharSpriteHandler>();
    }

    protected virtual void GenderChange()
    {
        if (BasicChar.DidGenderChange())
        {
            SpriteHandler.ChangeSprite();
        }
    }

    protected virtual void DoEveryMin(int times)
    {
        // Do this in a central timemanger instead of indvidualy so that sleeping speeds up digesion & pregnancy etc.
        //   BasicChar.RefreshOrgans();
        BasicChar.DoEveryMin(times);
    }

    public virtual void DoEveryHour()
    {
    }

    public virtual void DoEveryDay()
    {
        BasicChar.DoEveryDay();
    }

    public virtual void BeforeDestroy()
    {
    }

    public void SelfDestroy() => Destroy(gameObject);

    protected virtual void Bind()
    {
        DateSystem.NewMinuteEvent += DoEveryMin;
        DateSystem.NewDayEvent += DoEveryDay;
        BasicChar.SexualOrgans.AllOrgans.ForEach(so => so.Change += GenderChange);
        SpriteHandler.Setup(BasicChar);
        BasicChar.DestroyHolderEvent += SelfDestroy;
    }

    protected virtual void OnDestroy()
    {
        Unbind();
        BeforeDestroy();
        BasicChar.DestroyHolderEvent -= SelfDestroy;
        HolderIsDestroyed?.Invoke();
        HolderIsDestroyed = null;
    }

    protected virtual void Unbind()
    {
        DateSystem.NewMinuteEvent -= DoEveryMin;
        DateSystem.NewDayEvent -= DoEveryDay;
        BasicChar.SexualOrgans.AllOrgans.ForEach(so => so.Change -= GenderChange);
    }

    public virtual void Load(BasicChar basicChar)
    {
        Unbind();
        BasicChar = basicChar;
        Bind();
    }

    public virtual void Load(string jsonSave)
    {
        Unbind();
        JsonUtility.FromJsonOverwrite(jsonSave, BasicChar);
        Bind();
    }

    [SerializeField] private CharSpriteHandler spriteHandler = null;

    public CharSpriteHandler SpriteHandler
    {
        get
        {
            if (spriteHandler == null)
            {
                spriteHandler = GetComponent<CharSpriteHandler>();
            }
            return spriteHandler;
        }
    }

    public BasicChar BasicChar { get; protected set; } = new BasicChar();

    public delegate void IsDestroyed();

    public event IsDestroyed HolderIsDestroyed;
}