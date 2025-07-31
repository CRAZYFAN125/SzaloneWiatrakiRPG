using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class InventoryGUIItemViewer:MonoBehaviour
    {
        public ItemSO itemView;
        public int itemAmount;
        public TMP_Text text;

        public void UpdateData()
        {
            text.text = $"{itemAmount}x {itemView.ItemName}";
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }
    }
}
