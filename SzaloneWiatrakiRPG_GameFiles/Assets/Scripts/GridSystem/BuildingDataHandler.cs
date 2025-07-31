using Assets.Scripts.GridSystem;
using Assets.Scripts.InventorySystem;
using UnityEngine;

public class BuildingDataHandler : MonoBehaviour
{
    Inventory.Fractions fractionOwned;
    ObjectData @object;

    public void AddData(Inventory.Fractions fractionOwned, ObjectData @object)
    {
        this.fractionOwned = fractionOwned;
        this.@object = @object;
    }

    public void GetData(out Inventory.Fractions fractionOwned, out ObjectData @object)
    {
        fractionOwned = this.fractionOwned;
        @object = this.@object;
    }
}
