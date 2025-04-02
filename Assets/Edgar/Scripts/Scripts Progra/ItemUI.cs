
using UnityEngine;
using TMPro;
using UnityEngine.UI;
    public class ItemUI : MonoBehaviour
    {
       [SerializeField] private TextMeshProUGUI itemName;
       [SerializeField] private TextMeshProUGUI itemDescription;
       [SerializeField] private  Image itemImage;

       public void SetItemInfo(SOItem item)
       {
                itemName.text = item.names;
                itemDescription.text = item.description;
                itemImage.sprite = item.sprite;
       }
    }
 

