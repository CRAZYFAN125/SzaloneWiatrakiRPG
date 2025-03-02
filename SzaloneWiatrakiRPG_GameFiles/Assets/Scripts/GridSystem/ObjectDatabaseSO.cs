using System;
using Assets.Scripts.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    [CreateAssetMenu(menuName = "BuildGridSystem/BuildObjectDatabase")]
    public class ObjectDatabaseSO : ScriptableObject
    {
        public List<ObjectData> objectsData;
    }

    [Serializable]
    public class ObjectData
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public int ID { get; private set; }
        [field: SerializeField]
        public Vector2Int Size { get; private set; } = Vector2Int.one;
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
        [field: SerializeField]
        public int Layer { get; private set; } = 1;
        [field: SerializeField]
        public List<Recipe> BuildRecipe { get; private set; }
    }

    [Serializable]
    public class Recipe
    {
        [field: SerializeField]
        public ItemSO Item { get; private set; }
        [field: SerializeField]
        public int Amount { get; private set; }
    }
}