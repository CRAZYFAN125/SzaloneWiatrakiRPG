using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.MovementSystem
{
    public class CameraMovementSystem : MonoBehaviour
    {
        public InputActionReference movement;
        public InputActionReference rotation;
        public InputActionReference zoom;

        private void OnEnable()
        {
            movement.action.performed += MoveCamera;
            rotation.action.performed += RotateCamera;
            zoom.action.performed += ZoomCamera;
        }

        private void OnDisable()
        {
            movement.action.performed -= MoveCamera;
            rotation.action.performed -= RotateCamera;
            zoom.action.performed -= ZoomCamera;
        }

        private void MoveCamera(InputAction.CallbackContext context)
        {
            //Camera Movement Logic
        }

        private void RotateCamera(InputAction.CallbackContext context)
        {
            //Camera Rotation Logic
        }


        private void ZoomCamera(InputAction.CallbackContext context)
        {
            //Camera Zoom Logic
        }
    }
}