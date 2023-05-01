using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actors.InputThings
{
    public class PlayerActorInput : MonoBehaviour, IActorInput
    {
        public Vector2 Movement { get; private set; }
        public Vector3 Look { get; private set; }
        public bool Fire { get; private set; }

        private Camera _mainCamera;

        private bool _isActive = true;

        public void OnFire(InputValue value)
        {
            if (!_isActive)
                return;

            Fire = Math.Abs(value.Get<float>() - 1f) < 0.1f;
        }

        public void OnLook(InputValue value)
        {
            if (!_isActive)
                return;

            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = _mainCamera.farClipPlane * .5f;
            var worldPoint = _mainCamera.ScreenToWorldPoint(mousePos);
            Look = worldPoint;
        }

        public void OnMove(InputValue context)
        {
            if (!_isActive)
                return;

            Movement = context.Get<Vector2>();
        }

        public void ToggleInput(bool isActive)
        {
            _isActive = isActive;

            Look = default;
            Movement = default;
            Fire = default;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
    }
}