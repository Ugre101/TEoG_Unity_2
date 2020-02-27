using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "LargePouch", menuName = "Item/Misc/LargePouch")]
    public class LargePouch : Misc
    {
        public LargePouch() : base(ItemIds.LargePouch, "Large pouch") => useName = "Open";

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(25, 100);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}