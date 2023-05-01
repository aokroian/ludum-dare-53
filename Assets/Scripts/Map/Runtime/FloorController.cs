using Actors;
using Actors.InputThings;
using Common;
using DG.Tweening;
using Game;
using Map.Model;
using UnityEngine;

namespace Map.Runtime
{
    public class FloorController: SingletonScene<FloorController>
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private CrawlController crawlController;
        [SerializeField] private int startRoomsCount = 10;

        public int CurrentDepth { get; private set; }

        public void TryToEnterLevel(PlayerActorInput player, int depth, bool downstairs, Vector3Int startRoomPos)
        {
            if (downstairs && (depth == 0 || PackageController.Instance.currentPackage?.receiverDepth == depth))
            {
                EnterLevel(player, depth, downstairs, startRoomPos);
            }
            else if (!downstairs)
            {
                player.ToggleInput(false);
                DialogueController.Instance.CantGoUpstairs(() => player.ToggleInput(true));
            }
        }
        
        private void EnterLevel(PlayerActorInput player, int depth, bool downstairs, Vector3Int startRoomPos)
        {
            
            RandomConfig.Instance.ResetRandom(depth);
            
            CurrentDepth = depth;
            
            if (crawlController.currentLevel != null)
                Destroy(crawlController.currentLevel.gameObject);

            var config = new LevelConstructionConfig(
                startRoomPos,
                CalcRoomsCount(depth),
                depth == 0);
            var level = levelGenerator.GenerateLevel(config);
            crawlController.currentLevel = level;
            crawlController.SetPlayerPosStairsRoom(player, downstairs);
        }

        private int CalcRoomsCount(int depth)
        {
            return Mathf.FloorToInt(startRoomsCount + (depth * 0.5f));
        }
    }
}