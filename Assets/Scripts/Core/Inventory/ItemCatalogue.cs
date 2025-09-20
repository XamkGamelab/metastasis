using System.Collections.Generic;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    /// <summary>
    /// ItemCatalogue houses references to all items in the game
    /// in the form of a dictionary for easy lookups via item ID
    /// </summary>
    [CreateAssetMenu(menuName = "ItemCatalogue")]
    public class ItemCatalogue : ScriptableObject
    {
        public Dictionary<string, Item> itemDictionary;
        private AmmoItem[] ammoItems;
        private ConsumableItem[] consumableItems;
        private KeyItem[] keyItems;
        private NoteItem[] noteItems;
        private MeleeWeapon[] meleeWeapons;
        private RangeWeapon[] rangeWeapons;

        /// <summary>
        /// This method fetches references to all Item ScriptableObjects
        /// directly from their subfolders within the Resources-folder.
        /// </summary>
        public void InitializeItemDictionary()
        {
            itemDictionary = new();
            ammoItems = Resources.LoadAll<AmmoItem>("Items/AmmoItems");
            consumableItems = Resources.LoadAll<ConsumableItem>("Items/ConsumableItems");
            keyItems = Resources.LoadAll<KeyItem>("Items/KeyItems");
            noteItems = Resources.LoadAll<NoteItem>("Items/NoteItems");
            meleeWeapons = Resources.LoadAll<MeleeWeapon>("Items/MeleeWeapons");
            rangeWeapons = Resources.LoadAll<RangeWeapon>("Items/RangeWeapons");

            foreach (AmmoItem ammoItem in ammoItems)
            {
                itemDictionary.Add(ammoItem.itemId, ammoItem);
            }

            foreach (ConsumableItem consumableItem in consumableItems)
            {
                itemDictionary.Add(consumableItem.itemId, consumableItem);
            }

            foreach (KeyItem keyItem in keyItems)
            {
                itemDictionary.Add(keyItem.itemId, keyItem);
            }

            foreach (NoteItem noteItem in noteItems)
            {
                itemDictionary.Add(noteItem.itemId, noteItem);
            }

            foreach (MeleeWeapon meleeWeapon in meleeWeapons)
            {
                itemDictionary.Add(meleeWeapon.itemId, meleeWeapon);
            }

            foreach (RangeWeapon rangeWeapon in rangeWeapons)
            {
                itemDictionary.Add(rangeWeapon.itemId, rangeWeapon);
            }
        }
    }
}