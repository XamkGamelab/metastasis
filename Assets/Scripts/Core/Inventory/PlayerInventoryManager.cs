using System.Collections.Generic;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    /// <summary>
    /// This is a barebones test class, used to test Inventory UI.
    /// Expand as needed with future player inventory UI functionality.
    /// </summary>
    public class PlayerInventoryManager : MonoBehaviour
    {
        [SerializeField] private Transform inventoryEntryHolder;
        [SerializeField] private GameObject itemEntryPrefab;
        [SerializeField] private Inventory inventory;

        public void UpdateInventoryState()
        {
            ClearInventory();

            foreach (KeyValuePair<string, InventoryEntry> item in inventory.InventoryItems)
            {
                ItemUI itemUI = Instantiate(itemEntryPrefab, inventoryEntryHolder).GetComponent<ItemUI>();

                //Calc some values and remove unnecessary zeroes from the end of item total weight.
                float itemTotalWeight = (float)item.Value.Amount * item.Value.Item.itemWeight;
                string formattedWeight = itemTotalWeight.ToString("0.000");
                if (formattedWeight[^2] == '0' && formattedWeight[^1] == '0')
                {
                    formattedWeight = formattedWeight[..^2];
                }
                else if (formattedWeight[^1] == '0')
                {
                    formattedWeight = formattedWeight[..^1];
                }

                //Update UI
                itemUI.itemNameField.text = item.Value.Item.itemName;
                itemUI.itemDescField.text = item.Value.Item.itemDescription;
                itemUI.itemCountField.text = string.Concat("x", item.Value.Amount.ToString());
                itemUI.itemWeightField.text = string.Concat(formattedWeight, "kg");
            }
        }

        private void ClearInventory()
        {
            foreach (Transform child in inventoryEntryHolder)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
