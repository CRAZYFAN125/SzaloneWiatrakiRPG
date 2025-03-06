using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShopSystem
{
    public class ShopGUIManager : MonoBehaviour
    {
        public InputActionReference openShop;

        private void OnEnable()
        {
            openShop.action.performed += OpenOrCloseShop;
        }
        private void OnDisable()
        {
            openShop.action.performed -= OpenOrCloseShop;
        }
        private void OpenOrCloseShop(InputAction.CallbackContext context)
        {
            Debug.Log("Open/Close Inventory");
        }
    }
}