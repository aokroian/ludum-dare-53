using Actors.InputThings.AI;
using UnityEngine;

namespace Actors.Spawn
{
    public static class SpawnUtil
    {
        public static AIActorInput SpawnEnemy(EnemyTypes enemyType, Transform parent, Vector2 localPosition)
        {
            var enemiesConfig = Resources.Load<EnemiesConfig>("EnemiesConfig");
            var enemyPrefab = enemiesConfig.GetEnemyPrefab(enemyType);

            var spawned = Object.Instantiate(enemyPrefab, parent);
            spawned.transform.localPosition = localPosition;
            return spawned;
        }
    }
}