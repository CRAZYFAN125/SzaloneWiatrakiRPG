using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.GridSystem
{
    public class PlacementState : IBuildingState
    {
        private int selectedObjectIndex = -1;
        int ID;
        Grid grid;
        PreviewSystem previewSystem;
        ObjectDatabaseSO database;
        GridData floorData;
        GridData furnitureData;
        ObjectPlacer objectPlacer;
        BuildSystemVFXHandler visualEffect;

        public PlacementState(int iD,
                              Grid grid,
                              PreviewSystem previewSystem,
                              ObjectDatabaseSO database,
                              GridData floorData,
                              GridData furnitureData,
                              ObjectPlacer objectPlacer,
                              BuildSystemVFXHandler visualEffect)
        {
            ID = iD;
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.database = database;
            this.floorData = floorData;
            this.furnitureData = furnitureData;
            this.objectPlacer = objectPlacer;
            this.visualEffect = visualEffect;

            selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
            if (selectedObjectIndex > -1)
            {
                previewSystem.StartShowingPlacementPreview(database.objectsData[selectedObjectIndex].Prefab,
                    database.objectsData[selectedObjectIndex].Size);
            }
            else
                throw new System.Exception($"No build with ID: {iD}");

        }


        public void EndState()
        {
            previewSystem.StopShowingPreview();
        }

        public void OnAction(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if (!placementValidity)
            {
                visualEffect.PlayErrorVFX(gridPosition+new Vector3(.5f,1.5f,.5f));
                return;
            }

            int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

            GridData selectedData = database.objectsData[selectedObjectIndex].Layer == 0 ?
                floorData : furnitureData;
            selectedData.AddObjectAt(gridPosition,
                database.objectsData[selectedObjectIndex].Size,
                database.objectsData[selectedObjectIndex].ID,
                index);

            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
        }
        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        {
            GridData selectedData = database.objectsData[selectedObjectIndex].Layer == 0 ?
                floorData : furnitureData;

            return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
        }
    }
}