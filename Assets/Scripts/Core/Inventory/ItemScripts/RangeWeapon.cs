using UnityEngine;

namespace SLC.RetroHorror.Core
{
    [CreateAssetMenu(fileName = "NewRangeWeapon", menuName = "Items/RangeWeapon")]
    public class RangeWeapon : Item
    {
        public enum GunType
        {
            handgun = 0,
            rifle,
        }

        public enum ReloadMethod
        {
            single = 0,
            clip
        }

        //Could include other stuff like pump-action but I'd like to know how weapon animations
        //are handled before adding potentially unnecessary options, i.e. how will the animation
        //be decided when for example the future Shoot() method is called.
        public enum ActionType
        {
            fullAuto = 0,
            semiAuto
        }

        [field: SerializeField] public GunType gunType { get; private set; }
        [field: SerializeField] public ReloadMethod reloadMethod { get; private set; }
        [field: SerializeField] public ActionType actionType { get; private set; }
        [field: SerializeField] public float shotCooldownSeconds { get; private set; }
        [field: SerializeField] public float minDamagePerProjectile { get; private set; }
        [field: SerializeField] public float maxDamagePerProjectile { get; private set; }
        [field: SerializeField] public int projectilesPerShot { get; private set; }
        [field: SerializeField] public float range { get; private set; }
        [field: SerializeField] public float minFiringSpread { get; private set; }
        [field: SerializeField] public float maxFiringSpread { get; private set; }
        [field: SerializeField] public float spreadRecoveryPerSecond { get; private set; }
        [field: SerializeField] public string ammoId { get; private set; }
    }
}