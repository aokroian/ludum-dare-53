using System.Collections.Generic;
using UnityEngine;

namespace Map.Model
{
    public class Level : MonoBehaviour
    {
        public Dictionary<Vector3Int, Room> rooms = new();
    }
}