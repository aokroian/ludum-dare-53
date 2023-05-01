using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Model
{
    public class Room : MonoBehaviour
    {
        public RoomType roomType;
        
        public RoomEntrance[] entrances;
        public RoomExit[] exits;
        public Vector3 center;
        public WalkArea walkArea;

        public int distanceFromStart;

        public bool visited;

        private Tilemap[] _tilemaps;

        public void InitializeRoom(RoomType roomType)
        {
            this.roomType = roomType;
            FindEntrances();
            FindExits();
            FindWalkArea();
            LoadTilemaps();
            HideAllTilemaps();
        }

        public void FadeOut(float duration)
        {
            TweenToColor(Color.black, duration);
        }
        
        public void FadeIn(float duration)
        {
            TweenToColor(Color.white, duration);
        }

        public void CloseDoors()
        {
            foreach (var roomExit in exits)
            {
                roomExit.CloseDoor();
            }
        }
        
        public void OpenDoors()
        {
            foreach (var roomExit in exits)
            {
                roomExit.OpenDoor();
            }
        }
        
        private void TweenToColor(Color color, float duration)
        {
            foreach (var tilemap in _tilemaps)
            {
                DOTween.To(() => tilemap.color, c => tilemap.color = c, color, duration);
            }
        }

        private void FindEntrances()
        {
            entrances = GetComponentsInChildren<RoomEntrance>();
            center = GetComponentInChildren<TilemapRenderer>().bounds.center; // TODO: it takes first object inside!!
        }
        
        private void FindExits()
        {
            exits = GetComponentsInChildren<RoomExit>();
        }
        
        private void FindWalkArea()
        {
            walkArea = GetComponentInChildren<WalkArea>();
        }

        private void LoadTilemaps()
        {
            _tilemaps = GetComponentsInChildren<Tilemap>();
        }

        private void HideAllTilemaps()
        {
            foreach (var tilemap in _tilemaps)
            {
                tilemap.color = Color.black;
            }
        }
    }
}