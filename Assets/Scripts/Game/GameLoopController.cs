using Actors;
using Actors.ActorSystems;
using Actors.InputThings;
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

        public bool skipIntro;
        

        public void Initialize()
        {
            var player = SpawnPlayer();
            floorController.TryToEnterLevel(player, 0, true, Vector3Int.zero);
        }

        private PlayerActorInput SpawnPlayer()
        {
            var playerObj = Instantiate(playerPrefab);
            var playerInput = playerObj.GetComponent<PlayerActorInput>();
            playerObj.GetComponent<ActorHealth>().OnDeath += _ => OnPlayerDeath(playerInput);
            return playerInput;
        }

        private void OnPlayerDeath(PlayerActorInput playerInput)
        {
            playerInput.ToggleInput(false);
            DialogueController.Instance.PlayerDeath(
                () => RestartGame(),
                () => SceneController.Instance.LoadStartMenuScene()
            );
        }
        
        public void RestartGame()
        {
            SceneController.Instance.LoadGameScene();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}