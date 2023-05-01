using System;
using System.Linq;
using Actors;
using Actors.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "LD53/SoundsConfig", order = 1)]
    public class SoundsConfig : ScriptableObject
    {
        [Header("Player Sounds")]
        public AudioClip actorDamageSound;
        public AudioClip actorDeathSound;
        public AudioClip actorHealSound;

        [Header("Combat Sounds")]
        public BulletTypeToSoundBinding[] bulletsSounds;
        public GunTypeToSoundBinding[] gunShotsSound;
    }

    [Serializable]
    public class BulletTypeToSoundBinding
    {
        public BulletTypes bulletType;
        public AudioClip bulletHitSound;
    }

    [Serializable]
    public class GunTypeToSoundBinding
    {
        public GunTypes gunType;
        public AudioClip shotSound;
    }
}