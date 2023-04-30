using System;
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

        public bool visited;

        public void InitializeRoom(RoomType roomType)
        {
            FindEntrances();
            FindExits();
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
    }
}