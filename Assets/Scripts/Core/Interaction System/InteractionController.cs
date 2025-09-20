using System.Collections.Generic;
using System.Linq;
using SLC.RetroHorror.Input;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class InteractionController : MonoBehaviour
    {
        #region variables

        private PlayerController playerController;
        private Inventory playerInventory;
        private Transform playerTransform;

        [Header("Input Variables")]
        [SerializeField] private InputReader inputReader;

        [Header("Interaction Settings")]
        [SerializeField] private BoxCollider interactionCollider;
        private List<InteractableBase> interactables;

        #endregion

        #region Unity default methods

        private void Start()
        {
            playerController = GetComponentInParent<PlayerController>();
            playerInventory = playerController.Inventory;
            playerTransform = playerController.transform;
            if (playerTransform == null)
            {
                string error = string.Concat("InteractionController couldn't find PlayerController!",
                "Make sure that InteractionController is attached to a child of PlayerController.",
                "Destroying InteractionController to stop cascading errors.");
                Debug.LogError(error);
                Destroy(this);
            }

            interactables = new();
            inputReader.InteractEvent += HandleInteractDown;
            inputReader.InteractEventCancelled += HandleInteractUp;
        }

        private void Update()
        {
            if (playerController.IsMoving && interactables.Count > 0)
            {
                SortInteractables();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == null || !other.TryGetComponent(out InteractableBase interactable)) return;

            if (!interactables.Contains(interactable))
            {
                interactables.Add(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other == null || !other.TryGetComponent(out InteractableBase interactable)) return;

            if (interactables.Contains(interactable))
            {
                interactables.Remove(interactable);
                if (interactable.IndicatorIsVisible) interactable.DeactivateIndicator();
            }
        }

        private void OnDestroy()
        {
            UnsubscribeInputs();
        }

        #endregion

        #region interact methods

        private void TryInteract()
        {
            if (interactables == null || interactables.Count == 0) return;
            interactables[0].OnInteract(this);
        }

        /// <summary>
        /// This method sorts available interactable objects by their distance to
        /// the player's position. Used to make sure the player always interacts
        /// with the closest available interactable.
        /// </summary>
        private void SortInteractables()
        {
            List<InteractableBase> oldOrder = interactables;
            interactables = interactables.OrderBy(col => Vector3.Distance(playerTransform.position, col.transform.position)).ToList();
            if (oldOrder[0] == interactables[0])
            {
                if (!interactables[0].IndicatorIsVisible) interactables[0].ActivateIndicator();
                return;
            }

            bool closestActivated = false;
            for (int i = 0; i < interactables.Count; i++)
            {
                if (!closestActivated)
                {
                    interactables[i].ActivateIndicator();
                    closestActivated = true;
                }
                else interactables[i].DeactivateIndicator();
            }
        }

        public void RemoveColliderFromInteractableList(InteractableBase _interactable)
        {
            if (_interactable == null) return;

            if (interactables.Contains(_interactable))
            {
                interactables.Remove(_interactable);
            }
        }

        #endregion

        #region inventory handling

        public void HandlePickup(Item _item, int _amount)
        {
            playerInventory.AddItem(_item, _amount);
        }

        public bool PlayerHasKey(Item _key)
        {
            return playerInventory.InventoryHasItem(_key);
        }

        #endregion

        #region door handling

        public void SendPlayerToTransform(Transform _newTransform)
        {
            playerController.SendPlayerToTransform(_newTransform);
        }

        #endregion

        #region input

        private void HandleInteractDown()
        {
            TryInteract();
        }

        private void HandleInteractUp()
        {

        }

        private void UnsubscribeInputs()
        {
            inputReader.InteractEvent -= HandleInteractDown;
            inputReader.InteractEventCancelled -= HandleInteractUp;
        }

        #endregion

        #region gizmos

        private void OnDrawGizmos()
        {
            if (interactables == null) Gizmos.color = Color.green;
            else Gizmos.color = interactables.Count == 0 ? Color.green : Color.red;
            Gizmos.matrix = interactionCollider.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, interactionCollider.size);
        }

        #endregion
    }
}