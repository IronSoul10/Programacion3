using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    public class InventoryHandler1 : MonoBehaviour
    {

        public List<SOItem> inventory = new List<SOItem>();
        [SerializeField] private Image newItemImage;

        public void AddItem(SOItem item)
        {
            inventory.Add(item);
            Debug.Log("Se ha añadido " + item.names + " a tu inventario");
            Debug.Log("Descripcion: " + item.description);
            newItemImage.sprite = item.sprite;
        }


    }

