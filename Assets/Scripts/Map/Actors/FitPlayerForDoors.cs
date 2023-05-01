using System;
using Actors.ActorSystems;
using Actors.Upgrades;
using DG.Tweening;
using UnityEngine;

namespace Map.Actors
{
    public class FitPlayerForDoors : MonoBehaviour
    {
        private Tweener _scaleTweener;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            var dist = Vector3.Distance(other.transform.position, transform.position);
            var scaleValue = Mathf.Lerp(0.3f, 1f, dist / 4);
            other.transform.localScale = new Vector3(scaleValue, scaleValue, 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TryToRestoreScale(other);
        }

        private void TryToRestoreScale(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            _scaleTweener?.Kill();
            var scale = Vector3.one * GetUnaffectedScaleValue(other.gameObject);
            scale.z = 1;
            other.transform.localScale = scale;
        }

        private static float GetUnaffectedScaleValue(GameObject player)
        {
            var dynamicActorStats = player.GetComponent<DynamicActorStats>();
            var actorPhysics = player.GetComponent<ActorPhysics>();

            var scaleValue = actorPhysics.DefaultScale + dynamicActorStats.ActorStatsSo.addedScaleModifier;
            return scaleValue;
        }
    }
}