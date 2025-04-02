using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaMunicion : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private LayerMask playerMask;

    private Arma arma;
    private Arma2 arma2;
    [SerializeField] private ManagerBalas contador;

    private void Start()
    {
        arma = FindAnyObjectByType<Arma>();
        arma2 = FindAnyObjectByType<Arma2>();
        AudioManager.AudioInstance.Stop("Recarga");
    }

    private void Update()
    {
        TomarMunicion();
    }

    bool InteractuarConMunicion()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool Detectar()
    {
        return Physics.CheckSphere(transform.position, radio, playerMask);
    }

    void TomarMunicion()
    {
        if (InteractuarConMunicion() && Detectar())
        {
            AudioManager.AudioInstance.Play("Recarga");

            if (arma != null)
            {
                arma.MunicionActualEnArma();
                contador.ActualizarContadorArma1(arma.municionActual);
            }

            if (arma2 != null)
            {
                arma2.MunicionActualEnArma();
                contador.ActualizarContadorArma2(arma2.municionActual);
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}

