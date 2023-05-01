using Actors;
using Actors.Combat;
using Actors.InputThings;
using UnityEngine;

namespace Bonus.BonusTypes
{
    public class ShotgunBonus : AbstractBonus
    {
        [SerializeField] private int shotsAmount = 10;
        
        private int _shotsLeft;
        private ActorGunSystem _playerGunSystem;
        
        protected override void ApplyBonus(PlayerActorInput player)
        {
            
            _playerGunSystem = player.GetComponent<ActorGunSystem>();
            var gun = _playerGunSystem.ChangeActiveGun(GunTypes.Shotgun);
            gun.OnFire += OnFire;
            _shotsLeft = shotsAmount;
        }

        private void OnFire()
        {
            _shotsLeft--;
            if (_shotsLeft <= 0)
            {
                _playerGunSystem.ChangeActiveGun(GunTypes.Pistol);
                Destroy(gameObject);
            }
        }
    }
}