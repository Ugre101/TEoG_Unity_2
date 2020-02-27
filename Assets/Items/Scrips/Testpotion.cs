using UnityEngine;

[CreateAssetMenu(fileName = "TestPotion", menuName = "TestPotion")]
public class TestPotion : Drinkable
{
    public TestPotion() : base(ItemIds.WoodenStick, "Test potion")
    {
    }

    public override string Use(BasicChar user)
    {
        return base.Use(user);
    }
}