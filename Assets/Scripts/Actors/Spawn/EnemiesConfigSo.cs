using System;
using System.Linq;
using Actors.InputThings.AI;
using UnityEngine;

namespace Actors.Spawn
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "LD53/EnemiesConfig", order = 0)]
    public class EnemiesConfigSo : ScriptableObject
    {
        [field: SerializeField] public EnemyTypeAndPrefabPair[] Enemies { get; private set; }

        public AIActorInput GetEnemyPrefab(EnemyTypes enemyType)
        {
            return Enemies.FirstOrDefault(e => e.enemyType == enemyType)?.enemyPrefab;
        }

        public AIActorInput[] GetEnemiesPrefabsByDifficultyWeight(int difficultyWeight)
        {
            return Enemies.Where(e => e.difficultyWeight == difficultyWeight).Select(e => e.enemyPrefab).ToArray();
        }
    }

    [Serializable]
    public class EnemyTypeAndPrefabPair
    {
        public EnemyTypes enemyType;
        public AIActorInput enemyPrefab;
        public int difficultyWeight;
    }
}