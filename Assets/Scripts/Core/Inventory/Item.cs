using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class Item : ScriptableObject
    {
        /// <summary>
        /// This should be unique for each different item type!
        /// </summary>
        public string ItemId;
        public string ItemName;
        [TextArea(2, 10)] public string ItemDescription;
        public int ItemMaxStack;
        public float ItemWeight;
        public Mesh ItemMesh;
        public Material ItemMaterial;
    }
}