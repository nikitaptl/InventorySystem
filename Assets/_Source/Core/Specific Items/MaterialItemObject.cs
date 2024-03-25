﻿using UnityEngine;

namespace _Source.Core.Specific_Items
{
    [CreateAssetMenu(fileName = "Material", menuName = "Items/Material")]
    public class MaterialItemObject : ItemObject
    {
        public float cost;

        private void Start()
        {
            itemType = ItemType.Material;
            maximumAmount = 64;
        }
    }
}