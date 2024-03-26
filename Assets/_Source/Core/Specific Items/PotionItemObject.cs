﻿using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion")]
    public class PotionItemObject : ItemObject
    {
        public string effect;

        private void OnEnable()
        {
            itemType = ItemType.Potion;
            maximumAmount = 1;
        }
    }
}