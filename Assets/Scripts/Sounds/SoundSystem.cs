using System.Collections;
using System.Linq;
using Actors;
using Actors.Combat;
using Actors.InputThings;
using Map.Model;
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

        private static Camera MainCamera
        {
            get
            {
                if (_camera == null)
                    _camera = Camera.main;
                return _camera;
            }
        }

        private static Camera _camera;


        #region Music

        private static AudioSource MusicAudioSource1
        {
            get
            {
                if (_musicAudioSource1 != null)
                    return _musicAudioSource1;
                var spawnedGo = new GameObject("Music_AudioSource_1");
                _musicAudioSource1 = spawnedGo.AddComponent<AudioSource>();
                _musicAudioSource1.loop = true;
                Object.DontDestroyOnLoad(spawnedGo);

                _crossFadeAudioCoroutineOwner = spawnedGo.AddComponent<DummyMonoBehaviour>();
                return _musicAudioSource1;
            }
        }

        private static AudioSource _musicAudioSource1;

        private static AudioSource MusicAudioSource2
        {
            get
            {
                if (_musicAudioSource2 != null)
                    return _musicAudioSource2;
                var spawnedGo = new GameObject("Music_AudioSource_2");
                _musicAudioSource2 = spawnedGo.AddComponent<AudioSource>();
                _musicAudioSource2.loop = true;
                Object.DontDestroyOnLoad(spawnedGo);

                return _musicAudioSource2;
            }
        }

        private static AudioSource _currentMusicAudioSource;
        private static AudioSource _musicAudioSource2;
        private static Coroutine _crossFadeAudioCoroutine;
        private static MonoBehaviour _crossFadeAudioCoroutineOwner;

        private static IEnumerator CrossFadeAudio(AudioClip clip)
        {
            if (_currentMusicAudioSource != null &&
                _currentMusicAudioSource.isPlaying &&
                _currentMusicAudioSource.clip != null &&
                _currentMusicAudioSource.clip == clip)
            {
                yield break;
            }

            var from = _currentMusicAudioSource == MusicAudioSource2
                ? MusicAudioSource2
                : MusicAudioSource1;
            var to = _currentMusicAudioSource == MusicAudioSource2
                ? MusicAudioSource1
                : MusicAudioSource2;

            _currentMusicAudioSource = to;

            to.clip = clip;
            to.volume = 0f;
            to.Play();

            float t = 0;
            var v = from.volume;

            while (t < 0.98f)
            {
                t = Mathf.Lerp(t, 1f, Time.deltaTime * 0.7f);
                from.volume = Mathf.Lerp(v, 0f, t);
                to.volume = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            from.Play();
            from.volume = 0f;
            to.volume = 1f;
        }

        private static void CrossFadeMusic(AudioClip musicClip)
        {
            if (_crossFadeAudioCoroutineOwner == null)
                Debug.Log(MusicAudioSource1.name);
            if (_crossFadeAudioCoroutine != null)
                _crossFadeAudioCoroutineOwner.StopCoroutine(_crossFadeAudioCoroutine);
            _crossFadeAudioCoroutineOwner.StartCoroutine(CrossFadeAudio(musicClip));
        }

        public static void PlayMenuMusic()
        {
            CrossFadeMusic(Sounds.menuMusic);
        }

        public static void PlayCombatMusic()
        {
            CrossFadeMusic(Sounds.combatMusic);
        }

        public static void PlayPeacefulMusic()
        {
            CrossFadeMusic(Sounds.peacefulMusic);
        }

        #endregion

        #region Actor_Sounds

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

        #endregion

        #region Combat_Sounds

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

        #endregion

        #region Map_Related_Sounds

        public static void PlayMoveToAnotherDepthSound(PlayerActorInput player)
        {
            AudioSource.PlayClipAtPoint(Sounds.moveToAnotherDepthSound, player.transform.position);
        }

        public static void PlayDoorOpenSound(Room room)
        {
            AudioSource.PlayClipAtPoint(Sounds.doorsOpenSound, room.transform.position);
        }

        public static void PlayDoorCloseSound(Room room)
        {
            AudioSource.PlayClipAtPoint(Sounds.doorsCloseSound, room.transform.position);
        }

        public static void PlayCollectableSound(MonoBehaviour collectable)
        {
            AudioSource.PlayClipAtPoint(Sounds.collectableSound, collectable.transform.position);
        }

        public static void PlayDeliveryReceivedSound(PlayerActorInput player)
        {
            AudioSource.PlayClipAtPoint(Sounds.deliveryReceivedSound, player.transform.position);
        }

        public static void PlayDeliverySuccessSound(PlayerActorInput player)
        {
            AudioSource.PlayClipAtPoint(Sounds.deliverySuccessSound, player.transform.position);
        }

        #endregion

        #region UI_Sounds

        public static void PlayMenuButtonClickSound()
        {
            AudioSource.PlayClipAtPoint(Sounds.menuButtonClickSound, MainCamera.transform.position);
        }

        public static void PlayUpgradeButtonClickSound()
        {
            AudioSource.PlayClipAtPoint(Sounds.upgradeButtonClickSound, MainCamera.transform.position);
        }

        public static void PlayDialogueSound()
        {
            AudioSource.PlayClipAtPoint(Sounds.dialogueSound, MainCamera.transform.position);
        }

        #endregion
    }
}