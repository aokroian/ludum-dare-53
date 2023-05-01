using System.Linq;
using Actors;
using Actors.Combat;
using UnityEngine;

namespace Sounds
{
    public static class SoundSystem
    {
        private static SoundsConfig Sounds
        {
            get
            {
                if (_sounds == null)
                    _sounds = Resources.Load<SoundsConfig>("SoundsConfig");

                return _sounds;
            }
        }

        private static SoundsConfig _sounds;

        public static void ActorDamageSound(ActorHealth actorHealth)
        {
            AudioSource.PlayClipAtPoint(Sounds.actorDamageSound, actorHealth.transform.position);
        }

        public static void ActorDeathSound(ActorHealth actorHealth)
        {
            AudioSource.PlayClipAtPoint(Sounds.actorDeathSound, actorHealth.transform.position);
        }

        public static void ActorHealSound(ActorHealth actorHealth)
        {
            AudioSource.PlayClipAtPoint(Sounds.actorHealSound, actorHealth.transform.position);
        }

        public static void BulletHitSound(Bullet bullet)
        {
            var sound = Sounds.bulletsSounds.FirstOrDefault(p => p.bulletType == bullet.BulletType)?.bulletHitSound;
            if (sound != null)
                AudioSource.PlayClipAtPoint(sound, bullet.transform.position);
        }

        public static void GunShotSound(Gun gun)
        {
            var sound = Sounds.gunShotsSound.FirstOrDefault(p => p.gunType == gun.GunType)?.shotSound;
            if (sound != null)
                AudioSource.PlayClipAtPoint(sound, gun.transform.position);
        }
    }
}