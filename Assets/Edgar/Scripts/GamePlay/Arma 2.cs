using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma2 : MonoBehaviour
{
    public float velocidadBala;
    public GameObject balaPrefab;
    public Transform puntoTiro;

    public int municionActual = 300;
    public int capacidadMaxima = 300;

    private ManagerBalas contador;


    private void Start()
    {
        contador = FindAnyObjectByType<ManagerBalas>();
        AudioManager.AudioInstance.Stop("Disparo");
    }

    void Update()
    {
        AccionarArma();
    }
    void AccionarArma()
    {
        if (JalaGatillo())
        {
            Disparar();

        }
    }
    bool JalaGatillo()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    void Disparar()
    {
        if (JalaGatillo() && municionActual > 0)
        {
            AudioManager.AudioInstance.Play("Disparo");
            GameObject clone = Instantiate(balaPrefab, puntoTiro.position, puntoTiro.rotation);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(puntoTiro.forward * velocidadBala, ForceMode.Impulse);
            Destroy(clone, 3);
            municionActual--;
            ActualizarHUD();
        }
        if ( municionActual <= 0 )
        {
            AudioManager.AudioInstance.Stop("Disparo");

        }
    }

    public void MunicionActualEnArma()
    {
        municionActual = 300;

    }
    public void ActualizarHUD()
    {
        contador.ActualizarContadorArma2(municionActual);
    }
}
