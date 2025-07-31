using Assets.Scripts.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> placedGameObjects = new();

        public int PlaceObject(ObjectData objectData, Vector3 position)
        {
            GameObject newStructure = Instantiate(objectData.Prefab);
            newStructure.transform.position = position;
            placedGameObjects.Add(newStructure);
            BuildingDataHandler bdh = newStructure.AddComponent<BuildingDataHandler>();
            bdh.AddData(Inventory.Instance.selectedFraction, objectData);
            return placedGameObjects.Count - 1;
        }

        internal void RemoveObject(int gameObjectIndex)
        {
            if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
                return;

            placedGameObjects[gameObjectIndex].GetComponent<BuildingDataHandler>().GetData(out Inventory.Fractions objectfraction, out ObjectData _objectD);
            foreach (var _item in _objectD.BuildRecipe)
            {
                Inventory.Instance.AddItems(objectfraction, _item.Item,_item.Amount);
            }
            Destroy(placedGameObjects[gameObjectIndex]);
            placedGameObjects[gameObjectIndex] = null;
        }
    }
}