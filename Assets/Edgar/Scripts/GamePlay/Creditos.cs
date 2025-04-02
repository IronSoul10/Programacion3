using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    public float radio;
    public LayerMask playerMask;
    
    void Update()
    {
      bool detenccion = Physics.CheckSphere(transform.position, radio,playerMask);

        if (detenccion)
        {
            SceneManager.LoadScene("Creditos");
           
        }
       
    }

       
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
