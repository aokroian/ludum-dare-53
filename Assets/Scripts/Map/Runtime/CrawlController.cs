using System.Linq;
using Actors;
using Actors.InputThings;
using Actors.Spawn;
using Common;
using DG.Tweening;
using Map.Model;
using UnityEngine;
using Utils;

namespace Map.Runtime
{
    public class CrawlController : SingletonScene<CrawlController>
    {
        [SerializeField] private float roomSwitchDuration = 1f;
        [SerializeField] private GameCameraController _cameraController;
        
        public Level currentLevel;


        public void SetPlayerPosStartRoom(PlayerActorInput player)
        {
            var startRoom = currentLevel.rooms.Values.First(it => it.roomType == RoomType.Start);
            var center = startRoom.center;
            player.transform.position = center + new Vector3(4, -3, 0);
            startRoom.FadeIn(0f);
            _cameraController.MoveToRoom(startRoom);
            
            player.gameObject.GetComponent<ActorMovement>().enabled = false;
            player.gameObject.GetComponent<Collider2D>().enabled = false;
            
            player.transform.DOMove(center + new Vector3(4, 0, 0), 0.6f)
                .OnComplete(() => OnRoomEntered(player, startRoom));
        }
        
        public void ExitRoom(PlayerActorInput player, Room room, RoomExit exit)
        {
            player.gameObject.GetComponent<ActorMovement>().enabled = false;
            player.gameObject.GetComponent<Collider2D>().enabled = false;
            
            Debug.Log($"Exiting room {room.roomType}");
            var newRoom = currentLevel.rooms[currentLevel.GetRoomPosition(room) +
                                             CommonUtils.DirectionToVector(exit.Direction)];
            var newRoomEntrance = newRoom.entrances.First(
                it => it.Direction == CommonUtils.OppositeDirection(exit.Direction));
            
            OnRoomSwitchStarted(room, exit, newRoom, newRoomEntrance);

            player.transform.DOMove(newRoomEntrance.transform.position, roomSwitchDuration)
                .OnComplete(() => OnRoomEntered(player, newRoom));
        }

        private void OnRoomSwitchStarted(Room prevRoom, RoomExit exit, Room newRoom, RoomEntrance entrance)
        {
            Debug.Log("Room switch started");
            _cameraController.MoveToRoom(newRoom, roomSwitchDuration);
            prevRoom.FadeOut(roomSwitchDuration);
            newRoom.FadeIn(roomSwitchDuration);
            // TODO: enemies spawn if not visited
            
            if (!newRoom.visited && newRoom.roomType is RoomType.Common)
            {
                var enemies = SpawnUtil.SpawnEnemiesForRoom(newRoom);
            }
        }

        private void OnRoomEntered(PlayerActorInput player, Room room)
        {
            Debug.Log($"Entered room {room.roomType}");
            player.gameObject.GetComponent<ActorMovement>().enabled = true;
            player.gameObject.GetComponent<Collider2D>().enabled = true;
            room.visited = true;
        }
        
        private void PlayerMoveToRoom(PlayerActorInput player, Room room)
        {
            var center = room.center;
            player.transform.position = center + new Vector3(4, -3, 0);
            room.FadeIn(0f);
            _cameraController.MoveToRoom(room);
            
            player.gameObject.GetComponent<ActorMovement>().enabled = false;
            player.gameObject.GetComponent<Collider2D>().enabled = false;
            
            player.transform.DOMove(center + new Vector3(4, 0, 0), 0.6f)
                .OnComplete(() => OnRoomEntered(player, room));
        }
    }
}