using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class DoorInteractable : InteractableBase
    {
        [SerializeField] private Transform newPositionAnchor;
        [SerializeField] private bool requiresKey;
        [SerializeField] private Item keyItem;

        public override void OnInteract(InteractionController controller)
        {
            //Base interact just writes a debug log with interaction details
            base.OnInteract(controller);

            if (requiresKey && !controller.PlayerHasKey(keyItem))
            {
                //Probably add some UI indicator here telling player they need a key.
                //Loud incorrect buzzer idk.
                Debug.Log("Door requires a key you don't have!");
            }
            else controller.SendPlayerToTransform(newPositionAnchor);
        }
    }
}