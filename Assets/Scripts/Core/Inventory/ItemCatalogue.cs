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
            noteItems = Resources.LoadAll<NoteItem>("Items/NoteItems");
            meleeWeapons = Resources.LoadAll<MeleeWeapon>("Items/MeleeWeapons");
            rangeWeapons = Resources.LoadAll<RangeWeapon>("Items/RangeWeapons");

            foreach (AmmoItem ammoItem in ammoItems)
            {
                itemDictionary.Add(ammoItem.ItemId, ammoItem);
            }

            foreach (ConsumableItem consumableItem in consumableItems)
            {
                itemDictionary.Add(consumableItem.ItemId, consumableItem);
            }

            foreach (NoteItem noteItem in noteItems)
            {
                itemDictionary.Add(noteItem.ItemId, noteItem);
            }

            foreach (MeleeWeapon meleeWeapon in meleeWeapons)
            {
                itemDictionary.Add(meleeWeapon.ItemId, meleeWeapon);
            }

            foreach (RangeWeapon rangeWeapon in rangeWeapons)
            {
                itemDictionary.Add(rangeWeapon.ItemId, rangeWeapon);
            }
        }
    }
}