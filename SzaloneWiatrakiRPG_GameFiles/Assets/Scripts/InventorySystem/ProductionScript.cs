using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class ProductionScript : MonoBehaviour
    {
        /// <summary>
        /// Informacja o produktach
        /// </summary>
        [Serializable]
        public class ProductionInfo
        {
            /// <summary>
            /// Dane przedmiotu
            /// </summary>
            [SerializeField]
            public ItemSO Item;
            /// <summary>
            /// Ilość przedmiotu
            /// </summary>
            [SerializeField,Min(1)]
            public int AmountProduced = 1;
        }
        /// <summary>
        /// Spis produkowanych przedmiotów
        /// </summary>
        public ProductionInfo[] producedItems;
        /// <summary>
        /// Czas produkcji
        /// </summary>
        public float productionTime;
        /// <summary>
        /// Czas ile już produkuje
        /// </summary>
        private float timeNow;
        /// <summary>
        /// Połączenie do Inventory System
        /// </summary>
        private Inventory InventorySys;

        [Min(1)]
        public int multiplier = 1;

        private void Start() => InventorySys = Inventory.Instance;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (timeNow>=productionTime)
            {
                timeNow = 0;
                foreach (var item in producedItems)
                    InventorySys.AddItems(Inventory.Instance.selectedFraction,item.Item, item.AmountProduced*multiplier);
                

                timeNow += Time.fixedDeltaTime;
            }
        }
    }
}