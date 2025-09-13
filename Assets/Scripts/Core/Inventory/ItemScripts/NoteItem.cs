using UnityEngine;

namespace SLC.RetroHorror.Core
{
    [CreateAssetMenu(fileName = "NewNoteItem", menuName = "Items/NoteItem")]
    public class NoteItem : Item
    {
        public string NoteText;
    }
}