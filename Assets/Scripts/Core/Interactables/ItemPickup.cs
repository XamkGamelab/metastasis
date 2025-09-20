using SLC.RetroHorror.DataPersistence;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class ItemPickup : InteractableBase
    {
        [SerializeField] private Item itemToAdd;
        [SerializeField] private int amountToAdd;
        [SerializeField] private bool hideAfterCollecting = true;
        public bool ItemWasCollected { get; private set; }

        #region interaction

        public override void OnInteract(InteractionController controller)
        {
            //Base interact just writes a debug log with interaction details
            base.OnInteract(controller);
            
            controller.HandlePickup(itemToAdd, amountToAdd);
            ItemWasCollected = true;
            if (hideAfterCollecting) gameObject.SetActive(false);
        }

        #endregion

        #region data persistence

        private const string collectedKey = "pickupWasCollected";

        public override SaveData Save()
        {
            SaveData data = base.Save();
            data.bools.Add(collectedKey, ItemWasCollected);

            return data;
        }

        public override void Load(SaveData data)
        {
            base.Load(data);
            ItemWasCollected = data.bools[collectedKey];
            if (hideAfterCollecting && ItemWasCollected) gameObject.SetActive(false);
        }

        #endregion
    }
}
