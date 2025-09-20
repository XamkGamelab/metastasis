using SLC.RetroHorror.DataPersistence;
using TMPro;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class InteractableBase : SaveableMonoBehaviour, IInteractable
    {
        [Space, Header("Interaction Settings")]
        public bool isInteractable = true;
        
        [Tooltip("Show this message whenever within interaction range.")]
        public string interactionMessage = "Interact";
        [SerializeField] private GameObject interactIndicator;
        [SerializeField] private TextMeshProUGUI interactText;

        #region Properties
        public Collider InteractCollider { get; private set; }
        public bool IndicatorIsVisible { get; private set; } = false;

        #endregion

        protected virtual void Start()
        {
            if (!TryGetComponent(out Collider attachedCollider))
            {
                Debug.LogWarning($"Interactable {gameObject.name} does not have the collider required for interaction!");
            }
            InteractCollider = attachedCollider;
            interactText.text = interactionMessage;
        }

        public virtual void OnInteract(InteractionController controller)
        {
            Debug.Log("Interacted with: " + gameObject.name);
        }

        public void ActivateIndicator()
        {
            if (interactIndicator == null) return;
            IndicatorIsVisible = true;
            interactIndicator.SetActive(true);
        }

        public void DeactivateIndicator()
        {
            if (interactIndicator == null) return;
            IndicatorIsVisible = false;
            interactIndicator.SetActive(false);
        }
    }
}