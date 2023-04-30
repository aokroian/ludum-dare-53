﻿using Actors.InputThings;
using Map;
using Map.Runtime;
using Scene;
using UnityEngine;

namespace Game
{
    public class GameLoopController : MonoBehaviour, Initializable
    {
        [SerializeField] private GameObject playerPrefab;
        // [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private FloorController floorController;
        
        

        public void Initialize()
        {
            var player = SpawnPlayer();
            floorController.EnterLevel(player, 0, Vector3Int.zero);
        }

        private PlayerActorInput SpawnPlayer()
        {
            var playerObj = Instantiate(playerPrefab);
            return playerObj.GetComponent<PlayerActorInput>();
        }
    }
}