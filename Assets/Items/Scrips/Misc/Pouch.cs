using UnityEngine;

namespace ItemScripts
{
    [CreateAssetMenu(fileName = "Pouch", menuName = "Item/Misc/Pouch")]
    public class Pouch : Misc
    {
        public Pouch() : base(ItemId.Pouch, "Pouch") => useName = "Open";

        public override string Use(BasicChar user)
        {
            int gain = Random.Range(10, 50);
            user.Currency.Gold += gain;
            return $"What's in the bag? It's {gain} coins!";
        }
    }
}