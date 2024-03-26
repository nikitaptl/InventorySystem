using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Instrument", menuName = "Items/Instrument")]
    public class InstrumentItemObject : ItemObject
    {
        public int strengh;
        public float damage;

        private void OnEnable()
        {
            itemType = ItemType.Instrument;
            maximumAmount = 1;
        }
    }
}