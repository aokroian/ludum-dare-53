using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actors.InputThings
{
    public class PlayerActorInput : MonoBehaviour, IActorInput
    {
        public Vector2 Movement { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public bool Fire { get; private set; }


        private Camera _mainCamera;

        public void OnFire(InputValue value)
        {
            Fire = Math.Abs(value.Get<float>() - 1f) < 0.1f;
        }

        public void OnLook(InputValue value)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = _mainCamera.farClipPlane * .5f;
            var worldPoint = _mainCamera.ScreenToWorldPoint(mousePos);
            TargetPosition = worldPoint;
        }

        public void OnMove(InputValue context)
        {
            Movement = context.Get<Vector2>();
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
    }
}