using Common;
using UnityEngine;

namespace Map
{
    public struct RoomConstructionConfig
    {
        public readonly WallDirection[] doors;
        public readonly Vector3 position;
        public readonly Transform parent;
        
        public RoomConstructionConfig(WallDirection[] doors, Vector3 position, Transform parent)
        {
            this.doors = doors;
            this.position = position;
            this.parent = parent;
        }
    }
}