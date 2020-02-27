using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "SmallPouch", menuName = "Item/Misc/SmallPouch")]
    public class SmallPouch : Misc
    {
        public SmallPouch() : base(ItemIds.SmallPouch, "Small pouch") => useName = "Open";

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(5, 30);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}