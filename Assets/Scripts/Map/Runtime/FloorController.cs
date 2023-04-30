using Actors.InputThings;
using Common;
using UnityEngine;

namespace Map.Runtime
{
    public class FloorController: MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private CrawlController crawlController;
        [SerializeField] private int startRoomsCount = 10;
        
        public void EnterLevel(PlayerActorInput player, int depth, Vector3Int startRoomPos)
        {
            RandomConfig.Instance.ResetRandom(depth);

            var config = new LevelConstructionConfig(
                startRoomPos,
                CalcRoomsCount(depth),
                depth == 0);
            var level = levelGenerator.GenerateLevel(config);
            crawlController.currentLevel = level;
            
            crawlController.SetPlayerPosStartRoom(player);
        }

        private int CalcRoomsCount(int depth)
        {
            return Mathf.FloorToInt(startRoomsCount + (depth * 0.5f));
        }
    }
}