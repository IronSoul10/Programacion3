using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaEnemigo;
    public int daņoPelota;
    public GameObject player;
    
   
        public void Danio(int danio)
        {
            vidaEnemigo -= danio;

            if (vidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            Danio(daņoPelota);
            gameObject.transform.GetComponent<VidaEnemigo>().Danio(daņoPelota);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(player);
        }
    }

}
