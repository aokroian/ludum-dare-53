using System;
using Actors.InputThings;
using Common;
using Map.Runtime;
using UnityEngine;

namespace Map.Model
{
    public class RoomExit : MonoBehaviour
    {
        [SerializeField] private WallDirection direction;
        public WallDirection Direction => direction;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<PlayerActorInput>();
                CrawlController.Instance.ExitRoom(player, GetComponentInParent<Room>(), this);
            }
        }
    }
}