using UnityEngine;

namespace SLC.RetroHorror.Core
{
    [CreateAssetMenu(fileName = "NewNoteItem", menuName = "Items/NoteItem")]
    public class NoteItem : Item
    {
        [field: SerializeField, TextArea(4, 16)]  public string noteText { get; private set; }
    }
}