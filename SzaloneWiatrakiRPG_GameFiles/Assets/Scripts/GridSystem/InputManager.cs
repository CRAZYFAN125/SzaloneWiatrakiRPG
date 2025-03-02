using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GridSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Camera sceneCamera;
        private Vector3 lastPosition;
        [SerializeField]
        private LayerMask placementLayerMask;

        public event Action OnClicked, OnExit;
        public InputActionReference Build;
        public InputActionReference Cancel;

        private void OnEnable()
        {
            Build.action.performed += BuildActionPerformed;
            Cancel.action.performed += CancelActionPerformed;
        }
        private void OnDisable()
        {
            Build.action.performed -= BuildActionPerformed;
            Cancel.action.performed -= CancelActionPerformed;
        }

        private void CancelActionPerformed(InputAction.CallbackContext context)
        {
            OnExit?.Invoke();
        }

        private void BuildActionPerformed(InputAction.CallbackContext context)
        {
            OnClicked?.Invoke();
        }


        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //        OnClicked?.Invoke();
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //        OnExit?.Invoke();
        //    //onGUI = false;
        //}

        public bool IsPointerOnUI()
        {
            return EventSystem.current.currentSelectedGameObject != null;
        }

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
                lastPosition = hit.point;

            return lastPosition;
        }
    }
}