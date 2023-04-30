using Actors.InputThings.AI;
using Map.Model;
using UnityEngine;

namespace Actors.Spawn
{
    public static class SpawnUtil
    {
        private static EnemiesConfigSo _enemiesConfig;

        public static AIActorInput[] SpawnEnemiesForRoom(Room room)
        {
            var walkArea = room.walkArea;

            // temp spawn random enemies
            var randCount = Random.Range(1, 4);
            var spawnedEnemies = new AIActorInput[randCount];
            for (var i = 0; i < randCount; i++)
            {
                var randEnemyType = EnemyTypes.SimpleWanderer; // Random.Range(0, 2);
                var enemyType = (EnemyTypes) randEnemyType;
                var enemy = SpawnUtil.SpawnEnemy(enemyType, walkArea.transform, Vector2.zero);
                spawnedEnemies[i] = enemy;
            }

            return spawnedEnemies;
        }

        private static AIActorInput SpawnEnemy(EnemyTypes enemyType, Transform parent, Vector2 localPosition)
        {
            if (_enemiesConfig == null)
                _enemiesConfig = Resources.Load<EnemiesConfigSo>("EnemiesConfig");

            var enemyPrefab = _enemiesConfig.GetEnemyPrefab(enemyType);
            var spawned = Object.Instantiate(enemyPrefab, parent);
            spawned.transform.localPosition = localPosition;
            return spawned;
        }
    }
}