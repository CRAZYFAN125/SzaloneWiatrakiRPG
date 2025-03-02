using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    public class GridData
    {
        Dictionary<Vector3Int, PlacementData> placedObjectsData = new();

        public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int PlacedObjectIndex)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
            PlacementData data = new(positionToOccupy, ID, PlacedObjectIndex);
            foreach (var pos in positionToOccupy)
            {
                if (placedObjectsData.ContainsKey(pos))
                {
                    throw new System.Exception("Dictionary Contains " + pos);
                }
                placedObjectsData[pos] = data;
            }
        }

        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> returnVal = new();
            for (int x = 0; x < objectSize.x; x++)
            {
                for (int y = 0; y < objectSize.y; y++)
                {
                    returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
                }
            }
            return returnVal;
        }

        public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
            foreach (var pos in positionsToOccupy)
            {
                if (placedObjectsData.ContainsKey(pos))
                    return false;
            }
            return true;
        }

        internal int getRepresentationIndex(Vector3Int gridPosition)
        {
            if (!placedObjectsData.ContainsKey(gridPosition)) { return -1; }
            return placedObjectsData[gridPosition].PlacedObjectIndex;
        }

        internal void RemoveObjectAt(Vector3Int gridPosition)
        {
            foreach (var position in placedObjectsData[gridPosition].occupiedPositions)
            {
                placedObjectsData.Remove(position);
            }
        }

        public class PlacementData
        {
            public List<Vector3Int> occupiedPositions;

            public int ID { get; private set; }
            public int PlacedObjectIndex { get; private set; }

            public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
            {
                this.occupiedPositions = occupiedPositions;
                ID = iD;
                PlacedObjectIndex = placedObjectIndex;
            }

        }
    }
}