using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Food", menuName = "Items/Food")]
    public class FoodItemObject : ItemObject
    {
        public float hungerAmount;

        private void Start()
        {
            itemType = ItemType.Food;
            maximumAmount = 16;
        }
    }
}
