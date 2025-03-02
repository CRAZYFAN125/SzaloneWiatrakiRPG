using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.GridSystem
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField]
        GameObject mouseIndicator;
        [SerializeField]
        InputManager inputManager;
        [SerializeField]
        Grid grid;

        [SerializeField]
        private ObjectDatabaseSO database;

        [SerializeField]
        private GameObject gridVisualisation;

        private GridData floorData, furnitureData;

        [SerializeField]
        ObjectPlacer objectPlacer;

        [SerializeField]
        private PreviewSystem preview;
        [SerializeField]
        BuildSystemVFXHandler visualEffect;

        private Vector3Int lastDetectedPos = Vector3Int.zero;

        IBuildingState buildingState;
        private void Start()
        {
            StopPlacement();
            floorData = new GridData();
            furnitureData = new GridData();
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            buildingState = new PlacementState(ID,
                                               grid,
                                               preview,
                                               database,
                                               floorData,
                                               furnitureData,
                                               objectPlacer,
                                               visualEffect);
            inputManager.OnClicked += PlaceObject;
            inputManager.OnExit += StopPlacement;
        }

        public void StartRemoving()
        {
            StopPlacement();
            gridVisualisation.SetActive(true);
            buildingState = new RemovingState(grid,
                                              preview,
                                              floorData,
                                              furnitureData,
                                              objectPlacer,
                                               visualEffect);
            inputManager.OnClicked += PlaceObject;
            inputManager.OnExit += StopPlacement;
        }

        private void PlaceObject()
        {
            if (inputManager.IsPointerOnUI())
            {
                return;
            }

            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            buildingState.OnAction(gridPosition);
        }

        //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        //{
        //    GridData selectedData = database.objectsData[selectedObjectIndex].Layer == 0 ?
        //        floorData : furnitureData;

        //    return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
        //}

        private void StopPlacement()
        {
            if (buildingState == null)
                return;
            buildingState.EndState();
            inputManager.OnClicked -= PlaceObject;
            inputManager.OnExit -= StopPlacement;
            lastDetectedPos = Vector3Int.zero;
            buildingState = null;
        }

        private void Update()
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            mouseIndicator.transform.position = mousePosition;
            if (buildingState==null)
                return;

            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            if (lastDetectedPos != gridPosition)
            {
                buildingState.UpdateState(gridPosition);
                lastDetectedPos= gridPosition;
            }
        }
    }
}