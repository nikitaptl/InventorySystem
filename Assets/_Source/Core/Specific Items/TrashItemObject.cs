using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Trash", menuName = "Items/Trash")]
    public class TrashItemObject : ItemObject
    {
        public float cost;

        private void OnEnable()
        {
            itemType = ItemType.Trash;
            maximumAmount = 64;
        }
    }
}