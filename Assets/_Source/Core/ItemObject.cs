using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.Core
{
    public abstract class ItemObject : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        public Sprite itemIcon;

        // A field indicating the maximum amount of resource that can be stored in 1 inventory slot
        public int maximumAmount;

        // In order to throw an Item Object out of the inventory, it is necessary to store information about Prefab
        public GameObject itemPrefab;
    }
}