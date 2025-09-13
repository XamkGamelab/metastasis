using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class Item : ScriptableObject
    {
        /// <summary>
        /// This should be unique for each different item type!
        /// </summary>
        [field: SerializeField] public string itemId { get; private set; }
        [field: SerializeField] public string itemName { get; private set; }

        [field: SerializeField, TextArea(2, 10)]
        public string itemDescription { get; private set; }
        [field: SerializeField] public int itemMaxStack { get; private set; }
        [field: SerializeField] public float itemWeight { get; private set; }
        [field: SerializeField] public GameObject itemPrefab { get; private set; }
    }
}