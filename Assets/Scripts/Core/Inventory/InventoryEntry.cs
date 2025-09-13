namespace SLC.RetroHorror.Core
{
    /// <summary>
    /// Expanding the Item class to add an amount so inventories can handle multiple items
    /// </summary>
    [System.Serializable]
    public class InventoryEntry
    {
        public Item Item { get; private set; }
        public int Amount { get; set; }

        public InventoryEntry(Item _item, int _amount = 1)
        {
            Item = _item;
            Amount = _amount;
        }
    }
}