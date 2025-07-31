using Assets.Scripts.GridSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.InventorySystem
{
    public class InventoryGUIManager : MonoBehaviour
    {
        public InputActionReference openInventory;

        [SerializeField]
        private GameObject InventoryScreen;
        [SerializeField]
        private GameObject InventoryListSpace;
        [SerializeField]
        private GameObject InventoryPrefab;
        private float time;
        PlacementSystem placementSystem;

        private void OnEnable()
        {
            openInventory.action.performed += OpenOrCloseInventory;
        }

        private void OnDisable()
        {
            openInventory.action.performed -= OpenOrCloseInventory;
        }
        private void Start()
        {
            placementSystem = FindObjectOfType<PlacementSystem>();
        }

        //private void FixedUpdate()
        //{
        //    time += Time.fixedDeltaTime;
        //    if (time > 5) {
        //        UpdateInventoryData();
        //        time = 0;
        //    }
        //}

        /// <summary>
        /// Updates inventory screen
        /// </summary>
        private void UpdateInventoryData()
        {
            Inventory inventory = Inventory.Instance;
            Dictionary<ItemSO, int> _inv = inventory.GetItemsInInventory(inventory.selectedFraction);
            if (_inv != null && _inv.Count > 0)
                foreach (var item in _inv)
                {
                    var ViewData = InventoryListSpace.GetComponentsInChildren<InventoryGUIItemViewer>();
                    bool found = false;
                    foreach (var vData in ViewData)
                    {
                        if (vData.gameObject.name == item.Key.ItemName)
                        {
                            vData.itemAmount = item.Value;
                            found = true;
                            vData.UpdateData();
                            break;
                        }
                    }
                    if (!found)
                    {
                        var y = Instantiate(InventoryPrefab, InventoryListSpace.transform).GetComponent<InventoryGUIItemViewer>();
                        y.itemAmount = item.Value;
                        y.itemView = item.Key;
                        y.gameObject.name = item.Key.ItemName;
                        y.UpdateData();
                    }
                }
        }

        private void OpenOrCloseInventory(InputAction.CallbackContext context)
        {
            if (InventoryScreen.activeSelf)
                InventoryScreen.SetActive(false);
            else
            {
                InventoryScreen.SetActive(true);
                UpdateInventoryData();
            }

            placementSystem.StopPlacement();
        }
    }
}
