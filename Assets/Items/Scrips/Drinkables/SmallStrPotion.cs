namespace ItemScripts
{
    public class SmallStrPotion : Drinkable
    {
        public SmallStrPotion() : base(ItemIds.SmallStrPotion, "Small strength potion")
        {
        }

        public override string Use(BasicChar user)
        {
            return base.Use(user);
        }
    }
}