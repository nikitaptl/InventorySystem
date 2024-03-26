using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
    public class ArmorItemObject : ItemObject
    {
        public float protectionAmount;

        private void OnEnable()
        {
            itemType = ItemType.Armor;
            maximumAmount = 1;
        }
    }
}