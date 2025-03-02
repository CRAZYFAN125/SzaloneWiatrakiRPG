using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.InventorySystem
{
    public class InventoryGUIManager : MonoBehaviour
    {
        public InputActionReference openInventory;

        private void OnEnable()
        {
            openInventory.action.performed += OpenOrCloseInventory;
        }

        private void OnDisable()
        {
            openInventory.action.performed -= OpenOrCloseInventory;
        }

        private void OpenOrCloseInventory(InputAction.CallbackContext context)
        {
            Debug.Log("OpenInventory");
        }
    }
}
