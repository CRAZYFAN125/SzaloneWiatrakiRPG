using Assets.Scripts.GridSystem;
using Assets.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.ShopSystem
{
    public class ShopGUIManager : MonoBehaviour
    {
        public InputActionReference openShop;
        PlacementSystem placementSystem;
        public GameObject ShopScreen;
        public ObjectDatabaseSO _ObjectDatabaseSO;
        public GameObject Prefab;
        public Transform Container;

        private void OnEnable()
        {
            openShop.action.performed += OpenOrCloseShop;
        }

        private void Start()
        {
            placementSystem = FindObjectOfType<PlacementSystem>();
            SetupStore();
        }

        private void SetupStore()
        {
            foreach (var objectData in _ObjectDatabaseSO.objectsData)
            {
                if (objectData.fractionOwned == Inventory.Instance.selectedFraction||objectData.fractionOwned==Inventory.Fractions.Both)
                    Instantiate(Prefab, Container).GetComponent<ShopGUIItemViewer>().Setup(objectData);
            }
        }

        private void OnDisable()
        {
            openShop.action.performed -= OpenOrCloseShop;
        }

        private void OpenOrCloseShop(InputAction.CallbackContext context)
        {
            if (ShopScreen.activeSelf)
                ShopScreen.SetActive(false);
            else
            {
                ShopScreen.SetActive(true);
                placementSystem.StopPlacement();
            }
        }
        public void OpenOrCloseShop()
        {
            if (ShopScreen.activeSelf)
                ShopScreen.SetActive(false);
            else
            {
                ShopScreen.SetActive(true);
                placementSystem.StopPlacement();
            }
        }
    }
}