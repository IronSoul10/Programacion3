
using TMPro;
using UnityEngine;

public class ContadorBajas : MonoBehaviour
{
    [SerializeField] int objetivoBajas;
    public TextMeshProUGUI Bajas;
    public int numeroBajas;

    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closeDoor;

    [SerializeField] public InventoryHandler1 inventoryHandler;

    private void Start()
    {
        ActualizarContador();
        
    }

    private void Update()
    {
        Salida();
      
    }
   
    public void BajasActuales()
    {
        numeroBajas++;
        ActualizarContador();
    }
    public void ActualizarContador()
    {
        Bajas.text = ("") + numeroBajas;
    }

   public void Salida()
    {
        if (inventoryHandler.inventory.Count >= 6) 
        {
            Debug.Log("Se Acabo");
            openDoor.SetActive(true);
            closeDoor.SetActive(false);
            
        }
    }
    

    

}
