using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "Pouch", menuName = "Item/Pouch")]
    public class ItemPouch : Item
    {
        public ItemPouch() : base(ItemId.Pouch, "Pouch", ItemTypes.Misc)
        {
        }

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(10, 50);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}