using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class WeaponItemObject : ItemObject
    {
        // Means how mush you can use Weapon
        public int strengh;
        public float damage;
        
        private void OnEnable()
        {
            itemType = ItemType.Weapon;
            maximumAmount = 1;
        }
    }
}