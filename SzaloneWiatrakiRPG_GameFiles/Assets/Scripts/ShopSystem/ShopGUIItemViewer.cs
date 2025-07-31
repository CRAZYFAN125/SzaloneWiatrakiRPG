using Assets.Scripts.GridSystem;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ShopSystem
{
    public class ShopGUIItemViewer : MonoBehaviour
    {
        public Image image;
        public TMP_Text objectName;
        public TMP_Text objectDescription;
        private PlacementSystem placementSystem;
        private ShopGUIManager shopGUIManager;
        private void Start()
        {
            placementSystem = FindAnyObjectByType<PlacementSystem>();
            shopGUIManager = FindAnyObjectByType<ShopGUIManager>();   
        }
        internal void Setup(ObjectData objectData)
        {
            image.sprite = objectData.Image;
            objectName.text = objectData.Name;
            SetDescription(objectData);
            gameObject.GetComponent<Button>().onClick.AddListener(() => placementSystem.StartPlacement(objectData.ID));
            gameObject.GetComponent<Button>().onClick.AddListener(() => shopGUIManager.OpenOrCloseShop());
            gameObject.SetActive(true);
            gameObject.name = objectData.Name;
        }
        /// <summary>
        /// Sets Description of the shop item
        /// </summary>
        private void SetDescription(ObjectData objectData)
        {
            objectDescription.text =
                $"{objectData.Size.x}x{objectData.Size.y}\n" +
                $"Layer: {objectData.Layer}\n" +
                $"Materials to build: {objectData.ToStringRecipe()}\n" +
                $"{objectData.description}";
        }
    }
}