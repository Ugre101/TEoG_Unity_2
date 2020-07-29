using System.Collections.Generic;

public class DormSex : BaseSexMonoBehavior
{
    public void Setup(BasicChar partner) => Setup(new List<BasicChar>() { partner });

    public override void Setup(List<BasicChar> partners)
    {
        base.Setup(partners);
    }
}
