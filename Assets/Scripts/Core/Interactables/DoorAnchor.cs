using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class DoorAnchor : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3f);
        }
    }
}
