using UnityEngine;



public class Item1 : MonoBehaviour, IInteractable
{
    [SerializeField] private SOItem item;
    [SerializeField] InventoryHandler1 inventory;

    public void Interact()
    {
        inventory.AddItem(item);
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }

}

