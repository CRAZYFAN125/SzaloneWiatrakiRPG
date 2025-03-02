using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    [CreateAssetMenu(menuName = "InventorySystem/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public string ItemName;
        public int ItemID;
    }
}