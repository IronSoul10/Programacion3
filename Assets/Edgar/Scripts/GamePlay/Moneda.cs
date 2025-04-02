using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private bool canGet;
    [SerializeField] private float radio;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool mouseIn;
    

    private void OnMouseEnter()
    {
        mouseIn = true;
    }

    private void OnMouseExit()
    {
        mouseIn = false;

    }



    void Update()
    {
        canGet = Physics.CheckSphere(transform.position,radio,playerMask);
        TakeCoin();
    }

    void TakeCoin()
    {
        if (Input.GetKeyDown(KeyCode.E) && canGet && mouseIn)
        {
            GameManager.Instance.moneda++;
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radio);
    }
}
