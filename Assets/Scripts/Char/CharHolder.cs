using UnityEngine;

public abstract class CharHolder : MonoBehaviour
{
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
        BasicChar.OverTimeTick(times);
    }

    public virtual void DoEveryHour()
    {
    }

    public virtual void DoEveryDay()
    {
        BasicChar.GrowFetuses();
        BasicChar.PregnancySystem.GrowChild();
    }

    public virtual void BeforeDestroy()
    {
    }

    public virtual void Setup() => Bind();

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
    }

    protected virtual void Unbind()
    {
        DateSystem.NewMinuteEvent -= DoEveryMin;
        DateSystem.NewDayEvent -= DoEveryDay;
        BasicChar.SexualOrgans.AllOrgans.ForEach(so => so.Change -= GenderChange);
    }

    public virtual void Load(BasicChar basicChar)
    {
        BasicChar = basicChar;
        Unbind();
        Bind();
    }

    public virtual void Load(string jsonSave)
    {
        JsonUtility.FromJsonOverwrite(jsonSave, BasicChar);
        Unbind();
        Bind();
    }

    private CharSpriteHandler spriteHandler = null;

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

    public abstract BasicChar BasicChar { get; protected set; }
}