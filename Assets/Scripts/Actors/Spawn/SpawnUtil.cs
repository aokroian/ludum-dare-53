using System.Collections.Generic;
using Actors.InputThings.AI;
using Common;
using Map.Model;
using Map.Runtime;
using UnityEngine;
using Utils;

namespace Actors.Spawn
{
    public static class SpawnUtil
    {
        private static EnemiesConfigSo _enemiesConfig;
        private static SpawnConfigSo _spawnConfig;

        public static List<AIActorInput> SpawnEnemiesForRoom(Room room, WallDirection entranceDirection)
        {
            if (_spawnConfig == null)
                _spawnConfig = Resources.Load<SpawnConfigSo>("SpawnConfig");
            if (_enemiesConfig == null)
                _enemiesConfig = Resources.Load<EnemiesConfigSo>("EnemiesConfig");
                
            var walkArea = room.walkArea;

            // temp spawn random enemies
            // var randCount = Random.Range(1, 4);
            // var spawnedEnemies = new AIActorInput[randCount];
            // for (var i = 0; i < randCount; i++)
            // {
            //     // var randEnemyType = EnemyTypes.StationaryShooter; // Random.Range(0, 2);
            //     // var enemyType = (EnemyTypes) randEnemyType;
            //     var enemy = SpawnUtil.SpawnEnemy(enemyType, walkArea.transform, walkArea.walkArea.bounds, entranceDirection);
            //     spawnedEnemies[i] = enemy;
            // }
            //
            // return spawnedEnemies;

            var prefabs = GetEnemiesByDifficultyWeight(
                CalcDifficulty(room),
                out var difficultyCostLeft);
            
            var spawnedEnemies = new List<AIActorInput>();
            foreach (var pair in prefabs)
            {
                spawnedEnemies.Add(SpawnEnemy(
                    pair.enemyPrefab,
                    walkArea.transform,
                    walkArea.walkArea.bounds,
                    entranceDirection));
            }

            return spawnedEnemies;
        }

        private static float CalcDifficulty(Room room)
        {
            var result = _spawnConfig.StartCost;
            result += FloorController.Instance.CurrentDepth * _spawnConfig.DepthCostMultiplier;
            result += room.distanceFromStart * _spawnConfig.DistanceFromStartMultiplier;

            return result;
        }

        private static AIActorInput SpawnEnemy(AIActorInput prefab,
            Transform parent,
            Bounds area,
            WallDirection entranceDirection)
        {
            var spawned = Object.Instantiate(prefab, parent);
            var spawnedTransform = spawned.transform;
            spawnedTransform.position = RandomPointInBounds(area, entranceDirection);
            return spawned.GetComponent<AIActorInput>();
        }

        private static List<EnemyTypeAndPrefabPair> GetEnemiesByDifficultyWeight(
            float difficultyWeight,
            out float difficultyCostLeft)
        {
            var enemies = new List<EnemyTypeAndPrefabPair>();

            var costLeft = difficultyWeight;
            while (costLeft > 0)
            {
                var enemy = _enemiesConfig.GetRandomLessThenDifficultyWeight(costLeft, 2);
                if (enemy == null)
                    break;
                enemies.Add(enemy);
                costLeft -= enemy.difficultyWeight;
            }

            difficultyCostLeft = costLeft;
            return enemies;
        }

        private static Vector3 RandomPointInBounds(Bounds bounds, WallDirection entranceDirection)
        {
            var smallerBounds = new Bounds(bounds.center, bounds.size - (Vector3.one * 2));
            var offset = CommonUtils.DirectionToVector(entranceDirection);

            return new Vector3(
                Random.Range(smallerBounds.min.x, smallerBounds.max.x),
                Random.Range(smallerBounds.min.y, smallerBounds.max.y),
                0
            ) - offset;
        }
    }
}