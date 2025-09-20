namespace SLC.RetroHorror.Core
{
    public interface IInteractable
    {
        void OnInteract(InteractionController controller);
        void ActivateIndicator();
        void DeactivateIndicator();
    }
}