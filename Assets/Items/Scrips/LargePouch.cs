using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "LargePouch", menuName = "Item/LargePouch")]
    public class LargePouch : Item
    {
        public LargePouch() : base(ItemId.Pouch, "Large pouch", ItemTypes.Misc)
        {
        }

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(25, 100);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}