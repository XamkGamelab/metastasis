using UnityEngine;

namespace SLC.RetroHorror.Core
{
    [CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Items/MeleeWeapon")]
    public class MeleeWeapon : Item
    {
        [field: SerializeField] public float range { get; private set; }
        [field: SerializeField] public float attackCooldownSeconds { get; private set; }
    }
}