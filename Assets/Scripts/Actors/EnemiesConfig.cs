using System;
using System.Linq;
using Actors.InputThings.AI;
using UnityEngine;

namespace Actors
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "LD53/EnemiesConfig", order = 0)]
    public class EnemiesConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyTypeAndPrefabPair[] Enemies { get; private set; }

        public AIActorInput GetEnemyPrefab(EnemyTypes enemyType)
        {
            return Enemies.FirstOrDefault(e => e.enemyType == enemyType)?.enemyPrefab;
        }
    }

    [Serializable]
    public class EnemyTypeAndPrefabPair
    {
        public EnemyTypes enemyType;
        public AIActorInput enemyPrefab;
    }
}