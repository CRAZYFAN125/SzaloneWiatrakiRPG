using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.MovementSystem
{
    public class CameraMovementSystem : MonoBehaviour
    {
        public InputActionReference movement;
        public InputActionReference rotation;
        public InputActionReference zoom;
        public GameObject PlayerCamera;

        public float moveSpeed = 10f;

        Vector2 movementDir = Vector2.zero;
        private Transform camTransf;

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

        private void Start()
        {
            camTransf = PlayerCamera.transform;
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            camTransf.transform.Translate(movementDir.y * Time.deltaTime * moveSpeed * camTransf.forward, Space.World);
            camTransf.Translate(movementDir.x * Time.deltaTime * moveSpeed * camTransf.right, Space.World);
            movementDir = Vector2.zero;
            if (Camera.main.transform.position.y < 1)
                Camera.main.transform.Translate(Camera.main.transform.forward * -2);
            if (Camera.main.transform.position.y > 25)
                Camera.main.transform.Translate(Camera.main.transform.forward * 2);

        }

        private void MoveCamera(InputAction.CallbackContext context)
        {
            movementDir = context.action.ReadValue<Vector2>();
            movementDir.Normalize();
        }

        private void RotateCamera(InputAction.CallbackContext context)
        {
            camTransf.transform.rotation = Quaternion.Euler(new Vector3(0, camTransf.transform.rotation.eulerAngles.y + context.action.ReadValue<float>() * 45, 0));
            //print(camTransf.rotation.eulerAngles);
        }


        private void ZoomCamera(InputAction.CallbackContext context)
        {
            //Camera Zoom Logic
            Camera.main.transform.Translate(Camera.main.transform.forward * context.action.ReadValue<float>());
        }
    }
}