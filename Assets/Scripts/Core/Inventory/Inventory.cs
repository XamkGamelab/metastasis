using System.Collections.Generic;
using System.Linq;
using SLC.RetroHorror.DataPersistence;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class Inventory : SaveableMonoBehaviour
    {
        #region inventory functionality

        public Dictionary<string, InventoryEntry> InventoryItems { get; private set; }
        [SerializeField] private ItemCatalogue catalogue;

        protected override void Awake()
        {
            base.Awake();
            InventoryItems = new();
        }

        public void AddItem(string _itemId, int _amount = 1)
        {
            if (_itemId == null || _itemId == "") return;
            HandleAddItemEdgeCases(_amount);

            //Edge cases have been handled, actual functionality below
            if (InventoryItems.ContainsKey(_itemId))
            {
                InventoryItems[_itemId].Amount += _amount;
            }
            else
            {
                if (catalogue.itemDictionary.ContainsKey(_itemId))
                    InventoryItems.Add(_itemId, new InventoryEntry(catalogue.itemDictionary[_itemId], _amount));
                else
                {
                    Debug.LogError($"itemId {_itemId} doesn't match any existing item!");
                }
            }
        }

        public void AddItem(Item _item, int _amount = 1)
        {
            if (_item == null) return;
            HandleAddItemEdgeCases(_amount);

            //Edge cases have been handled, actual functionality below
            if (InventoryItems.ContainsKey(_item.itemId))
            {
                InventoryItems[_item.itemId].Amount += _amount;
            }
            else
            {
                InventoryItems.Add(_item.itemId, new InventoryEntry(_item, _amount));
            }
        }

        private void HandleAddItemEdgeCases(int _amount)
        {
            if (_amount < 0)
            {
                string warning = string.Concat("You are using AddItem to try to remove items from an inventory, ",
                "this is unintended and can potentially break things!");
                Debug.LogWarning(warning);
            }
            else if (_amount == 0)
            {
                Debug.LogWarning("Unnecessary AddItem call, remove this.");
            }
        }

        /// <summary>
        /// Consider using <see cref="GetItemCount"/> to check the amount of an item in an inventory first!
        /// </summary>
        /// <param name="_item"></param>
        /// <param name="_amount"></param>
        public void RemoveItem(string _itemId, int _amount = 1)
        {
            if (_itemId == null || _itemId == "" || !InventoryItems.ContainsKey(_itemId)) return;
            else if (_amount < 0)
            {
                string warning = string.Concat("You are using RemoveItem to try and add items to an inventory,",
                "this is untested and can potentially break things!");
                Debug.LogWarning(warning);
            }
            else if (_amount == 0)
            {
                Debug.LogWarning("Unnecessary RemoveItem call, remove this.");
            }

            //Edge cases have been handled, actual functionality below
            if (InventoryItems[_itemId].Amount <= _amount)
            {
                //This is meant to be a dumb function and it's left up to the developer to first check that the
                //inventory even has items to remove so here we just remove the whole ItemEntry from the dictionary
                InventoryItems.Remove(_itemId);
            }
            else
            {
                InventoryItems[_itemId].Amount -= _amount;
            }
        }

        /// <summary>
        /// Moves an item from this inventory to specified other inventory.
        /// You should probably first check how many items this inventory has.
        /// </summary>
        /// <param name="_destinationInventory"></param>
        /// <param name="_itemToMove"></param>
        /// <param name="_amount">If left null, moves all available items</param>
        public void MoveItemToOtherInventory(Inventory _destinationInventory, Item _itemToMove, uint? _amount = null)
        {
            if (_itemToMove == null || !InventoryItems.ContainsKey(_itemToMove.itemId)) return;
            else if (_amount == 0)
            {
                Debug.LogWarning("Unnecessary RemoveItem call, remove this.");
            }

            if (_amount == null || (int)_amount > InventoryItems[_itemToMove.itemId].Amount)
            {
                _destinationInventory.AddItem(_itemToMove, InventoryItems[_itemToMove.itemId].Amount);
                InventoryItems.Remove(_itemToMove.itemId);
            }
            else
            {
                _destinationInventory.AddItem(_itemToMove, (int)_amount);
                RemoveItem(_itemToMove.itemId, (int)_amount);
            }
        }

        /// <summary>
        /// Moves all items from this inventory to another inventory.
        /// </summary>
        /// <param name="_destinationInventory"></param>
        public void MoveAllToOtherInventory(Inventory _destinationInventory)
        {
            if (InventoryItems == null || InventoryItems.Count == 0) return;

            foreach (KeyValuePair<string, InventoryEntry> item in InventoryItems)
            {
                _destinationInventory.AddItem(item.Value.Item.itemId, item.Value.Amount);
            }
            InventoryItems.Clear();
        }

        /// <summary>
        /// Gets the amount of a specific item in an inventory
        /// </summary>
        /// <param name="_item">Item to get the count of</param>
        /// <returns>Returns -1 if item wasn't found in inventory</returns>
        public int GetItemCount(Item _item)
        {
            if (_item == null) return -2;
            else if (!InventoryItems.ContainsKey(_item.itemId)) return -1;
            else
            {
                return InventoryItems[_item.itemId].Amount;
            }
        }

        /// <summary>
        /// Checks if a specific item is in an inventory
        /// </summary>
        /// <param name="_item">Item to get the count of</param>
        /// <returns></returns>
        public bool InventoryHasItem(Item _item)
        {
            return InventoryHasItem(_item.itemId);
        }

        /// <summary>
        /// Checks if a specific item is in an inventory
        /// </summary>
        /// <param name="_itemID">ItemID to get the count of</param>
        /// <returns></returns>
        public bool InventoryHasItem(string _itemID)
        {
            if (_itemID == null || _itemID == "") return false;
            else if (!InventoryItems.ContainsKey(_itemID)) return false;
            else return true;
        }

        public void RemoveAllItemsOfType(Item _item)
        {
            if (_item == null) return;
            else if (!InventoryItems.ContainsKey(_item.itemId)) return;
            else
            {
                InventoryItems.Remove(_item.itemId);
            }
        }

        #endregion

        #region data persistence

        public override SaveData Save()
        {
            SaveData data = base.Save();

            foreach (KeyValuePair<string, InventoryEntry> keyValuePair in InventoryItems)
            {
                //It's fine to use an itemId as the key since those will never have duplicates
                data.ints.Add(keyValuePair.Key, keyValuePair.Value.Amount);
            }

            return data;
        }

        public override void Load(SaveData data)
        {
            base.Load(data);

            InventoryItems = new();

            foreach (KeyValuePair<string, int> loadedItem in data.ints)
            {
                Item item = catalogue.itemDictionary.Where(entry => entry.Key == loadedItem.Key).First().Value;
                AddItem(item, loadedItem.Value);
            }
        }

        #endregion
    }
}