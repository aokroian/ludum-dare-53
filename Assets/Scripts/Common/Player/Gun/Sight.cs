using UnityEngine;

namespace Common.Player.Gun
{
    public class Sight : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float distanceSight = 2f;
        
        
        void Update()
        {
            var mouse = Input.mousePosition;
            var mousePosition =
                camera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, camera.nearClipPlane + distanceSight));
            gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }
}
