using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarJugador : MonoBehaviour
{
    public Transform jugador;

    void Update()
    {
        if (jugador != null)
        {
            transform.LookAt(jugador);
        }
    }
}


