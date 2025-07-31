using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public enum Fractions
        {
            Burgers,
            Breads,
            Both
        }

        public class InventoryDataCenter
        {
            /// <summary>
            /// NazwaFrakcji
            /// </summary>
            public Fractions FractionName;
            /// <summary>
            /// Zbiór danych o przedmiotach, [Item szukany]=ilość
            /// </summary>
            public Dictionary<ItemSO, int> InventoryData { get; private set; } = new();
        }
        /// <summary>
        /// Database of items in fractions
        /// </summary>
        private List<InventoryDataCenter> inventories;
        public Fractions selectedFraction;
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
            selectedFraction = Fractions.Burgers;
            SetupFractionsInventory();
        }
        /// <summary>
        /// Setups base data for inventories of 2 factions
        /// </summary>
        /// <returns>True if succesfully set up</returns>
        private bool SetupFractionsInventory()
        {
            inventories = new List<InventoryDataCenter>
            {
                //Burgers
                new() { FractionName = Fractions.Burgers },
                //Breads
                new() { FractionName = Fractions.Breads }
            };
            AddItems(0, 100, Fractions.Burgers);
            AddItems(1, 100, Fractions.Burgers);

            return true;
        }

        /// <summary>
        /// Adds item to inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="amount">Amount to add</param>
        /// <param name="targetFraction">Fraction to which add items</param>
        public void AddItems(Fractions targetFraction, ItemSO item, int amount)
        {
            foreach (var _inv in inventories)
            {
                if (_inv.FractionName == targetFraction)
                {
                    if(_inv.InventoryData.ContainsKey(item))
                        _inv.InventoryData[item] += amount;
                    else
                        _inv.InventoryData.Add(item, amount);
                    
                    OnItemAdded?.Invoke();
                    break;
                }
            }
        }

        /// <summary>
        /// Checks for ID of item then adds it to the Inventory
        /// </summary>
        /// <param name="itemID">ID of desired Item</param>
        /// <param name="amount">Amount you want to add</param>
        /// <param name="targetFraction">Fraction to which add items</param>
        public void AddItems(int itemID, int amount, Fractions targetFraction)
        {
            foreach (var _inv in inventories)
            {
                if (_inv.FractionName == targetFraction)
                {
                    foreach (var item in ItemDatabase)
                    {
                        if (item.ItemID == itemID)
                        {
                            if (_inv.InventoryData.ContainsKey(item))
                                _inv.InventoryData[item] += amount;
                            else
                                _inv.InventoryData.Add(item, amount);
                            OnItemAdded?.Invoke();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes item from inventory
        /// </summary>
        /// <param name="item">Item to reduce quantity</param>
        /// <param name="amount">Amount to reduce</param>
        /// <param name="targetFraction">Fraction from which deduct items</param>
        public void RemoveItems(ItemSO item, int amount, Fractions targetFraction)
        {
            foreach (var _inv in inventories)
            {
                if (_inv.FractionName == targetFraction)
                {
                    if (_inv.InventoryData.ContainsKey(item))
                    {
                        _inv.InventoryData[item] -= amount;
                        OnItemRemoved?.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if item can be reduced
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="amount">Amount that is needed</param>
        /// <param name="targetFraction">Fraction from which check for items</param>
        /// <returns>True if can be reduced, false if can't or isn't even in stock</returns>
        public bool CheckForAvaibleStock(ItemSO item, int amount, Fractions targetFraction)
        {
            foreach (var _inv in inventories)
            {
                if (_inv.FractionName == targetFraction)
                {
                    if (_inv.InventoryData.ContainsKey(item))
                        if (_inv.InventoryData[item] >= amount)
                            return true;
                }
            }
            return false;
        }

        public Dictionary<ItemSO, int> GetItemsInInventory(Fractions targetFraction)
        {
            var inventoryList = new Dictionary<ItemSO, int>();
            if (inventories.Count != 0)
                foreach (var _inv in inventories)
                    if (_inv.FractionName == targetFraction)
                        inventoryList = _inv.InventoryData;

            return inventoryList;
        }
    }
}