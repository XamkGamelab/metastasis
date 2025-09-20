using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class WorldUICameraHandler : MonoBehaviour
    {
        private void Update()
        {
            transform.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);
        }
    }
}
