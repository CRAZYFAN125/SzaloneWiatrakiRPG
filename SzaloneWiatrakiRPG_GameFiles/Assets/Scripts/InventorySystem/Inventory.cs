using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        /// <summary>
        /// Zbiór danych o przedmiotach, [Item szukany]=ilość
        /// </summary>
        public Dictionary<ItemSO, int> InventoryData { get; private set; } = new();
        /// <summary>
        /// Database of items
        /// </summary>
        [SerializeField]
        private List<ItemSO> ItemDatabase;
        /// <summary>
        /// Item update action
        /// </summary>
        public event Action OnItemAdded, OnItemRemoved;

        /// <summary>
        /// World instance of Inventory
        /// </summary>
        public static Inventory Instance { get; private set; }

        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There is already an Inventory system!");
                Destroy(this);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Adds item to inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="amount">Amount to add</param>
        public void AddItems(ItemSO item, int amount)
        {
            InventoryData.Add(item, amount);
            OnItemAdded?.Invoke();
        }

        /// <summary>
        /// Checks for ID of item then adds it to the Inventory
        /// </summary>
        /// <param name="itemID">ID of desired Item</param>
        /// <param name="amount">Amount you want to add</param>
        public void AddItems(int itemID, int amount)
        {
            foreach (var item in ItemDatabase)
            {
                if (item.ItemID == itemID)
                {
                    InventoryData.Add(item, amount);
                    OnItemAdded?.Invoke();
                    break;
                }
            }
        }

        /// <summary>
        /// Removes item from inventory
        /// </summary>
        /// <param name="item">Item to reduce quantity</param>
        /// <param name="amount">Amount to reduce</param>
        public void RemoveItems(ItemSO item, int amount)
        {
            if (InventoryData.ContainsKey(item))
            {
                InventoryData[item] -= amount;
                OnItemRemoved?.Invoke();
            }
        }

        /// <summary>
        /// Checks if item can be reduced
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="amount">Amount that is needed</param>
        /// <returns>True if can be reduced, false if can't or isn't even in stock</returns>
        public bool CheckForAvaibleStock(ItemSO item, int amount)
        {
            if (InventoryData.ContainsKey(item))
                if (InventoryData[item] >= amount)
                    return true;
            return false;
        }
    }
}