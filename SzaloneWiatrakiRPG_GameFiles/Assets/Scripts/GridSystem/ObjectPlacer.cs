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

        public int PlaceObject(GameObject prefab, Vector3 position)
        {
            GameObject newStructure = Instantiate(prefab);
            newStructure.transform.position = position;
            placedGameObjects.Add(newStructure);
            return placedGameObjects.Count - 1;
        }

        internal void RemoveObject(int gameObjectIndex)
        {
            if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
                return;
            Destroy(placedGameObjects[gameObjectIndex]);
            placedGameObjects[gameObjectIndex] = null;
        }
    }
}