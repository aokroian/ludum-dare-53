using Actors;
using Actors.Upgrades;
using DG.Tweening;
using UnityEngine;

namespace Map.Actors
{
    public class FitPlayerForDoors : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            TryToScaleDown(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TryToRestoreScale(other);
        }

        private void TryToScaleDown(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            other.transform.DOScale(.6f, .4f).SetEase(Ease.OutExpo);
        }

        private void TryToRestoreScale(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            other.transform.DOScale(GetUnaffectedScaleValue(other.gameObject), .3f).SetEase(Ease.OutExpo);
        }

        private float GetUnaffectedScaleValue(GameObject player)
        {
            var dynamicActorStats = player.GetComponent<DynamicActorStats>();
            var actorPhysics = player.GetComponent<ActorPhysics>();

            var scaleValue = actorPhysics.DefaultScale + dynamicActorStats.ActorStatsSo.addedScaleModifier;
            return scaleValue;
        }
    }
}