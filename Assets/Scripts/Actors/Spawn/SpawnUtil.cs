using Actors.InputThings.AI;
using Map.Model;
using UnityEngine;

namespace Actors.Spawn
{
    public static class SpawnUtil
    {
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
            var enemiesConfig = Resources.Load<EnemiesConfig>("EnemiesConfig");
            var enemyPrefab = enemiesConfig.GetEnemyPrefab(enemyType);

            var spawned = Object.Instantiate(enemyPrefab, parent);
            spawned.transform.localPosition = localPosition;
            return spawned;
        }
    }
}