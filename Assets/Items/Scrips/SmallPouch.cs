using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "SmallPouch", menuName = "Item/SmallPouch")]
    public class SmallPouch : Item
    {
        public SmallPouch() : base(ItemId.Pouch, "Small pouch", ItemTypes.Misc)
        {
        }

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(5, 30);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}