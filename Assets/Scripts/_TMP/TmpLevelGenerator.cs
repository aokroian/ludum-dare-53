using Map;
using UnityEngine;

namespace _TMP
{
    public class TmpLevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private int roomsCount = 10;

        private GameObject prevLevel;

        public void Generate()
        {
            Destroy(prevLevel);
            prevLevel = levelGenerator.GenerateLevel(Vector3Int.zero, roomsCount);
        }
    }
}