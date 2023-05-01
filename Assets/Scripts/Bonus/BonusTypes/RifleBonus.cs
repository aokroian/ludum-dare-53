using Actors;
using Actors.Combat;
using Actors.InputThings;
using UnityEngine;

namespace Bonus.BonusTypes
{
    public class RifleBonus : AbstractBonus
    {
        [SerializeField] private float shotsAmount = 10;
        
        protected override void ApplyBonus(PlayerActorInput player)
        {
            player.GetComponent<ActorGunSystem>().ChangeActiveGun(GunTypes.Rifle);
        }
    }
}