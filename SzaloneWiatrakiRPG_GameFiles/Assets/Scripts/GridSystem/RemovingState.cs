using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.GridSystem
{
    public class RemovingState : IBuildingState
    {
        private int gameObjectIndex = -1;
        readonly Grid grid;
        readonly PreviewSystem previewSystem;
        readonly GridData floorData;
        readonly GridData furnitureData;
        readonly ObjectPlacer objectPlacer;
        readonly VisualEffect removeErrorEffect;

        public RemovingState(Grid grid,
                             PreviewSystem previewSystem,
                             GridData floorData,
                             GridData furnitureData,
                             ObjectPlacer objectPlacer,
                              VisualEffect visualEffect)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.floorData = floorData;
            this.furnitureData = furnitureData;
            this.objectPlacer = objectPlacer;
            removeErrorEffect = visualEffect;

            previewSystem.StartShowingRemovePreview();
        }

        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

        public void OnAction(Vector3Int gridPosition)
        {
            GridData selectedData = null;
            if (furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
            {
                selectedData = furnitureData;
            }
            else if (floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
            {
                selectedData = floorData;
            }

            if (selectedData == null)
            {
                removeErrorEffect.Play();
            }
            else
            {
                gameObjectIndex = selectedData.getRepresentationIndex(gridPosition);
                if (gameObjectIndex == -1)
                    return;

                selectedData.RemoveObjectAt(gridPosition);

                objectPlacer.RemoveObject(gameObjectIndex);
            }

            Vector3 cellPosition = grid.CellToWorld(gridPosition);
            previewSystem.UpdatePosition(cellPosition,CheckIfSelectionIsValid(gridPosition));
        }

        private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
        {
            return !(furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one) && floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool validity = CheckIfSelectionIsValid(gridPosition);
            previewSystem.UpdatePosition(gridPosition, validity);
        }
    }
}